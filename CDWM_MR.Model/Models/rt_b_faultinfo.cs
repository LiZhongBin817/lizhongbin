using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 派工故障信息管理
    /// </summary>
    public class rt_b_faultinfo
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// 抄表数据id
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "抄表数据id")]
        public int readdataid { get; set; }

        /// <summary>
        /// 故障编号（自动生成）
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 20,ColumnDescription = "故障编号（自动生成）")]
        public string faultnumber { get; set; }

        /// <summary>
        /// 故障类型sys_config
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "故障类型sys_config")]
        public int faulttype { get; set; }

        /// <summary>
        /// 水表编号(t_b_watermeters)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10,ColumnDescription = "水表编号(t_b_watermeters)")]
        public string meternum { get; set; }

        /// <summary>
        /// 任务账期(201909)
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 10,ColumnDescription = "任务账期(201909)")]
        public string taskperiodname { get; set; }

        /// <summary>
        /// 用户账号信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 15,ColumnDescription = "用户账号信息")]
        public string autoaccount { get; set; }

        /// <summary>
        /// 故障信息内容
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200,ColumnDescription = "故障信息内容")]
        public string faultcontent { get; set; }

        /// <summary>
        /// 上报时间,故障工单上传时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "上报时间,故障工单上传时间")]
        public DateTime reporttime { get; set; }

        /// <summary>
        /// gis位置信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100,ColumnDescription = "gis位置信息")]
        public string gisinfo { get; set; }

        /// <summary>
        /// 水表状态0--正常(默认)其他状态来源于sys_config
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "水表状态0--正常(默认)其他状态来源于sys_config")]
        public int meterstatus { get; set; }

        /// <summary>
        /// 抄表员ID
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "抄表员ID")]
        public int readerid { get; set; }

        /// <summary>
        /// 上报人
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 30,ColumnDescription = "上报人")]
        public string reportpeople { get; set; }

        /// <summary>
        /// 处理状态0--未受理;1--已受理;2--已处理;3--已存档(已审核)
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "处理状态0--未受理;1--已受理;2--已处理;3--已存档(已审核)")]
        public int faultstatus { get; set; }

        public static implicit operator rt_b_faultinfo(List<rt_b_faultinfo> v)
        {
            throw new NotImplementedException();
        }
    }
}
