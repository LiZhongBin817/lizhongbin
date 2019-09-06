using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 水量调整记录表
    /// </summary>
    public class rt_b_wateradjust
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// 水量结转id唯一
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "水量结转id唯一")]
        public int carryoverid { get; set; }

        /// <summary>
        /// 调整用量（+ - 带符号）
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "调整用量（+ - 带符号）")]
        public decimal adjustwatercount { get; set; }

        /// <summary>
        /// 调整人(来源于sys_userinfo)
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 30,ColumnDescription = "调整人(来源于sys_userinfo)")]
        public string adjustperson { get; set; }

        /// <summary>
        /// 调整时间
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "调整时间")]
        public DateTime adjusttime { get; set; }

        /// <summary>
        /// 调整说明
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50,ColumnDescription = "调整说明")]
        public string adjustremark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "创建时间")]
        public DateTime createtime { get; set; }

        /// <summary>
        /// 创建人(来源于sys_userinfo)
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 20,ColumnDescription = "创建人")]
        public string createperson { get; set; }
    }
}
