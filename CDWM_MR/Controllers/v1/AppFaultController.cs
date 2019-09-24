using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// 故障工单
    /// </summary>
    [Route("api/[controller]")]
    //或者是写[Route("api/[controller]/[action]")]，下面就不要写Route啥的了
    public class AppFaultController : Controller
    {
        #region 相关变量
        readonly Irt_b_faultinfoServices _rt_b_faultinfoServices;
        readonly Iv_rb_b_faultprocessServices _v_rb_b_faultprocessServices;
        readonly Irb_b_faultprocessServices _rb_b_faultprocessServices;
        #endregion
        public AppFaultController(Irt_b_faultinfoServices rt_b_faultinfoServices, Iv_rb_b_faultprocessServices v_rb_b_faultprocessServices, Irb_b_faultprocessServices rb_b_faultprocessServices)
        {
            _rt_b_faultinfoServices = rt_b_faultinfoServices;
            _v_rb_b_faultprocessServices = v_rb_b_faultprocessServices;
            _rb_b_faultprocessServices = rb_b_faultprocessServices;
        }
        #region  提交用户故障工单接口
        /// <summary>
        /// 提交用户故障工单接口
        /// </summary>
        /// <param name="faultDate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateFaultWorkOrder")]
        [AllowAnonymous]//允许所有都访问
        public async Task<int> UpdateFaultWorkOrder([FromBody] List<rt_b_faultinfo> faultDate)
        {
            int Status = 0;
            string faultnumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            List<rt_b_faultinfo> dateist = await _rt_b_faultinfoServices.Query(c => c.faultnumber.Contains(faultnumber));
            faultnumber += (dateist.Count() + 1).ToString().PadLeft(3, '0');
            faultDate.ForEach(c => c.faultnumber = faultnumber);
            await _rt_b_faultinfoServices.Add(faultDate);
            Status = 1;
            return Status;
        }
        #endregion

        #region  获取本周期有异常的数据的接口
        /// <summary>
        /// 获取本周期有异常的数据的接口
        /// </summary>
        /// <param name="readdataid">抄表数据ID</param>
        /// <param name="taskperiodname">任务账单</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetFaultWorkOrder")]
        [AllowAnonymous]//允许所有都访问
        public async Task<object> GetFaultWorkOrder(int readdataid,string taskperiodname)
        {
            List<v_rb_b_faultprocess> datelist = await _v_rb_b_faultprocessServices.Query(c => c.readdataid == readdataid && c.taskperiodname == taskperiodname);
            datelist.Select(c => new
            {
                CustomerNumber = c.autoaccount,//用户家庭编号
                CustomerMeterNumber = c.meternum,//用户水表编号
                CustomerName = c.username,//用户名字
                CustomerUseWaterAddress = c.areaname + c.address,//用户用水地址
                CustomerMeterStatus=c.faulttype,//故障上报
                AcceptPerson=c.processpreson,//受理人的名字
                AcceptTime=c.processdatetime,//后台受理时间
                WorkDisposeName=c.reportpeople,//工单接收人
                EndDisposeTime=c.reporttime,//最迟处理时间
            });
            return new JsonResult(new
            {
                code = 0,
                msg = "成功",
                data = datelist
            });
        }
        #endregion

        #region 故障处理接口
        /// <summary>
        /// 故障处理接口
        /// </summary>
        /// <param name="FaultHandlinglist"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FaultHandling")]
        [AllowAnonymous]//允许所有都访问
        public async Task<object> FaultHandling([FromBody]List<rb_b_faultprocess> FaultHandlinglist)
        {
            int Status = 0;
            await _rb_b_faultprocessServices.Add(FaultHandlinglist);
            Status = 1;
            return Status;
        }
        #endregion
    }
}
