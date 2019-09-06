using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 复审（审核）记录表
    /// </summary>
    public class rt_b_recheck
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// 抄表数据id
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "抄表数据id")]
        public int readdataid { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10,ColumnDescription = "水表编号,（t_b_watermeters::meternum）")]
        public string meternum { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "用户id(来源于t_b_users)")]
        public int userid { get; set; }

        /// <summary>
        /// 任务账期201909
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 10,ColumnDescription = "任务账期201909")]
        public string taskperiodname { get; set; }

        /// <summary>
        /// 状态0--通过;1--不通过
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "状态0--通过;1--不通过")]
        public int recheckstatus { get; set; } = 0;

        /// <summary>
        /// 抄表数据审核数据
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10,ColumnDescription = "抄表数据审核数据")]
        public decimal recheckdata { get; set; }

        /// <summary>
        /// 审核原因或结果
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 100,ColumnDescription = "审核原因或结果")]
        public string recheckresult { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "审核时间")]
        public DateTime checksuccesstime { get; set; }

        /// <summary>
        /// 审核人（sys_userinfo::ID,0为系统自动）
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 20,ColumnDescription = "审核人（sys_userinfo::ID,0为系统自动）")]
        public string checkor { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "创建时间")]
        public DateTime createtime { get; set; }

        /// <summary>
        /// 创建人（来源于sys_userinfo）
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 20,ColumnDescription = "创建人（来源于sys_userinfo）")]
        public string createpeople { get; set; }
    }
}
