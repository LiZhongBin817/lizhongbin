using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CDWM_MR.Common;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 
    /// </summary>
   
    [Route("api/DispatchSheet")]
    [AllowAnonymous]
    [EnableCors("LimitRequests")]
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
        readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="B_FaultprocessServices"></param>
        /// <param name="B_FaultinfoServices"></param>
        /// <param name="B_PhotoattachmentServices"></param>
        /// <param name="iv_Rt_B_FaultinfoServices"></param>
        /// <param name="imr_b_ReaderServices"></param>
        /// <param name="iv_Rb_B_FaultprocessServices"></param>
        /// <param name="imr_Datainfo"></param>
        /// <param name="mapper"></param>
        public DispatchSheetController(Irb_b_faultprocessServices B_FaultprocessServices, Irt_b_faultinfoServices B_FaultinfoServices, Irt_b_photoattachmentServices B_PhotoattachmentServices, Iv_rt_b_faultinfoServices iv_Rt_B_FaultinfoServices, Imr_b_readerServices imr_b_ReaderServices, Iv_rb_b_faultprocessServices iv_Rb_B_FaultprocessServices, Imr_datainfoServices imr_Datainfo, IMapper mapper)
        {

            _B_FaultprocessServices = B_FaultprocessServices;
            _B_FaultinfoServices = B_FaultinfoServices;
            _B_PhotoattachmentServices = B_PhotoattachmentServices;
            v_rt_b_faultinfoServices = iv_Rt_B_FaultinfoServices;
            imr_B_ReaderServices = imr_b_ReaderServices;
            v_rb_b_faultprocessServices = iv_Rb_B_FaultprocessServices;
            imr_datainfoservices = imr_Datainfo;
            _mapper = mapper;
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
        public async Task<TableModel<object>> ShowFaultTable(string DSMNumber, string DSMType, string DSMStatus, string StartTime, string EndTime, int page = 1, int limit = 10)
        {
            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();
            PageModel<object> datainfor = new PageModel<object>();
            #region Lambda表达式
            Expression<Func<v_rt_b_faultinfo, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(DSMNumber))
            {
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.faultnumber == DSMNumber);
            }
            if (!string.IsNullOrEmpty(DSMType))
            {
                int faulttype = Convert.ToInt32(DSMType);
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.faulttype == faulttype);
            }
            if (!string.IsNullOrEmpty(DSMStatus))
            {
                int faultstatus = Convert.ToInt32(DSMStatus);
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.faultstatus == faultstatus);
            }
            if (!string.IsNullOrEmpty(StartTime) && string.IsNullOrEmpty(EndTime))
            {
                startTime = Convert.ToDateTime(StartTime);
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.reporttime > startTime);
            }
            else if (!string.IsNullOrEmpty(EndTime) && string.IsNullOrEmpty(StartTime))
            {
                endTime = Convert.ToDateTime(EndTime);
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.reporttime < endTime);
            }
            else if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                startTime = Convert.ToDateTime(StartTime);
                endTime = Convert.ToDateTime(EndTime);
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.reporttime > startTime && c.reporttime < endTime);
            }
            #endregion
            Expression<Func<v_rt_b_faultinfo, object>> expression = c => new
            {
                DSMID = c.id,
                DSMNumber = c.faultnumber,
                DSMType = c.faulttype,
                DSMContent = c.faultcontent,
                DSMTime = c.reporttime,
                DSMReportPerson = c.reportpeople,
                DSMEnclosure = c.v_status,
                DSMStatus = c.faultstatus,
                DSMAddress = "查看"
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
        public async Task<MessageModel<object>> ShowPhoto(int id)
        {
            List<object> alllist = new List<object>();
            List<rt_b_photoattachment> list = await _B_PhotoattachmentServices.Query(c => c.billid == id);
            string ipadress = Appsettings.app(new string[] { "AppSettings", "StaticFileUrl", "Connectionip" });
            for (int i = 0; i < list.Count; i++)
            {
                alllist.Add($"{ipadress}{list[i].photourl.Split("wwwroot")[1]}");
            }
            return new MessageModel<object>()
            {
                code = 0,
                data = alllist,
                msg = ""
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
        public async Task<MessageModel<object>> ShowAddress(int id)
        {
            List<rt_b_faultinfo> list = await _B_FaultinfoServices.Query(c => c.id == id);
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
        public async Task<MessageModel<object>> AcceptanceOperation(int id, int Dispatchedworker, DateTime Latesttime)
        {
            try
            {
                string worker;
                int workerid;
                List<mr_b_reader> reader;
                List<rt_b_faultinfo> faultinfo = await _B_FaultinfoServices.Query(c => c.id == id);
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
                data.createperson = Permissions.UersName;
                data.taskperiodname = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                data.processresult = 0;
                data.processsource = "后台管理系统";
                data.createtime = DateTime.Now;
                data.faultid = id;
                data.meternum = faultinfo[0].meternum;
                var list = faultprocess.FindAll(c => c.faultid == id);//判重
                if (list.Count != 0)
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
                    faultstatus = 1
                }, c => c.id == id) == true ? "ok" : "error";//将状态改为已受理
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
        /// <summary>
        /// 给处理界面传值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowProcessingoperationdateinfo")]
        public async Task<MessageModel<object>> ShowProcessingoperationdateinfo(int id)
        {
            List<v_rb_b_faultprocess> alllist = await v_rb_b_faultprocessServices.Query(c => c.faultid == id);
            return new MessageModel<object>()
            {
                code = 0,
                msg = "ok",
                data = alllist
            };
        }
        #endregion

        #region 接受上传的图片
        /// <summary>
        /// 接受上传的图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPhoto")]
        public async Task<MessageModel<object>> GetPhoto([FromServices]IHostingEnvironment environment)
        {
            var data = new MessageModel<string>();
            try
            {
                List<object> alllist = new List<object>();
                IFormFileCollection files = Request.Form.Files;
                alllist.Add(files[0].FileName);
                List<UploadPhotoModel> uploadmodel = JsonHelper.GetObject<List<UploadPhotoModel>>(Request.Form["UploadPhotoModel"].ObjToString());
                if (files.Count <= 0)
                {
                    return new MessageModel<object>() {
                        code=1001,
                        msg= "图片上传个数为0",
                        data=0
                    };
                }
                 List<rt_b_photoattachment> addlist = new List<rt_b_photoattachment>();
                for (int i = 0; i < files.Count; i++)
                {
                    uploadmodel[i].photonname = files[i].FileName;
                    uploadmodel[i].photoext = Path.GetExtension(files[i].FileName);
                    uploadmodel[i].photourl = $@"{Path.Combine(environment.WebRootPath, "images")}\Type_{uploadmodel[i].phototype}\{uploadmodel[i].taskperiodname}\Reader_{uploadmodel[i].readercode}\Taskid_{uploadmodel[i].taskid}";
                    uploadmodel[i].createpeople = "抄表员";//暂时写
                    uploadmodel[i].createtime = DateTime.Now;
                    string tasknumber = uploadmodel[i].taskperiodname, meternum = uploadmodel[i].metercode;
                    string file = Path.Combine(uploadmodel[i].photourl, files[i].FileName);
                    if (!System.IO.Directory.Exists(uploadmodel[i].photourl))
                    {
                        System.IO.Directory.CreateDirectory(uploadmodel[i].photourl);
                    }
                    using (var stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        await files[i].CopyToAsync(stream);
                    }
                    rt_b_photoattachment photomodel = _mapper.Map<rt_b_photoattachment>(uploadmodel[i]);
                    addlist.Add(photomodel);
                }

                int resid=await _B_PhotoattachmentServices.Add(addlist);//添加到图片表
                return new MessageModel<object>() {
                    code=0,
                    msg="成功",
                    data=null
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<object>() {
                    code=1001,
                    msg = ex.ToString(),
                    data=null
                };
            }

        }
        #endregion

        #region 处理操作 
        /// <summary>
        /// 处理操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Processingoperations")]
        public async Task<MessageModel<object>> Processingoperations()
        {
            var data = new MessageModel<int>();
            List<rb_b_faultprocess> FaultHandlinglist = JsonHelper.GetObject<List<rb_b_faultprocess>>(Request.Form["FaultHandlinglist"].ObjToString());
            if (FaultHandlinglist == null || FaultHandlinglist?.Count == 0)
            {
                return new MessageModel<object>()
                {
                    code = 1001,
                    msg = "无上传数据！",
                    data = 0
                };
            }
            FaultHandlinglist[0].createperson = Permissions.UersName;
            FaultHandlinglist[0].createtime = DateTime.Now;
            int a = await _B_FaultprocessServices.Add(FaultHandlinglist[0]);
            string meternum = FaultHandlinglist[0].meternum, tasknumber = FaultHandlinglist[0].taskperiodname;
            await _B_PhotoattachmentServices.Update(c => new rt_b_photoattachment() { billid = a }, c => c.metercode == meternum && c.taskperiodname == tasknumber);//修改图片表billid
            int faultid = FaultHandlinglist[0].faultid;
            string str = await _B_FaultinfoServices.Update(c => new rt_b_faultinfo
            {
                faultstatus = 2
            }, c => c.id == faultid) == true ? "ok" : "error";//将状态改为已处理
            return new MessageModel<object>()
            {
                code = 0,
                msg = str,
                data = null
            };
        }
        #endregion

        #region 故障信息展示
        /// <summary>
        /// 故障信息展示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FaultInformationDisplay")]
        public async Task<MessageModel<object>> FaultInformationDisplay(int id)
        {
            List<object> list = new List<object>();
            List<object> firstphotolist = new List<object>();
            List<object> secondphotolist = new List<object>();
            List<rt_b_faultinfo> faultinfolist = await _B_FaultinfoServices.Query(c => c.id == id);
            List<rb_b_faultprocess> faultprocesslist = await _B_FaultprocessServices.Query(c => c.faultid == id && c.faulttype == 0);
            List<rb_b_faultprocess> faultprocessessecondlist = await _B_FaultprocessServices.Query(c => c.faultid == id && c.faulttype == 1);
            //faultprocessessecondlist[0].createtime = DateTime.Now;
            //faultprocessessecondlist[0].id = 0;
            //faultprocessessecondlist[0].processdatetime = DateTime.Now;
            //faultprocessessecondlist[0].processpreson = Permissions.UersName;
            //faultprocessessecondlist[0].faulttype = 2;
            //faultprocessessecondlist[0].processresult = 0;
            //int a = await _B_FaultprocessServices.Add(faultprocessessecondlist[0]);
            List<rt_b_photoattachment> photolist = await _B_PhotoattachmentServices.Query(c => c.billid == id&&c.phototype==2);//现场照片
            string ipadress = Appsettings.app(new string[] { "AppSettings", "StaticFileUrl", "Connectionip" });
            if (photolist.Count!=0)
            {
                
                for (int i = 0; i < photolist.Count; i++)
                {
                    firstphotolist.Add($"{ipadress}{photolist[i].photourl.Split("wwwroot")[1]}");
                }
            }
            List<rt_b_photoattachment> dealphotolist = await _B_PhotoattachmentServices.Query(c=>c.billid==id&&c.phototype==3);//处理后照片
            if (dealphotolist.Count!=0)
            {
                for (int j = 0; j < dealphotolist.Count; j++)
                {
                    secondphotolist.Add($"{ipadress}{dealphotolist[j].photourl.Split("wwwroot")[1]}");
                }
            }
            list.Add(faultinfolist);
            list.Add(faultprocesslist);
            list.Add(faultprocessessecondlist);
            list.Add(firstphotolist);
            list.Add(secondphotolist);
            return new MessageModel<object>()
            {
                code = 0,
                msg = "",
                data = list
            };
        }
        #endregion

        # region 编辑操作
        /// <summary>
        /// 编辑操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DSEdits")]
        public async Task<MessageModel<object>> DSEdit(int id, string data)
        {
            rt_b_faultinfo faultinfo = Common.Helper.JsonHelper.GetObject<rt_b_faultinfo>(data);
            string str = await _B_FaultinfoServices.Update(c => new rt_b_faultinfo
            {
                readdataid = faultinfo.readdataid,
                faulttype = faultinfo.faulttype,
                faultcontent = faultinfo.faultcontent,
                reporttime = faultinfo.reporttime,
                reportpeople = faultinfo.reportpeople,
                faultstatus = faultinfo.faultstatus
            }, c => c.id == id) == true ? "ok" : "error";
            return new MessageModel<object>()
            {
                code = 0,
                data = null,
                msg = str
            };
        }
        #endregion

        #region  删除操作
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DSDelete")]
        public async Task<MessageModel<object>> DSDelete(int id)
        {
            string str = await _B_FaultinfoServices.DeleteById(id) == true ? "ok" : "error";
            return new MessageModel<object>()
            {
                code = 0,
                msg = str,
                data = null
            };
        }
        #endregion

        #region 给处理界面上传图片传输数据
        /// <summary>
        /// 给处理界面上传图片传输数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TransferData")]
        public async Task<MessageModel<object>> TransferData(int id)
        {
            List<object> list = new List<object>();
            List<rt_b_faultinfo> faultinfolist = await _B_FaultinfoServices.Query(c => c.id == id);
            int readerid = faultinfolist[0].readerid;
            string meternum = faultinfolist[0].meternum;
            string autoaccount = faultinfolist[0].autoaccount;
            List<mr_b_reader> readerlist = await imr_B_ReaderServices.Query(c => c.id == readerid);
            string mrreadernumber = readerlist[0].mrreadernumber;//抄表员编号
            list.Add(meternum);
            list.Add(autoaccount);
            list.Add(mrreadernumber);

            return new MessageModel<object>()
            {
                code = 0,
                data = list,
                msg = "成功"
            };
        }
        #endregion
    }
}