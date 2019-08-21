using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表计划单历史表
    /// </summary>
    public class mr_planinfo_history
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 抄表计划单编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string mplannumber { get; set; }

        /// <summary>
        /// 抄表计划单名称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string mplanname { get; set; }

        /// <summary>
        /// 所属年
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10)]
        public string mplanyear { get; set; }

        /// <summary>
        /// 所属月份
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 2)]
        public string mplanmonth { get; set; }

        /// <summary>
        /// 计划单开始时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime planstarttime { get; set; }

        /// <summary>
        /// 计划单结束时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime planendtime { get; set; }
    }
}
