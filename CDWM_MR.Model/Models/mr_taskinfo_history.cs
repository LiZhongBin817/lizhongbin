using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表任务单历史表
    /// </summary>
    public class mr_taskinfo_history
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true,ColumnDescription = "ID主键")]
        public int id { get; set; }

        /// <summary>
        /// 抄表任务单编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50,ColumnDescription = "抄表任务单编号")]
        public string tasknumber { get; set; }

        /// <summary>
        /// 抄表任务单名称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 80,ColumnDescription = "抄表任务单名称")]
        public string taskname { get; set; }

        /// <summary>
        /// 抄表册编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50,ColumnDescription = "抄表册编号")]
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表员编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50,ColumnDescription = "抄表员编号")]
        public string mrreadernumber { get; set; }

        /// <summary>
        /// 抄表计划单编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50,ColumnDescription = "抄表计划单编号")]
        public string mplannumber { get; set; }

        /// <summary>
        /// 任务单起抄时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "任务单起抄时间")]
        public DateTime taskstarttime { get; set; }

        /// <summary>
        /// 任务单止抄时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "任务单止抄时间")]
        public DateTime taskendtime { get; set; }

        /// <summary>
        /// 下载任务单开始时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "下载任务单开始时间")]
        public DateTime downloadstarttime { get; set; }

        /// <summary>
        /// 下载任务单结束时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "下载任务单结束时间")]
        public DateTime downloadendtime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "创建时间")]
        public System.DateTime createtime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人--关联Sys_UserInfo
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 20,ColumnDescription = "创建人--关联Sys_UserInfo")]
        public string createpeople { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true,ColumnDescription = "备注")]
        public string remark { get; set; }

        /// <summary>
        /// 任务账期
        /// </summary>
        public string taskperiodname { get; set; }
    }
}
