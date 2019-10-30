using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.ViewModels
{
    /// <summary>
    /// 上传故障信息视图对象
    /// </summary>
    public class UploadFaultModel
    {
        /// <summary>
        /// 抄表数据id
        /// </summary>
        public int readdataid { get; set; }

        /// <summary>
        /// 故障编号（自动生成）
        /// </summary>
        public string faultnumber { get; set; }

        /// <summary>
        /// 故障类型sys_config
        /// </summary>
        public int faulttype { get; set; }

        /// <summary>
        /// 水表编号(t_b_watermeters)
        /// </summary>
        public string meternum { get; set; }

        /// <summary>
        /// 任务账期(201909)
        /// </summary>
        public string taskperiodname { get; set; }

        /// <summary>
        /// 用户账号信息
        /// </summary>
        public string autoaccount { get; set; }

        /// <summary>
        /// 故障信息内容
        /// </summary>
        public string faultcontent { get; set; }

        /// <summary>
        /// 上报时间,故障工单上传时间
        /// </summary>
        public DateTime reporttime { get; set; }

        /// <summary>
        /// gis位置信息
        /// </summary>
        public string gisinfo { get; set; }

        /// <summary>
        /// 水表状态0--正常(默认)其他状态来源于sys_config
        /// </summary>
        public int meterstatus { get; set; }

        /// <summary>
        /// 抄表员ID
        /// </summary>
        public int readerid { get; set; }

        /// <summary>
        /// 上报人
        /// </summary>
        public string reportpeople { get; set; }

        /// <summary>
        /// 处理状态0--未受理;1--已受理;2--已处理;3--已存档(已审核)
        /// </summary>
        public int faultstatus { get; set; }

        /// <summary>
        /// 是否上传过故障图片数据0--未上传;1--已上传
        /// </summary>
        public int isupdateimg { get; set; }

    }
}
