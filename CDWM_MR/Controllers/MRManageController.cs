using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 抄表数据管理
    /// </summary>
    public class MRManageController : ControllerBase
    {
        #region 相关变量
        readonly Iv_mr_datainfoServices _Mr_DatainfoServices;
        readonly Irt_b_watercarryover_historyServices _B_Watercarryover_HistoryServices;
        readonly Irt_b_recheckServices _B_RecheckServices;
        readonly Irt_b_watercarryoverServices _B_WatercarryoverServices;
        readonly Imr_datainfoServices _DatainfoServices;
        readonly Iv_recheck_recheckhistoryServices _Recheck_RecheckhistoryServices;
        readonly Iv_union_datainfoocrlog_datainfohistoryocrloghistoryServices _Union_Datainfoocrlog_DatainfohistoryocrloghistoryServices;
        readonly Imr_datainfo_historyServices _Datainfo_HistoryServices;
        readonly Imr_b_readerServices _B_ReaderServices;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mr_DatainfoServices"></param>
        /// <param name="b_Watercarryover_HistoryServices"></param>
        /// <param name="b_RecheckServices"></param>
        /// <param name="b_WatercarryoverServices"></param>
        /// <param name="datainfoServices"></param>
        /// <param name="recheck_RecheckhistoryServices"></param>
        /// <param name="union_Datainfoocrlog_DatainfohistoryocrloghistoryServices"></param>
        public MRManageController(Iv_mr_datainfoServices mr_DatainfoServices, Irt_b_watercarryover_historyServices b_Watercarryover_HistoryServices, Irt_b_recheckServices b_RecheckServices, Irt_b_watercarryoverServices b_WatercarryoverServices, Imr_datainfoServices datainfoServices, Iv_recheck_recheckhistoryServices recheck_RecheckhistoryServices, Iv_union_datainfoocrlog_datainfohistoryocrloghistoryServices union_Datainfoocrlog_DatainfohistoryocrloghistoryServices, Imr_datainfo_historyServices datainfo_HistoryServices, Isys_userinfoServices userinfoServices, Imr_b_readerServices b_ReaderServices)
        {
            _Mr_DatainfoServices = mr_DatainfoServices;
            _B_Watercarryover_HistoryServices = b_Watercarryover_HistoryServices;
            _B_RecheckServices = b_RecheckServices;
            _B_WatercarryoverServices = b_WatercarryoverServices;
            _DatainfoServices = datainfoServices;
            _Recheck_RecheckhistoryServices = recheck_RecheckhistoryServices;
            _Union_Datainfoocrlog_DatainfohistoryocrloghistoryServices = union_Datainfoocrlog_DatainfohistoryocrloghistoryServices;
            _Datainfo_HistoryServices = datainfo_HistoryServices;
            _B_ReaderServices = b_ReaderServices;
        }

        /// <summary>
        /// 显示抄表数据
        /// </summary>
        /// <param name="username"></param>
        /// <param name="account"></param>
        /// <param name="meternum"></param>
        /// <param name="address"></param>
        /// <param name="mrreadername"></param>
        /// <param name="bookno"></param>
        /// <param name="rtrecheckstatus"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Show_CB_DataInfo")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> Show_CB_DataInfo(string username, string account, string meternum, string address, string mrreadername, string bookno, int rtrecheckstatus = 3, int page = 1, int limit = 20)
        {
            //跟踪登录用户
            string FUserName = Permissions.UersName;
            string last_month = DateTime.Now.AddMonths(-1).Month.ToString();//上个月
            string the_month_before_last = DateTime.Now.AddMonths(-2).Month.ToString();//上上个月
            PageModel<object> pageModel = new PageModel<object>();
            #region lambda拼接式 
            Expression<Func<v_mr_datainfo, bool>> wherelambda = c => true;
            if (rtrecheckstatus != 3 && rtrecheckstatus != 1)
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.rtrecheckstatus == rtrecheckstatus);
            }
            if (rtrecheckstatus == 1)
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.rtrecheckstatus == null);
            }
            if (!string.IsNullOrEmpty(username))
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.username == username);
            }
            if (!string.IsNullOrEmpty(account))
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.account == account);
            }
            if (!string.IsNullOrEmpty(meternum))
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.meternum == meternum);
            }
            if (!string.IsNullOrEmpty(address))
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.address == address);
            }
            if (!string.IsNullOrEmpty(mrreadername))
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.mrreadername == mrreadername);
            }
            if (!string.IsNullOrEmpty(bookno))
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.bookno == bookno);
            }
            #endregion
            Expression<Func<v_mr_datainfo, object>> expression = c => new
            {
                id = c.ID,
                autoaccount = c.autoaccount,
                account = c.account,
                username = c.username,
                telephone = c.telephone,
                meternum = c.meternum,
                metername = c.metername,
                areano = c.areano,
                areaname = c.areaname,
                regionname = c.regionname,
                buildno = c.buildno,
                address = c.address,
                mrreadername = c.mrreadername,
                bookname = c.bookname,
                bookno = c.bookno,
                readtype = c.readtype,
                taskperiodname = c.taskperiodname,
                inputdata = c.inputdata,
                ocrdata = c.ocrdata,
                uploadgisplace = c.uploadgisplace,
                recheckstatus = c.recheckstatus,
                recheckresult = c.recheckresult,
                checkor = c.checkor,
                rtrecheckstatus = c.rtrecheckstatus,
                FUserName = FUserName,
                checksuccesstime = c.checksuccesstime,
                checktime = c.checktime,
                readstatus = c.readstatus,
                carrystatus = c.carrystatus,
                carryime = c.carryime,
                lastmonthdata = c.lastmonthdata == null ? 0 : c.lastmonthdata,
                nowmonthdata = c.nowmonthdata == null ? 0 : c.nowmonthdata,
                last_month = last_month,
                the_month_before_last = the_month_before_last

            };
            pageModel = await _Mr_DatainfoServices.QueryPage(wherelambda, expression, page, limit, "");

            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                data = pageModel.data,
                count = pageModel.dataCount

            };

        }

        /// <summary>
        /// 结转数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CarryData")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> CarryData()
        {
            #region 连接数据库查询
            //查询结转表里面的数据
            List<rt_b_watercarryover> carryedData = await _B_WatercarryoverServices.Query();
            //查询审核表中审核状态为0（审核通过了的）的所有数据
            List<rt_b_recheck> checkPassData = await _B_RecheckServices.Query(c => c.recheckstatus == 0);
            List<v_recheck_recheckhistory> rt_B_Rechecks = await _Recheck_RecheckhistoryServices.Query();
            List<mr_datainfo_history> datainfo_Histories = await _Datainfo_HistoryServices.Query();
            List<mr_datainfo> dataInfo = await _DatainfoServices.Query();
            #endregion
            List<rt_b_watercarryover> Datalist = new List<rt_b_watercarryover>();           
            List<rt_b_watercarryover> data = new List<rt_b_watercarryover>();         
            if (checkPassData.Count == 0)//没有审核通过的数据
            {
                return new TableModel<object>
                {
                    code = 0,
                    msg = "NO",
                    data = "",
                };
            }
            foreach (var item in checkPassData)
            {
                rt_b_watercarryover addData = new rt_b_watercarryover();
                data = carryedData.FindAll(c => c.autoaccount == item.userid && c.taskperiodname == item.taskperiodname);
                if (data.Count == 0)//判重
                {
                    List<v_recheck_recheckhistory> rtRecheckData = new List<v_recheck_recheckhistory>();
                    List<mr_datainfo_history> datainfoHistory_Data = new List<mr_datainfo_history>();

                    //如果当月月份是月初，将查询上一年12月份的数据
                    if (item.taskperiodname == DateTime.Now.Year.ToString() + "01")
                    {
                        string datetime = item.taskperiodname.Insert(4, "-");
                        string ReadTime = (DateTime.Parse(datetime).AddMonths(-1)).ToString().Substring(0, 7).Replace("/", "");
                        rtRecheckData = rt_B_Rechecks.FindAll(c => c.userid == item.userid && c.taskperiodname == ReadTime);
                        datainfoHistory_Data = datainfo_Histories.FindAll(c => c.autoaccount == item.userid && c.taskperiodname == ReadTime);
                    }
                    else//不是月初
                    {
                        int number = Convert.ToInt32(item.taskperiodname);
                        rtRecheckData = rt_B_Rechecks.FindAll(c => c.userid == item.userid && c.taskperiodname == (number - 1).ToString());
                        datainfoHistory_Data = datainfo_Histories.FindAll(c => c.autoaccount == item.userid && c.taskperiodname == (number - 1).ToString());
                    }
                    addData.autoaccount = item.userid.ToString();
                    addData.taskperiodname = item.taskperiodname;
                    addData.meternum = item.meternum;
                    if (rtRecheckData.Count == 0)
                    {
                        addData.startnum = 0;
                    }
                    else
                    {
                        addData.startnum = (decimal)rtRecheckData[0].recheckdata;
                    }

                    if (datainfoHistory_Data.Count == 0)
                    {
                        addData.starttime = DateTime.Now;
                        addData.startid = 0;
                    }
                    else
                    {
                        addData.starttime = datainfoHistory_Data[0].uploadtime;
                        addData.startid = datainfoHistory_Data[0].id;
                    }
                    addData.endtime = dataInfo.FindAll(c => c.autoaccount == item.userid && c.taskperiodname == item.taskperiodname)[0].uploadtime;
                    addData.endid = dataInfo.FindAll(c => c.autoaccount == item.userid && c.taskperiodname == item.taskperiodname)[0].id;
                    addData.endnum = item.recheckdata;
                    addData.carrywatercount = addData.endnum - addData.startnum;
                    addData.adjustwatercount = 0;
                    addData.createtime = DateTime.Now;
                    addData.createperson = Permissions.UersName;
                    if (addData.carrywatercount >= 0)
                    {
                        addData.carrystatus = 1;
                    }
                    else
                    {
                        addData.carrystatus = 2;
                    }
                    addData.remark = "";
                    Datalist.Add(addData);
                }
            }
            if (Datalist.Count == 0)
            {
                return new TableModel<object>
                {
                    code = 0,
                    msg = "Over",
                    data = "",
                };
            }
            int b = await _B_WatercarryoverServices.Add(Datalist);
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                data = "",
            };
        }

        /// <summary>
        /// 字段转换
        /// </summary>
        /// <param name="JsonData"></param>
        /// <param name="taskperiodname"></param>
        /// <param name="autoaccount"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Change")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> Change(string JsonData, string taskperiodname, string autoaccount)
        {
            string Month = (Convert.ToInt32(taskperiodname) - 2).ToString();
            List<v_recheck_recheckhistory> rt_B_Rechecks = await _Recheck_RecheckhistoryServices.Query();
            List<v_union_datainfoocrlog_datainfohistoryocrloghistory> Data = await _Union_Datainfoocrlog_DatainfohistoryocrloghistoryServices.Query();
            List<object> returnData = new List<object>();
            string[] array = JsonData.Split(',');
            for (int i = 0; i < array.Length; i++)
            {
                decimal recheckdata = 0;
                decimal ocrdata = 0;
                int photoid = 0;
                var recheckData = rt_B_Rechecks.FindAll(c => c.userid == autoaccount && c.taskperiodname == Month);
                var ocrData = Data.FindAll(c => c.autoaccount == autoaccount && c.taskperiodname == Month);
                string[] array2 = array[i].Split('/');
                if (recheckData.Count != 0)
                {
                    recheckdata = (decimal)recheckData[0].recheckdata;

                }
                if (ocrData.Count != 0)
                {
                    ocrdata = (decimal)ocrData[0].ocrdata;
                    photoid = ocrData[0].photoid == null ? 0 : (int)ocrData[0].photoid;
                }
                var data = new
                {
                    updateData = array2[0],
                    ocrData = ocrdata,
                    recheckdata = recheckdata,
                    pircture = photoid,
                    month = Month

                };
                returnData.Add(data);
                Month = (Convert.ToInt32(Month) + 1).ToString();
            }
            return new TableModel<object>
            {
                code = 0,
                data = returnData,
                count = returnData.Count,
                msg = "OK"
            };
        }

        /// <summary>
        /// 确认审核按钮
        /// </summary>
        /// <param name="JsonData"></param>
        /// <param name="RecheckData"></param>
        /// <param name="RecheckStatus"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SubmitChecked")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> SubmitChecked(string JsonData, decimal RecheckData, int RecheckStatus, string result)
        {
            v_mr_datainfo mr_Datainfos = JsonHelper.GetObject<v_mr_datainfo>(JsonData);
            //查询审核表的所有数据
            List<rt_b_recheck> _B_Rechecks = await _B_RecheckServices.Query();
            List<string> AutoAccount = new List<string>();
            List<rt_b_recheck> AddData = new List<rt_b_recheck>();
            //声明一个审核表的一个对象用于插入或者修改
            rt_b_recheck b_Recheck = new rt_b_recheck();

            //存放审核表里有的用户自动编号，便于做判重处理
            string RecheckResult = "原因：" + mr_Datainfos.recheckresult + ",结果:" + result;
            foreach (var item in _B_Rechecks)
            {
                AutoAccount.Add(item.userid);
            }

            b_Recheck.readdataid = mr_Datainfos.ID;
            b_Recheck.meternum = mr_Datainfos.meternum;
            b_Recheck.userid = mr_Datainfos.autoaccount;
            b_Recheck.taskperiodname = mr_Datainfos.taskperiodname;
            b_Recheck.recheckstatus = RecheckStatus;
            b_Recheck.recheckdata = RecheckData;
            b_Recheck.recheckresult = RecheckResult;
            b_Recheck.checksuccesstime = DateTime.Now;
            b_Recheck.checkor = "1";//代表人工审核的
            b_Recheck.createtime = DateTime.Now;
            b_Recheck.createpeople = Permissions.UersName;
            AddData.Add(b_Recheck);
            int b = await _B_RecheckServices.Add(AddData);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                data = "",
            };
        }


        /// <summary>
        /// 显示抄表路径
        /// </summary>
        /// <param name="month">抄表月份</param>
        /// <param name="name">抄表员姓名</param>
        /// <param name="date">抄表日期</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowMRPath")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> ShowMRPath(string month, string date, string name, int page = 1, int limit = 20)
        {
            return await _Mr_DatainfoServices.ShowMRPath(month, date, name, page, limit);
        }

        /// <summary>
        /// 渲染抄表员下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("RenderSelect")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> RenderSelect()
        {
            List<string> readerName = new List<string>();
            var readerData = await _B_ReaderServices.Query();
            foreach (var item in readerData)
            {
                readerName.Add(item.mrreadername);
            }
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                data = readerName
            };
        }

        /// <summary>
        /// 展示审核历史数据
        /// </summary>
        /// <param name="autoaccount"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowHistoryRecheckData")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> ShowHistoryRecheckData(string autoaccount)
        {
            //查出审核历史数据
            List<v_recheck_recheckhistory> recheck_Recheckhistories = await _Recheck_RecheckhistoryServices.Query(c=>c.userid== autoaccount);
            //查询拿到审核历史记录的图片和图片识别的读数
            List<v_union_datainfoocrlog_datainfohistoryocrloghistory> ocrlogData = await _Union_Datainfoocrlog_DatainfohistoryocrloghistoryServices.Query(c=>c.autoaccount== autoaccount);
            List<object> returnData = new List<object>();
            foreach (var item in recheck_Recheckhistories)
            {
                var ocrlogDataAndinputdata = ocrlogData.FindAll(c=>c.taskperiodname==item.taskperiodname);
                var data = new
                {

                    taskperiodname = item.taskperiodname,
                    recheckdata=item.recheckdata,
                    recheckstatus=item.recheckstatus,
                    createtime=item.createtime,
                    remark=item.recheckresult,
                    inputdata= ocrlogDataAndinputdata[0].inputdata,
                    ocrlogdata= ocrlogDataAndinputdata[0].ocrdata,
                    pirctureID= ocrlogDataAndinputdata[0].photoid

                };
                returnData.Add(data);

            }
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                data = returnData,
                count= returnData.Count


            };
        }
    }
}