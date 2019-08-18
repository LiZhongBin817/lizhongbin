using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 派工故障处理信息表
    /// </summary>
    public class dispatch_fault_handleinfo : BaseModel
    {
        /// <summary>
        /// 派工单id-关联dispatchsheet_info
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int dispatchid { get; set; }

        /// <summary>
        /// 故障工单id-关联dispatch_faultinfo
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int faultid{ get; set; }

        /// <summary>
        /// 处理来源(0--APP;1--后台管理系统)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int hendlesource { get; set; }

        /// <summary>
        /// 处理人(sys_userinfo、t_b_users)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int handlepeople { get; set; }

        /// <summary>
        /// 故障处理时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime handletime { get; set; }

        /// <summary>
        /// 故障处理信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string handleinfo { get; set; }

        /// <summary>
        /// 处理后上传的图片
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "text")]
        public string handleimg { get; set; }
    }
}
