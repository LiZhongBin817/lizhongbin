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
        /// 
        /// </summary>
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 抄表册编号
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 50,ColumnDescription = "抄表册编号")]
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表人(mr_reader：ID)
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "抄表人(mr_reader：ID)")]
        public System.Int32 readmanid { get; set; }

        /// <summary>
        /// 抄表册名称
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 80,ColumnDescription = "抄表册名称")]
        public System.String bookname { get; set; }

        /// <summary>
        /// 抄表册中表具类型
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "普表、数码表、远传表、混合等，来源：数据字典")]
        public int booktype { get; set; }

        /// <summary>
        /// 抄表周期(11:按月 21:单月 22:双月31:按季度1 32:按季度2 33:按季度3)
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "抄表周期(11:按月 21:单月 22:双月31:按季度1 32:按季度2 33:按季度3)")]
        public System.Int32? readperiod { get; set; }

        /// <summary>
        /// 区域编号(t_b_regions:regionno)
        /// </summary>
        public System.String regionno { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime createtime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public System.String createpeople { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? updatetime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public System.String updatepeople { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get; set; }

 

        /// <summary>
        /// 分配状态(0--已分配;1--未分配)
        /// </summary>
        public System.Int32 allotstatus { get; set; }

        /// <summary>
        /// 关联用户数量
        /// </summary>
        public System.Int32? contectusernum { get; set; }

    }
}
