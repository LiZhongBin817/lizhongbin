using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 任务单视图
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
        /// 
        /// </summary>
        public System.Int32? bookid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int16? dowloadstatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? downloadendtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? downloadstarttime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? planid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? taskendtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? readerid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String tasknumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String taskname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? taskstatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CreatePeople { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String bookname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String regionname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String contectusernum { get; set; }

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
        public System.String mplanmonth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mplanyear { get; set; }
    }
}
