using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class sys_role_menu:BaseModel
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

        /// <summary>
        /// 判断是接口id还是权限id(0是权限id，1是接口id)
        /// </summary>
        public short judgetype { get; set; }

        /// <summary>
        /// 导航sys_interface_info表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public sys_interface_info interfaceinfo { get; set; }

        /// <summary>
        /// 导航sys_role表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public sys_role Role { get; set; }

        /// <summary>
        /// 导航sys_menu表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public sys_menu Menu { get; set; }

        /// <summary>
        /// 导航sys_operation
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public sys_operation Operation { get; set; }
        

    }
}
