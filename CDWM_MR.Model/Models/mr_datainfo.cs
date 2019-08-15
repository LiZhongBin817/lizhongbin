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
        public int ID { get; set; }

        /// <summary>
        /// 任务单id(来源于mr_taskinfo)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int taskid { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10)]
        public string meternum { get; set; }

        /// <summary>
        /// 抄表员id（mr_b_reader：:ID）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int readerid { get; set; }

        /// <summary>
        /// 人为抄表数据
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10)]
        public decimal byhanddata { get; set; }

        /// <summary>
        /// 图像识别抄表数据
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10)]
        public decimal bymachinedata { get; set; }

        /// <summary>
        /// 上传的GIS信息
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 80)]
        public string uploadgisplace { get; set; }

        /// <summary>
        /// 抄表时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime readDateTime { get; set; }

        /// <summary>
        /// 抄表状态(0--已抄表;1--未抄表)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool readstatus { get; set; }

        /// <summary>
        /// 审核状态0--已审核;1--未审核
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int checkstatus { get; set; }

        /// <summary>
        /// 审核信息
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 200)]
        public string checkinfo { get; set; }

        /// <summary>
        /// 审核成功时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime checksuccesstime { get; set; }

        /// <summary>
        /// 审核人（sys_userinfo::ID）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int checkor { get; set; }

        /// <summary>
        /// 结转状态0--已结转;1--未结转;
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int turnstatus { get; set; } = 1;

        /// <summary>
        /// 结转完成日期
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime turndate { get; set; }

        /// <summary>
        /// 复审读数
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10)]
        public decimal againcheckdata { get; set; }

        /// <summary>
        /// 上传的图片内容
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "text")]
        public string imginfo { get; set; }

        /// <summary>
        /// 最终审核水量数据(和审核数据一样)
        /// </summary>
        [SugarColumn(IsNullable = true, DecimalDigits = 10)]
        public decimal lastreadnum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 500)]
        public string remark { get; set; }
    }
}
