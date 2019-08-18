using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 派工故障信息管理
    /// </summary>
    public class dispatch_faultinfo
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        #region 上传故障工单
        /// <summary>
        /// 故障编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 20)]
        public string faultnumber { get; set; }

        /// <summary>
        /// 故障类型sys_config
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int faulttype { get; set; }

        /// <summary>
        /// 水表编号(t_b_watermeters)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10)]
        public string meternum { get; set; }

        /// <summary>
        /// 用户账号信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 15)]
        public string autoaccount { get; set; }

        /// <summary>
        /// 故障信息内容
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string faultcontent { get; set; }

        /// <summary>
        /// 附件信息---至少3张照片信息
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "text")]
        public string imginfos { get; set; }

        /// <summary>
        /// 上报时间,故障工单上传时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime reporttime { get; set; }

        /// <summary>
        /// gis位置信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string gisinfo { get; set; }

        /// <summary>
        /// 抄表员ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int readerid { get; set; }

        /// <summary>
        /// 上报人
        /// </summary>
        public int reportpeople { get; set; }
        #endregion

        /// <summary>
        /// 处理状态0--未受理;1--已受理;2--已处理;3--已存档(已审核)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short handlestatus { get; set; }

    }
}
