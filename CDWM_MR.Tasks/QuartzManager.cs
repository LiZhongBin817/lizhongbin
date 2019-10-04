using CDWM_MR.Tasks.Job;
using CDWM_MR.Tasks.Log;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// <summary>
        /// 定时任务初始化
        /// </summary>
        /// <returns></returns>
        public async static Task Init()
        {
            #region 日志记录
            LogProvider.SetCurrentLogProvider(new CustomerLogProvider());
            #endregion

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();//启动单元

            #region 任务一 创建任务单Excel文件
            //创建作业
            IJobDetail buildexcel = JobBuilder.Create<BuildTaskExcel>()
                .WithIdentity("BuildTaskExcel", "task1")
                .WithDescription("创建任务单Excel文件")
                .Build();

            //创建时间策略
            ITrigger triggerbuildexcel = TriggerBuilder.Create()
                              .WithIdentity("BuildTaskExceltigger", "task1")
                              .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(10)))
                             //.StartNow()//StartAt  Cron
                             .WithCronSchedule("0/1 * * * * ?")
                             .WithDescription("生成抄表册EXCEL文件！")
                             .Build();
            await scheduler.ScheduleJob(buildexcel, triggerbuildexcel);
            #endregion

        }

    }
}
