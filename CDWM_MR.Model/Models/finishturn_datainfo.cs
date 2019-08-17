using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 结转数据表
    /// </summary>
    public class finishturn_datainfo:BaseModel
    {
        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, Length = 15)]
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10)]
        public string meternum { get; set; }

        /// <summary>
        /// 结转月份
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime turnmonth { get; set; }

        /// <summary>
        /// 记账水量
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10)]
        public decimal bookkeepingnum { get; set; }
        
        /// <summary>
        /// 用量调整
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10)]
        public decimal changewaternum { get; set; }

        /// <summary>
        /// 调整水量类型
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int changetype { get; set; }

        /// <summary>
        /// 调整人员(关联sys_userinfo)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int changepeople { get; set; }

        /// <summary>
        /// 调整水量原因
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 100)]
        public string changereasoninfo { get; set; }

        /// <summary>
        /// 结转用水量
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10)]
        public decimal turnwaternum { get; set; }

        /// <summary>
        /// 起码抄表时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime turnstarttime { get; set; }

        /// <summary>
        /// 结转起码
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10)]
        public decimal turnstartwaternum { get; set; }

        /// <summary>
        /// 止码抄表时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime turnendtime { get; set; }

        /// <summary>
        /// 结转止码
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10)]
        public decimal turnendwaternum { get; set; }
    }
}
