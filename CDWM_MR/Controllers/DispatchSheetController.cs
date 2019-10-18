using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers
{
    
    public class DispatchSheetController : ControllerBase
    {
        #region 相关参数
        readonly Irb_b_faultprocessServices _B_FaultprocessServices;
        readonly Irt_b_faultinfoServices _B_FaultinfoServices;
        readonly Irt_b_photoattachmentServices _B_PhotoattachmentServices;
        readonly Iv_rt_b_faultinfoServices v_rt_b_faultinfoServices;
        readonly Imr_b_readerServices imr_B_ReaderServices;
        readonly Iv_rb_b_faultprocessServices v_rb_b_faultprocessServices;
        readonly Imr_datainfoServices imr_datainfoservices;
        #endregion

        public DispatchSheetController(Irb_b_faultprocessServices B_FaultprocessServices,Irt_b_faultinfoServices B_FaultinfoServices,Irt_b_photoattachmentServices B_PhotoattachmentServices,Iv_rt_b_faultinfoServices iv_Rt_B_FaultinfoServices, Imr_b_readerServices imr_b_ReaderServices, Iv_rb_b_faultprocessServices iv_Rb_B_FaultprocessServices, Imr_datainfoServices imr_Datainfo)
        {
           
            _B_FaultprocessServices = B_FaultprocessServices;
            _B_FaultinfoServices = B_FaultinfoServices;
            _B_PhotoattachmentServices = B_PhotoattachmentServices;
            v_rt_b_faultinfoServices = iv_Rt_B_FaultinfoServices;
            imr_B_ReaderServices = imr_b_ReaderServices;
            v_rb_b_faultprocessServices = iv_Rb_B_FaultprocessServices;
            imr_datainfoservices = imr_Datainfo;
        }

        #region 故障表展示  
        /// <summary>
        /// 故障表展示  
        /// </summary>
        /// <param name="DSMNumber"></param>
        /// <param name="DSMType"></param>
        /// <param name="DSMStatus"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowFaultTable")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> ShowFaultTable(string DSMNumber,string DSMType,string DSMStatus,string StartTime,string EndTime, int page = 1, int limit = 10)
        {
            DateTime startTime=new DateTime();
            DateTime endTime = new DateTime();
            PageModel<object> datainfor = new PageModel<object>();
            #region Lambda表达式
            Expression<Func<v_rt_b_faultinfo, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(DSMNumber))
            {
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.faultnumber==DSMNumber);
            }
            if (!string.IsNullOrEmpty(DSMType))
            {
                int faulttype = 0;
                if (DSMType== "表埋")
                {
                    faulttype = 6;
                }
                else if(DSMType== "表坏")
                {
                    faulttype = 7;
                }
                else if(DSMType== "漏水")
                {
                    faulttype = 10;
                }
                else
                {
                    faulttype = 11;
                }
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.faulttype == faulttype);
            }
            if (!string.IsNullOrEmpty(DSMStatus))
            {
                int faultstatus = 0;
                if (DSMStatus== "未受理")
                {
                    faultstatus = 0;
                }
                else if (DSMStatus=="已受理")
                {
                    faultstatus = 1;
                }
                else if(DSMStatus== "已处理")
                {
                    faultstatus = 2;
                }
                else
                {
                    faultstatus = 3;
                }
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda,c=>c.faultstatus==faultstatus);
            }
            if (!string.IsNullOrEmpty(StartTime)&&string.IsNullOrEmpty(EndTime))
            {
                startTime = Convert.ToDateTime(StartTime);
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda,c=>c.reporttime>startTime);
            }
            else if (!string.IsNullOrEmpty(EndTime)&&string.IsNullOrEmpty(StartTime))
            {
                endTime = Convert.ToDateTime(EndTime);
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda,c=>c.reporttime<endTime);
            }
            else if (!string.IsNullOrEmpty(StartTime)&&!string.IsNullOrEmpty(EndTime))
            {
                startTime = Convert.ToDateTime(StartTime);
                endTime = Convert.ToDateTime(EndTime);
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.reporttime > startTime && c.reporttime < endTime);
            }
            #endregion
            Expression<Func<v_rt_b_faultinfo, object>> expression =  c => new
            {
                DSMID=c.id,
                DSMNumber=c.faultnumber,
                DSMType=c.faulttype,
                DSMContent=c.faultcontent,
                DSMTime=c.reporttime,
                DSMReportPerson=c.reportpeople,
                DSMEnclosure=c.v_status,
                DSMStatus=c.faultstatus,
                DSMAddress="查看"
            };
            datainfor = await v_rt_b_faultinfoServices.QueryPage(wherelambda, expression, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = datainfor.dataCount,
                data = datainfor.data
            };

        }
        #endregion

        #region 展示附件图片
        /// <summary>
        /// 展示附件图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowPhoto")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> ShowPhoto(int id)
        {
            List<object> Alllist = new List<object>();
            List<rt_b_photoattachment> list = await _B_PhotoattachmentServices.Query(c=>c.billid==id);
            foreach (var item in list)
            {
                Alllist.Add(item.photourl);
            }
            return new MessageModel<object>()
            {
                code = 0,
                data=Alllist,
                msg=""
            };
        }
        #endregion

        #region 显示地理位置
        /// <summary>
        /// 显示地理位置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowAddress")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> ShowAddress(int id)
        {
           List<rt_b_faultinfo>list= await _B_FaultinfoServices.Query(c => c.id == id);
            List<object> redlist = new List<object>();
            redlist.Add(list[0].gisinfo);
            return new MessageModel<object>()
            {
                code = 0,
                data = redlist,
                msg = ""
            };
        }
        #endregion

        #region 受理操作
        /// <summary>
        /// 受理操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AcceptanceOperation")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> AcceptanceOperation(int id,int Dispatchedworker,DateTime Latesttime)
        {
            try
            {
                string worker;
                int workerid;
                List<mr_b_reader> reader;
                List<rt_b_faultinfo> faultinfo = await _B_FaultinfoServices.Query(c=>c.id==id);
                var readerlist = await imr_B_ReaderServices.Query();
                var faultprocess = await _B_FaultprocessServices.Query();
                workerid = faultinfo[0].id;
                if (Dispatchedworker == 0)
                {
                    reader = readerlist.FindAll(c => c.id == workerid);
                    worker = reader[0].mrreadername;
                }
                else
                {
                    reader = readerlist.FindAll(c => c.id == Dispatchedworker);
                    worker = reader[0].mrreadername;
                }
                rb_b_faultprocess data = new rb_b_faultprocess();
                data.faulttype = 0;
                data.processdatetime = Latesttime;
                data.processpreson = worker;
                data.createperson = "1";
                data.taskperiodname = "201909";
                data.processresult = 1;
                data.processsource = "后台管理系统";
                data.createtime = DateTime.Now;
                data.faultid = id;
                var list = faultprocess.FindAll(c=>c.faultid==id);//判重
                if (list.Count!=0)
                {
                    return new MessageModel<object>()
                    {
                        code = 1,
                        msg = "故障处理记录存在",
                        data = null
                    };
                }
                string message = await _B_FaultprocessServices.Add(data) > 0 ? "ok" : "error";
                string mes = await _B_FaultinfoServices.Update(c => new rt_b_faultinfo
                {
                    faultstatus=1
                },c=>c.id==id) == true ? "ok" : "error";//将状态改为已受理
                return new MessageModel<object>()
                {
                    code = 0,
                    msg = message,
                    data = null
                };
            }
            catch (Exception)
            {

                return new MessageModel<object>()
                {
                    code = 1,
                    msg = "error",
                    data = null
                };
            }
        }
        #endregion

        #region 派工下拉框
        /// <summary>
        /// 派工下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowDispatchedWorker")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> ShowDispatchedWorker()
        {
            var alllist = await imr_B_ReaderServices.Query();
            List<object> list = new List<object>();
            foreach (var item in alllist)
            {
                var data = new { ID = item.ID, Name = item.mrreadername };
                list.Add(data);
            }
            return new MessageModel<object>()
            {
                code = 0,
                msg = "ok",
                data = list
            };
        }
        #endregion

        #region 给处理界面传值
        [HttpPost]
        [Route("ShowProcessingoperationdateinfo")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> ShowProcessingoperationdateinfo(int id)
        {
            List<v_rb_b_faultprocess> alllist = await v_rb_b_faultprocessServices.Query(c=>c.id==id);
            return new MessageModel<object>() {
                code=0,
                msg="ok",
                data=alllist
            };
        }
        #endregion

        #region 接受上传的图片
        [HttpPost]
        [Route("GetPhoto")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> GetPhoto(string[] str)
         {
            string[] strArray = str[0].Split(",");
            string subPath = @"E:\常德水表\现场处理图片";
            if (false == System.IO.Directory.Exists(subPath))
            {
                System.IO.Directory.CreateDirectory(subPath);//如果没有该文件夹则创建
            }
            for (int i = 0; i < strArray.Length; i++)
            {
            }
            return new MessageModel<object>()
            {
                code = 0,
                data = null,
                msg = ""
            };
        }
        #endregion

        #region 处理操作
        [HttpPost]
        [Route("Processingoperations")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> Processingoperations(string s,int id,string worker,int result,string mark)
        {
            string[] str = s.Split(",");
            List<v_rt_b_faultinfo> vlist = await v_rt_b_faultinfoServices.Query(c => c.id == id);
            List<rt_b_photoattachment> alllist = await _B_PhotoattachmentServices.Query();
            List<rt_b_photoattachment> list = new List<rt_b_photoattachment>();
            int readerid = vlist[0].readerid;
            List<mr_b_reader> readerlist = await imr_B_ReaderServices.Query(c => c.id == readerid);
            string subPath = @"E:\常德水表\现场处理图片";
            for (int i = 0; i < str.Length; i++)
            {
                rt_b_photoattachment photo = new rt_b_photoattachment();
                photo.createpeople = worker;
                photo.createtime = DateTime.Now;
                photo.metercode = vlist[0].meternum;
                photo.photocode = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + alllist[alllist.Count-1].id + 1;
                photo.photoext = Path.GetExtension(str[i]);
                photo.photonname = Path.GetFileNameWithoutExtension(str[i]);
                photo.phototime = DateTime.Now;
                photo.phototype = 3;
                photo.photourl = subPath;
                photo.readercode = readerlist[0].mrreadernumber;
                photo.billid = id;
                photo.taskperiodname = "201909";//需修改
                photo.updatepeople = worker;
                photo.updatetime = DateTime.Now;
                photo.usercode = "1";
                list.Add(photo);
            }
            await _B_PhotoattachmentServices.Add(list);

            int type =1;
            List<rb_b_faultprocess> faultprocesslist = await _B_FaultprocessServices.Query(c=>c.faultid==id&&c.faulttype==0);
            if (result==1)//已处理
            {
                type = 2;
            }
            List<rb_b_faultprocess> lastlist = await _B_FaultprocessServices.Query(c => c.faultid == id && c.faulttype == 0);
            rb_b_faultprocess data = new rb_b_faultprocess();
            data.faulttype = type;
            data.faultid = lastlist[0].faultid;
            data.processdatetime = lastlist[0].processdatetime;
            data.processmark = mark;
            data.processpreson = worker;
            data.processsource = "后台管理系统";
            data.taskperiodname = lastlist[0].taskperiodname;
            data.createperson = "1";
            data.createtime = DateTime.Now;
            string String = await _B_FaultprocessServices.Add(data)>0 ? "ok" : "error";
            string mes = await _B_FaultinfoServices.Update(c => new rt_b_faultinfo
            {
                faultstatus = 2
            }, c => c.id == id) == true ? "ok" : "error";//将状态改为已处理
            return new MessageModel<object>() {
                code=0,
                msg=mes,
                data=null
            };
        }
        #endregion

        #region 故障信息展示
        [HttpPost]
        [Route("FaultInformationDisplay")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> FaultInformationDisplay(int id)
        {
            List<object> list = new List<object>();
            List<rt_b_faultinfo> faultinfolist = await _B_FaultinfoServices.Query(c=>c.id==id);
            List<rb_b_faultprocess> faultprocesslist = await _B_FaultprocessServices.Query(c=>c.faultid==id&&c.faulttype==0);
            List<rb_b_faultprocess> faultprocessessecondlist = await _B_FaultprocessServices.Query(c => c.faultid == id && c.faulttype == 1);
            List<rt_b_photoattachment> photolist = await _B_PhotoattachmentServices.Query(c=>c.billid==id);
            list.Add(faultinfolist);
            list.Add(faultprocesslist);
            list.Add(faultprocessessecondlist);
            list.Add(photolist);
            return new MessageModel<object>()
            {
                code = 0,
                msg="",
                data=list
            };
        }
        #endregion




    }
}