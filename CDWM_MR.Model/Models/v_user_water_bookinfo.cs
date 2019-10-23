using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace CDWM_MR.Model.Models
{


    namespace Entitys
    {
      
        public class v_user_water_bookinfo
        {
            /// <summary>
            /// VIEW
            /// </summary>

                public v_user_water_bookinfo()
                {
                }

                private System.String _autoaccount;
                /// <summary>
                /// 自动帐号(系统自动生成)(关联水表用户信息)
                /// </summary>
                public System.String autoaccount { get { return this._autoaccount; } set { this._autoaccount = value; } }

                private System.String _username;
                /// <summary>
                /// 
                /// </summary>
                public System.String username { get { return this._username; } set { this._username = value; } }

                private System.String _meternum;
                /// <summary>
                /// 水表编号,（t_b_watermeters::meternum）
                /// </summary>
                public System.String meternum { get { return this._meternum; } set { this._meternum = value; } }

                private System.String _address;
                /// <summary>
                /// 家庭住址
                /// </summary>
                public System.String address { get { return this._address; } set { this._address = value; } }

                private System.Decimal? _lastwaternum;
                /// <summary>
                /// 上月用量
                /// </summary>
                public System.Decimal? lastwaternum { get { return this._lastwaternum; } set { this._lastwaternum = value; } }

                private System.Decimal _carrywatercount;
                /// <summary>
                /// 冗余,用水量=本止-上止，本月用量
                /// </summary>
                public System.Decimal carrywatercount { get { return this._carrywatercount; } set { this._carrywatercount = value; } }

                private System.String _taskperiodname;
                /// <summary>
                /// 任务账期201909，抄表月份
                /// </summary>
                public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

                private System.Decimal _startnum;
                /// <summary>
                /// 上期止码,本期起码
                /// </summary>
                public System.Decimal startnum { get { return this._startnum; } set { this._startnum = value; } }

                private System.Decimal _endnum;
                /// <summary>
                /// 本期止码,下期起码
                /// </summary>
                public System.Decimal endnum { get { return this._endnum; } set { this._endnum = value; } }

                private System.String _readname;
                /// <summary>
                /// 抄表员姓名
                /// </summary>
                public System.String readname { get { return this._readname; } set { this._readname = value; } }
            private System.String _bookno;
            /// <summary>
            /// 抄表册编号
            /// </summary>
            public System.String bookno { get { return this._bookno; } set { this._bookno = value; } }

            private System.String _uploadgisplace;
            /// <summary>
            /// 上传的GIS信息
            /// </summary>
            public System.String uploadgisplace { get { return this._uploadgisplace; } set { this._uploadgisplace = value; } }
        }
        }
    }
    


