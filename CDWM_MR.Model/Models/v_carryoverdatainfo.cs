using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    // <summary>
    /// 结转数据管理展示数据
    /// </summary>
    public class v_carryoverdatainfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_carryoverdatainfo()
        {
        }

        private System.String _autoaccount;
        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
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

        private System.String _address;
        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get { return this._address; } set { this._address = value; } }

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

        private System.String _taskperiodname;
        /// <summary>
        /// 任务账期201909
        /// </summary>
        public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

        private System.Decimal _carrywatercount;
        /// <summary>
        /// 冗余,用水量=本止-上止
        /// </summary>
        public System.Decimal carrywatercount { get { return this._carrywatercount; } set { this._carrywatercount = value; } }

        private System.Decimal? _bookkeepingcount;
        /// <summary>
        /// 记账用量,冗余
        /// </summary>
        public System.Decimal? bookkeepingcount { get { return this._bookkeepingcount; } set { this._bookkeepingcount = value; } }


        private System.String _meternum;
        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        public System.String meternum { get { return this._meternum; } set { this._meternum = value; } }

        private System.Decimal _startnum;
        /// <summary>
        /// 上期止码,本期起码
        /// </summary>
        public System.Decimal startnum { get { return this._startnum; } set { this._startnum = value; } }

        private System.DateTime _starttime;
        /// <summary>
        /// 上期抄表时间
        /// </summary>
        public System.DateTime starttime { get { return this._starttime; } set { this._starttime = value; } }

        private System.Decimal _endnum;
        /// <summary>
        /// 本期止码,下期起码
        /// </summary>
        public System.Decimal endnum { get { return this._endnum; } set { this._endnum = value; } }

        private System.DateTime _endtime;
        /// <summary>
        /// 本期抄表时间
        /// </summary>
        public System.DateTime endtime { get { return this._endtime; } set { this._endtime = value; } }

        private System.String _naturename;
        /// <summary>
        /// 用水性质名称
        /// </summary>
        public System.String naturename { get { return this._naturename; } set { this._naturename = value; } }

        private System.Decimal? _adjustwatercount;
        /// <summary>
        /// 调整用量（+ - 带符号）
        /// </summary>
        public System.Decimal? adjustwatercount { get { return this._adjustwatercount; } set { this._adjustwatercount = value; } }

        private System.String _adjustremark;
        /// <summary>
        /// 调整说明
        /// </summary>
        public System.String adjustremark { get { return this._adjustremark; } set { this._adjustremark = value; } }
    }


}
