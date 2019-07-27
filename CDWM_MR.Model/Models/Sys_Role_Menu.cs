using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class Sys_Role_Menu:BaseModel
    {
        /// <summary>
        /// 角色ID--关联Sys_Role
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int RoleID { get; set; }

        /// <summary>
        /// 菜单ID--关联Sys_Menu
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int MenuID { get; set; }

        /// <summary>
        /// 操作者ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int OperationID { get; set; }

    }
}
