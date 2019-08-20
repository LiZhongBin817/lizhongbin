using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 系统参数表
    /// </summary>
    public class sys_parameter:BaseModel
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string parametername { get; set; }

        /// <summary>
        /// 参数编号
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string parameternumber { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int parametertype { get; set; }

        /// <summary>
        /// 参数类型名称
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 40)]
        public string parametertypename { get; set; }

        /// <summary>
        /// 参数key
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string parameterkey { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string parametervalue { get; set; }

    }
}
