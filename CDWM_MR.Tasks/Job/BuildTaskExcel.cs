using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks.Job
{
    public class BuildTaskExcel : IJob
    {

        public BuildTaskExcel()
        {

        }

        /// <summary>
        /// 定时任务执行方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => {

            });
        }
    }
}
