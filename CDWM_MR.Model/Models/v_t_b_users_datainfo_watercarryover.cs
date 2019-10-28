using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class v_t_b_users_datainfo_watercarryover
    {
        /// <summary>
        /// 应抄明细查询表
        /// </summary>
        public v_t_b_users_datainfo_watercarryover()
        {
        }

        private System.String _autoaccount;
        /// <summary>
        /// 自动帐号(系统自动生成)
        /// </summary>
        public System.String autoaccount { get { return this._autoaccount; } set { this._autoaccount = value; } }

        private System.String _account;
        /// <summary>
        /// 户号
        /// </summary>
        public System.String account { get { return this._account; } set { this._account = value; } }

        private System.String _username;
        /// <summary>
        /// 用户名字
        /// </summary>
        public System.String username { get { return this._username; } set { this._username = value; } }

        private System.String _areaname;
        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get { return this._areaname; } set { this._areaname = value; } }

        private System.String _regionname;
        /// <summary>
        /// 片区名称
        /// </summary>
        public System.String regionname { get { return this._regionname; } set { this._regionname = value; } }

        private System.String _address;
        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get { return this._address; } set { this._address = value; } }

        private System.String _bookno;
        /// <summary>
        /// 抄表册编号（t_c_readmeterbook::bookno）
        /// </summary>
        public System.String bookno { get { return this._bookno; } set { this._bookno = value; } }

        private System.String _mrreadername;
        /// <summary>
        /// 抄表员名字
        /// </summary>     
        public System.String mrreadername { get { return this._mrreadername; } set { this._mrreadername = value; } }

        private System.String _bookname;
        /// <summary>
        /// 抄表册名称
        /// </summary>
        public System.String bookname { get { return this._bookname; } set { this._bookname = value; } }

        private System.Decimal? _startnum;
        /// <summary>
        /// 上期止码,本期起码
        /// </summary>
        public System.Decimal? startnum { get { return this._startnum; } set { this._startnum = value; } }

        private System.Decimal? _inputdata;
        /// <summary>
        /// 人为抄表数据
        /// </summary>
        public System.Decimal? inputdata { get { return this._inputdata; } set { this._inputdata = value; } }

        private System.Int32? _readtype;
        /// <summary>
        /// 抄表状态(0--默认;1--实抄;2--估抄;3--异常),根据gis位置进行判断
        /// </summary>
        public System.Int32? readtype { get { return this._readtype; } set { this._readtype = value; } }

        private System.Int32? _carrystatus;
        /// <summary>
        /// 状态0--未结转;1--正常;2--异常
        /// </summary>
        public System.Int32? carrystatus { get { return this._carrystatus; } set { this._carrystatus = value; } }
    }
}
