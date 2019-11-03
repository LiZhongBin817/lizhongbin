using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 照片故障关联表
    /// </summary>
    public class v_photo_faultinfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public v_photo_faultinfo()
        {
        }

        /// <summary>
        /// 服务器存储路径
        /// </summary>
        public System.String photourl { get; set; }

        /// <summary>
        /// 主键id
        /// </summary>
        public System.Int32? faultid { get; set; }

        /// <summary>
        /// 抄表数据id
        /// </summary>
        public System.Int32? readdataid { get; set; }

        /// <summary>
        /// 故障编号（自动生成）
        /// </summary>
        public System.String faultnumber { get; set; }

        /// <summary>
        /// 故障类型sys_config
        /// </summary>
        public System.Int32? faulttype { get; set; }

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
        public System.Int32? meterstatus { get; set; }

        /// <summary>
        /// 抄表员ID
        /// </summary>
        public System.Int32? readerid { get; set; }

        /// <summary>
        /// 上报人
        /// </summary>
        public System.String reportpeople { get; set; }

        /// <summary>
        /// 处理状态0--未受理;1--已受理;2--已处理;3--已存档(已审核)
        /// </summary>
        public System.Int32? faultstatus { get; set; }

        /// <summary>
        /// 受理业务--派工人;处理业务--处理人
        /// </summary>
        public System.String dispatchperson { get; set; }

        /// <summary>
        /// 受理业务:最后处理时间;处理业务:处理时间
        /// </summary>
        public System.DateTime? lasthandletime { get; set; }

        /// <summary>
        /// 备注或描述说明
        /// </summary>
        public System.String remark { get; set; }

        /// <summary>
        /// 处理人(关联sys_userinfo/mr_b_reader)
        /// </summary>
        public System.String acceptanceor { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public System.DateTime? acceptancetime { get; set; }
    }
}
