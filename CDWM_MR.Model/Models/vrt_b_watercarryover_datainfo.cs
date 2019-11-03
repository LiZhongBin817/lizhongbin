using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class vrt_b_watercarryover_datainfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public vrt_b_watercarryover_datainfo()
        {
        }

        private System.DateTime _endtime;
        /// <summary>
        /// 本期抄表时间
        /// </summary>
        public System.DateTime endtime { get { return this._endtime; } set { this._endtime = value; } }

        private System.Decimal _carrywatercount;
        /// <summary>
        /// 冗余,用水量=上止-本止
        /// </summary>
        public System.Decimal carrywatercount { get { return this._carrywatercount; } set { this._carrywatercount = value; } }

        private System.String _autoaccount;
        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
        /// </summary>
        public System.String autoaccount { get { return this._autoaccount; } set { this._autoaccount = value; } }

        private System.Int32? _readerid;
        /// <summary>
        /// 抄表员id（mr_b_reader：:id）
        /// </summary>
        public System.Int32? readerid { get { return this._readerid; } set { this._readerid = value; } }
    }
}
