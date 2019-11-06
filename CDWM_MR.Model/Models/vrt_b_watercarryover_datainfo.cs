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

        private System.String _autoaccount;
        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
        /// </summary>
        public System.String autoaccount { get { return this._autoaccount; } set { this._autoaccount = value; } }

        private System.String _taskperiodname;
        /// <summary>
        /// 任务账期201909
        /// </summary>
        public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

        private System.Decimal _startnum;
        /// <summary>
        /// 上期止码,本期起码
        /// </summary>
        public System.Decimal startnum { get { return this._startnum; } set { this._startnum = value; } }

        private System.Decimal _endnum;
        /// <summary>
        /// 本期止码,下期起码
        /// </summary>
        public System.Decimal endnum { get { return this._endnum; } set { this._endnum = value; } }

        private System.Decimal _carrywatercount;
        /// <summary>
        /// 冗余,用水量=上止-本止
        /// </summary>
        public System.Decimal carrywatercount { get { return this._carrywatercount; } set { this._carrywatercount = value; } }

        private System.Decimal? _bookkeepingcount;
        /// <summary>
        /// 记账用量,冗余
        /// </summary>
        public System.Decimal? bookkeepingcount { get { return this._bookkeepingcount; } set { this._bookkeepingcount = value; } }
    }
}
