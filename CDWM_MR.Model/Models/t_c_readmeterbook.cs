using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 72. 抄表册信息 t_c_readmeterbook
    /// </summary>
    public class t_c_readmeterbook
    {
        /// <summary>
        /// 72. 手工抄表册信息 t_c_readmeterbook
        /// </summary>
        public t_c_readmeterbook()
        {
        }

        /// <summary>
        /// 抄表册编号1
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表人(t_s_opertor::optid)
        /// </summary>
        public System.String readmanid { get; set; }

        /// <summary>
        /// 抄表册名称
        /// </summary>
        public System.String bookname { get; set; }

        /// <summary>
        /// 抄表周期(11:按月 21:单月 22:双月31:按季度1 32:按季度2 33:按季度3)
        /// </summary>
        public System.Int32? readperiod { get; set; }

        /// <summary>
        /// 记录录入人
        /// </summary>
        public System.String createby { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public System.DateTime? createtime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public System.String remarks { get; set; }
    }
}
