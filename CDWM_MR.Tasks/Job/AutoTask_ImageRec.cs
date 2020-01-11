using CDWM_MR_Common.Redis;
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
        private IRedisHelper _redis;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        public AutoTask_ImageRec(IRedisHelper redis)
        {
            _redis = redis;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            //var jude = await _redis.KeyExistsAsync("User");
            //if (jude)
            //{

            //}
            //Common.Helper.LoadDllHelper.ImgORCMethod("");
            throw new NotImplementedException();
        }
    }
}
