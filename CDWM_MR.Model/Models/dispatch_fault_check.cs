using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 派工故障审核表
    /// </summary>
    public class dispatch_fault_check
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 派工单id-关联dispatchsheet_info
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int dispatchid { get; set; }

        /// <summary>
        /// 故障工单id-关联dispatch_faultinfo
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int faultid { get; set; }

        /// <summary>
        /// 处理故障表id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int hendleid { get; set; }

        /// <summary>
        /// 用户账号信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 15)]
        public string autoaccount { get; set; }

        /// <summary>
        /// 审核来源0--APP手机端;1--后台管理系统;
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int checksource { get; set; }

        /// <summary>
        /// 审核人sys_userinfo,m_b_reader
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int checker { get; set; }

        /// <summary>
        /// 审核信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string checkinfo { get; set; }

        /// <summary>
        /// 审核状态0--审核已通过;1--审核未通过
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int checkstatus { get; set; }

    }
}
