using SqlSugar;
using System;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// CDWM_MR基类表
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

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
