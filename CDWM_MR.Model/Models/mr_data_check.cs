using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表审核表
    /// </summary>
    public class mr_data_check
    {
        /// <summary>
        /// 抄表数据ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int datainfoid { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10)]
        public string meternum { get; set; }

        /// <summary>
        /// 审核信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string checkinfo { get; set; }

        /// <summary>
        /// 审核状态0--审核未通过,1--审核已通过
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short checkstatus { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime checksuccesstime { get; set; }

        /// <summary>
        /// 审核人（sys_userinfo::ID,0为系统自动）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int checkor { get; set; }
    }
}
