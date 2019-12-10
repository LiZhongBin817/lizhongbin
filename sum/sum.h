#pragma once


typedef struct _ARRAYSTRUCT {
	char* img_path;
	char* template_path;
	bool flag;

	int datas[5];

} ARRAYSTRUCT;

//extern "C" __declspec(dllexport) int* sum(char* img_path);

extern "C" __declspec(dllexport) void stctarr(_ARRAYSTRUCT* data);