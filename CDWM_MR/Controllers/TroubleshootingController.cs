using CDWM_MR.Common;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CDWM_MR.Controllers
{
    [Route("api/Troubleshooting")]
    [AllowAnonymous]
    [EnableCors("LimitRequests")]
    public class TroubleshootingController : ControllerBase
    {

        #region 相关参数
        readonly Iv_rt_b_faultinfoServices v_rt_b_faultinfoServices;
        readonly Iv_userwatermetersinfoServices v_userwatermetersinfoServices;
        readonly Irt_b_photoattachmentServices rt_b_photoattachmentServices;
        #endregion

        #region 构造函数
        public TroubleshootingController(Iv_rt_b_faultinfoServices iv_Rt_B_Faultinfo, Iv_userwatermetersinfoServices iv_UserwatermetersinfoServices, Irt_b_photoattachmentServices irt_B_PhotoattachmentServices)
        {
            v_rt_b_faultinfoServices = iv_Rt_B_Faultinfo;
            v_userwatermetersinfoServices = iv_UserwatermetersinfoServices;
            rt_b_photoattachmentServices = irt_B_PhotoattachmentServices;
        }
        #endregion

        #region 显示表格
        /// <summary>
        /// 显示表格
        /// </summary>
        /// <param name="autoaccount"></param>
        /// <param name="TroubleStarTime"></param>
        /// <param name="TroubleEndTime"></param>
        /// <param name="TroubleType"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowTroubleshootingTable")]
        public async Task<TableModel<object>> ShowTroubleshootingTable(string autoaccount, string TroubleStarTime, string TroubleEndTime, int TroubleType, int page = 1, int limit = 10)
        {
            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();
            startTime = TroubleStarTime.ObjToDate();
            endTime = TroubleEndTime.ObjToDate();
            PageModel<object> datainfor = new PageModel<object>();
            #region Lambda表达式
            Expression<Func<v_rt_b_faultinfo, bool>> wherelambda = c => true;
            wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.autoaccount == autoaccount);
            if (!string.IsNullOrEmpty(TroubleStarTime) && !string.IsNullOrEmpty(TroubleEndTime) && TroubleType!=0)
            {
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.autoaccount == autoaccount && c.reporttime > startTime && c.reporttime < endTime && c.faulttype == TroubleType);
            }
            if (string.IsNullOrEmpty(TroubleEndTime) && !string.IsNullOrEmpty(TroubleStarTime)&& TroubleType == 0)
            {
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.reporttime > startTime&& c.autoaccount == autoaccount);
            }
            if (string.IsNullOrEmpty(TroubleStarTime) && !string.IsNullOrEmpty(TroubleEndTime) && TroubleType == 0)
            {
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.reporttime < endTime&& c.autoaccount == autoaccount);
            }
            if (string.IsNullOrEmpty(TroubleEndTime) && string.IsNullOrEmpty(TroubleEndTime) && TroubleType!=0)
            {
                wherelambda = PredicateExtensions.And<v_rt_b_faultinfo>(wherelambda, c => c.faulttype == TroubleType&& c.autoaccount == autoaccount);
            }
            #endregion
            Expression<Func<v_rt_b_faultinfo, object>> expression = c => new
            {
                TroubleID = c.id,
                TroubleStatus = c.faultstatus,
                TroubleType = c.faulttype,
                TroubleReportTime = c.reporttime,
                TroubleReporter = c.reportpeople,
                TroubleReason = c.faultcontent,
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

        #region  显示用户信息及图片
        /// <summary>
        /// 显示用户信息及图片
        /// </summary>
        /// <param name="autoaccount"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAutoaccountinfo")]
        public async Task<MessageModel<object>> GetAutoaccountinfo(string autoaccount,string starttime,string endtime,int type)
        {
            try
            {
                DateTime start = starttime.ObjToDate();
                DateTime end = endtime.ObjToDate();
                List<object> alllist = new List<object>();
                List<v_userwatermetersinfo> infolist = await v_userwatermetersinfoServices.Query();
                List<rt_b_photoattachment> photolist = await rt_b_photoattachmentServices.Query();
                List<v_userwatermetersinfo> info = infolist.FindAll(c => c.autoaccount == autoaccount);
                List<rt_b_photoattachment> photo = new List<rt_b_photoattachment>();
                string ipadress = Appsettings.app(new string[] { "AppSettings", "StaticFileUrl", "Connectionip" });
                alllist.Add(info[0]);
                if (string.IsNullOrEmpty(starttime)&&string.IsNullOrEmpty(endtime)&&type==0)
                {
                    photo = photolist.FindAll(c => c.metercode == info[0].meternum);
                    for (int i = 0; i < photo.Count; i++)
                    {
                        photo[i].photourl = $"{ipadress}{photo[i].photourl.Split("wwwroot")[1]}";
                    }
                    alllist.Add(photo);
                }
                if (!string.IsNullOrEmpty(starttime)&&!string.IsNullOrEmpty(endtime)&& type!= 0)
                {
                    photo = photolist.FindAll(c => c.metercode == info[0].meternum && c.phototime > start && c.phototime < end && c.phototype == type);
                    for (int i = 0; i < photo.Count; i++)
                    {
                        photo[i].photourl = $"{ipadress}{photo[i].photourl.Split("wwwroot")[1]}";
                    }
                    alllist.Add(photo);
                }
                if (!string.IsNullOrEmpty(starttime)&&string.IsNullOrEmpty(endtime)&& type == 0)
                {
                    photo = photolist.FindAll(c=>c.phototime>start&& c.metercode == info[0].meternum);
                    for (int i = 0; i < photo.Count; i++)
                    {
                        photo[i].photourl = $"{ipadress}{photo[i].photourl.Split("wwwroot")[1]}";
                    }
                    alllist.Add(photo);
                }
                if (string.IsNullOrEmpty(starttime)&&!string.IsNullOrEmpty(endtime)&& type == 0)
                {
                    photo = photolist.FindAll(c => c.phototime < end&& c.metercode == info[0].meternum);
                    for (int i = 0; i < photo.Count; i++)
                    {
                        photo[i].photourl = $"{ipadress}{photo[i].photourl.Split("wwwroot")[1]}";
                    }
                    alllist.Add(photo);
                }
                if (string.IsNullOrEmpty(starttime)&&string.IsNullOrEmpty(endtime)&&type != 0)
                {
                    photo = photolist.FindAll(c => c.metercode == info[0].meternum && c.phototype == type);
                     for (int i = 0; i < photo.Count; i++)
                    {
                        photo[i].photourl = $"{ipadress}{photo[i].photourl.Split("wwwroot")[1]}";
                    }
                    alllist.Add(photo);
                }
                return new MessageModel<object>()
                {
                    code = 0,
                    msg = "成功",
                    data = alllist
                };
            }
            catch (Exception)
            {
                return new MessageModel<object>()
                {
                    code = 1,
                    msg = "失败",
                    data = null
                };
            }
         
        }
        #endregion

        #region 显示用户信息
        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <param name="autoaccount"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Getinfo")]
        public async Task<MessageModel<object>> Getinfo(string autoaccount)
        {
            try
            {
                List<v_userwatermetersinfo> info = await v_userwatermetersinfoServices.Query(c => c.autoaccount == autoaccount);
                return new MessageModel<object>()
                {
                    code = 0,
                    msg = "成功",
                    data = info[0]
                };
            }
            catch (Exception)
            {
                return new MessageModel<object>()
                {
                    code = 1,
                    msg = "失败",
                    data = null
                };
            }
          
        }
        #endregion
    }
}
