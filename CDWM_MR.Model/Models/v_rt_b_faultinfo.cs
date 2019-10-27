using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 故障信息视图
    /// </summary>
    public class v_rt_b_faultinfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_rt_b_faultinfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get; set; }

        /// <summary>
        /// 抄表数据id
        /// </summary>
        public System.Int32 readdataid { get; set; }

        /// <summary>
        /// 任务单id(来源于mr_taskinfo)
        /// </summary>
        public System.Int32 taskid { get; set; }

        /// <summary>
        /// 故障编号（自动生成）
        /// </summary>
        public System.String faultnumber { get; set; }

        /// <summary>
        /// 故障类型sys_config
        /// </summary>
        public System.Int32 faulttype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String parametername { get; set; }

        /// <summary>
        /// 水表编号(t_b_watermeters)
        /// </summary>
        public System.String meternum { get; set; }

        /// <summary>
        /// 任务账期(201909)
        /// </summary>
        public System.String taskperiodname { get; set; }

        /// <summary>
        /// 用户账号信息
        /// </summary>
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 故障信息内容
        /// </summary>
        public System.String faultcontent { get; set; }

        /// <summary>
        /// 上报时间,故障工单上传时间
        /// </summary>
        public System.DateTime? reporttime { get; set; }

        /// <summary>
        /// gis位置信息
        /// </summary>
        public System.String gisinfo { get; set; }

        /// <summary>
        /// 水表状态0--正常(默认)其他状态来源于sys_config
        /// </summary>
        public System.Int32 meterstatus { get; set; }

        /// <summary>
        /// 抄表员ID
        /// </summary>
        public System.Int32 readerid { get; set; }

        /// <summary>
        /// 上报人
        /// </summary>
        public System.String reportpeople { get; set; }

        /// <summary>
        /// 处理状态0--未受理;1--已受理;2--已处理;3--已存档(已审核)
        /// </summary>
        public System.Int32 faultstatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 v_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String username { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get; set; }
    }
}
