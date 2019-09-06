using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 数据结转表
    /// </summary>
    public class rt_b_watercarryover
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 15,ColumnDescription = "自动帐号(系统自动生成)(关联水表用户信息)")]
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 任务账期201909
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 10, ColumnDescription = "任务账期201909")]
        public string taskperiodname { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10,ColumnDescription = "水表编号,（t_b_watermeters::meternum）")]
        public string meternum { get; set; }

        /// <summary>
        /// 上期止码,本期起码
        /// </summary>
        [SugarColumn(IsNullable = false,DecimalDigits = 10,ColumnDescription = "上期止码,本期起码")]
        public decimal startnum { get; set; }

        /// <summary>
        /// 上期抄表时间
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "上期抄表时间")]
        public DateTime starttime { get; set; }

        /// <summary>
        /// Readdataid(抄表数据表id)
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "Readdataid(抄表数据表id)")]
        public int startid { get; set; }

        /// <summary>
        /// 本期止码,下期起码
        /// </summary>
        [SugarColumn(IsNullable = false,DecimalDigits = 10, ColumnDescription = "本期止码,下期起码")]
        public decimal endnum { get; set; }

        /// <summary>
        /// 本期抄表时间
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "本期抄表时间")]
        public DateTime endtime { get; set; }

        /// <summary>
        /// Readdataid(抄表数据表id)
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "Readdataid(抄表数据表id)")]
        public int endid { get; set; }

        /// <summary>
        /// 冗余,用水量=上止-本止
        /// </summary>
        [SugarColumn(IsNullable = false,DecimalDigits = 10,ColumnDescription = "冗余,用水量=上止-本止")]
        public decimal carrywatercount { get; set; }

        /// <summary>
        /// 记账用量,冗余
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10,ColumnDescription = "记账用量,冗余")]
        public decimal bookkeepingcount { get; set; }

        /// <summary>
        /// 调整用量（+-带符号）
        /// </summary>
        [SugarColumn(IsNullable = false,DecimalDigits = 10,ColumnDescription = "调整用量（+-带符号）")]
        public decimal adjustwatercount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "创建时间")]
        public DateTime createtime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 30,ColumnDescription = "创建人")]
        public string createperson { get; set; }

        /// <summary>
        /// 状态0--未结转;1--正常;2--异常
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "状态0--未结转;1--正常;2--异常")]
        public int carrystatus { get; set; } = 1;

        /// <summary>
        /// 备注说明
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50,ColumnDescription = "备注说明")]
        public string remark { get; set; }

    }
}
