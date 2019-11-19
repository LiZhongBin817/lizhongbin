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
        /// 更换水表时间
        /// </summary>
        public System.DateTime? updatemetertime { get; set; }

        /// <summary>
        /// 使用标记 (0:否；1:是)
        /// </summary>
        public System.SByte? delflag { get; set; }

        /// <summary>
        /// 水户自动帐号（t_b_users:: autoaccount）
        /// </summary>
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 水表型号(t_b_watermodel::bmlid)
        /// </summary>
        public System.Int16? metermodel { get; set; }

        /// <summary>
        /// 水表类型（t_b_watermetertype::bwtid）
        /// </summary>
        public System.Int16? metertype { get; set; }

        /// <summary>
        /// 生产厂商(t_b_factory::bftid)
        /// </summary>
        public System.Int16? factory { get; set; }

        /// <summary>
        /// 安装位置(t_b_installpos::bipid)
        /// </summary>
        public System.Int16? installpos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String readername { get; set; }

        /// <summary>
        /// 初始读数（初始表码）
        /// </summary>
        public System.Int32? bwcode { get; set; }

        /// <summary>
        /// 最大量程
        /// </summary>
        public System.Int32? maxrange { get; set; }

        /// <summary>
        /// 口径
        /// </summary>
        public System.Int32? caliber { get; set; }

        /// <summary>
        /// 安装地址
        /// </summary>
        public System.String adress { get; set; }

        /// <summary>
        /// 抄表册编号（t_c_readmeterbook::bookno）
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// GIS位置(190808新增)
        /// </summary>
        public System.String GISPlace { get; set; }

        /// <summary>
        /// 截止用水量
        /// </summary>
        public System.Int32? lastwaternum { get; set; }

        /// <summary>
        /// 状态(0:未使用1:正常2:暂停用水3:注销)
        /// </summary>
        public System.Int16? meterstate { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public System.String posname { get; set; }


    }
}