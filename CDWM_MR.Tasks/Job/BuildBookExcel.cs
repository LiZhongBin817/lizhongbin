using CDWM_MR.IServices.Content;
using Quartz;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks.Job
{
    [DisallowConcurrentExecution]//拒绝同一时间重复执行
    public class BuildBookExcel : IJob
    {
        private IBuildBookServices _IBuildBookServices;


        /// <summary>
        /// 定时任务执行方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.MergedJobDataMap;
            _IBuildBookServices = dataMap.Get("buildService") as IBuildBookServices;
            await Task.Run(() =>
            {
                _IBuildBookServices.DoworkAsync();
            });

        }
    }
}
