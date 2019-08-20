using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 26. 用水性质 t_b_nature
    /// </summary>
    public class t_b_nature
    {
        /// <summary>
        /// 26. 用水性质 t_b_nature（在本系统中为用水类型）
        /// </summary>
        public t_b_nature()
        {
        }

        /// <summary>
        /// 用水性质编码
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String bntid { get; set; }

        /// <summary>
        /// 用水性质名称
        /// </summary>
        public System.String naturename { get; set; }

        /// <summary>
        /// 状态（1：使用中；2：注销；其它：未定）
        /// </summary>
        public System.Int16? bntstate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? createtime { get; set; }

        /// <summary>
        /// 创建人(t_s_operator:: optid)
        /// </summary>
        public System.String createby { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public System.DateTime? lastmodifytime { get; set; }

        /// <summary>
        /// 最后修改人(t_s_operator:: optid)
        /// </summary>
        public System.String lastmodifyby { get; set; }
    }
}
