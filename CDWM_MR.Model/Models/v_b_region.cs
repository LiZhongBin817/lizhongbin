using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_b_region
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_b_region()
        {
        }

        private System.String _regionno;
        /// <summary>
        /// 片区编号
        /// </summary>
        public System.String regionno { get { return this._regionno; } set { this._regionno = value; } }

        private System.String _regionname;
        /// <summary>
        /// 片区名称
        /// </summary>
        public System.String regionname { get { return this._regionname; } set { this._regionname = value; } }

        private System.Int16? _regionstate;
        /// <summary>
        /// 状态(1:可用;2:注销)
        /// </summary>
        public System.Int16? regionstate { get { return this._regionstate; } set { this._regionstate = value; } }
    }
}