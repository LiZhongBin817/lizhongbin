using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common;
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
    /// 主页抄表记录
    /// </summary>
    [Route("api/HomePageMeterReadingRecord")]
    [AllowAnonymous]
    [EnableCors("LimitRequests")]
    public class HomePageMeterReadingRecordController : ControllerBase
    {
        readonly Iv_meterreading_recordServices Meterreading_RecordServices;
        readonly Iv_rt_b_photoattachment_rt_b_photoattachment_histotyServices _rt_b_photoservices;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="meterreading_RecordServices"></param>
        public HomePageMeterReadingRecordController(Iv_meterreading_recordServices meterreading_RecordServices, Iv_rt_b_photoattachment_rt_b_photoattachment_histotyServices photoattachment_Rt_B_Photoattachment_HistotyServices)
        {
            Meterreading_RecordServices = meterreading_RecordServices;
            _rt_b_photoservices = photoattachment_Rt_B_Photoattachment_HistotyServices;
        }

        /// <summary>
        /// 抄表记录显示
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("MeterReadingRecordInfo")]
        public async Task<TableModel<object>> MeterReadingRecordInfo(string autoaccount ,string startdate,string enddate, int index=0,int page=1,int limit=20)
        {
            List<object> returnData = new List<object>();
            string ipadress = Appsettings.app(new string[] { "AppSettings", "StaticFileUrl", "Connectionip" });
            PageModel<v_meterreading_record> pageModel = new PageModel<v_meterreading_record>();
            #region lambda拼接式 
            if (index == 1)
            {
                Expression<Func<v_meterreading_record, bool>> wherelambda = c =>true;
                if (!string.IsNullOrEmpty(autoaccount))
                {
                    wherelambda = PredicateExtensions.And<v_meterreading_record>(wherelambda, c => c.autoaccount == autoaccount);
                }
                if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate))
                {
                    DateTime date1 = Convert.ToDateTime(startdate);
                    DateTime date2 = Convert.ToDateTime(enddate);
                    wherelambda = PredicateExtensions.And<v_meterreading_record>(wherelambda, c => c.readDateTime >= date1 && c.readDateTime <= date2);                 
                }
                pageModel = await Meterreading_RecordServices.QueryPage(wherelambda, page, limit, "");
            }
            else
            {
                Expression<Func<v_meterreading_record, bool>> wherelambda = c => c.recheckstatus == 0;
                if (!string.IsNullOrEmpty(autoaccount))
                {
                    wherelambda = PredicateExtensions.And<v_meterreading_record>(wherelambda, c => c.autoaccount == autoaccount);
                }
                if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate))
                {
                    DateTime date1 = Convert.ToDateTime(startdate);
                    DateTime date2 = Convert.ToDateTime(enddate);
                    wherelambda = PredicateExtensions.And<v_meterreading_record>(wherelambda, c => c.readDateTime >= date1 && c.readDateTime <= date2);
                }
                pageModel = await Meterreading_RecordServices.QueryPage(wherelambda, page, limit, "");
            }
                  
            #endregion
            List<v_rt_b_photoattachment_rt_b_photoattachment_histoty> photo = await _rt_b_photoservices.Query(c => c.phototype == 1 || c.phototype == 2);           
            for (int i = 0; i < pageModel.dataCount; i++)
            {
                var photoinfo = photo.FindAll(c => c.usercode == autoaccount && c.taskperiodname == pageModel.data[i].taskperiodname);
                photoinfo.ForEach(c => {
                    if (!string.IsNullOrEmpty(c.photourl))
                        c.photourl = $@"{ipadress}{c.photourl.Split("wwwroot")[1]}";
                    c.photourl.Replace(@"\", @"/");
                });//循环修改每一项的值
                var data = new
                {
                    autoaccount= pageModel.data[i].autoaccount,
                    account = pageModel.data[i].account,
                    username = pageModel.data[i].username,
                    taskperiodname = pageModel.data[i].taskperiodname,
                    lastmonthdata = pageModel.data[i].lastmonthdata,
                    inputdata = pageModel.data[i].inputdata,
                    readcheckdata = pageModel.data[i].readcheckdata,
                    readDateTime = pageModel.data[i].readDateTime,
                    ocrdata = pageModel.data[i].ocrdata,
                    mrreadername = pageModel.data[i].mrreadername,
                    photourl = pageModel.data[i].photourl,
                    checkor = pageModel.data[i].checkor,
                    meternum = pageModel.data[i].meternum,
                    regionname = pageModel.data[i].regionname,
                    areaname = pageModel.data[i].areaname,
                    address = pageModel.data[i].address,
                    createtime= pageModel.data[i].createtime,
                    recheckstatus = pageModel.data[i].recheckstatus,
                    recheckresult=pageModel.data[i].recheckresult,
                    pircture = String.Join(',', photoinfo.Select(c => c.photourl).ToArray()),
                    phototype = String.Join(',', photoinfo.Select(c => c.phototype).ToArray())
                };
                returnData.Add(data);
            }
            return new TableModel<object>
            {
                code = 0,
                data = returnData,
                count = returnData.Count,
                msg = "OK"
            };
        }

        [HttpPost]
        [Route("ShowRecheckedPircure")]
        public async Task<TableModel<object>> ShowRecheckedPircure(string autoaccount,string taskperiodname)
        {
            string ipadress = Appsettings.app(new string[] { "AppSettings", "StaticFileUrl", "Connectionip" });
            List<v_rt_b_photoattachment_rt_b_photoattachment_histoty> photo = await _rt_b_photoservices.Query(c => c.phototype == 1 || c.phototype == 2);
            var photoinfo = photo.FindAll(c => c.usercode == autoaccount && c.taskperiodname == taskperiodname);
            photoinfo.ForEach(c => {
                if (!string.IsNullOrEmpty(c.photourl))
                    c.photourl = $@"{ipadress}{c.photourl.Split("wwwroot")[1]}";
                c.photourl.Replace(@"\", @"/");
            });//循环修改每一项的值
            var data = new
            {
                pircture = String.Join(',', photoinfo.Select(c => c.photourl).ToArray()),
                phototype = String.Join(',', photoinfo.Select(c => c.phototype).ToArray())
            };

            return new TableModel<object>
            {
                code = 0,
                data = data,
                msg = "OK"
            };
        }
    }
}