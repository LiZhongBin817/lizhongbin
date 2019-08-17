using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 菜单表
    /// </summary>
    public class sys_menu:BaseModel
    {
        /// <summary>
        /// 默认第一级菜单
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int ParentID { get; set; } = 0;

        /// <summary>
        /// 菜单级别
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short MenuLevel { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string MenuNumber { get; set; }

        /// <summary>
        /// 菜单顺序号
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short MenuOrder { get; set; }

        /// <summary>
        /// 菜单Url
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 500)]
        public string MenuUrl { get; set; }

        /// <summary>
        /// 菜单类型0--为主菜单
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int MenuType { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 30)]
        public string MenuImg { get; set; }
        /// <summary>
        /// 子类集合
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Children Childrenlist{ get; set; }
    }
}
