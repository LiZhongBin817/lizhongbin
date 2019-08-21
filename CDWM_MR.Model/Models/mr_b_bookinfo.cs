using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表册信息
    /// </summary>
    public class mr_b_bookinfo:BaseModel
    {
        /// <summary>
        /// 抄表册编号
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 50)]
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表人(mr_reader：ID)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public System.String readmanid { get; set; }

        /// <summary>
        /// 抄表册名称
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 80)]
        public System.String bookname { get; set; }

        /// <summary>
        /// 抄表周期(11:按月 21:单月 22:双月31:按季度1 32:按季度2 33:按季度3)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public System.Int32? readperiod { get; set; }

        /// <summary>
        /// 区域编号(t_b_regions:regionno)
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 10)]
        public string regionno { get; set; }
    }
}
