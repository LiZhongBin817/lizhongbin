namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_b_bookuserinfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_b_bookuserinfo()
        {
        }

        /// <summary>
        /// 抄表册ID,来源于mr_bookinfo
        /// </summary>
        public System.Int32 bookid { get; set; }

        /// <summary>
        /// 水表编号
        /// </summary>
        public System.String watermeternumber { get; set; }

        /// <summary>
        /// 用户编号（冗余）
        /// </summary>
        public System.String useraccount { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public System.String username { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get; set; }

        /// <summary>
        /// 口径(190808新增)
        /// </summary>
        public System.String caliber { get; set; }

        /// <summary>
        /// 所属小区(t_b_areas::areano)
        /// </summary>
        public System.String areano { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public System.String regionname { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public System.String telephone { get; set; }

        /// <summary>
        /// 区域编号
        /// </summary>
        public string regionno { get; set; }
    }
}
