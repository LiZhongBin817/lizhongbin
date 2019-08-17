using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 菜单按钮接口表
    /// </summary>
    public class sys_operation:BaseModel
    {
        /// <summary>
        /// 操作编号--生成规则为:
        /// ON0001
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10)]
        public string OperationNumber { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string OperationName { get; set; }

        /// <summary>
        /// 菜单ID--关联Sys_Menu
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int MenuID { get; set; }

        /// <summary>
        /// 连接的接口地址
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string LinkUrl { get; set; }

        /// <summary>
        /// 接口ID --关联Sys_Interface_Info
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int InterfaceID { get; set; }

        /// <summary>
        /// 操作状态0--启用;1--禁用
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short OperationStatus { get; set; } = 0;
        /// <summary>
        /// 权限种类 0:增加 1:删除 2:修改  3:查看
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int OperationType { get; set; }

    }
}
