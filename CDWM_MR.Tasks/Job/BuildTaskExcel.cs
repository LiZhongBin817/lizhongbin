using CDWM_MR.IServices.Content;
using CDWM_MR.Services.Content;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks.Job
{
    [DisallowConcurrentExecution]//拒绝同一时间重复执行
    public class BuildTaskExcel : IJob
    {
        private readonly Isys_userinfoServices _userservices;

        public BuildTaskExcel(Isys_userinfoServices userservices)
        {
            _userservices = userservices;
        }

        /// <summary>
        /// 定时任务执行方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            //var builder = new ContainerBuilder();//获取Autofac工作容器类对象
            //var t = builder.RegisterType<sys_userinfoServices>().As<Isys_userinfoServices>();
            //builder.RegisterModule;
            var users = _userservices.Query();
            await Task.Run(() => {
                Console.WriteLine("123");

            });
        }
    }
}
