using SqlSugar;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_watermeterinfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_watermeterinfo()
        {
        }

        /// <summary>
        /// 水表自动表号
        /// </summary>
        public System.String meternum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String account { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get; set; }

        /// <summary>
        /// 口径(190808新增)
        /// </summary>
        public System.String caliber { get; set; }

        /// <summary>
        /// 初始读数（初始表码）
        /// </summary>
        public System.Int32? bwcode { get; set; }

        /// <summary>
        /// 最大量程
        /// </summary>
        public System.Int32? maxrange { get; set; }

        /// <summary>
        /// 安装地址
        /// </summary>
        public System.String adress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mrreader { get; set; }

        /// <summary>
        /// 抄表册编号（t_c_readmeterbook::bookno）
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String telephone { get; set; }

        /// <summary>
        /// GIS位置(190808新增)
        /// </summary>
        public System.String GISPlace { get; set; }

        /// <summary>
        /// 截止用水量
        /// </summary>
        public System.Int32? lastwaternum { get; set; }
    }
}
