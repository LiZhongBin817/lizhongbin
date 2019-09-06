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
        [SugarColumn(IsNullable = false,ColumnDescription = "抄表册ID,来源于mr_bookinfo")]
        public int bookid { get; set; }

        /// <summary>
        /// 水表编号
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "水表编号")]
        public string watermeternumber { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "用户编号（冗余）")]
        public string useraccount { get; set; }

        /// <summary>
        /// 表册内水表顺序
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "表册内水表顺序")]
        public int meterseq { get; set; }
    }
}
