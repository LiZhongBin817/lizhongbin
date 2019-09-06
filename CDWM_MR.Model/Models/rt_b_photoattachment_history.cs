﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 图片附件历史表
    /// </summary>
    public class rt_b_photoattachment_history
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public int id { get; set; }

        /// <summary>
        /// 照片编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50,ColumnDescription = "照片编号")]
        public string photocode { get; set; }

        /// <summary>
        /// 照片类型(0--其他类型;1--表盘抄表;2--现场表况;3--故障处理后(故障);4--其他照片)
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "照片类型(0--其他类型;1--表盘抄表;2--现场表况;3--故障处理后(故障);4--其他照片)")]
        public int phototype { get; set; } = 0;

        /// <summary>
        /// 业务编号如抄表关联id,故障关联故障id,0--默认不关联
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "业务编号如抄表关联id,故障关联故障id,0--默认不关联")]
        public int billid { get; set; } = 0;

        /// <summary>
        /// 服务器存储路径
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 200, ColumnDescription = "服务器存储路径")]
        public string photourl { get; set; }

        /// <summary>
        /// 文件名后缀
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100, ColumnDescription = "文件名后缀")]
        public string photoext { get; set; }

        /// <summary>
        /// 任务账期(201909)
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 10, ColumnDescription = "任务账期(201909)")]
        public string taskperiodname { get; set; }

        /// <summary>
        /// 抄表员编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50, ColumnDescription = "抄表员编号")]
        public string readercode { get; set; }

        /// <summary>
        /// 水表编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "水表编号")]
        public string metercode { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50, ColumnDescription = "用户编号")]
        public string usercode { get; set; }

        /// <summary>
        /// 拍照时间
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "拍照时间")]
        public DateTime phototime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "创建时间")]
        public DateTime createtime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50,ColumnDescription = "创建人")]
        public string createpeople { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 500,ColumnDescription = "备注")]
        public string remark { get; set; }
    }
}
