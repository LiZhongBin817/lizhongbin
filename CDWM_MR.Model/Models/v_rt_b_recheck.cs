using SqlSugar;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_rt_b_recheck
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_rt_b_recheck()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get; set; }

        /// <summary>
        /// 抄表数据id
        /// </summary>
        public System.Int32 readdataid { get; set; }

        /// <summary>
        /// 抄表员id（mr_b_reader：:id）
        /// </summary>
        public System.Int32? readerid { get; set; }

        /// <summary>
        /// 任务账期201909
        /// </summary>
        public System.String taskperiodname { get; set; }

        /// <summary>
        /// 抄表数据审核数据
        /// </summary>
        public System.Decimal? recheckdata { get; set; }
    }
}
