namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_bookexcel
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_bookexcel()
        {
        }

        /// <summary>
        /// 抄表册名称
        /// </summary>
        public System.String bookname { get; set; }

        /// <summary>
        /// 用户编号（冗余）
        /// </summary>
        public System.String useraccount { get; set; }

        /// <summary>
        /// 抄表周期(11:按月 21:单月 22:双月31:按季度1 32:按季度2 33:按季度3)
        /// </summary>
        public System.Int32? readperiod { get; set; }

        /// <summary>
        /// 抄表册编号
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 关联用户数量
        /// </summary>
        public System.Int32? contectusernum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadername { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadernumber { get; set; }

        /// <summary>
        /// 表册内水表顺序
        /// </summary>
        public System.Int32? meterseq { get; set; }

        /// <summary>
        /// 水表编号
        /// </summary>
        public System.String watermeternumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String username { get; set; }

        /// <summary>
        /// 帐户余额
        /// </summary>
        public System.Decimal? balance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String telephone { get; set; }

        /// <summary>
        /// 用水性质名称
        /// </summary>
        public System.String naturename { get; set; }

        /// <summary>
        /// 口径(190808新增)
        /// </summary>
        public System.String caliber { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public System.String metertypename { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public System.String posname { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get; set; }

        /// <summary>
        /// 片区名称
        /// </summary>
        public System.String regionname { get; set; }
    }
}
