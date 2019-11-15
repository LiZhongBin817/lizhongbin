using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks.Job
{
    /// <summary>
    ///图像识别
    /// </summary>
    public class AutoTask_ImageRec : IJob
    {
        /// <summary>
        /// 构造函数注入
        /// </summary>
        public AutoTask_ImageRec()
        {
            
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
