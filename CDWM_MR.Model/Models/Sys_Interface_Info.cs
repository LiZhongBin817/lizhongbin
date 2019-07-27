using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    public class Sys_Interface_Info
    {

        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 30)]
        public string InterfaceUrl { get; set; }

        /// <summary>
        /// 接口版本
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 5)]
        public string OperationVersion { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 40)]
        public string InterfaceName { get; set; } 

        /// <summary>
        /// 是否为第三方接口0--是，1--否
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short ExternalInterface { get; set; }

        /// <summary>
        /// 是否验证权限0--是，1--否
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short Verify { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 100)]
        public string Remark { get; set; }

    }
}
