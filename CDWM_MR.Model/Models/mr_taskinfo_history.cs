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
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 抄表任务单编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string tasknumber { get; set; }

        /// <summary>
        /// 抄表任务单名称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 80)]
        public string taskname { get; set; }

        /// <summary>
        /// 抄表册编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50)]
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表员编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50)]
        public string mrreadernumber { get; set; }

        /// <summary>
        /// 抄表计划单编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string mplannumber { get; set; }

        /// <summary>
        /// 任务单起抄时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime taskstarttime { get; set; }

        /// <summary>
        /// 任务单止抄时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime taskendtime { get; set; }

        /// <summary>
        /// 下载任务单开始时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime downloadstarttime { get; set; }

        /// <summary>
        /// 下载任务单结束时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime downloadendtime { get; set; }

    }
}
