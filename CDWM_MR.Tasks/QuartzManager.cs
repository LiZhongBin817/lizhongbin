using CDWM_MR.Tasks.Job;
//using CDWM_MR.Tasks.Log;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Quartz.Spi;
using System;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks
{
    /// <summary>
    /// 定时任务架构管理
    /// IScheduler：单元
    /// Ijob：动作，任务
    /// ITigger：触发器
    /// </summary>
    public class QuartzManager
    {
        #region 相关变量
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _iocJobfactory;
        private IScheduler _scheduler;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="iocJobfactory">job工厂类</param>
        /// <param name="schedulerFactory"></param>
        public QuartzManager(IJobFactory iocJobfactory, ISchedulerFactory schedulerFactory)
        {
            this._schedulerFactory = schedulerFactory;
            this._iocJobfactory = iocJobfactory;
        }

        /// <summary>
        /// 定时任务初始化
        /// </summary>
        /// <returns></returns>
        public async Task Init()
        {
            #region 日志记录
            //LogProvider.SetCurrentLogProvider(new CustomerLogProvider());
            #endregion

            _scheduler = await _schedulerFactory.GetScheduler();
            _scheduler.JobFactory = this._iocJobfactory;//  替换默认工厂
            await _scheduler.Start();//启动单元
            #region 任务一 创建任务单Excel文件
            //创建作业
            IJobDetail buildexcel = JobBuilder.Create<BuildBookExcel>()
                .WithIdentity("BuildBookExcel", "task1")
                .WithDescription("创建抄表册Excel文件")
                .Build();

            //创建时间策略
            ITrigger triggerbuildexcel = TriggerBuilder.Create()
                              .WithIdentity("BuildBookExceltigger", "task1")
                              .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(10)))
                             //.StartNow()//StartAt  Cron
                             .WithCronSchedule("0 0/2 * * * ?")
                             .WithDescription("生成抄表册EXCEL文件！")
                             .Build();
            await _scheduler.ScheduleJob(buildexcel, triggerbuildexcel);
            #endregion

        }

        public void Stop()
        {
            if (_scheduler == null)
            {
                return;
            }

            if (_scheduler.Shutdown(waitForJobsToComplete: true).Wait(30000))
                _scheduler = null;
            else
            {
            }
            //_logger.LogCritical("Schedule job upload as application stopped");
        }

    }
}
