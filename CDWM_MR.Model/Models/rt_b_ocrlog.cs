using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// OCR识别记录表
    /// </summary>
    public class rt_b_ocrlog:BaseModel
    {
        /// <summary>
        /// 抄表数据id
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "抄表数据id")]
        public int readdataid { get; set; }

        /// <summary>
        /// 照片附件id
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "照片附件id")]
        public int photoid { get; set; } = 0;

        /// <summary>
        /// 识别出来的读数
        /// </summary>
        [SugarColumn(IsNullable = false, DecimalDigits = 10,ColumnDescription = "识别出来的读数")]
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
        [SugarColumn(IsNullable = true,ColumnDescription = "使用用时时间：秒")]
        public int ocrusesecond { get; set; }

        /// <summary>
        /// 任务周期
        /// </summary>
        public string taskperiodname { get; set; }


    }
}
