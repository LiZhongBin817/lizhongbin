using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{ /// <summary>
  /// VIEW
  /// </summary>
    public class v_mr_date_reader
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_mr_date_reader()
        {
        }

        private System.Int32 _id;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get { return this._id; } set { this._id = value; } }

        private System.String _mrreadername;
        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadername { get { return this._mrreadername; } set { this._mrreadername = value; } }

        private System.DateTime? _readtime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? readtime { get { return this._readtime; } set { this._readtime = value; } }

        private System.DateTime? _mindatatime;
        /// <summary>
        /// 重要基础数据
        /// </summary>
        public System.DateTime? mindatatime { get { return this._mindatatime; } set { this._mindatatime = value; } }

        private System.DateTime? _maxdatetime;
        /// <summary>
        /// 重要基础数据
        /// </summary>
        public System.DateTime? maxdatetime { get { return this._maxdatetime; } set { this._maxdatetime = value; } }

        private System.Int32? _meternum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? meternum { get { return this._meternum; } set { this._meternum = value; } }

        private System.Int64? _readmetertime;
        /// <summary>
        /// 
        /// </summary>
        public System.Int64? readmetertime { get { return this._readmetertime; } set { this._readmetertime = value; } }

        private System.String _metermonth;
        /// <summary>
        /// 任务账期201909冗余
        /// </summary>
        public System.String metermonth { get { return this._metermonth; } set { this._metermonth = value; } }
    }
}