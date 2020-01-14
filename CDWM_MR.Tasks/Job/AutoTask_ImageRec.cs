using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
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
        readonly Iv_ocrlogServices iv_Ocrlog;
        readonly Irt_b_ocrlogServices irt_B_OcrlogServices;
        readonly Irt_b_photoattachmentServices photoattachmentService;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        public AutoTask_ImageRec(IRedisHelper redis, Iv_ocrlogServices ocrlogServices, Irt_b_ocrlogServices ocrservices, Irt_b_photoattachmentServices b_PhotoattachmentServices)
        {
            _redis = redis;
            iv_Ocrlog = ocrlogServices;
            irt_B_OcrlogServices = ocrservices;
            photoattachmentService = b_PhotoattachmentServices;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            //var jude = await _redis.KeyExistsAsync("User");
            //if (jude)
            //{

            //}
            //查询所有需要识别的图片
            List<rt_b_ocrlog> ocrlist = new List<rt_b_ocrlog>();
            List<v_ocrlog> orcloglist = await iv_Ocrlog.Query(c => c.temp == 0);
            foreach (var item in orcloglist)
            {
                rt_b_ocrlog orc = new rt_b_ocrlog();
                //调用图像识别方法
                orc.ocrdata = Convert.ToDecimal(Common.Helper.LoadDllHelper.ImgORCMethod(item.photourl));//存放读出来的数据
                //增加一个识别不出来时的判断
                //if (判断条件)
                //{

                //}
                orc.ocrstatus = 0;//需修改
                orc.ocrtime = DateTime.Now;
                orc.ocrusesecond = 1;//需修改
                orc.photoid = item.id;
                orc.readdataid = item.readdataid;
                orc.createpeople = "系统自动识别";
                orc.createtime = DateTime.Now;
                orc.taskperiodname = DateTime.Now.Year.ToString() + DateTime.Now.Month;
                ocrlist.Add(orc);
                //将识别完的图片识别字段改为已识别
                await photoattachmentService.Update(c => new rt_b_photoattachment
                {
                    temp = 1
                }, c => c.id == item.id);
            }
            await irt_B_OcrlogServices.Add(ocrlist);//添加到图像识别表中
            throw new NotImplementedException();
        }
    }
}
