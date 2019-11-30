using CDWM_MR.Common.Helper;
using CDWM_MR.Common.HttpContextUser;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 抄表计划
    /// </summary>
    [Route("api/MeterReadingPlan")]
    [AllowAnonymous]
    [EnableCors("LimitRequests")]
    public class MeterReadingPlanController : ControllerBase
    {

        #region 相关变量
        readonly Imr_planinfoServices mr_planinfoServices;
        readonly Imr_taskinfoServices mr_taskinfoServices;
        readonly Imr_b_readerServices mr_b_readerServices;
        readonly Imr_b_bookinfoServices mr_b_bookinfoServices;
        readonly Iv_taskinfoServices v_taskinfoServices;
        readonly IUser _user;
        readonly Imr_book_meterServices mr_book_meterServices;
        readonly Imr_datainfoServices mr_datainfoServices;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="imr_PlaninfoServices"></param>
        /// <param name="imr_Taskinfoservices"></param>
        /// <param name="imr_B_ReaderServices"></param>
        /// <param name="imr_B_BookinfoServices"></param>
        /// <param name="iv_TaskinfoServices"></param>
        public MeterReadingPlanController(Imr_planinfoServices imr_PlaninfoServices, Imr_taskinfoServices imr_Taskinfoservices, Imr_b_readerServices imr_B_ReaderServices, Imr_b_bookinfoServices imr_B_BookinfoServices, Iv_taskinfoServices iv_TaskinfoServices, IUser user,Imr_book_meterServices imr_Book_MeterServices,Imr_datainfoServices imr_DatainfoServices)
        {
            mr_planinfoServices = imr_PlaninfoServices;
            mr_taskinfoServices = imr_Taskinfoservices;
            mr_b_readerServices = imr_B_ReaderServices;
            mr_b_bookinfoServices = imr_B_BookinfoServices;
            v_taskinfoServices = iv_TaskinfoServices;
            _user = user;
            mr_book_meterServices = imr_Book_MeterServices;
            mr_datainfoServices = imr_DatainfoServices;
        }
        #endregion

        #region 添加计划单 已完成
        /// <summary>
        /// 添加计划单 已完成
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPlan")]
        public async Task<MessageModel<object>> AddPlan(string data)
        {
            try
            {
                var alllist = await mr_planinfoServices.Query();
                int Number = 1;
                Number = alllist.Count == 0 ? 1 : alllist[alllist.Count - 1].id + 1;
                mr_planinfo Data = Common.Helper.JsonHelper.GetObject<mr_planinfo>(data);
                Data.createtime = DateTime.Now;
                Data.createpeople = "1";
                Data.mplannumber = Number.ToString();
                Data.finishstatus = 0;
                //判重
                var querylist = alllist.FindAll(c => c.mplannumber == Number.ToString());
                if (querylist.Count != 0)
                {
                    return new MessageModel<object>()
                    {
                        code = 1,
                        msg = "计划单存在"
                    };
                }
                int Message = await mr_planinfoServices.Add(Data);
                await v_taskinfoServices.AutoCreat(Message);//添加任务单
                string message = Message == 0 ? "error" : "ok";
                return new MessageModel<object>()
                {
                    code = 0,
                    msg = message
                };
            }
            catch (Exception ex)
            {

                return new MessageModel<object>()
                {
                    code = 2,
                    msg = "数据格式不对",
                    data = ex.ObjToString()
                };
            }

        }
        #endregion

        #region 显示计划单 已完成
        /// <summary>
        /// 显示计划单 已完成
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowPlan")]
        public async Task<TableModel<object>> ShowPlan(int page = 1, int limit = 10)
        {
            PageModel<object> datainfor = new PageModel<object>();
            Expression<Func<mr_planinfo, bool>> wherelambda = c => true;
            Expression<Func<mr_planinfo, object>> expression = c => new
            {
                ID = c.id,
                Company = c.mplanname,
                Year = c.mplanyear,
                Month = c.mplanmonth,
                StartTime = c.planstarttime,
                EndTime = c.planendtime
            };
            datainfor = await mr_planinfoServices.QueryPage(wherelambda, expression, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = datainfor.dataCount,
                data = datainfor.data
            };
        }
        #endregion

        #region 显示计划单中抄表册 完成
        /// <summary>
        /// 显示计划单中抄表册 未完成
        /// </summary>
        /// <param name="bookno"></param>
        /// <param name="bookname"></param>
        /// <param name="reader"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowMeterReadingBooks")]
        public async Task<TableModel<object>> ShowMeterReadingBooks(string bookno, string bookname, string reader, int page = 1, int limit = 10)
        {
            PageModel<object> datainfor = new PageModel<object>();
            #region Lambda拼接式
            Expression<Func<v_taskinfo, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(bookno))
            {
                wherelambda = PredicateExtensions.And<v_taskinfo>(wherelambda, c => c.bookno.Contains(bookno));
            }
            if (!string.IsNullOrEmpty(bookname))
            {
                wherelambda = PredicateExtensions.And<v_taskinfo>(wherelambda, c => c.bookname.Contains(bookname));
            }
            if (!string.IsNullOrEmpty(reader))
            {
                wherelambda = PredicateExtensions.And<v_taskinfo>(wherelambda, c => c.mrreadername.Contains(reader));
            }
            #endregion        
            Expression<Func<v_taskinfo, object>> expression = c => new
            {
                MRID = c.taskid,
                MRNumber = c.bookno,
                MRName = c.bookname,
                MRPeople = c.mrreadername,
                MRStartTime = c.taskstarttime,
                MREndTime = c.taskendtime,
                MRMonth = c.mplanmonth,
                MRTaskStatus = c.taskstatus == 0 ? "未下载" : "已下载"
            };

            datainfor = await v_taskinfoServices.QueryPage(wherelambda, expression, page, limit, "");

            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = datainfor.dataCount,
                data = datainfor.data
            };
        }
        #endregion

        #region 显示抄表员下拉框 已完成
        /// <summary>
        /// 显示抄表员下拉框 已完成
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowSelect")]
        public async Task<MessageModel<object>> ShowSelect()
        {
            var alllist = await mr_b_readerServices.Query();
            List<object> list = new List<object>();
            foreach (var item in alllist)
            {
                var datalist = new { ID = item.id, name = item.mrreadername };
                list.Add(datalist);
            }
            return new MessageModel<object>()
            {
                code = 0,
                msg = "ok",
                data = list
            };
        }
        #endregion

        #region 显示分配抄表册 已完成
        /// <summary>
        /// 显示分配抄表册 已完成
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DistributionOfMeterReadingBooks")]
        public async Task<TableModel<object>> DistributionOfMeterReadingBooks(int status, int page = 1, int limit = 10)
        {
            PageModel<object> datainfor = new PageModel<object>();
            #region Lambda拼接式
            Expression<Func<mr_b_bookinfo, bool>> wherelambda = c => true;
            wherelambda = PredicateExtensions.And<mr_b_bookinfo>(wherelambda, c => c.allotstatus == status);
            #endregion
            Expression<Func<mr_b_bookinfo, object>> expression = c => new
            {
                Show_ID = c.id,
                Show_MeterReading = c.bookname,
                Show_Number = c.bookno,
                Show_Status = c.allotstatus == 0 ? "已分配" : "未分配"
            };
            datainfor = await mr_b_bookinfoServices.QueryPage(wherelambda, expression, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = datainfor.dataCount,
                data = datainfor.data
            };
        }
        #endregion

        #region 点击分配确认时 已完成 
        /// <summary>
        /// 点击分配确认时 已完成
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <param name="planid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AllocationOfData")]
        public async Task<MessageModel<object>> AllocationOfData(int planid, string[] data, int[] idlist, int status = 0)
        {
            if (idlist.Length == 0)
            {
                return new MessageModel<object>()
                {
                    code = 0,
                    data = null,
                    msg = "抄表册已分配"
                };
            }
            List<mr_book_meter> mr_bookmeterlist = await mr_book_meterServices.Query();
            List<mr_datainfo> mr_datainfolist = new List<mr_datainfo>();
            var msg = await mr_b_bookinfoServices.Update(c => new mr_b_bookinfo
            {
                allotstatus = status
            }, c => data.Contains(c.bookno)) == true ? "ok" : "error";
            //获取所有分配的抄表册
            List<mr_b_bookinfo> booklist = await mr_b_bookinfoServices.Query(c => data.Contains(c.bookno));
            var alllist = await mr_taskinfoServices.Query();
            List<mr_taskinfo> tasklist = new List<mr_taskinfo>();
            foreach (var item in booklist)
            {
                mr_taskinfo taskinfo = new mr_taskinfo();
                taskinfo.bookid = item.id;
                taskinfo.planid = planid;
                taskinfo.readerid = 1;
                taskinfo.taskname = "任务单" + alllist.Count;
                taskinfo.tasknumber = (DateTime.Now.Year + DateTime.Now.Month + alllist[alllist.Count - 1].ID).ToString();
                taskinfo.createpeople = _user.Name;
                taskinfo.createtime = DateTime.Now;
                taskinfo.taskperiodname = DateTime.Now.Year.ToString() + DateTime.Now.Month;
                tasklist.Add(taskinfo);
            }
            //string n= await mr_taskinfoServices.Add(tasklist) > 0 ? "ok" : "error";
            //把分配抄表册中的用户信息添加到mr_datainfo
            for (int i = 0; i < idlist.Length; i++)
            {
                List<mr_book_meter> book_meter = new List<mr_book_meter>();
                book_meter = mr_bookmeterlist.FindAll(c => c.bookid == idlist[i]); //查找抄表册下的用户
                //将抄表册下用户信息添加到mr_datainfo
                for (int j = 0; j < book_meter.Count; j++)
                {
                    mr_datainfo mr_data = new mr_datainfo();
                    mr_data.autoaccount = book_meter[j].useraccount;
                    mr_data.meternum = book_meter[j].watermeternumber;
                    mr_data.readstatus = 0;
                    mr_data.readtype = 0;
                    mr_data.recheckstatus = 0;
                    mr_data.taskid = planid;
                    mr_data.taskperiodname = DateTime.Now.Year.ToString() + DateTime.Now.Month;
                    mr_datainfolist.Add(mr_data);
                }
            }
            await mr_taskinfoServices.Add(tasklist); //把分配的抄表册数据添加到mr_taskinfo表中
            await mr_datainfoServices.Add(mr_datainfolist);
            return new MessageModel<object>
            {
                code = 0,
                data = null,
                msg = "成功"
            };
        }
        #endregion

        #region 抄表计划编辑 已完成
        /// <summary>
        /// 抄表计划编辑 已完成
        /// </summary>
        /// <param name="senddata"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowTaskEdit")]
        public async Task<MessageModel<object>> ShowTaskEdit(string senddata, int ID)
        {
            string message = "";
            v_taskinfo Data = Common.Helper.JsonHelper.GetObject<v_taskinfo>(senddata);
            #region 判断抄表册是否已分配
            Expression<Func<v_taskinfo, bool>> wherelambda = c => c.bookid != ID && (c.bookname == Data.bookname);
            var list = await v_taskinfoServices.Query(wherelambda);
            if (list.Count != 0)
            {
                return new MessageModel<object>()
                {
                    code = 1,
                    data = null,
                    msg = "抄表册已分配"
                };
            }
            #endregion
            message = await v_taskinfoServices.Update(c => new v_taskinfo
            {
                bookname = Data.bookname,
                mrreadername = Data.mrreadername,
                mplanmonth = Data.mplanmonth,
                taskstarttime = Data.taskstarttime,
                taskendtime = Data.taskendtime,
                downloadstarttime = Data.downloadstarttime,
                downloadendtime = Data.downloadendtime
            }, c => c.bookname == Data.bookname) == true ? "ok" : "error";
            return new MessageModel<object>()
            {
                code = 0,
                data = null,
                msg = message
            };
        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 到处EXCEL
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("OutExcel")]
        public async Task<FileResult> OutExcel()
        {
            List<v_taskinfo> ExcelList = await v_taskinfoServices.Query(c => true);
            var lalala = ExcelList.Select(c => new v_taskinfo
            {
                taskid = c.taskid,
                bookid = c.bookid,
                dowloadstatus = c.dowloadstatus,
                downloadendtime = c.downloadendtime,
                downloadstarttime = c.downloadstarttime,
                planid = c.planid,
                taskstarttime = c.taskstarttime,
                taskendtime = c.taskendtime,
                readerid = c.readerid,
                tasknumber = c.tasknumber,
                taskname = c.taskname,
                taskstatus = c.taskstatus,
                CreateTime = c.CreateTime,
                CreatePeople = c.CreatePeople,
                Remark = c.Remark,
                bookno = c.bookno,
                bookname = c.bookname,
                contectusernum = c.contectusernum,
                regionname = c.regionname,
                mrreadernumber = c.mrreadernumber,
                mrreadername = c.mrreadername,
                mplanmonth = c.mplanmonth,
                mplanyear = c.mplanyear
            }).ToList();
            Hashtable tb = new Hashtable();
            tb.Add("taskid", "任务单ID");
            tb.Add("bookid", "抄表册ID");
            tb.Add("dowloadstatus", "下载状态");
            tb.Add("downloadendtime", "下载结束时间");
            tb.Add("downloadstarttime", "下载开始时间");
            tb.Add("planid", "计划单ID");
            tb.Add("taskstarttime", "任务开始时间");
            tb.Add("taskendtime", "任务结束时间");
            tb.Add("readerid", "抄表员ID");
            tb.Add("tasknumber", "任务单编号");
            tb.Add("taskname", "任务单名称");
            tb.Add("taskstatus", "任务状态");
            tb.Add("CreateTime", "创建时间");
            tb.Add("CreatePeople", "创建人");
            tb.Add("Remark", "备注");
            tb.Add("bookno", "抄表册编号");
            tb.Add("bookname", "抄表册名称");
            tb.Add("contectusernum", "内容");
            tb.Add("regionname", "登录人");
            tb.Add("mrreadernumber", "抄表员编号");
            tb.Add("mrreadername", "抄表员");
            tb.Add("mplanmonth", "所属月");
            tb.Add("mplanyear", "所属年");
            MemoryStream data = OfficeHelper.getExcel<v_taskinfo>(lalala, tb);
            string fileExt = ".xls";
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            return File(data, memi, "任务单信息.xlsx");
        }
        #endregion
    }
}
