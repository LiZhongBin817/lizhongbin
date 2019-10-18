using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表数据信息表
    /// </summary>
    public class mr_datainfo
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 15,ColumnDescription = "自动帐号(系统自动生成)(关联水表用户信息)")]
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 任务单id(来源于mr_taskinfo)
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "任务单id(来源于mr_taskinfo)")]
        public int taskid { get; set; }

        /// <summary>
        /// 任务账期201909冗余
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "任务账期201909冗余")]
        public string taskperiodname { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 10,ColumnDescription = "水表编号,（t_b_watermeters::meternum）")]
        public string meternum { get; set; }

        /// <summary>
        /// 抄表员id（mr_b_reader：:id）
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "抄表员id（mr_b_reader：:id）")]
        public int readerid { get; set; }

        /// <summary>
        /// 人为抄表数据
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10,ColumnDescription = "人为抄表数据")]
        public decimal inputdata { get; set; }

        /// <summary>
        /// 图像识别抄表数据
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10,ColumnDescription = "图像识别抄表数据Optical Choractor Recognittion光学字符识别")]
        public decimal ocrdata { get; set; }

        /// <summary>
        /// 上传的GIS信息
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 80,ColumnDescription = "上传的GIS信息")]
        public string uploadgisplace { get; set; }

        /// <summary>
        /// 抄表时间
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "重要基础数据")]
        public DateTime readDateTime { get; set; }

        /// <summary>
        /// 数据上传时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "上传数据时间")]
        public DateTime uploadtime { get; set; }

        /// <summary>
        /// 抄表状态(0--默认;1--实抄;2--估抄;3--异常)
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "抄表状态(0--默认;1--实抄;2--估抄;3--异常),根据gis位置进行判断")]
        public int readtype { get; set; } = 0;

        /// <summary>
        /// 水表状态,其余从数据字典中读取字符型
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "水表状态,从数据字典中读取字符型")]
        public int meterstatus { get; set; }

        /// <summary>
        /// 状态0--未审;1--已审;2--异常
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "状态0--未审;1--已审;2--异常")]
        public int recheckstatus { get; set; } = 0;

        /// <summary>
        /// 复审读数(抄表数据审核数据)
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10,ColumnDescription = "复审读数(抄表数据审核数据)")]
        public decimal readcheckdata { get; set; }

        /// <summary>
        /// 抄表数据审核异常原因--冗余
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 100,ColumnDescription = "抄表数据审核异常原因，冗余")]
        public string recheckresult { get; set; }

        /// <summary>
        /// 0--未抄;1--已抄回;2--已识别;3--已复审;4--已结转;5--已归档;6--其余未定义
        /// </summary>
        [SugarColumn(IsNullable = false,ColumnDescription = "0--未抄;1--已抄回;2--已识别;3--已复审;4--已结转;5--已归档;6--其余未定义")]
        public int readstatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 500,ColumnDescription = "备注")]
        public string remark { get; set; }
    }
}
