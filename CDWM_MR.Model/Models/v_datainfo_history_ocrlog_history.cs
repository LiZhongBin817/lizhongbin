using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    public class v_datainfo_history_ocrlog_history
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_datainfo_history_ocrlog_history()
        {
        }

        private System.Int32 _id;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get { return this._id; } set { this._id = value; } }

        private System.String _autoaccount;
        /// <summary>
        /// 用水账号--自动生成(t_b_users)
        /// </summary>
        public System.String autoaccount { get { return this._autoaccount; } set { this._autoaccount = value; } }

        private System.String _account;
        /// <summary>
        /// 户号
        /// </summary>
        public System.String account { get { return this._account; } set { this._account = value; } }

        private System.String _taskperiodname;
        /// <summary>
        /// 任务账期201909冗余
        /// </summary>
        public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }


        private System.Decimal? _ocrdata;
        /// <summary>
        /// 识别出来的读数
        /// </summary>
        public System.Decimal? ocrdata { get { return this._ocrdata; } set { this._ocrdata = value; } }

        private System.Int32? _photoid;
        /// <summary>
        /// 照片附件id
        /// </summary>
        public System.Int32? photoid { get { return this._photoid; } set { this._photoid = value; } }
    }
}
