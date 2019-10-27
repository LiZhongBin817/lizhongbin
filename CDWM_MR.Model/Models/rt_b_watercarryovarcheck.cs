using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 结转状态审核表
    /// </summary>
    public class rt_b_watercarryovarcheck
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// 数据结转表id（来源于rt_b_watercarryover）
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "数据结转表id（来源于rt_b_watercarryover）")]
        public int carryoverid { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "用户id(来源于t_b_users)")]
        public string userid { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10,ColumnDescription = "水表编号,（t_b_watermeters::meternum）")]
        public string meternum { get; set; }

        /// <summary>
        /// 任务账期201909
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 10, ColumnDescription = "任务账期201909")]
        public string taskperiodname { get; set; }

        /// <summary>
        /// 结转信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200,ColumnDescription = "结转信息")]
        public string turndatainfo { get; set; }

        /// <summary>
        /// 结转完成日期
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "结转完成日期")]
        public DateTime turndate { get; set; }

        /// <summary>
        ///  结转状态0--结转未通过,1--结转已通过
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "结转状态0--结转未通过,1--结转已通过")]
        public short finishturnstatus { get; set; }
    }
}
