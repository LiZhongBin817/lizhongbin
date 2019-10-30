using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{ /// <summary>
  /// VIEW
  /// </summary>
    public class v_reader_analysis
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_reader_analysis()
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

        private System.Decimal? _meternum;
        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? meternum { get { return this._meternum; } set { this._meternum = value; } }

        private System.Decimal? _readmetertime;
        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? readmetertime { get { return this._readmetertime; } set { this._readmetertime = value; } }

        private System.String _readmonth;
        /// <summary>
        /// 任务账期201909冗余
        /// </summary>
        public System.String readmonth { get { return this._readmonth; } set { this._readmonth = value; } }
    }
}
