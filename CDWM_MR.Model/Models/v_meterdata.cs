using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{

    /// <summary>
    /// VIEW
    /// </summary>
    public class v_meterdata
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_meterdata()
        {
        }

        private System.Int32 _readerid;
        /// <summary>
        /// 抄表员id（mr_b_reader：:id）
        /// </summary>
        public System.Int32 readerid { get { return this._readerid; } set { this._readerid = value; } }

        private System.Int64? _shouldcopysum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? shouldcopysum { get { return this._shouldcopysum; } set { this._shouldcopysum = value; } }

        private System.Int64? _alreadysum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? alreadysum { get { return this._alreadysum; } set { this._alreadysum = value; } }

        private System.String _taskperiodname;
        /// <summary>
        /// 任务账期201909冗余
        /// </summary>
        public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

        private System.Decimal? _carrywatercount;
        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? carrywatercount { get { return this._carrywatercount; } set { this._carrywatercount = value; } }

        private System.Int64? _faultcount;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? faultcount { get { return this._faultcount; } set { this._faultcount = value; } }

        private System.Int64? _faultCumulativecount;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? faultCumulativecount { get { return this._faultCumulativecount; } set { this._faultCumulativecount = value; } }

        private System.Int64? _faultalready;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? faultalready { get { return this._faultalready; } set { this._faultalready = value; } }

        private System.Int64? _faultalreadyCumulative;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? faultalreadyCumulative { get { return this._faultalreadyCumulative; } set { this._faultalreadyCumulative = value; } }
    }
}
