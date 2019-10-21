using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表数据历史表
    /// </summary>
    public class mr_datainfo_history
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public int id { get; set; }

        /// <summary>
        /// 水表编号（t_b_watermeters）
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10,ColumnDescription = "水表编号（t_b_watermeters）")]
        public string meternum { get; set; }

        /// <summary>
        /// 用水账号--自动生成(t_b_users)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 15,ColumnDescription = "用水账号--自动生成(t_b_users)")]
        public string autoaccount { get; set; }

        /// <summary>
        /// 用户姓名t_b_users
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200,ColumnDescription = "用户姓名t_b_users")]
        public string username { get; set; }

        /// <summary>
        /// 家庭地址t_b_users
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 100,ColumnDescription = "家庭地址t_b_users")]
        public string address { get; set; }

        /// <summary>
        /// 联系电话t_b_users
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50,ColumnDescription = "联系电话t_b_users")]
        public string telephone { get; set; }

        /// <summary>
        /// 抄表册名称(mr_bookinfo)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 80,ColumnDescription = "抄表册名称(mr_bookinfo)")]
        public string meterbookname { get; set; }

        /// <summary>
        /// 抄表册编号(mr_bookinfo)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50,ColumnDescription = "抄表册编号(mr_bookinfo)")]
        public string meterbooknumber { get; set; }

        /// <summary>
        /// 抄表员编号(mr_b_reader)
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50,ColumnDescription = "抄表员编号(mr_b_reader)")]
        public string mrreadernumber { get; set; }

        /// <summary>
        /// 抄表员姓名(mr_b_reader)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50,ColumnDescription = "抄表员姓名(mr_b_reader)")]
        public string mrreadername { get; set; }

        /// <summary>
        /// 小区编号t_b_areas
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10,ColumnDescription = "小区编号t_b_areas")]
        public System.String areano { get; set; }

        /// <summary>
        /// 小区名称t_b_areas
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50,ColumnDescription = "小区名称t_b_areas")]
        public System.String areaname { get; set; }

        /// <summary>
        /// 片区编号t_b_regions
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10,ColumnDescription = "片区编号t_b_regions")]
        public System.String regionno { get; set; }

        /// <summary>
        /// 片区名称t_b_regions
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50,ColumnDescription = "片区名称t_b_regions")]
        public System.String regionname { get; set; }

        /// <summary>
        /// 抄表月份(年+月格式201908)
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10,ColumnDescription = "抄表月份(年+月格式201908)")]
        public string taskperiodname { get; set; }

        /// <summary>
        /// 上月抄表读数
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10,ColumnDescription = "上月抄表读数")]
        public decimal lastmonthdata { get; set; }

        /// <summary>
        /// 人为抄表数据
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10, ColumnDescription = "人为抄表数据")]
        public decimal inputdata { get; set; }

        /// <summary>
        /// 当前月份用水量
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10,ColumnDescription = "当前月份用水量")]
        public decimal usewaternum { get; set; }

        /// <summary>
        /// 抄表详细时间
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = " 抄表详细时间")]
        public DateTime omrdatetime { get; set; }

        /// <summary>
        /// 抄表时间
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "抄表时间,重要基础数据")]
        public DateTime readDateTime { get; set; }

        /// <summary>
        /// 数据上传时间
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "上传数据时间")]
        public DateTime uploadtime { get; set; }

        /// <summary>
        /// 抄表状态(0--默认;1--实抄;2--估抄;3--异常)
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "抄表状态(0--默认;1--实抄;2--估抄;3--异常),根据gis位置进行判断")]
        public int readtype { get; set; } = 0;

        /// <summary>
        /// 水表状态,其余从数据字典中读取字符型
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50,ColumnDescription = "水表状态,从数据字典中读取字符型")]
        public string meterstatus { get; set; }

        /// <summary>
        /// 复审读数(抄表数据审核数据)
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10, ColumnDescription = "复审读数(抄表数据审核数据)")]
        public decimal readcheckdata { get; set; }

        /// <summary>
        /// 抄表数据审核异常原因--冗余
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100, ColumnDescription = "抄表数据审核异常原因，冗余")]
        public string recheckresult { get; set; }

        /// <summary>
        /// 0--未抄;1--已抄回;2--已识别;3--已复审;4--已结转;5--已归档;6--其余未定义
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "0--未抄;1--已抄回;2--已识别;3--已复审;4--已结转;5--已归档;6--其余未定义")]
        public int readstatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 500, ColumnDescription = "备注")]
        public string remark { get; set; }
    }
}
