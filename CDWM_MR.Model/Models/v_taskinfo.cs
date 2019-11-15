namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_taskinfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_taskinfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 taskid { get; set; }

        /// <summary>
        /// 抄表册ID,来源于mr_b_bookinfo::id
        /// </summary>
        public System.Int32 bookid { get; set; }

        /// <summary>
        /// 下载状态0--已下载;1--未下载
        /// </summary>
        public System.Int16? dowloadstatus { get; set; }

        /// <summary>
        /// 下载任务单结束时间
        /// </summary>
        public System.DateTime? downloadendtime { get; set; }

        /// <summary>
        /// 下载任务单开始时间
        /// </summary>
        public System.DateTime? downloadstarttime { get; set; }

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
        /// 抄表员ID,mr_b_reader::id
        /// </summary>
        public System.Int32 readerid { get; set; }

        /// <summary>
        /// 抄表任务单编号
        /// </summary>
        public System.String tasknumber { get; set; }

        /// <summary>
        /// 抄表任务单名称
        /// </summary>
        public System.String taskname { get; set; }

        /// <summary>
        /// 任务单完成状态0--计划;1--下达;2--完成
        /// </summary>
        public System.Int32? taskstatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public System.String CreatePeople { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get; set; }

        /// <summary>
        /// 抄表册编号
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表册名称
        /// </summary>
        public System.String bookname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String regionname { get; set; }

        /// <summary>
        /// 关联用户数量
        /// </summary>
        public System.Int32? contectusernum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadernumber { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string appcount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadername { get; set; }

        /// <summary>
        /// 所属月份
        /// </summary>
        public System.String mplanmonth { get; set; }

        /// <summary>
        /// 所属年
        /// </summary>
        public System.String mplanyear { get; set; }
        /// <summary>
        /// 抄表周期
        /// </summary>
        public int readperiod { get; set; }

        /// <summary>
        /// 任务周期
        /// </summary>
        public string taskperiodname { get; set; }

        /// <summary>
        /// 计划单编号
        /// </summary>
        public string mplannumber { get; set; }
    }
}
