using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// 抄表计划
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AppMeterReadingPlan : Controller
    {
        #region  相关变量 
        readonly Iv_taskinfoServices _v_taskinfoServices;
        #endregion
        public AppMeterReadingPlan(Iv_taskinfoServices v_taskinfoServices)
        {
            _v_taskinfoServices = v_taskinfoServices;
        }

        #region   获取抄表计划接口
        /// <summary>
        /// 获取抄表计划接口
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMeterReadingPlan")]
        [EnableCors("LimitRequests")]
        public async Task<object> GetMeterReadingPlan(string account)
        {
            List<v_taskinfo> list = await _v_taskinfoServices.Query(c => c.appcount == account);
            return list.Select(c => new
            {
                TableNumber=c.bookno,//抄表册编号
                TableName=c.bookname,//抄表册名称
                HouseNumber=c.contectusernum,//该表册下面的用户
                EndReadingTime=c.taskendtime,//该表册最后完成的截止时间
                Period=c.readperiod,//该表册任务所在的抄表周期
                DownloadStatus=c.dowloadstatus,//该表册完成状态  1-未下载  2-已下载
            });
        }
        #endregion
    }
}
