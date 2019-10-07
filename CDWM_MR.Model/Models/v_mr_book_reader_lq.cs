using SqlSugar;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_mr_book_reader_lq
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_mr_book_reader_lq()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 抄表册ID,来源于抄表册
        /// </summary>
        public System.Int32 bookid { get; set; }

        /// <summary>
        /// 抄表员ID
        /// </summary>
        public System.Int32 readerid { get; set; }

        /// <summary>
        /// 抄表册编号
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表周期(11:按月 21:单月 22:双月31:按季度1 32:按季度2 33:按季度3)
        /// </summary>
        public System.Int32? readperiod { get; set; }

        /// <summary>
        /// 关联用户数量
        /// </summary>
        public System.Int32? contectusernum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadernumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadername { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String appcount { get; set; }
    }
}
