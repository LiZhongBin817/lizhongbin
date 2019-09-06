using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表计划单
    /// </summary>
    public class mr_planinfo:BaseModel
    {
        /// <summary>
        /// 抄表计划单编号
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50,ColumnDescription = "抄表计划单编号")]
        public string mplannumber { get; set; }

        /// <summary>
        /// 抄表计划单名称
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50,ColumnDescription = "抄表计划单名称")]
        public string mplanname { get; set; }

        /// <summary>
        /// 所属年
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 10,ColumnDescription = "所属年")]
        public string mplanyear { get; set; }

        /// <summary>
        /// 所属月份
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 2,ColumnDescription = "所属月份")]
        public string mplanmonth { get; set; }

        /// <summary>
        /// 计划单开始时间
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "计划单开始时间")]
        public DateTime planstarttime { get; set; }

        /// <summary>
        /// 计划单结束时间
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "计划单结束时间")]
        public DateTime planendtime { get; set; }

        /// <summary>
        /// 完成状态0--计划;1--下达;2--完成
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "完成状态0--计划;1--下达;2--完成")]
        public int finishstatus { get; set; } = 0;


    }
}
