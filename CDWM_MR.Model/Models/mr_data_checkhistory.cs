using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表数据审核历史表
    /// </summary>
    public class mr_data_checkhistory
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

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
        /// 水表表号(t_b_watermeters)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10)]
        public string meternum { get; set; }

        /// <summary>
        /// 区域名称（t_b_region::regionname）
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string regionname { get; set; }

        /// <summary>
        /// 小区名称t_b_areas
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string areaname { get; set; }

        /// <summary>
        /// 月份（一个周期）年+月
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10)]
        public string yearmonth { get; set; }

        /// <summary>
        /// 抄表上传读数
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10)]
        public decimal uploaddata { get; set; }

        /// <summary>
        /// 图像识别读数
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10)]
        public decimal bymachinedadata { get; set; }

        /// <summary>
        /// 复审读数
        /// </summary>
        [SugarColumn(IsNullable = true,DecimalDigits = 10)]
        public decimal againcheckdata { get; set; }

        /// <summary>
        /// 上传的图片内容
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "text")]
        public string uploadimg { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime checkdatetime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 500)]
        public string remark { get; set; } = "系统自动";

        /// <summary>
        /// 审核状态(0--通过;1--不通过)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool checkstatus { get; set; }
    }
}
