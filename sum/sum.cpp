#pragma once
#include "pch.h"
#include "sum.h"
#include <stdio.h>
#include<opencv2/opencv.hpp>
#include<iostream>
#include"math.h"
#include <vector>
#include<algorithm>
#include<string> 
#include "stdlib.h"

using namespace cv;
using namespace std;

int  getSubtract(Mat& src, int TemplateNum);

void stctarr(_ARRAYSTRUCT* data)
{
	Mat img;
	string url = data->img_path;
	VideoCapture cap(url);
	if (!cap.isOpened())
	{
		cout << "在线图片读取失败" << endl;
	}
	else {
		cap.read(img);
		cap.release();
		//imshow("img", img);
		//waitKey();
		//Mat img = imread(data->img_path);

		/*if (img.empty())
			return "-1";*/
			/**************************************************定位*******************************************************************************/
		Mat img_copy, img_gray, img_GaussianBlur, img_binary;
		img_copy = img.clone();
		cvtColor(img_copy, img_gray, CV_BGR2GRAY);   //灰度图
		GaussianBlur(img_gray, img_GaussianBlur, Size(3, 3), 0, 0);//平滑
		threshold(img_GaussianBlur, img_binary, 160, 255, THRESH_BINARY);//二值化

		vector<vector<Point> >contours;
		vector<Vec4i> hierarchy;
		findContours(img_binary, contours, hierarchy, RETR_TREE, CHAIN_APPROX_SIMPLE);
		vector<Rect> boundRect(contours.size());
		vector<RotatedRect> roRect(contours.size());

		Mat img_mask, mask;
		Mat img_rotation(img.rows, img.cols, CV_8UC1);
		for (int i = 0; i < contours.size(); i++)
		{
			//最小外接矩形
			roRect[i] = minAreaRect(Mat(contours[i]));
			//将旋转矩形的四个端点存储进pts.
			Point2f pts[4] = { };
			roRect[i].points(pts);
			double g_dConArea = fabs(contourArea(contours[i]));
			if (g_dConArea > 20000 && g_dConArea < 400000)
			{
				int line1 = sqrt((pts[1].y - pts[0].y) * (pts[1].y - pts[0].y) + (pts[1].x - pts[0].x) * (pts[1].x - pts[0].x));//宽
				int line2 = sqrt((pts[3].y - pts[0].y) * (pts[3].y - pts[0].y) + (pts[3].x - pts[0].x) * (pts[3].x - pts[0].x));//长
				if (line2 / line1 >= 3 && line2 / line1 <= 4)
				{
					mask = Mat::zeros(img.size(), CV_8UC1);
					mask.setTo(0);
					rectangle(mask, pts[0], pts[2], Scalar(255), 10, 8, 0);
					Point center = roRect[i].center;
					float angle = roRect[i].angle;

					/*if (line1 > line2)
					{
						angle = 90 + angle;
						cout << "新角度" << angle;
					}*/
					floodFill(mask, center, Scalar(255), NULL, cvScalarAll(0), cvScalarAll(0), CV_FLOODFILL_FIXED_RANGE);
					bitwise_and(img_gray, img_gray, img_mask, mask);
					//图像旋转					
					img_rotation.setTo(255);
					Mat M2 = getRotationMatrix2D(center, angle, 1);
					warpAffine(img_mask, img_rotation, M2, img.size(), 1, 0, Scalar(0));
				}

			}

		}
		/****************************************************裁剪*********************************************************************/
		Mat img_contours, img_roi, img_resize;
		img_contours = img_rotation.clone();
		vector<vector<Point>> contours2;
		findContours(img_contours, contours2, CV_RETR_EXTERNAL, CHAIN_APPROX_SIMPLE);

		for (int i = 0; i < contours2.size(); i++)
		{
			Rect r = boundingRect(Mat(contours2[i]));
			rectangle(img_contours, r, Scalar(255), 2);
			img_roi = img_rotation(r);              //裁剪目标区域
			resize(img_roi, img_resize, Size(300, 300 * img_roi.rows / img_roi.cols));

		}

		vector<vector<Point>> contours1;
		findContours(img_resize, contours1, RETR_EXTERNAL, CHAIN_APPROX_SIMPLE);
		drawContours(img_resize, contours1, -1, Scalar(255), 20);

		/*******************************************分割成单个数字****************************************************************/
		Mat img_GaussianBlur1, img_rect, img_RGB, img_threshold;
		Mat leftImg, rightImg;

		cvtColor(img_resize, img_RGB, COLOR_BayerGR2RGB);
		GaussianBlur(img_resize, img_GaussianBlur1, Size(3, 3), 0, 0);
		threshold(img_GaussianBlur1, img_threshold, 70, 255, CV_THRESH_BINARY_INV);

		int width = img_threshold.cols;
		Mat licenseN = Mat(Scalar(0));

		for (int i = 1; i < 6; i++)
		{
			/*等分7份，取中间6份*/
			int x = i * width / 7;
			int licenseY[30], licenseX[30], licenseW[30], licenseH[30];
			licenseX[i] = x;
			licenseY[i] = 0;
			licenseW[i] = width / 7;
			licenseH[i] = img_threshold.rows;

			Rect rect(licenseX[i], licenseY[i], licenseW[i], licenseH[i]);
			licenseN = img_threshold(rect);

			Mat kernel = getStructuringElement(MORPH_RECT, Size(3, 3), Point(-1, -1));
			morphologyEx(licenseN, licenseN, CV_MOP_OPEN, kernel);
			//下面两种方法选一个
			/*保存分割出的数字*/
			//1
			ostringstream oss;
			oss << i << ".jpg";
			imwrite(oss.str(), licenseN);
			//2
			//char fileName1[150];
			//sprintf_s(fileName1, "D:\\images_del\\src_%d.png", i); //保存图片的路径
			//cv::imwrite(fileName1, licenseN); //保存处理后的图片
		}

		String src_path = "*.jpg";/*分割数字的地址 需要修改*/
		vector<String> src_list;
		glob(src_path, src_list);  //遍历文件夹下的图片

		//readNum = new char[10];

		for (int i = 0; i < src_list.size(); i++)//分别读取每一个数字
		{
			Mat img_final;
			img_final = imread(src_list[i]);
			int index = getSubtract(img_final, 10);//将匹配度最高的数字返回
			data->datas[i] = index;
			//printf("%d", index);
		}
		data->flag = true;
		/*namedWindow("img", WINDOW_FREERATIO);
		imshow("img", img);

		cv::waitKey();*/
		//return  readNum;

	}
}

int  getSubtract(Mat& src, int TemplateNum)/*与模板做对比，返回匹配度最高的*/
{
	int sereNum = 0;
	vector<double> scores;
	//string tempNum[10] = { "0","1","2","3","4","5","6","7","8","9" };
	for (int i = 0; i < TemplateNum; i++)
	{
		/*string s1 = data->template_path;
		string s2 = tempNum[i] + ".jpg";
		string s3 = s1 + s2;*/
		char name[100];
		sprintf_s(name, "C:\\Users\\Administrator\\Desktop\\CDWM发布\\wwwroot\\modelphoto\\%d.jpg", i);/*读取模板 需要修改*/
		Mat  Template = imread(name);
		if (Template.empty())
		{
			return -1;
		}
		int width = src.cols - Template.cols + 1;//result宽度
		int height = src.rows - Template.rows + 1;//result高度
		Mat result(height, width, CV_32FC1);//创建结果映射图像
		/*模板匹配*/
		matchTemplate(src, Template, result, TM_CCOEFF_NORMED);
		//	normalize(result, result, 0, 1, NORM_MINMAX, -1);//归一sss化到0-1范围
			/*imshow("src", result);*/
		double minValue, maxValue;
		Point minLoc, maxLoc;
		minMaxLoc(result, &minValue, &maxValue, &minLoc, &maxLoc);
		scores.push_back(maxValue);
	
	}

	double max = 0;
	int index = -1;
	for (int i = 0; i < scores.size(); i++)
	{
		//cout << scores[i] << " ";
		if (max < scores[i])
		{
			max = scores[i];
			index = i;
		}
		if (max < 0.5)
		{
			index = -1;
		}
	}

	return index;
}

//void stctarr(_ARRAYSTRUCT* data)
//{
//	for (int i = 0; i < 50; i++) {
//		data->datas[i] = i + 50 * 1;
//	}
//
//	data->flag = true;
//}
