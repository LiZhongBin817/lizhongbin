using SqlSugar;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class mr_taskinfo
    {
        /// <summary>
        /// 
        /// </summary>
        public mr_taskinfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 抄表任务单编号
        /// </summary>
        public System.String tasknumber { get; set; }

        /// <summary>
        /// 抄表任务单名称
        /// </summary>
        public System.String taskname { get; set; }

        /// <summary>
        /// 抄表册ID,来源于mr_b_bookinfo::id
        /// </summary>
        public System.Int32 bookid { get; set; }

        /// <summary>
        /// 抄表员ID,mr_b_reader::id
        /// </summary>
        public System.Int32 readerid { get; set; }

        /// <summary>
        /// 计划单ID(mr_planinfo::id)
        /// </summary>
        public System.Int32 planid { get; set; }

        /// <summary>
        /// 任务单起抄时间
        /// </summary>
        public System.DateTime? taskstarttime { get; set; }

        /// <summary>
        /// 任务单止抄时间
        /// </summary>
        public System.DateTime? taskendtime { get; set; }

        /// <summary>
        /// 下载任务单开始时间
        /// </summary>
        public System.DateTime? downloadstarttime { get; set; }

        /// <summary>
        /// 下载任务单结束时间
        /// </summary>
        public System.DateTime? downloadendtime { get; set; }

        /// <summary>
        /// 任务单完成状态0--计划;1--下达;2--完成
        /// </summary>
        public System.Int32? taskstatus { get; set; }

        /// <summary>
        /// 下载状态0--已下载;1--未下载
        /// </summary>
        public System.Int16? dowloadstatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime createtime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public System.String createpeople { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? updatetime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public System.String updatepeople { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get; set; }

        /// <summary>
        /// 任务账期 201909 冗余
        /// </summary>
        public System.String taskperiodname { get; set; }
    }
}
