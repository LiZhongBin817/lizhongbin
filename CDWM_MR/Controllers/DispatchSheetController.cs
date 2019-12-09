﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CDWM_MR.Common;
using CDWM_MR.Common.Helper;
using CDWM_MR.Common.HttpContextUser;
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
        readonly IUser _user;

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
        public DispatchSheetController(Irb_b_faultprocessServices B_FaultprocessServices, Irt_b_faultinfoServices B_FaultinfoServices, Irt_b_photoattachmentServices B_PhotoattachmentServices, Iv_rt_b_faultinfoServices iv_Rt_B_FaultinfoServices, Imr_b_readerServices imr_b_ReaderServices, Iv_rb_b_faultprocessServices iv_Rb_B_FaultprocessServices, Imr_datainfoServices imr_Datainfo, IMapper mapper, IUser user)
        {

            _B_FaultprocessServices = B_FaultprocessServices;
            _B_FaultinfoServices = B_FaultinfoServices;
            _B_PhotoattachmentServices = B_PhotoattachmentServices;
            v_rt_b_faultinfoServices = iv_Rt_B_FaultinfoServices;
            imr_B_ReaderServices = imr_b_ReaderServices;
            v_rb_b_faultprocessServices = iv_Rb_B_FaultprocessServices;
            imr_datainfoservices = imr_Datainfo;
            _mapper = mapper;
            _user = user;
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
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.faultnumber.Contains(DSMNumber));
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
                readerid = c.readerid,
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
                string theurl = $"{ipadress}{list[i].photourl.Split("wwwroot")[1]}";

                var data = new { url = theurl.Replace(@"\", @"/"), phototype = list[i].phototype };
                alllist.Add(data);
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
                data.createperson = _user.Name;
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
                var data = new { ID = item.id, Name = item.mrreadername };
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
                    return new MessageModel<object>()
                    {
                        code = 1001,
                        msg = "图片上传个数为0",
                        data = 0
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

                int resid = await _B_PhotoattachmentServices.Add(addlist);//添加到图片表
                return new MessageModel<object>()
                {
                    code = 0,
                    msg = "成功",
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<object>()
                {
                    code = 1001,
                    msg = ex.ToString(),
                    data = null
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
            List<rb_b_faultprocess> faultlist = await _B_FaultprocessServices.Query();
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
            FaultHandlinglist[0].createperson = _user.Name;
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
        public async Task<MessageModel<object>> FaultInformationDisplay(int id, int DSMStatus)
        {
            List<object> list = new List<object>();
            List<object> firstphotolist = new List<object>();
            List<object> secondphotolist = new List<object>();
            List<rt_b_photoattachment> thephotolist = await _B_PhotoattachmentServices.Query();
            List<rt_b_faultinfo> faultinfolist = await _B_FaultinfoServices.Query(c => c.id == id);
            List<rb_b_faultprocess> processlist = await _B_FaultprocessServices.Query();//查找所有故障处理
            List<rb_b_faultprocess> faultprocesslist = processlist.FindAll(c => c.faultid == id && c.faulttype == 0);
            List<rb_b_faultprocess> faultprocessessecondlist = processlist.FindAll(c => c.faultid == id && c.faulttype == 1);
            List<rb_b_faultprocess> faultprocessthirdlist = processlist.FindAll(c => c.faultid == id && c.faulttype == 2);
            List<rt_b_photoattachment> photolist = thephotolist.FindAll(c => c.billid == id && c.phototype == 2);//现场照片
            string ipadress = Appsettings.app(new string[] { "AppSettings", "StaticFileUrl", "Connectionip" });
            if (photolist.Count != 0)
            {

                for (int i = 0; i < photolist.Count; i++)
                {
                    string thephoto = $"{ipadress}/{photolist[i].photourl.Split("wwwroot")[1]}";
                    thephoto = thephoto.Replace(@"\", @"/");
                    firstphotolist.Add(thephoto);
                }
            }
            List<rt_b_photoattachment> dealphotolist = thephotolist.FindAll(c => c.billid == id && c.phototype == 3);//处理后照片
            if (dealphotolist.Count != 0)
            {
                for (int j = 0; j < dealphotolist.Count; j++)
                {
                    string url = $"{ipadress}/{dealphotolist[j].photourl.Split("wwwroot")[1]}";
                    secondphotolist.Add(url.Replace(@"\", @"/"));
                }
            }
            list.Add(faultinfolist);
            list.Add(faultprocesslist);
            list.Add(faultprocessessecondlist);
            list.Add(firstphotolist);
            list.Add(secondphotolist);
            list.Add(faultprocessthirdlist);
            //在故障记录界面使用
            if (DSMStatus == 0)
            {
                return new MessageModel<object>()
                {
                    code = 0,
                    msg = "",
                    data = list
                };
            }
            //if (DSMStatus != 3)//未存档则添加一条数据
            //{
            //    //在点击按钮时重复添加了 
            //    rb_b_faultprocess addfaultprocesslist = new rb_b_faultprocess();
            //    addfaultprocesslist.taskperiodname = faultprocessessecondlist[0].taskperiodname;
            //    addfaultprocesslist.faultid = faultprocessessecondlist[0].faultid;
            //    addfaultprocesslist.faulttype = 2;
            //    addfaultprocesslist.processresult = 1;
            //    addfaultprocesslist.processsource = "后台管理系统";
            //    addfaultprocesslist.createperson = _user.Name;
            //    addfaultprocesslist.processdatetime = DateTime.Now;
            //    addfaultprocesslist.processpreson = _user.Name;
            //    addfaultprocesslist.createtime = DateTime.Now;
            //    addfaultprocesslist.meternum = faultprocessessecondlist[0].meternum;
            //    int addid = await _B_FaultprocessServices.Add(addfaultprocesslist);
            //    list.Add(addid);
            //}
            return new MessageModel<object>()
            {
                code = 0,
                msg = "",
                data = list
            };
        }
        #endregion

        #region 故障信息审核 
        [HttpPost]
        [Route("Failureinformationreview")]
        public async Task<MessageModel<object>> Failureinformationreview(int id, int result)
        {
            try
            {
                //先添加一条审核记录
                List<rb_b_faultprocess> processlist = await _B_FaultprocessServices.Query();
                List<rb_b_faultprocess> faultprocessessecondlist = processlist.FindAll(c => c.faultid == id && c.faulttype == 1);
                rb_b_faultprocess addfaultprocesslist = new rb_b_faultprocess();
                addfaultprocesslist.taskperiodname = faultprocessessecondlist[0].taskperiodname;
                addfaultprocesslist.faultid = faultprocessessecondlist[0].faultid;
                addfaultprocesslist.faulttype = 2;
                addfaultprocesslist.processresult = 1;
                addfaultprocesslist.processsource = "后台管理系统";
                addfaultprocesslist.createperson = _user.Name;
                addfaultprocesslist.processdatetime = DateTime.Now;
                addfaultprocesslist.processpreson = _user.Name;
                addfaultprocesslist.createtime = DateTime.Now;
                addfaultprocesslist.meternum = faultprocessessecondlist[0].meternum;
                int addid = await _B_FaultprocessServices.Add(addfaultprocesslist);


                List<rb_b_faultprocess> list = await _B_FaultprocessServices.Query(c => c.id == addid);
                await _B_FaultprocessServices.Update(c => new rb_b_faultprocess
                {
                    processpreson = _user.Name,
                    processdatetime = DateTime.Now,
                    processresult = result
                }, c => c.id == addid);
                string str = await _B_FaultinfoServices.Update(c => new rt_b_faultinfo
                {
                    faultstatus = 3
                }, c => c.id == list[0].faultid) == true ? "ok" : "error";//将状态改为已存档
                //审核不通过重新处理
                if (result == 1)
                {
                    List<rt_b_faultinfo> alllist = await _B_FaultinfoServices.Query();
                    List<rt_b_faultinfo> faultlist = await _B_FaultinfoServices.Query(c => c.id == list[0].faultid && c.faultstatus == 3);//查询之前处理的那条记录
                    //将其改为新的处理
                    rt_b_faultinfo newfaultdeal = new rt_b_faultinfo();
                    newfaultdeal.readdataid = faultlist[0].readdataid;
                    newfaultdeal.readerid = faultlist[0].readerid;
                    newfaultdeal.reportpeople = faultlist[0].reportpeople;
                    newfaultdeal.faultnumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + alllist[alllist.Count - 1].id + 1;
                    newfaultdeal.reporttime = faultlist[0].reporttime;
                    newfaultdeal.taskperiodname = faultlist[0].taskperiodname;
                    newfaultdeal.meterstatus = faultlist[0].meterstatus;
                    newfaultdeal.meternum = faultlist[0].meternum;
                    newfaultdeal.gisinfo = faultlist[0].gisinfo;
                    newfaultdeal.faulttype = faultlist[0].faulttype;
                    newfaultdeal.faultstatus = 0;
                    newfaultdeal.faultcontent = faultlist[0].faultcontent;
                    newfaultdeal.autoaccount = faultlist[0].autoaccount;
                    int resid = await _B_FaultinfoServices.Add(newfaultdeal);
                }
                return new MessageModel<object>()
                {
                    code = 0,
                    msg = "审核成功",
                    data = null
                };
            }
            catch (Exception)
            {
                return new MessageModel<object>()
                {
                    code = 1,
                    msg = "审核失败",
                    data = null
                };
            }

        }
        #endregion

        #region 编辑操作
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
            v_rt_b_faultinfo faultinfo = Common.Helper.JsonHelper.GetObject<v_rt_b_faultinfo>(data);
            string str = await _B_FaultinfoServices.Update(c => new rt_b_faultinfo
            {
                faultnumber = faultinfo.faultnumber,
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