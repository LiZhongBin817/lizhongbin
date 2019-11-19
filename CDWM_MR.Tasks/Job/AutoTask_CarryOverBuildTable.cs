using CDWM_MR.IServices.Content;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks.Job
{
    /// <summary>
    /// 自动生成账单类任务引擎
    /// </summary>
    public class AutoTask_CarryOverBuildTable : IJob
    {

        #region 相关变量
        private readonly Irt_b_watercarryoverServices _rt_b_watercarryoverservices;
        //private readonly 
        #endregion

        public AutoTask_CarryOverBuildTable(Irt_b_watercarryoverServices rt_b_watercarryoverservices)
        {
            _rt_b_watercarryoverservices = rt_b_watercarryoverservices;

        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
