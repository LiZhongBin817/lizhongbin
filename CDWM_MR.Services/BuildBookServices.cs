using AutoMapper;
using CDWM_MR.IRepository;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using CDWM_MR_Common.Redis;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace CDWM_MR.Services
{
    public partial class BuildBookServices : BaseServices<mr_b_bookinfo>, IBuildBookServices
    {

        private readonly Iv_bookexcelRepository _Iv_bookexcelServices;
        private readonly Irt_b_watercarryover_historyRpository _Irt_b_watercarryover_historyServices;
        private readonly IRedisHelper _redishelper;
        readonly IHostingEnvironment env;
        private IMapper _mapper;

        public BuildBookServices(Imr_b_bookinfoRepository mrbbookinfo, Iv_bookexcelRepository Iv_bookexcelService, Irt_b_watercarryover_historyRpository Irt_b_watercarryover_historyService, IRedisHelper redis, IMapper iMapper)
        {
            _Iv_bookexcelServices = Iv_bookexcelService;
            _Irt_b_watercarryover_historyServices = Irt_b_watercarryover_historyService;
            _redishelper = redis;
            _mapper = iMapper;
            base.BaseDal = mrbbookinfo;
        }




        public async void BuildEXCELMethodAsync(string bookno)
        {
            List<v_bookexcel> data = new List<v_bookexcel>();
            List<bookExcel> storage = new List<bookExcel>();
            DateTime starttime = Convert.ToDateTime("1949-10-1 00:00"); ;
            decimal startnum = Convert.ToDecimal(0);
            decimal carrywatercount = Convert.ToDecimal(0);
            decimal treeaverage = Convert.ToDecimal(0);
            data = await _Iv_bookexcelServices.Query(c => c.bookno == bookno);
            if (data.Count == 0 && bookno != "")
            {

                await _redishelper.ListLeftPushAsync<string>("CDWM_BuildExcel", bookno);//没有数据，代表此时还未分配用户
                return;
            }
            for (int i = 0; i < data.Count; i++)
            {
                bookExcel bookexcel = new bookExcel();
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                string taskperiodname = year.ToString() + month.ToString();
                string taskperiodname1 = "";
                string taskperiodname2 = "";
                if (month == 1)
                {
                    taskperiodname1 = (year - 1).ToString() + 12;//上上月
                    taskperiodname2 = year.ToString() + 11;//上上上月
                }
                else if (month == 2)
                {
                    taskperiodname1 = year.ToString() + (month - 1).ToString();//上上月
                    taskperiodname2 = (year - 1).ToString() + 12;//上上上月
                }
                else
                {
                    taskperiodname1 = year.ToString() + (month - 1).ToString(); //上上月
                    taskperiodname2 = year.ToString() + (month - 2).ToString(); //上上上月
                }
                List<rt_b_watercarryover_history> dataobj = await _Irt_b_watercarryover_historyServices.Query(c => c.taskperiodname == taskperiodname);//上月
                List<rt_b_watercarryover_history> dataobj1 = await _Irt_b_watercarryover_historyServices.Query(c => c.taskperiodname == taskperiodname1);//上上月
                List<rt_b_watercarryover_history> dataobj2 = await _Irt_b_watercarryover_historyServices.Query(c => c.taskperiodname == taskperiodname2);//上上上月
                bookexcel = _mapper.Map<bookExcel>(data[i]);
                //bookexcel = data[i];
                if (dataobj.Count != 0)
                {
                    starttime = dataobj[0].starttime;
                    startnum = dataobj[0].startnum;
                    carrywatercount = dataobj[0].carrywatercount;
                    if (dataobj1.Count != 0 && dataobj2.Count != 0)
                    {
                        treeaverage = (dataobj[0].carrywatercount + dataobj1[0].carrywatercount + dataobj2[0].carrywatercount);//三月均量
                    }
                }
                bookexcel.starttime = starttime;//上期抄表时间
                bookexcel.startnum = startnum;
                bookexcel.carrywatercount = carrywatercount;
                bookexcel.treeaverage = treeaverage;
                bookexcel.bookinfo = "";//抄表信息
                bookexcel.laststate = "";//上期表况
                storage.Add(bookexcel);
            }
            var bookName = bookno;
            var path = bookName + ".xls";
            var rootPath = "c:/bookExcels/";
            if (System.IO.Directory.Exists(rootPath) == false)
                System.IO.Directory.CreateDirectory(rootPath);

            var newFile = rootPath + path;
            if (System.IO.File.Exists(newFile))
            {
                try
                {
                    System.IO.File.Delete(newFile);
                }
                catch (Exception)
                {

                }
            }
            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.ReadWrite))
            {
                IWorkbook workbook = new XSSFWorkbook();
                var sheet = workbook.CreateSheet("orders");
                var header = sheet.CreateRow(0);
                header.CreateCell(0).SetCellValue("表册名");
                header.CreateCell(1).SetCellValue("户名");
                header.CreateCell(2).SetCellValue("抄表周期");
                header.CreateCell(3).SetCellValue("表号");
                header.CreateCell(4).SetCellValue("上期抄表时间");
                header.CreateCell(5).SetCellValue("上期止码");
                header.CreateCell(6).SetCellValue("三月均量");
                header.CreateCell(7).SetCellValue("上期水量");
                header.CreateCell(8).SetCellValue("余额");
                header.CreateCell(9).SetCellValue("联系电话");
                header.CreateCell(10).SetCellValue("所属区域");
                header.CreateCell(11).SetCellValue("所属小区");
                header.CreateCell(12).SetCellValue("用水地址");
                header.CreateCell(13).SetCellValue("用水类型");
                header.CreateCell(14).SetCellValue("抄表员");
                header.CreateCell(15).SetCellValue("册内序号");
                header.CreateCell(16).SetCellValue("抄表信息");
                header.CreateCell(17).SetCellValue("水表类型");
                header.CreateCell(18).SetCellValue("安装位置");
                header.CreateCell(19).SetCellValue("水表口径");
                header.CreateCell(20).SetCellValue("上期表况处理");
                var rowIndex = 1;
                foreach (var item in storage)
                {
                    var datarow = sheet.CreateRow(rowIndex);
                    datarow.CreateCell(0).SetCellValue(item.bookname);
                    datarow.CreateCell(1).SetCellValue(item.username);
                    datarow.CreateCell(2).SetCellValue(item.readperiod);
                    datarow.CreateCell(3).SetCellValue(item.watermeternumber);
                    datarow.CreateCell(4).SetCellValue(item.starttime);
                    datarow.CreateCell(5).SetCellValue(item.startnum.ToString());
                    datarow.CreateCell(6).SetCellValue(item.treeaverage.ToString());
                    datarow.CreateCell(7).SetCellValue(item.carrywatercount.ToString());//上期水量
                    datarow.CreateCell(8).SetCellValue(item.balance);
                    datarow.CreateCell(9).SetCellValue(item.telephone);
                    datarow.CreateCell(10).SetCellValue(item.regionname);
                    datarow.CreateCell(11).SetCellValue(item.areaname);//所属小区                                      
                    datarow.CreateCell(12).SetCellValue(item.address);//用水地址
                    datarow.CreateCell(13).SetCellValue(item.naturename);//用水类型
                    datarow.CreateCell(14).SetCellValue(item.mrreadername);//抄表员
                    datarow.CreateCell(15).SetCellValue(item.meterseq);//册内序号
                    datarow.CreateCell(16).SetCellValue(item.bookinfo);//抄表信息
                    datarow.CreateCell(17).SetCellValue(item.metertypename);//水表类型
                    datarow.CreateCell(18).SetCellValue(item.postname);//安装位置
                    datarow.CreateCell(19).SetCellValue(item.caliber);//水表口径
                    datarow.CreateCell(20).SetCellValue(item.laststate);//上期表况
                    rowIndex++;
                }
                workbook.Write(fs);
                var memory = new MemoryStream();
                using (var stream = new FileStream(newFile, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;
            }
            return;
        }


        public async void DoworkAsync()
        {
            var len = await _redishelper.ListLengthAsync("CDWM_BuildExcel");
            if (len <= 0)
            {
                return;
            }
            var bookNum = string.Empty;
            try
            {
                bookNum = await _redishelper.ListRightPopAsync<string>("CDWM_BuildExcel");
                BuildEXCELMethodAsync(bookNum);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _redishelper.ListLeftPushAsync<string>("CDWM_BuildExcel", bookNum);//如果发生异常，重新进入队列
            }
        }

    }
}
