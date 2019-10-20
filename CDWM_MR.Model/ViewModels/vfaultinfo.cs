using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.ViewModels
{
    /// <summary>
    /// 故障视图信息表
    /// </summary>
    public class vfaultinfo
    {
        /// <summary>
        /// 故障id
        /// </summary>
        public System.Int32 id { get; set; }

        /// <summary>
        /// 用水用户
        /// </summary>
        public string autoaccount { get; set; }

        /// <summary>
        /// 水表编号
        /// </summary>
        public string meternum { get; set; }

        /// <summary>
        /// 水表状态信息
        /// </summary>
        public System.String parametername { get; set; }

        /// <summary>
        /// 故障内容
        /// </summary>
        public string faultcontent { get; set; }
    }
}
