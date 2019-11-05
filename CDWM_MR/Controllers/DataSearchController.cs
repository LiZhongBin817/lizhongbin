using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 抄表数据查询/抄表量化分析
    /// </summary>
    ///  [Produces("application/json")]
    [Route("api/DataSearch")]
    [AllowAnonymous]
    [EnableCors("AllRequests")]
    public class DataSearchController : ControllerBase
    {

        #region 参数名
        readonly Iv_b_datasearch_historyServices _B_Datasearch_HistoryServices;
        readonly Iv_b_areas_regionServices _B_Areas_RegionServices;
        readonly Iv_b_regionServices _B_RegionsServices;
        readonly Iv_mr_date_readerServices _Date_ReaderServices;
        readonly Iv_mr_b_readerServices _Mr_B_ReaderServices;
        readonly Iv_reader_analysisServices _Reader_AnalysisServices;
        #endregion

        #region  构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="B_Datasearch_HistoryServices"></param>
        /// <param name="Date_ReaderServices"></param>
        /// <param name="B_Areas_RegionServices"></param>
        /// <param name="B_RegionsServices"></param>
        /// <param name="Mr_B_ReaderServices"></param>
        /// <param name="Reader_AnalysisServices"></param>
        public DataSearchController(Iv_b_datasearch_historyServices B_Datasearch_HistoryServices, Iv_mr_date_readerServices Date_ReaderServices, Iv_b_areas_regionServices B_Areas_RegionServices, Iv_b_regionServices B_RegionsServices, Iv_mr_b_readerServices Mr_B_ReaderServices, Iv_reader_analysisServices Reader_AnalysisServices)
        {
            _B_Datasearch_HistoryServices = B_Datasearch_HistoryServices;
            _B_Areas_RegionServices = B_Areas_RegionServices;
            _B_RegionsServices = B_RegionsServices;
            _Date_ReaderServices = Date_ReaderServices;
            _Mr_B_ReaderServices = Mr_B_ReaderServices;
            _Reader_AnalysisServices=Reader_AnalysisServices;

        }
        #endregion

        #region 抄表数据查询控制器
        /// <summary>
        /// 抄表数据查询
        /// </summary>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <param name="meternum"></param>
        /// <param name="telephone"></param>
        /// <param name="meterbooknumber"></param>
        /// <param name="mrreadername"></param>
        /// <param name="ordatatime01"></param>
        /// <param name="ordatatime02"></param>
        /// <param name="regionname"></param>
        /// <param name="areaname"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowDataSearchInfo")]
        public async Task<TableModel<object>> ShowDataSearchInfo(string account, string username, string meternum, string telephone, string meterbooknumber, string mrreadername, string ordatatime01, string ordatatime02, string regionname, string areaname, int page = 1, int limit = 20)
        {
            PageModel<object> pageModel = new PageModel<object>();
            #region  lambda拼接 
            Expression<Func<v_b_datasearch_history, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(ordatatime01) && !string.IsNullOrEmpty(ordatatime02))
            {
                DateTime d1 = Convert.ToDateTime(ordatatime01);
                DateTime d2 = Convert.ToDateTime(ordatatime02);
                wherelambda = PredicateExtensions.And<v_b_datasearch_history>(wherelambda, c => c.omrdatetime > d1 && c.omrdatetime < d2);

            }
            if (!string.IsNullOrEmpty(account))
            {
                wherelambda = PredicateExtensions.And<v_b_datasearch_history>(wherelambda, c => c.account.Contains(account));
            }
            if (!string.IsNullOrEmpty(username))
            {
                wherelambda = PredicateExtensions.And<v_b_datasearch_history>(wherelambda, c => c.username.Contains(username));
            }
            if (!string.IsNullOrEmpty(meternum))
            {
                wherelambda = PredicateExtensions.And<v_b_datasearch_history>(wherelambda, c => c.meternum.Contains(meternum));
            }
            if (!string.IsNullOrEmpty(telephone))
            {
                wherelambda = PredicateExtensions.And<v_b_datasearch_history>(wherelambda, c => c.telephone.Contains(telephone));
            }
            if (!string.IsNullOrEmpty(meterbooknumber))
            {
                wherelambda = PredicateExtensions.And<v_b_datasearch_history>(wherelambda, c => c.meterbooknumber.Contains(meterbooknumber));
            }
            if (!string.IsNullOrEmpty(mrreadername))
            {
                wherelambda = PredicateExtensions.And<v_b_datasearch_history>(wherelambda, c => c.mrreadername.Contains(mrreadername));
            }
            if (!string.IsNullOrEmpty(regionname))
            {
                wherelambda = PredicateExtensions.And<v_b_datasearch_history>(wherelambda, c => c.regionname.Contains(regionname));
            }
            if (!string.IsNullOrEmpty(areaname))
            {
                wherelambda = PredicateExtensions.And<v_b_datasearch_history>(wherelambda, c => c.areaname.Contains(areaname));
            }
            #endregion

            #region  拿值
            Expression<Func<v_b_datasearch_history, object>> expression = c => new
            {
                id = c.id,
                meternum = c.meternum,
                account = c.account,
                username = c.username,
                address = c.address,
                telephone = c.telephone,
                meterbookname = c.meterbookname,
                meterbooknumber = c.meterbooknumber,
                mrreadernumber = c.mrreadernumber,
                mrreadername = c.mrreadername,
                areaname = c.areaname,
                regionname = c.regionname,
                taskperiodname = c.taskperiodname,
                lastmonthdata = c.lastmonthdata,
                nowmonthdata = c.nowmonthdata,
                usewaternum = c.usewaternum,
                omrdatetime = c.omrdatetime,
                readstatus = c.readstatus,
                readDateTime = c.readDateTime,
                uploadtime = c.uploadtime,
                readtype = c.readtype,
                meterstatus = c.meterstatus,
            };
            #endregion
            pageModel = await _B_Datasearch_HistoryServices.QueryPage(wherelambda, expression, page, limit, "");


            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                count = pageModel.dataCount,
                data = pageModel.data
            };
        }
        #endregion

        #region 渲染第二个小区下拉框
        /// <summary>
        /// 渲染第二个小区下拉框
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("renderdataInfo")]
        public async Task<TableModel<object>> renderdataInfo(string JsonData)
        {
            List<v_b_areas_region> data = new List<v_b_areas_region>();
            var list = await _B_Areas_RegionServices.Query();
            List<object> datalist = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                data = list.FindAll(c => c.regionname == JsonData);
            }
            foreach (var item in data)
            {
                datalist.Add(item.areaname);
            }
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                data = datalist
            };
        }
        #endregion

        #region 渲染第一个区域下拉框
        /// <summary>
        /// 渲染第一个区域下拉框
        /// </summary>
        /// <returns></returns> 
        [HttpPost]
        [Route("render_regionInfo")]
        public async Task<TableModel<object>> render_regionInfo()
        {

            List<t_b_regions> data = new List<t_b_regions>();
            var list = await _B_RegionsServices.Query();
            List<object> datalist = new List<object>();
            foreach (var item in list)
            {
                datalist.Add(item.regionname);
            }
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                data = datalist
            };
        }
        #endregion

        #region 导出EXcel表格
        /// <summary>
        /// 导出EXcel表格
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        [Route("OutExcelDataSearch")]
        public async Task<FileResult> OutExcelDataSearch()
        {
            List<v_b_datasearch_history> ExcelList = await _B_Datasearch_HistoryServices.Query(c => true);
            #region  导出赋值
            var lalala = ExcelList.Select(c => new v_b_datasearch_history
            {
                id = c.id,
                account = c.account,
                username = c.username,
                autoaccount = c.autoaccount,
                mrreadernumber = c.mrreadernumber,
                areano = c.areano,
                regionno = c.regionno,
                taskperiodname = c.taskperiodname,
                recheckresult = c.recheckresult,
                readcheckdata = c.readcheckdata,
                remark = c.remark,
                meternum = c.meternum,
                regionname = c.regionname,
                areaname = c.areaname,
                telephone = c.telephone,
                meterbooknumber = c.meterbooknumber,
                address = c.address,
                mrreadername = c.mrreadername,
                readDateTime = c.readDateTime,
                lastmonthdata = c.lastmonthdata,
                nowmonthdata = c.nowmonthdata,
                usewaternum = c.usewaternum,
                omrdatetime = c.omrdatetime,
                readtype = c.readtype,
                meterstatus = c.meterstatus
            }).ToList();
            #endregion

            Hashtable tb = new Hashtable();
            tb.Add("id", "序号");
            tb.Add("account", "户号");
            tb.Add("username", "户名");
            tb.Add("meternum", "表号");
            tb.Add("regionname", "区域");
            tb.Add("areaname", "小区");
            tb.Add("telephone", "联系电话");
            tb.Add("meterbooknumber", "抄表册");
            tb.Add("address", "地址");
            tb.Add("mrreadername", "抄表员");
            tb.Add("readDateTime", "抄表月份");
            tb.Add("lastmonthdata", "上月读数");
            tb.Add("nowmonthdata", "本月读数");
            tb.Add("usewaternum", "本月用量");
            tb.Add("omrdatetime", "抄表时间");
            tb.Add("readtype", "抄表状态");
            tb.Add("meterstatus", "表况");
            MemoryStream data = OfficeHelper.getExcel<v_b_datasearch_history>(lalala, tb);
            string fileExt = ".xls";
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            return File(data, memi, "抄表数据查询表.xlsx");
        }
        #endregion

        #region 抄表员量化分析
        /// <summary>
        /// 抄表员量化分析
        /// </summary>
        /// <param name="readDatetime01"></param>
        /// <param name="mrreadername"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReadAnalysis")]
        public async Task<TableModel<object>> ReadAnalysis(string readDatetime01, string mrreadername)
        {
            List<v_mr_date_reader> pageModel = new List<v_mr_date_reader>();
            Expression<Func<v_mr_date_reader, bool>> wherelambda = c => true;
            #region lambda拼接
            if (!string.IsNullOrEmpty(readDatetime01))
            {

                wherelambda = PredicateExtensions.And<v_mr_date_reader>(wherelambda, c => c.readtime.ToString() == readDatetime01);
            }
            if (!string.IsNullOrEmpty(mrreadername))
            {
                wherelambda = PredicateExtensions.And<v_mr_date_reader>(wherelambda, c => c.mrreadername ==mrreadername);
            }
            #endregion 
            pageModel = await _Date_ReaderServices.Query(wherelambda);
            var t = pageModel
                 .OrderBy(c => c.readtime)
                 .OrderBy(c => c.mrreadername)
                 .Select(c => new {
                     id = c.id,
                     maxdatetime = c.maxdatetime,
                     metermonth = c.metermonth,
                     meternum = c.meternum,
                     mindatatime = c.mindatatime,
                     mrreadername = c.mrreadername,
                     readmetertime = c.readmetertime*1.00 / 3600,
                     readtime = c.readtime
                 });
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                count = pageModel.Count,
                data = t
            };
        }
        #endregion

        #region 抄表员量化导出
        /// <summary>
        /// 抄表员量化导出
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        [Route("OutExcelReadAnalysis")]
        public async Task<FileResult> OutExcelReadAnalysis()
        {
            List<v_mr_date_reader> ExcelList = await _Date_ReaderServices.Query(c => true);
            #region  导出赋值
            var daochu = ExcelList.Select(c => new v_mr_date_reader
            {
                id = c.id,
                mrreadername = c.mrreadername,
                readtime = c.readtime,
                mindatatime = c.mindatatime,
                maxdatetime = c.maxdatetime,
                meternum = c.meternum,
                readmetertime = c.readmetertime / 3600  ,
                metermonth = c.metermonth,

            }).ToList();
            #endregion

            Hashtable tb = new Hashtable();
            tb.Add("id", "序号");
            tb.Add("mrreadername", "抄表员姓名");
            tb.Add("readtime", "抄表日期");
            tb.Add("mindatatime", "开始时间");
            tb.Add("maxdatetime", "结束时间");
            tb.Add("meternum", "抄表个数");
            tb.Add("readmetertime", "抄表时长");
            tb.Add("metermonth", "抄表月份");
            MemoryStream data = OfficeHelper.getExcel<v_mr_date_reader>(daochu, tb);
            string fileExt = ".xls";
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            return File(data, memi, "抄表员量化分析表.xlsx");
        }
        #endregion

        #region 渲染抄表员的下拉框
        /// <summary>
        /// 渲染抄表员的下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("render_ReaderAnalysis")]
        public async Task<TableModel<object>> render_ReaderAnalysis()
        {

            List<v_mr_b_reader> data = new List<v_mr_b_reader>();
            var list01 = await _Mr_B_ReaderServices.Query();
            List<object> datalist = new List<object>();
            foreach (var item in list01)
            {
                datalist.Add(item.mrreadername);
            }
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                count = datalist.Count,
                data = datalist
            };
        }
        #endregion

        #region 抄表员量化汇总分析
        /// <summary>
        /// 抄表员量化汇总分析
        /// </summary>
        /// <param name="readDatetime01"></param>
        /// <param name="mrreadername"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReadAnalysis_Sum")]
        public async Task<TableModel<object>> ReadAnalysis_Sum(string readDatetime01, string mrreadername)
        {
            List<v_reader_analysis> pageModel = new List<v_reader_analysis>();
            Expression<Func<v_reader_analysis, bool>> wherelambda = c => true;
            #region lambda拼接
            if (!string.IsNullOrEmpty(readDatetime01))
            {

                wherelambda = PredicateExtensions.And<v_reader_analysis>(wherelambda, c => c.readmonth.ToString() == readDatetime01);
            }
            if (!string.IsNullOrEmpty(mrreadername))
            {
                wherelambda = PredicateExtensions.And<v_reader_analysis>(wherelambda, c => c.mrreadername == mrreadername);
            }
            #endregion

            #region 拿值
            Expression<Func<v_reader_analysis, object>> expression = c => new
            {
                id = c.id,
                maxdatetime = c.maxdatetime,
                readmonth = c.readmonth,
                meternum = c.meternum,
                mindatatime = c.mindatatime,
                mrreadername = c.mrreadername,
                readmetertime = c.readmetertime / 3600  ,
               

            };
            #endregion
            pageModel = await _Reader_AnalysisServices.Query(wherelambda);
            var t = pageModel
                 .OrderBy(c => c.mindatatime)
                 .OrderBy(c => c.mrreadername)
                 .Select(c => new {
                     id = c.id,
                     maxdatetime = c.maxdatetime,
                     readmonth = c.readmonth,
                     meternum = c.meternum,
                     mindatatime = c.mindatatime,
                     mrreadername = c.mrreadername,
                     readmetertime = c.readmetertime / 3600  ,
                      
                 });
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                count = pageModel.Count,
                data = t
            };

        }
        #endregion

        #region 抄表员量化汇总导出
        /// <summary>
        /// 抄表员量化汇总导出
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        [Route("OutExcelReadAnalysisSum")]
        public async Task<FileResult> OutExcelReadAnalysisSum()
        {
            List<v_reader_analysis> ExcelList = await _Reader_AnalysisServices.Query(c => true);
            #region  导出赋值
            var daochu = ExcelList.Select(c => new v_reader_analysis
            {
                id = c.id,
                mrreadername = c.mrreadername, 
                mindatatime = c.mindatatime,
                maxdatetime = c.maxdatetime,
                meternum = c.meternum,
                readmetertime = c.readmetertime / 3600  ,
                readmonth = c.readmonth,

            }).ToList();
            #endregion

            Hashtable tb = new Hashtable();
            tb.Add("id", "序号");
            tb.Add("mrreadername", "抄表员姓名"); 
            tb.Add("mindatatime", "开始时间");
            tb.Add("maxdatetime", "结束时间");
            tb.Add("meternum", "抄表个数");
            tb.Add("readmetertime", "抄表时长");
            tb.Add("readmonth", "抄表月份");
            MemoryStream data = OfficeHelper.getExcel<v_reader_analysis>(daochu, tb);
            string fileExt = ".xls";
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            return File(data, memi, "抄表员量化汇总分析表.xlsx");
        }
        #endregion 

        #region 抄表员与个数的键值对集合
        /// <summary>
        /// 抄表员键值对集合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ReadAnalysis_key")]
        public async Task<TableModel<object>> ReadAnalysis_key()
        { 
            List<v_reader_analysis> data = new List<v_reader_analysis>();
            var list01 = await _Reader_AnalysisServices.Query();
            List<object> datalist01 = new List<object>();//抄表员
            List<object> datalist02 = new List<object>();// 水表数
             
            foreach (var item in list01)
            {
                datalist01.Add(item.mrreadername);
                datalist02.Add(item.meternum);
            }
            var data1 = new
            {
                datalist01,
                datalist02,
            };
            return new TableModel<object>
            {
                code = 0,
                msg = "OK", 
                data = data1
            };

        }
        #endregion

        #region 抄表员与时间的键值对 
        /// <summary>
        /// 抄表员与时间的键值对
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ReadAnalysis_val")]
        public async Task<TableModel<object>> ReadAnalysis_val()
        {
            List<v_reader_analysis> data = new List<v_reader_analysis>();
            var list01 = await _Reader_AnalysisServices.Query();
            List<object> datalist01 = new List<object>();//抄表员
            List<object> datalist02 = new List<object>();// 水表数

            foreach (var item in list01)
            {
                datalist01.Add(item.mrreadername);
                datalist02.Add(item.readmetertime / 3600);
            }
            var data1 = new
            {
                datalist01,
                datalist02,
            };
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                data = data1
            };

        }
        #endregion
    }
}