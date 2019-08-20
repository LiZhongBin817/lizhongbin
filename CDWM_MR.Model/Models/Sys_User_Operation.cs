using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 用户操作权限表
    /// </summary>
    public class sys_user_operation:BaseModel
    {
        /// <summary>
        /// 用户ID--关联Sys_UserInfo
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int UserID { get; set; }

        /// <summary>
        /// 操作权限ID--关联Sys_Operation
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int OperationID { get; set; }

        /// <summary>
        /// 菜单ID--关联Sys_Menu
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int MenuID { get; set; }
    }
}
