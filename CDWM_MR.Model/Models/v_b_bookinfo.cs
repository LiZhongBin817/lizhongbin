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
        public System.Int32? ID { get; set; }
        /// <summary>
        /// 抄表人(mr_reader：ID)
        /// </summary>
        public System.String readmanid { get; set; }
        /// <summary>
        /// 抄表册编号
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表册名称
        /// </summary>
        public System.String bookname { get; set; }

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
        /// 
        /// </summary>
        public System.String regionname { get; set; }
    }
}
