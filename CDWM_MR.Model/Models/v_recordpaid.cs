using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    using SqlSugar;

    namespace Entitys
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public class v_recordpaid
        {
            /// <summary>
            /// VIEW
            /// </summary>
            public v_recordpaid()
            {
            }

            private System.String _payseq;
            /// <summary>
            /// 缴费流水号（t_d_payrecords:: payseq）
            /// </summary>
            public System.String payseq { get { return this._payseq; } set { this._payseq = value; } }

            private System.Decimal? _waterfee;
            /// <summary>
            /// 应收水费（按水价方案计算）
            /// </summary>
            public System.Decimal? waterfee { get { return this._waterfee; } set { this._waterfee = value; } }

            private System.String _taskperiodname;
            /// <summary>
            /// 任务账期201909
            /// </summary>
            public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

            private System.Decimal? _startnum;
            /// <summary>
            /// 上期止码,本期起码
            /// </summary>
            public System.Decimal? startnum { get { return this._startnum; } set { this._startnum = value; } }

            private System.Decimal? _endnum;
            /// <summary>
            /// 本期止码,下期起码
            /// </summary>
            public System.Decimal? endnum { get { return this._endnum; } set { this._endnum = value; } }

            private System.Decimal? _lastwaternum;
            /// <summary>
            /// 
            /// </summary>
            public System.Decimal? lastwaternum { get { return this._lastwaternum; } set { this._lastwaternum = value; } }

            private System.Decimal? _carrywatercount;
            /// <summary>
            /// 冗余,用水量=本止-上止
            /// </summary>
            public System.Decimal? carrywatercount { get { return this._carrywatercount; } set { this._carrywatercount = value; } }

            private System.String _starttime;
            /// <summary>
            /// 上期抄表时间
            /// </summary>
            public System.String starttime { get { return this._starttime; } set { this._starttime = value; } }

            private System.String _endtime;
            /// <summary>
            /// 本期抄表时间
            /// </summary>
            public System.String endtime { get { return this._endtime; } set { this._endtime = value; } }

            private System.Int32? _bmttype;
            /// <summary>
            /// 
            /// </summary>
            public System.Int32? bmttype { get { return this._bmttype; } set { this._bmttype = value; } }

            private System.Decimal? _cbalance;
            /// <summary>
            /// 
            /// </summary>
            public System.Decimal? cbalance { get { return this._cbalance; } set { this._cbalance = value; } }

            private System.String _autoaccount;
            /// <summary>
            /// 自动帐号(系统自动生成)
            /// </summary>
            public System.String autoaccount { get { return this._autoaccount; } set { this._autoaccount = value; } }

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
        }
    }

}
