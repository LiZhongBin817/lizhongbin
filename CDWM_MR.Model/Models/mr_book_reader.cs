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
        [SugarColumn(IsNullable = true)]
        public int bookid { get; set; }

        /// <summary>
        /// 抄表员ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int readerid { get; set; }

    }
}
