namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_b_bookinfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_b_bookinfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 抄表人(mr_reader：ID)
        /// </summary>
        public System.Int32? readmanid { get; set; }

        /// <summary>
        /// 抄表册编号
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表册名称
        /// </summary>
        public System.String bookname { get; set; }

        /// <summary>
        /// 普表、数码表、远传表、混合等，来源：数据字典
        /// </summary>
        public System.Int32? booktype { get; set; }

        /// <summary>
        /// 关联用户数量
        /// </summary>
        public System.Int32 contectusernum { get; set; }

        /// <summary>
        /// 抄表员姓名
        /// </summary>
        public System.String mrreadername { get; set; }

        /// <summary>
        /// 抄表员编号
        /// </summary>
        public System.String mrreadernumber { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public System.String regionname { get; set; }

        /// <summary>
        /// 抄表册类型名称
        /// </summary>
        public System.String booktypename { get; set; }
    }
}
