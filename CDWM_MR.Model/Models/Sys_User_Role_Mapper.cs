using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 用户,角色关联表
    /// </summary>
    public class Sys_User_Role_Mapper
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 角色ID--关联Sys_Role
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int RoleID { get; set; }

        /// <summary>
        /// 用户ID--关联Sys_UserInfo
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int UserID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public System.DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人--关联Sys_UserInfo
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 20)]
        public string CreatePeople { get; set; }
    }
}
