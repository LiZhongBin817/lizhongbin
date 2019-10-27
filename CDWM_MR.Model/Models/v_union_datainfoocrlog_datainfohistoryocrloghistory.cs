using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    public class v_union_datainfoocrlog_datainfohistoryocrloghistory
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_union_datainfoocrlog_datainfohistoryocrloghistory()
        {

        }

        private System.Int32 _ID;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ID { get { return this._ID; } set { this._ID = value; } }

        private System.String _autoaccount;
        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
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
        /// 图像识别抄表数据Optical Choractor Recognittion光学字符识别
        /// </summary>
        public System.Decimal? ocrdata { get { return this._ocrdata; } set { this._ocrdata = value; } }

        private System.Decimal? _inputdata;
        /// <summary>
        /// 人为抄表数据
        /// </summary>
        public System.Decimal? inputdata { get { return this._inputdata; } set { this._inputdata = value; } }

        private System.Int32? _photoid;
        /// <summary>
        /// 照片附件id
        /// </summary>
        public System.Int32? photoid { get { return this._photoid; } set { this._photoid = value; } }
    }
}
