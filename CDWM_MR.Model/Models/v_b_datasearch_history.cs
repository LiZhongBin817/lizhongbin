using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_b_datasearch_history
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_b_datasearch_history()
        {
        }

        private System.Int32 _id;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get { return this._id; } set { this._id = value; } }

        private System.String _meternum;
        /// <summary>
        /// 水表编号（t_b_watermeters）
        /// </summary>
        public System.String meternum { get { return this._meternum; } set { this._meternum = value; } }

        private System.String _autoaccount;
        /// <summary>
        /// 用水账号--自动生成(t_b_users)
        /// </summary>
        public System.String autoaccount { get { return this._autoaccount; } set { this._autoaccount = value; } }

        private System.String _account;
        /// <summary>
        /// 
        /// </summary>
        public System.String account { get { return this._account; } set { this._account = value; } }

        private System.String _username;
        /// <summary>
        /// 用户姓名t_b_users
        /// </summary>
        public System.String username { get { return this._username; } set { this._username = value; } }

        private System.String _address;
        /// <summary>
        /// 家庭地址t_b_users
        /// </summary>
        public System.String address { get { return this._address; } set { this._address = value; } }

        private System.String _telephone;
        /// <summary>
        /// 联系电话t_b_users
        /// </summary>
        public System.String telephone { get { return this._telephone; } set { this._telephone = value; } }

        private System.String _meterbookname;
        /// <summary>
        /// 抄表册名称(mr_bookinfo)
        /// </summary>
        public System.String meterbookname { get { return this._meterbookname; } set { this._meterbookname = value; } }

        private System.String _meterbooknumber;
        /// <summary>
        /// 抄表册编号(mr_bookinfo)
        /// </summary>
        public System.String meterbooknumber { get { return this._meterbooknumber; } set { this._meterbooknumber = value; } }

        private System.String _mrreadernumber;
        /// <summary>
        /// 抄表员编号(mr_b_reader)
        /// </summary>
        public System.String mrreadernumber { get { return this._mrreadernumber; } set { this._mrreadernumber = value; } }

        private System.String _mrreadername;
        /// <summary>
        /// 抄表员姓名(mr_b_reader)
        /// </summary>
        public System.String mrreadername { get { return this._mrreadername; } set { this._mrreadername = value; } }

        private System.String _areano;
        /// <summary>
        /// 小区编号t_b_areas
        /// </summary>
        public System.String areano { get { return this._areano; } set { this._areano = value; } }

        private System.String _areaname;
        /// <summary>
        /// 小区名称t_b_areas
        /// </summary>
        public System.String areaname { get { return this._areaname; } set { this._areaname = value; } }

        private System.String _regionno;
        /// <summary>
        /// 片区编号t_b_regions
        /// </summary>
        public System.String regionno { get { return this._regionno; } set { this._regionno = value; } }

        private System.String _regionname;
        /// <summary>
        /// 片区名称t_b_regions
        /// </summary>
        public System.String regionname { get { return this._regionname; } set { this._regionname = value; } }

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

        private System.Decimal? _nowmonthdata;
        /// <summary>
        /// 当前月份抄表读数
        /// </summary>
        public System.Decimal? nowmonthdata { get { return this._nowmonthdata; } set { this._nowmonthdata = value; } }

        private System.Decimal? _usewaternum;
        /// <summary>
        /// 当前月份用水量
        /// </summary>
        public System.Decimal? usewaternum { get { return this._usewaternum; } set { this._usewaternum = value; } }

        private System.DateTime? _omrdatetime;
        /// <summary>
        ///  抄表详细时间
        /// </summary>
        public System.DateTime? omrdatetime { get { return this._omrdatetime; } set { this._omrdatetime = value; } }

        private System.Int32 _readstatus;
        /// <summary>
        /// 0--未抄;1--已抄回;2--已识别;3--已复审;4--已结转;5--已归档;6--其余未定义
        /// </summary>
        public System.Int32 readstatus { get { return this._readstatus; } set { this._readstatus = value; } }

        private System.DateTime _readDateTime;
        /// <summary>
        /// 抄表时间,重要基础数据
        /// </summary>
        public System.DateTime readDateTime { get { return this._readDateTime; } set { this._readDateTime = value; } }

        private System.DateTime? _uploadtime;
        /// <summary>
        /// 上传数据时间
        /// </summary>
        public System.DateTime? uploadtime { get { return this._uploadtime; } set { this._uploadtime = value; } }

        private System.Int32 _readtype;
        /// <summary>
        /// 抄表状态(0--默认;1--实抄;2--估抄;3--异常),根据gis位置进行判断
        /// </summary>
        public System.Int32 readtype { get { return this._readtype; } set { this._readtype = value; } }

        private System.String _meterstatus;
        /// <summary>
        /// 水表状态,从数据字典中读取字符型
        /// </summary>
        public System.String meterstatus { get { return this._meterstatus; } set { this._meterstatus = value; } }

        private System.Decimal? _readcheckdata;
        /// <summary>
        /// 复审读数(抄表数据审核数据)
        /// </summary>
        public System.Decimal? readcheckdata { get { return this._readcheckdata; } set { this._readcheckdata = value; } }

        private System.String _recheckresult;
        /// <summary>
        /// 抄表数据审核异常原因，冗余
        /// </summary>
        public System.String recheckresult { get { return this._recheckresult; } set { this._recheckresult = value; } }

        private System.String _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public System.String remark { get { return this._remark; } set { this._remark = value; } }
    }
}
