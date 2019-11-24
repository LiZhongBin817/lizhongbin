using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    public partial class v_meterreading_record
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_meterreading_record()
        {
        }

        private System.String _autoaccount;
        /// <summary>
        /// 自动帐号(系统自动生成)
        /// </summary>
        public System.String autoaccount { get { return this._autoaccount; } set { this._autoaccount = value; } }

        private System.String _account;
        /// <summary>
        /// 
        /// </summary>
        public System.String account { get { return this._account; } set { this._account = value; } }

        private System.String _username;
        /// <summary>
        /// 
        /// </summary>
        public System.String username { get { return this._username; } set { this._username = value; } }

        private System.String _taskperiodname;
        /// <summary>
        /// 抄表月份(年+月格式201908)
        /// </summary>
        public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

        private System.Decimal? _lastmonthdata;
        /// <summary>
        /// 上月抄表读数
        /// </summary>
        public System.Decimal? lastmonthdata { get { return this._lastmonthdata; } set { this._lastmonthdata = value; } }

        private System.Decimal? _inputdata;
        /// <summary>
        /// 当前月份抄表读数
        /// </summary>
        public System.Decimal? inputdata { get { return this._inputdata; } set { this._inputdata = value; } }

        private System.Decimal? _readcheckdata;
        /// <summary>
        /// 复审读数(抄表数据审核数据)
        /// </summary>
        public System.Decimal? readcheckdata { get { return this._readcheckdata; } set { this._readcheckdata = value; } }

        private System.DateTime? _readDateTime;
        /// <summary>
        /// 抄表时间,重要基础数据
        /// </summary>
        public System.DateTime? readDateTime { get { return this._readDateTime; } set { this._readDateTime = value; } }

        private System.Decimal? _ocrdata;
        /// <summary>
        /// 识别出来的读数
        /// </summary>
        public System.Decimal? ocrdata { get { return this._ocrdata; } set { this._ocrdata = value; } }

        private System.String _mrreadername;
        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadername { get { return this._mrreadername; } set { this._mrreadername = value; } }

        private System.String _photourl;
        /// <summary>
        /// 服务器存储路径
        /// </summary>
        public System.String photourl { get { return this._photourl; } set { this._photourl = value; } }

        private System.String _checkor;
        /// <summary>
        /// 审核人（sys_userinfo::ID,0为系统自动）
        /// </summary>
        public System.String checkor { get { return this._checkor; } set { this._checkor = value; } }

        private System.Int32 _recheckstatus;
        /// <summary>
        /// 状态0--通过;1--不通过
        /// </summary>
        public System.Int32 recheckstatus { get { return this._recheckstatus; } set { this._recheckstatus = value; } }

        private System.DateTime _createtime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime createtime { get { return this._createtime; } set { this._createtime = value; } }

        private System.String _recheckresult;
        /// <summary>
        /// 审核原因或结果
        /// </summary>
        public System.String recheckresult { get { return this._recheckresult; } set { this._recheckresult = value; } }

        private System.String _meternum;
        /// <summary>
        /// 水表自动表号
        /// </summary>
        public System.String meternum { get { return this._meternum; } set { this._meternum = value; } }

        private System.String _regionname;
        /// <summary>
        /// 片区名称
        /// </summary>
        public System.String regionname { get { return this._regionname; } set { this._regionname = value; } }

        private System.String _areaname;
        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get { return this._areaname; } set { this._areaname = value; } }

        private System.String _address;
        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get { return this._address; } set { this._address = value; } }
    }
}
