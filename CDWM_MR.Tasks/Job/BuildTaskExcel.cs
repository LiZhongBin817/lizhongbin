using Quartz;
using System;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks.Job
{
    [DisallowConcurrentExecution]//拒绝同一时间重复执行
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
            await Task.Run(() =>
            {
                Console.WriteLine("123");

            });
        }
    }
}
