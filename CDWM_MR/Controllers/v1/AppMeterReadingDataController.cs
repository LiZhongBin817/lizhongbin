using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// 抄表数据接口
    /// </summary>
    [Route("api/[controller]")]
    //或者是写[Route("api/[controller]/[action]")]，下面就不要写Route啥的了
    public class AppMeterReadingDataController : Controller
    {
        #region  相关变量
        readonly Imr_datainfoServices _mr_datainfoServices;
        readonly Iv_mr_datainfoServices _v_mr_datainfoServices;
        private readonly Irt_b_photoattachmentServices _rt_b_photoservices;
        readonly Iv_meterdataServices _MeterdataServices;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="mr_datainfoServices"></param>
        /// <param name="v_mr_datainfoServices"></param>
        /// <param name="rtbphotoservices"></param>
        public AppMeterReadingDataController(Imr_datainfoServices mr_datainfoServices, Iv_mr_datainfoServices v_mr_datainfoServices, Irt_b_photoattachmentServices rtbphotoservices, Iv_meterdataServices MeterdataServices)
        {
            _mr_datainfoServices = mr_datainfoServices;
            _v_mr_datainfoServices = v_mr_datainfoServices;
            _rt_b_photoservices = rtbphotoservices;
            _MeterdataServices = MeterdataServices;
        }

        #region  上传用户抄表数据接口
        /// <summary>
        /// 上传用户抄表数据接口
        /// </summary>
        /// <param name="UserData">用户抄表数据接口对象</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateUserData")]
        [AllowAnonymous]//允许所有都访问
        public async Task<MessageModel<int>> UpdateUserData([FromBody]List<mr_datainfo> UserData)
        {
            var data = new MessageModel<int>();
            //int Status = 0;
            if (UserData == null || UserData?.Count == 0)
            {
                data.code = 1001;
                data.msg = "无上传数据！";
                data.data = 0;
                return data;
            }
            try
            {
                for (int i = 0; i < UserData.Count; i++)
                {
                    mr_datainfo updatemodel = UserData[i];
                    updatemodel.readstatus = 1;
                    List<string> updatefield = new List<string>() { "uploadgisplace", "readDateTime", "readstatus", "remark", "inputdata", "uploadtime", "readtype" };
                    await _mr_datainfoServices.Update(updatemodel, updatefield);
                    
                }
                //Status = UserData.Count();
            }
            catch(Exception ex)
            {
                data.code = 1001;
                data.msg = ex.ObjToString();
                data.data = 0;
                return data;
            }
            data.code = 0;
            data.msg = "成功";
            data.data = UserData.Count();
            return data;
        }
        #endregion

        #region   获取本周期已经审查的抄表数据接口
        /// <summary>
        /// 获取本周期已经审查的抄表数据接口
        /// </summary>
        /// <param name="taskperiodname">任务周期</param>
        /// <param name="taskid">任务单ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetcheckedReadingWaterDate")]
        [AllowAnonymous]//允许所有都访问
        public async Task<object> GetcheckedReadingWaterDate(string taskperiodname, int taskid)
        {
            #region lambda拼接式
            Expression<Func<v_mr_datainfo, bool>> wherelambda = c => true;
            if (taskid != 0)
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.taskid == taskid);
            }
            if (!string.IsNullOrEmpty(taskperiodname))
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.taskperiodname == taskperiodname);
            }
            #endregion
            List<v_mr_datainfo> dateinfolist = await _v_mr_datainfoServices.Query(wherelambda);
            List<object> datelist = new List<object>();
            for (int i = 0; i < dateinfolist.Count(); i++)
            {
                //地址=小区+家庭地址
                string address = dateinfolist[i].areaname + dateinfolist[i].address;
                var date = new
                {
                    ID = dateinfolist[i].ID,
                    CustomerNumber = dateinfolist[i].autoaccount,//用户家庭编号
                    CustomerMEterNumber = dateinfolist[i].meternum,//用户水表编号
                    CustomerName = dateinfolist[i].username,//用户名字
                    CustomerUseWaterAddress = address,//用户用水地址
                    CusWaterConsumption = dateinfolist[i].inputdata//用户用水量
                };
                datelist.Add(date);
            }
            return new JsonResult(new
            {
                code = 0,
                msg = "成功",
                data = datelist
            });
        }
        #endregion

        #region 抄表数据统计
        [HttpPost]
        [Route("readdatastatistics")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<object>> readdatastatistics(string month)
        { 
            List<v_meterdata> pageModel = new List<v_meterdata>();
            Expression<Func<v_meterdata, bool>> wherelambda = c => true;
            #region lambda拼接
            if (!string.IsNullOrEmpty(month))
            { 
                wherelambda = PredicateExtensions.And<v_meterdata>(wherelambda, c => c.taskperiodname == month);
            } 
            #endregion 
            pageModel = await _MeterdataServices.Query(wherelambda);
            var datalist = pageModel
                 .Select(c => new {
                     readstatussum=c.readstatussum,
                     readstatus0=c.readstatus0,
                     taskperiodname = c.taskperiodname,
                     usewaternum = c.usewaternum,
                     date01 = c.date01,
                     datehistory = c.datehistory,
                     faultinfocount = c.faultinfocount,
                     faultinfo_historycount = c.faultinfo_historycount,
                     faultstatus = c.faultstatus,
                     faultstatushistory = c.faultstatushistory,
                 }) ; string [] a; int i=1;
            for (int bb = 0; bb < datalist.First().readstatussum; bb++)
            {
                a[bb] = datalist.First().date01.ToString();
                List<object> li = new List<object>();
                li.Add(a[bb]);  
                if (!li.Contains(a[bb]))
                {
                    i++;
                }
            }
             
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                data = datalist
            };


        }
            #endregion

    }
}
