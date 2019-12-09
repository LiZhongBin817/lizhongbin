using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Tasks.Job;
//using CDWM_MR.Tasks.Log;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Quartz.Spi;
using System;
using System.Linq.Expressions;
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
        private readonly Isys_parameterServices _sys_parmeter;
        private IScheduler _scheduler;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="iocJobfactory">job工厂类</param>
        /// <param name="schedulerFactory"></param>
        public QuartzManager(IJobFactory iocJobfactory, ISchedulerFactory schedulerFactory, Isys_parameterServices sysparmeter)
        {
            this._schedulerFactory = schedulerFactory;
            this._iocJobfactory = iocJobfactory;
            _sys_parmeter = sysparmeter;
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

            #region 从数据库中获取定时任务触发
            var plansheettime = await _sys_parmeter.QueryById(1);
            var carryovertime =(await _sys_parmeter.QueryById(3)).parametervalue;
            //string carryovertime = DateTime.Now.Minute.ToString();
            #endregion

            _scheduler = await _schedulerFactory.GetScheduler();
            _scheduler.JobFactory = this._iocJobfactory;//  替换默认工厂
            await _scheduler.Start();//启动单元
            #region 任务一 生成计划单
            //创建作业
            IJobDetail plansheet = JobBuilder.Create<AutoTask_plansheet>()
                .WithIdentity("AutoTask_plansheet", "task1")
                .WithDescription("生成计划单")
                .Build();

            //创建时间策略
            ITrigger triggerplansheet = TriggerBuilder.Create()
                              .WithIdentity("AutoTask_plansheettigger", "task1")
                              .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(10)))
                             //.StartNow()//StartAt  Cron
                             .WithCronSchedule(plansheettime.parametervalue)
                             .WithDescription("生成计划单！")
                             .Build();
            await _scheduler.ScheduleJob(plansheet, triggerplansheet);
            #endregion

            #region 任务二 图像识别图片

            #endregion

            #region 任务三 结转数据到历史表
            //创建作业
            IJobDetail carryoverhistory = JobBuilder.Create<AutoTask_CarryOverData>()
                .WithIdentity("AutoTask_carryoverhistory", "task3")
                .WithDescription("结转数据到历史表!")
                .Build();

            //创建时间策略
            ITrigger triggercarryoverhistory = TriggerBuilder.Create()
                              .WithIdentity("AutoTask_carryoverhistorytigger", "task3")
                              .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(10)))
                             //.StartNow()//StartAt  Cron
                             .WithCronSchedule("0 30 22 * * ?")
                             .WithDescription("结转数据到历史表！")
                             .Build();
            await _scheduler.ScheduleJob(carryoverhistory,triggercarryoverhistory);
            #endregion

            #region 任务四 自动结转数据
            //创建作业
            IJobDetail autocarryover = JobBuilder.Create<AutoTask_AutoCarryOver>()
                .WithIdentity("AutoTask_autocarryover", "task4")
                .WithDescription("自动结转数据到结转表")
                .Build();

            //创建时间策略
            ITrigger triggerautocarryover = TriggerBuilder.Create()
                .WithIdentity("AutoTask_autocarryovertigger", "task4")
                .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(20)))
                .WithCronSchedule(carryovertime)
                .WithDescription("自动结转数据到结转表")
                .Build();

            await _scheduler.ScheduleJob(autocarryover, triggerautocarryover);
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
