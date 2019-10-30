using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class v_r_datainfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_r_datainfo()
        {
        }

        private System.Int32 _ID;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ID { get { return this._ID; } set { this._ID = value; } }

        private System.Int32 _readerid;
        /// <summary>
        /// 抄表员id（mr_b_reader：:id）
        /// </summary>
        public System.Int32 readerid { get { return this._readerid; } set { this._readerid = value; } }

        private System.String _mrreadername;
        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadername { get { return this._mrreadername; } set { this._mrreadername = value; } }

        private System.Int32 _readstatus;
        /// <summary>
        /// 0--未抄;1--已抄回;2--已识别;3--已复审;4--已结转;5--已归档;6--其余未定义
        /// </summary>
        public System.Int32 readstatus { get { return this._readstatus; } set { this._readstatus = value; } }

        private System.String _taskperiodname;
        /// <summary>
        /// 任务账期201909冗余
        /// </summary>
        public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

        private System.Int32 _readtype;
        /// <summary>
        /// 抄表状态(0--默认;1--实抄;2--估抄;3--异常),根据gis位置进行判断
        /// </summary>
        public System.Int32 readtype { get { return this._readtype; } set { this._readtype = value; } }

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
        private System.DateTime? _uploadtime;
        /// <summary>
        /// 上传数据时间
        /// </summary>
        public System.DateTime? uploadtime { get { return this._uploadtime; } set { this._uploadtime = value; } }
        private System.Int32? _contectusernum;
        /// <summary>
        /// 关联用户数量
        /// </summary>
        public System.Int32? contectusernum { get { return this._contectusernum; } set { this._contectusernum = value; } }
    }


}
