﻿namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_wateruserinfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_wateruserinfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String username { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get; set; }

        /// <summary>
        /// 水表自动表号
        /// </summary>
        public System.String meternum { get; set; }

        /// <summary>
        /// 口径(190808新增)
        /// </summary>
        public System.String caliber { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public System.String optname { get; set; }

        /// <summary>
        /// 抄表册编号（t_c_readmeterbook::bookno）
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String regionplace { get; set; }

        /// <summary>
        /// 用水性质名称
        /// </summary>
        public System.String naturename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String telephone { get; set; }

        /// <summary>
        /// GIS位置(190808新增)
        /// </summary>
        public System.String GISPlace { get; set; }
    }
}
