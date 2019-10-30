using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class v_recheck_recheckhistory
    {
        /// <summary>
        /// 审核表，审核历史表结合表
        /// </summary>
        public v_recheck_recheckhistory()
        {
        }

        private System.Int32 _id;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get { return this._id; } set { this._id = value; } }

        private System.Int32 _readdataid;
        /// <summary>
        /// 抄表数据id
        /// </summary>
        public System.Int32 readdataid { get { return this._readdataid; } set { this._readdataid = value; } }

        private System.String _meternum;
        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        public System.String meternum { get { return this._meternum; } set { this._meternum = value; } }

        private System.String _userid;
        /// <summary>
        /// 用户id(来源于t_b_users)
        /// </summary>
        public System.String userid { get { return this._userid; } set { this._userid = value; } }

        private System.String _taskperiodname;
        /// <summary>
        /// 任务账期201909
        /// </summary>
        public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

        private System.Int32 _recheckstatus;
        /// <summary>
        /// 状态0--通过;1--不通过
        /// </summary>
        public System.Int32 recheckstatus { get { return this._recheckstatus; } set { this._recheckstatus = value; } }

        private System.Decimal? _recheckdata;
        /// <summary>
        /// 抄表数据审核数据
        /// </summary>
        public System.Decimal? recheckdata { get { return this._recheckdata; } set { this._recheckdata = value; } }

        private System.String _recheckresult;
        /// <summary>
        /// 审核原因或结果
        /// </summary>
        public System.String recheckresult { get { return this._recheckresult; } set { this._recheckresult = value; } }

        private System.DateTime? _checksuccesstime;
        /// <summary>
        /// 审核时间
        /// </summary>
        public System.DateTime? checksuccesstime { get { return this._checksuccesstime; } set { this._checksuccesstime = value; } }

        private System.String _checkor;
        /// <summary>
        /// 审核人（sys_userinfo::ID,0为系统自动）
        /// </summary>
        public System.String checkor { get { return this._checkor; } set { this._checkor = value; } }

        private System.DateTime _createtime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime createtime { get { return this._createtime; } set { this._createtime = value; } }

        private System.String _createpeople;
        /// <summary>
        /// 创建人（来源于sys_userinfo）
        /// </summary>
        public System.String createpeople { get { return this._createpeople; } set { this._createpeople = value; } }
    }
}
