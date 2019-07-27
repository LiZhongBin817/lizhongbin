using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Sys_Role:BaseModel
    {
        /// <summary>
        /// 角色编号--自动生成,规则为:
        /// RN001
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string RoleNumber { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string RoleName { get; set; }

        /// <summary>
        /// 删除标记,默认为1--已删除,0--未删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short DeleteFlag { get; set; } = 0;
    }
}
