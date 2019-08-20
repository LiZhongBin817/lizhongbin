using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 结转状态审核历史表
    /// </summary>
    public class finishturn_check
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 抄表数据ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int datainfoid { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10)]
        public string meternum { get; set; }

        /// <summary>
        /// 结转信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string turndatainfo { get; set; }

        /// <summary>
        /// 结转完成日期
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime turndate { get; set; }

        /// <summary>
        ///  结转状态0--结转未通过,1--结转已通过
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short finishturnstatus { get; set; }

        /// <summary>
        /// 结转人（sys_userinfo::ID,0为系统自动）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int finishturnpeople { get; set; }
    }
}
