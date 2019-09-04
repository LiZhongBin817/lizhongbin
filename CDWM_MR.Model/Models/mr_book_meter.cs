using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表册与水表关联表
    /// </summary>
    public class mr_book_meter:BaseModel
    {
        /// <summary>
        /// 抄表册ID
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int bookid { get; set; }

        /// <summary>
        /// 水表ID
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int watermeterid { get; set; }

        /// <summary>
        /// 表册内水表顺序
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int meterseq { get; set; }
    }
}
