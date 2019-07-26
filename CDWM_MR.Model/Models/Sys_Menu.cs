using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 菜单表
    /// </summary>
    public class Sys_Menu:BaseModel
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 80)]
        public string MenuName { get; set; }
    }
}
