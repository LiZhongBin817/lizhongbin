using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_home_userinfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_home_userinfo()
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

        private System.String _telephone;
        /// <summary>
        /// 
        /// </summary>
        public System.String telephone { get { return this._telephone; } set { this._telephone = value; } }

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

        private System.String _meternum;
        /// <summary>
        /// 水表自动表号
        /// </summary>
        public System.String meternum { get { return this._meternum; } set { this._meternum = value; } }

        private System.String _caliber;
        /// <summary>
        /// 口径(190808新增)
        /// </summary>
        public System.String caliber { get { return this._caliber; } set { this._caliber = value; } }

        private System.Int32? _bwcode;
        /// <summary>
        /// 初始读数（初始表码）
        /// </summary>
        public System.Int32? bwcode { get { return this._bwcode; } set { this._bwcode = value; } }

        private System.String _posname;
        /// <summary>
        /// 安装位置(t_b_installpos::bipid)
        /// </summary>
        public System.String posname { get { return this._posname; } set { this._posname = value; } }

        private System.String _GISPlace;
        /// <summary>
        /// GIS位置(190808新增)
        /// </summary>
        public System.String GISPlace { get { return this._GISPlace; } set { this._GISPlace = value; } }

        private System.Int32? _lastwaternum;
        /// <summary>
        /// 截止用水量
        /// </summary>
        public System.Int32? lastwaternum { get { return this._lastwaternum; } set { this._lastwaternum = value; } }

        private System.Int16? _meterstate;
        /// <summary>
        /// 状态(0:未使用1:正常2:暂停用水3:注销)
        /// </summary>
        public System.Int16? meterstate { get { return this._meterstate; } set { this._meterstate = value; } }

        private System.String _bookno;
        /// <summary>
        /// 抄表册编号
        /// </summary>
        public System.String bookno { get { return this._bookno; } set { this._bookno = value; } }

        private System.String _bookname;
        /// <summary>
        /// 抄表册名称
        /// </summary>
        public System.String bookname { get { return this._bookname; } set { this._bookname = value; } }

        private System.String _mrreadernumber;
        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadernumber { get { return this._mrreadernumber; } set { this._mrreadernumber = value; } }

        private System.String _mrreadername;
        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadername { get { return this._mrreadername; } set { this._mrreadername = value; } }

        private System.Int32? _maxrange;
        /// <summary>
        /// 最大量程
        /// </summary>
        public System.Int32? maxrange { get { return this._maxrange; } set { this._maxrange = value; } }
    }

}
