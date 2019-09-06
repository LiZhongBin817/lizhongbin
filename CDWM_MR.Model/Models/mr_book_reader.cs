using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表册与抄表员关联表
    /// </summary>
    public class mr_book_reader:BaseModel
    {
        /// <summary>
        /// 抄表册ID
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "抄表册ID,来源于抄表册")]
        public int bookid { get; set; }

        /// <summary>
        /// 抄表员ID
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "抄表员ID")]
        public int readerid { get; set; }
    }
}
