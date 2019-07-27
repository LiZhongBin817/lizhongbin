using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

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
        public int ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public System.DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人--关联Sys_UserInfo
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 20)]
        public string CreatePeople { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public System.DateTime UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新人--关联Sys_UserInfo
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 20)]
        public string UpdatePeople { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string Remark { get; set; }
    }
}
