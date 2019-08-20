using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 派工单信息表
    /// </summary>
    public class dispatchsheet_info:BaseModel
    {
        /// <summary>
        /// 故障工单ID（关联 dispatch_faultinfo）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int faultid { get; set; }

        /// <summary>
        /// 派工人(mr_b_reader)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int dispatcher { get; set; }

        /// <summary>
        /// 操作派工人员(sys_userinfo)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int operadispatch { get; set; }

        /// <summary>
        /// 最迟处理时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime lasthandletime { get; set; }

    }
}
