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
        [SugarColumn(IsNullable = false, IsPrimaryKey = true,ColumnDescription = "ID主键")]
        public int id { get; set; }

        /// <summary>
        /// 抄表计划单编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50,ColumnDescription = "抄表计划单编号")]
        public string mplannumber { get; set; }

        /// <summary>
        /// 抄表计划单名称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50,ColumnDescription = "抄表计划单名称")]
        public string mplanname { get; set; }

        /// <summary>
        /// 所属年
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10,ColumnDescription = "所属年")]
        public string mplanyear { get; set; }

        /// <summary>
        /// 所属月份
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 2,ColumnDescription = "所属月份")]
        public string mplanmonth { get; set; }

        /// <summary>
        /// 计划单开始时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "计划单开始时间")]
        public DateTime planstarttime { get; set; }

        /// <summary>
        /// 计划单结束时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "计划单结束时间")]
        public DateTime planendtime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "创建时间")]
        public System.DateTime createtime { get; set; }  

        /// <summary>
        /// 创建人--关联sys_uerInfo
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 20,ColumnDescription = "创建人--关联sys_uerInfo")]
        public string createpeople { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true,ColumnDescription = "备注")]
        public string remark { get; set; }
    }
}
