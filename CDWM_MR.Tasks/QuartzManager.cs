using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks
{
    /// <summary>
    /// 定时任务架构管理
    /// </summary>
    public class QuartzManager
    {
        /// <summary>
        /// 定时任务初始化
        /// </summary>
        /// <returns></returns>
        public async static Task Init()
        {
            LogProvider.SetCurrentLogProvider();
        }

    }
}
