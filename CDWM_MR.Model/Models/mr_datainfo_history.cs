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
        /// 水表编号（t_b_watermeters）
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10)]
        public string meternum { get; set; }

        /// <summary>
        /// 用水账号--自动生成(t_b_users)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 15)]
        public string autoaccount { get; set; }

        /// <summary>
        /// 用户姓名t_b_users
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string username { get; set; }

        /// <summary>
        /// 家庭地址t_b_users
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 100)]
        public string address { get; set; }

        /// <summary>
        /// 联系电话t_b_users
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string telephone { get; set; }

        /// <summary>
        /// 抄表册名称(mr_bookinfo)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 80)]
        public string meterbookname { get; set; }

        /// <summary>
        /// 抄表册编号(mr_bookinfo)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string meterbooknumber { get; set; }

        /// <summary>
        /// 抄表员编号(mr_b_reader)
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50)]
        public string mrreadernumber { get; set; }

        /// <summary>
        /// 抄表员姓名(mr_b_reader)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string mrreadername { get; set; }

        /// <summary>
        /// 小区编号t_b_areas
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10)]
        public System.String areano { get; set; }

        /// <summary>
        /// 小区名称t_b_areas
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public System.String areaname { get; set; }

        /// <summary>
        /// 片区编号t_b_regions
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10)]
        public System.String regionno { get; set; }

        /// <summary>
        /// 片区名称t_b_regions
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public System.String regionname { get; set; }

        /// <summary>
        /// 抄表月份(年+月格式201908)
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10)]
        public string mrdateTime { get; set; }

        /// <summary>
        /// 上月抄表读数
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10)]
        public decimal lastmonthdata { get; set; }

        /// <summary>
        /// 当前月份抄表读数
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10)]
        public decimal nowmonthdata { get; set; }

        /// <summary>
        /// 当前月份用水量
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10)]
        public decimal usewaternum { get; set; }

        /// <summary>
        /// 抄表详细时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime mrdatetime { get; set; }

        /// <summary>
        /// 审核状态(0--已审核;1--未审核)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool checkstatus { get; set; }

        /// <summary>
        /// 抄表状态
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool readstatus { get; set; }

        /// <summary>
        /// 上传图片信息
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDataType = "text")]
        public string imginfo { get; set; }

        /// <summary>
        /// 水表状态0--正常;1--漏水
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short watermeterstatus { get; set; }

        /// <summary>
        /// 抄表方式0--实抄;1--估抄
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short mrtype { get; set; }
    }
}
