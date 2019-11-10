using CDWM_MR.Common;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks.Job
{
    /// <summary>
    /// 生成计划单/任务单
    /// </summary>
    public class AutoTask_plansheet : IJob
    {
        #region 相关变量
        private readonly Imr_planinfoServices _planservices;
        private readonly Iv_taskinfoServices _vtaskinfoservices;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        public AutoTask_plansheet(Imr_planinfoServices planservices, Iv_taskinfoServices vtaskinfoservice)
        {
            _planservices = planservices;
            _vtaskinfoservices = vtaskinfoservice;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            mr_planinfo plansheet = new mr_planinfo();
            string systemname = Appsettings.app(new string[] { "UseSystemName", "name" });
            DateTime firstday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//当前月份的第一天
            DateTime lastday = firstday.AddMonths(1).AddDays(-1);//当前月份的最后一天
            plansheet.mplanname = systemname;
            plansheet.mplanyear = DateTime.Now.Year.ObjToString();
            plansheet.mplanmonth = DateTime.Now.Month.ObjToString();
            plansheet.planstarttime = firstday;//计划开始时间
            plansheet.planendtime = lastday;//计划结束时间
            plansheet.createpeople = "系统自动创建";
            plansheet.createtime = DateTime.Now;
            plansheet.finishstatus = 0;
            int planaddid = await _planservices.Add(plansheet);
            await _vtaskinfoservices.AutoCreat(planaddid);
        }
    }
}
