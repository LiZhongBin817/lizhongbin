using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// OCR识别记录历史表
    /// </summary>
    public class rt_b_ocrlog_history
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public int id { get; set; }

        /// <summary>
        /// 抄表数据id
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "抄表数据id")]
        public int readdataid { get; set; }

        /// <summary>
        /// 照片附件id
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "照片附件id")]
        public int photoid { get; set; } = 0;

        /// <summary>
        /// 识别出来的读数
        /// </summary>
        [SugarColumn(IsNullable = false, DecimalDigits = 10, ColumnDescription = "识别出来的读数")]
        public decimal ocrdata { get; set; } = 0;

        /// <summary>
        /// 识别时间
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "识别时间")]
        public DateTime ocrtime { get; set; }

        /// <summary>
        /// 识别状态
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "识别状态0--成功;1--失败")]
        public int ocrstatus { get; set; } = 1;

        /// <summary>
        /// 使用用时时间：秒
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "使用用时时间：秒")]
        public int ocrusesecond { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "创建时间")]
        public System.DateTime createtime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 20, ColumnDescription = "创建人")]
        public string createpeople { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "更新时间")]
        public System.DateTime updatetime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新人
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 20, ColumnDescription = "更新人")]
        public string updatepeople { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true)]
        public string remark { get; set; }
    }
}
