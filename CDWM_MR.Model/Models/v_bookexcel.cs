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
        /// 抄表册编号
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String username { get; set; }

        /// <summary>
        /// 水表编号
        /// </summary>
        public System.String watermeternumber { get; set; }

        /// <summary>
        /// 用户编号（冗余）
        /// </summary>
        public System.String useraccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? lastreadtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? lastendnum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? avenum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? lastwaternum { get; set; }

        /// <summary>
        /// 帐户余额
        /// </summary>
        public System.Decimal? balance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String telephone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String regionname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String areaname { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get; set; }

        /// <summary>
        /// 用水性质名称
        /// </summary>
        public System.String naturename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadername { get; set; }

        /// <summary>
        /// 表册内水表顺序
        /// </summary>
        public System.Int32? meterseq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String readinfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String metertypename { get; set; }

        /// <summary>
        /// 安装地址
        /// </summary>
        public System.String installaddress { get; set; }

        /// <summary>
        /// GIS位置(190808新增)
        /// </summary>
        public System.String GISPlace { get; set; }

        /// <summary>
        /// 口径(190808新增)
        /// </summary>
        public System.String caliber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String lastmeterstatus { get; set; }
    }
}
