using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class v_mr_datainfo
    {
        /// <summary>
        /// 抄表数据展示表
        /// </summary>
        public v_mr_datainfo()
        {
        }

        private System.Int32 _ID;
        /// <summary>
        /// 主键
        /// </summary>
        public System.Int32 ID { get { return this._ID; } set { this._ID = value; } }

        private System.String _autoaccount;
        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
        /// </summary>
        public System.String autoaccount { get { return this._autoaccount; } set { this._autoaccount = value; } }

        private System.String _meternum;
        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        public System.String meternum { get { return this._meternum; } set { this._meternum = value; } }

        private System.Int32 _readerid;
        /// <summary>
        /// 抄表员id（mr_b_reader：:id）
        /// </summary>
        public System.Int32 readerid { get { return this._readerid; } set { this._readerid = value; } }

        private System.Decimal? _inputdata;
        /// <summary>
        /// 人为抄表数据
        /// </summary>
        public System.Decimal? inputdata { get { return this._inputdata; } set { this._inputdata = value; } }

        private System.Decimal? _ocrdata;
        /// <summary>
        /// 图像识别抄表数据Optical Choractor Recognittion光学字符识别
        /// </summary>
        public System.Decimal? ocrdata { get { return this._ocrdata; } set { this._ocrdata = value; } }

        private System.DateTime? _uploadtime;
        /// <summary>
        /// 上传数据时间
        /// </summary>
        public System.DateTime? uploadtime { get { return this._uploadtime; } set { this._uploadtime = value; } }

        private System.String _uploadgisplace;
        /// <summary>
        /// 上传的GIS信息
        /// </summary>
        public System.String uploadgisplace { get { return this._uploadgisplace; } set { this._uploadgisplace = value; } }

        private System.Int32 _readtype;
        /// <summary>
        /// 抄表状态(0--默认;1--实抄;2--估抄;3--异常),根据gis位置进行判断
        /// </summary>
        public System.Int32 readtype { get { return this._readtype; } set { this._readtype = value; } }

        private System.Int32 _taskid;
        /// <summary>
        /// 任务单id(来源于mr_taskinfo)
        /// </summary>
        public System.Int32 taskid { get { return this._taskid; } set { this._taskid = value; } }

        private System.Int32? _meterstatus;
        /// <summary>
        /// 水表状态,从数据字典中读取字符型
        /// </summary>
        public System.Int32? meterstatus { get { return this._meterstatus; } set { this._meterstatus = value; } }

        private System.Int32 _recheckstatus;
        /// <summary>
        /// 状态0--未审;1--已审;2--异常
        /// </summary>
        public System.Int32 recheckstatus { get { return this._recheckstatus; } set { this._recheckstatus = value; } }

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

        private System.String _taskperiodname;
        /// <summary>
        /// 任务账期201909冗余
        /// </summary>
        public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

        private System.Int32 _readstatus;
        /// <summary>
        /// 0--未抄;1--已抄回;2--已识别;3--已复审;4--已结转;5--已归档;6--其余未定义
        /// </summary>
        public System.Int32 readstatus { get { return this._readstatus; } set { this._readstatus = value; } }

        private System.String _account;
        /// <summary>
        /// 用户号
        /// </summary>
        public System.String account { get { return this._account; } set { this._account = value; } }

        private System.String _username;
        /// <summary>
        /// 用户名
        /// </summary>
        public System.String username { get { return this._username; } set { this._username = value; } }

        private System.String _telephone;
        /// <summary>
        /// 电话号码
        /// </summary>
        public System.String telephone { get { return this._telephone; } set { this._telephone = value; } }

        private System.String _address;
        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get { return this._address; } set { this._address = value; } }

        private System.String _areano;
        /// <summary>
        /// 所属小区(t_b_areas::areano)
        /// </summary>
        public System.String areano { get { return this._areano; } set { this._areano = value; } }

        private System.String _buildno;
        /// <summary>
        /// 楼栋号
        /// </summary>
        public System.String buildno { get { return this._buildno; } set { this._buildno = value; } }

        private System.String _metername;
        /// <summary>
        /// 水表名称
        /// </summary>
        public System.String metername { get { return this._metername; } set { this._metername = value; } }

        private System.String _bookno;
        /// <summary>
        /// 抄表册编号（t_c_readmeterbook::bookno）
        /// </summary>
        public System.String bookno { get { return this._bookno; } set { this._bookno = value; } }

        private System.String _bookname;
        /// <summary>
        /// 抄表册名称
        /// </summary>
        public System.String bookname { get { return this._bookname; } set { this._bookname = value; } }

        private System.String _mrreadername;
        /// <summary>
        /// 抄表员名字
        /// </summary>
        public System.String mrreadername { get { return this._mrreadername; } set { this._mrreadername = value; } }

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
        private System.DateTime? _checksuccesstime;
        /// <summary>
        /// 审核时间
        /// </summary>
        public System.DateTime? checksuccesstime { get { return this._checksuccesstime; } set { this._checksuccesstime = value; } }

        private System.DateTime? _checktime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? checktime { get { return this._checktime; } set { this._checktime = value; } }

        private System.String _checkor;
        /// <summary>
        /// 审核人（sys_userinfo::ID,0为系统自动）
        /// </summary>
        public System.String checkor { get { return this._checkor; } set { this._checkor = value; } }


        private System.Int32? _rtrecheckstatus;
        /// <summary>
        /// 状态0--通过;1--不通过
        /// </summary>
        public System.Int32? rtrecheckstatus { get { return this._rtrecheckstatus; } set { this._rtrecheckstatus = value; } }

        private System.Int32? _carrystatus;
        /// <summary>
        /// 状态0--未结转;1--正常;2--异常
        /// </summary>
        public System.Int32? carrystatus { get { return this._carrystatus; } set { this._carrystatus = value; } }

        private System.DateTime? _carryime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? carryime { get { return this._carryime; } set { this._carryime = value; } }

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
    }
}



