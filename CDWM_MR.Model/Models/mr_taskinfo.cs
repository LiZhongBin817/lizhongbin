using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表任务单信息表
    /// </summary>
    public class mr_taskinfo:BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 抄表任务单编号
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50,ColumnDescription = "抄表任务单编号")]
        public string tasknumber { get; set; }

        /// <summary>
        /// 抄表任务单名称
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 80,ColumnDescription = "抄表任务单名称")]
        public string taskname { get; set; }

        /// <summary>
        /// 抄表册ID,来源于mr_b_bookinfo::ID
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "抄表册ID,来源于mr_b_bookinfo::id")]
        public int bookid { get; set; }

        /// <summary>
        /// 抄表员ID,mr_b_reader::id
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "抄表员ID,mr_b_reader::id")]
        public int readerid { get; set; }

        /// <summary>
        /// 计划单ID(mr_planinfo::ID)
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "计划单ID(mr_planinfo::id)")]
        public int planid { get; set; }

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
        /// 任务单完成状态0--计划;1--下达;2--完成
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "任务单完成状态0--计划;1--下达;2--完成")]
        public int taskstatus { get; set; }

        /// <summary>
        /// 下载状态0--已下载;1--未下载
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "下载状态0--已下载;1--未下载")]
        public short dowloadstatus { get; set; } = 1;

        /// <summary>
        /// 任务账期 201909 冗余
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 10,ColumnDescription = "任务账期 201909 冗余")]
        public string taskperiodname { get; set; }
        #region 导航属性

        /// <summary>
        /// 抄表册
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public mr_b_bookinfo bookinfo { get; set; }

        /// <summary>
        /// 抄表员信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public mr_b_reader readerinfo { get; set; }

        /// <summary>
        /// 计划单信息表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public mr_planinfo planinfo { get; set; }

        #endregion

    }
}
