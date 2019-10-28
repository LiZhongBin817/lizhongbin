using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 抄见率分析
    /// </summary>
    public class CopySee : Controller
    {

        readonly Imr_b_readerServices _mr_b_readerServices;
        readonly Imr_datainfo_historyServices _mr_datainfo_historyServices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mr_b_readerServices"></param>
        /// <param name="mr_datainfo_historyServices"></param>
        public CopySee(Imr_b_readerServices mr_b_readerServices, Imr_datainfo_historyServices mr_datainfo_historyServices)
        {
            _mr_b_readerServices = mr_b_readerServices;
            _mr_datainfo_historyServices = mr_datainfo_historyServices;
        }
        /// <summary>
        /// 实现抄表率分析
        /// </summary>
        /// <param name="mrreadername">抄表员名称</param>
        /// <param name="taskperiodname">抄表的年月</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowCopysee")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> ShowCopysee(string mrreadername, string taskperiodname, int page = 1, int limit = 5)
        {
            PageModel<mr_datainfo_history> user = new PageModel<mr_datainfo_history>();
            List<object> datalist = new List<object>();
            Expression<Func<mr_datainfo_history, bool>> wherelambda = c => true;
            string dateTime = "";
            if (string.IsNullOrEmpty(taskperiodname))
            {
                taskperiodname = (DateTime.Now.Year).ToString() + (DateTime.Now.Month).ToString().PadLeft(2, '0');
                dateTime = taskperiodname;
                wherelambda = PredicateExtensions.And<mr_datainfo_history>(wherelambda, c => c.taskperiodname == taskperiodname);
            }
            if (!string.IsNullOrEmpty(taskperiodname))
            {
                dateTime = taskperiodname;
                wherelambda = PredicateExtensions.And<mr_datainfo_history>(wherelambda, c => c.taskperiodname == taskperiodname);
            }
            if (!string.IsNullOrEmpty(mrreadername))
            {
                wherelambda = PredicateExtensions.And<mr_datainfo_history>(wherelambda, c => c.mrreadername == mrreadername);
            }
            user = await _mr_datainfo_historyServices.QueryPage(wherelambda, page, limit);
            if (user.data.Count() == 0)
            {
                return new TableModel<object>()
                {
                    code = 0,
                    msg = "no",
                    count = 0,

                };
            }
            double allshoudcopy = user.data.FindAll(c => c.readstatus == 1).Count() + user.data.FindAll(a => a.readstatus == 0).Count();
            double allalreadycopy = user.data.FindAll(c => c.readstatus == 1).Count();
            double allreallycopy = user.data.FindAll(c => c.readtype == 1).Count();
            string allcopyrate = (Math.Round(Convert.ToDouble(allalreadycopy) / Convert.ToDouble(allshoudcopy) * 100, 2)).ToString();
            allcopyrate = allcopyrate + "%";
            string allreallyrate = (Math.Round(Convert.ToDouble(allreallycopy) / 1.0 / Convert.ToDouble(allshoudcopy) * 100, 2) / 1.0).ToString();
            allreallyrate = allreallyrate + "%";
            var datatwo = new
            {
                allshoudcopy = allshoudcopy,
                allalreadycopy = allalreadycopy,
                allreallycopy = allreallycopy,
                allcopyrate = allcopyrate,
                allreallyrate = allreallyrate

            };


            for (int i = 0; i < user.data.Count(); i++)
            {
                var data1 = user.data.FindAll(c => c.mrreadername == user.data[i].mrreadername && c.taskperiodname == dateTime);
                //if (!(name.Contains(user.data[i].mrreadername)))判重
                //已抄
                int drop = user.data.FindAll(c => c.readstatus == 1 && c.mrreadername == user.data[i].mrreadername && c.taskperiodname == dateTime).Count();
                //未抄
                int uncopied = user.data.FindAll(c => c.readstatus == 0 && c.mrreadername == user.data[i].mrreadername && c.taskperiodname == dateTime).Count();
                int copy = user.data.FindAll(c => c.readtype == 1 && c.mrreadername == user.data[i].mrreadername && c.taskperiodname == dateTime).Count();
                int shoudcopy = drop + uncopied;
                string droprate = (Math.Round(Convert.ToDouble(drop) / Convert.ToDouble(shoudcopy) * 100, 2)).ToString();
                droprate = droprate + "%";
                string copyrate = (Math.Round(Convert.ToDouble(copy) / 1.0 / Convert.ToDouble(shoudcopy) * 100, 2) / 1.0).ToString();
                copyrate = copyrate + "%";
                var data = new
                {
                    name = user.data[i].mrreadername,
                    month = dateTime,
                    drop = drop,
                    uncopied = uncopied,
                    copy = copy,
                    shoudcopy = shoudcopy,
                    droprate = droprate,
                    copyrate = copyrate,
                    datatwo = datatwo,

                };
                datalist.Add(data);

            }
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = datalist.Count(),
                data = datalist
            };

        }
        /// <summary>
        /// 给前台下拉框传值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Serchname")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> Serchname()
        {


            List<mr_b_reader> name = new List<mr_b_reader>();
            name = await _mr_b_readerServices.Query();
            List<object> dataname = new List<object>();
            for (int i = 0; i < name.Count(); i++)
            {
                dataname.Add(name[i].mrreadername);
            }
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = dataname.Count(),
                data = dataname

            };

        }


        #region 导出Excel
        /// <summary>
        /// /用于导出Exece文件
        /// </summary>
        /// <param name="taskperiodname">抄表的月份</param>
        /// <param name="mrreadername">抄表人</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("OutExcel1")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<FileResult> OutExcel1(string taskperiodname, string mrreadername, int page = 1, int limit = 5)
        {
            PageModel<mr_datainfo_history> user = new PageModel<mr_datainfo_history>();
            Expression<Func<mr_datainfo_history, bool>> wherelambda = c => true;
            Hashtable tb = new Hashtable();
            string dateTime = "";
            if (string.IsNullOrEmpty(taskperiodname))
            {
                taskperiodname = (DateTime.Now.Year).ToString() + (DateTime.Now.Month).ToString().PadLeft(2, '0');
                dateTime = taskperiodname;
                wherelambda = PredicateExtensions.And<mr_datainfo_history>(wherelambda, c => c.taskperiodname == taskperiodname);
            }
            if (!string.IsNullOrEmpty(taskperiodname))
            {
                dateTime = taskperiodname;
                wherelambda = PredicateExtensions.And<mr_datainfo_history>(wherelambda, c => c.taskperiodname == taskperiodname);
            }
            if (!string.IsNullOrEmpty(mrreadername))
            {
                wherelambda = PredicateExtensions.And<mr_datainfo_history>(wherelambda, c => c.mrreadername == mrreadername);
            }
            user = await _mr_datainfo_historyServices.QueryPage(wherelambda, page, limit);
            // double allyichao = user.data.FindAll(c => c.readstatus == 1).Count() + user.data.FindAll(a => a.readstatus == 0).Count();
            //  double allshichao = user.data.FindAll(c => c.readtype == 1).Count();
            //  List<object> datalist = new List<object>();
            int drop;
            int shoudcopy;
            int copy;
            int uncopied;
            string droprate;
            string copyrate;
            MemoryStream data = new MemoryStream();
            List<mr_datainfo_history> ExcelList = await _mr_datainfo_historyServices.Query(wherelambda);
            tb.Add("mrreadername", "抄表员");
            tb.Add("taskperiodname", "抄表月份");
            tb.Add("drop", "已抄表");
            tb.Add("copy", "实抄表");
            tb.Add("shoudecopy", "应抄表");
            tb.Add("droprate", "抄见率");
            tb.Add("copyrate", "实抄率");
            for (int i = 0; i < user.data.Count(); i++)
            {

                var data1 = user.data.FindAll(c => c.mrreadername == user.data[i].mrreadername && c.taskperiodname == dateTime);
                drop = user.data.FindAll(c => c.readstatus == 1 && c.mrreadername == user.data[i].mrreadername && c.taskperiodname == dateTime).Count();
                uncopied = user.data.FindAll(c => c.readstatus == 0 && c.mrreadername == user.data[i].mrreadername && c.taskperiodname == dateTime).Count();
                copy = user.data.FindAll(c => c.readtype == 1 && c.mrreadername == user.data[i].mrreadername && c.taskperiodname == dateTime).Count();
                shoudcopy = drop + uncopied;
                droprate = (Math.Round(Convert.ToDouble(shoudcopy) / Convert.ToDouble(shoudcopy) * 100, 2)).ToString();
                droprate = droprate + "%";
                copyrate = (Math.Round(Convert.ToDouble(copy) / 1.0 / Convert.ToDouble(shoudcopy) * 100, 2) / 1.0).ToString();
                copyrate = copyrate + "%";




                var lalala = ExcelList.Select(c => new AnalysisofMeterReadingRate
                {
                    mrreadername = c.mrreadername,
                    taskperiodname = c.taskperiodname,
                    drop = drop,
                    copy = copy,
                    shoudcopy = shoudcopy,
                    droprate = droprate,
                    copyrate = copyrate

                }).ToList();
                data = OfficeHelper.getExcel<AnalysisofMeterReadingRate>(lalala, tb);

            }

            string fileExt = ".xls";
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            return File(data, memi, "抄表率分析.xlsx");
        }
        #endregion

    }
}
