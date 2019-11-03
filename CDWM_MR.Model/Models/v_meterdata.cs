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

        private System.Int64? _readstatussum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? readstatussum { get { return this._readstatussum; } set { this._readstatussum = value; } }

        private System.Int64? _readstatus0;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? readstatus0 { get { return this._readstatus0; } set { this._readstatus0 = value; } }

        private System.String _taskperiodname;
        /// <summary>
        /// 任务账期201909冗余
        /// </summary>
        public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

        private System.Decimal? _usewaternum;
        /// <summary>
        /// 当前月份用水量
        /// </summary>
        public System.Decimal? usewaternum { get { return this._usewaternum; } set { this._usewaternum = value; } }

        private System.DateTime? _date01;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? date01 { get { return this._date01; } set { this._date01 = value; } }

        private System.DateTime? _datehistory;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? datehistory { get { return this._datehistory; } set { this._datehistory = value; } }

        private System.Int64? _faultinfocount;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? faultinfocount { get { return this._faultinfocount; } set { this._faultinfocount = value; } }

        private System.Int64? _faultinfo_historycount;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? faultinfo_historycount { get { return this._faultinfo_historycount; } set { this._faultinfo_historycount = value; } }

        private System.Int32? _faultstatus;
        /// <summary>
        /// 处理状态0--未受理;1--已受理;2--已处理;3--已存档(已审核)
        /// </summary>
        public System.Int32? faultstatus { get { return this._faultstatus; } set { this._faultstatus = value; } }

        private System.Int32? _faultstatushistory;
        /// <summary>
        /// 处理状态0--未受理;1--已受理;2--已处理;3--已存档(已审核)
        /// </summary>
        public System.Int32? faultstatushistory { get { return this._faultstatushistory; } set { this._faultstatushistory = value; } }
    }
}
