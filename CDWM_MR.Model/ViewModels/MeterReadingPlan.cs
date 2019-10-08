using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.ViewModels
{
    /// <summary>
    /// 抄表册计划
    /// </summary>
    public class MeterReadingPlan
    {
        //MRID = c.ID,
        //        MRNumber = c.tasknumber,
        //        MRName = c.taskname,
        //        MRPeople =,
        //        MRStartTime = c.taskstarttime,
        //        MREndTime = c.taskendtime,
        //        MRTaskStatus=c.taskstatus
        /// <summary>
        /// 序号
        /// </summary>
        public int ID{
            get;
            set;
        }

        /// <summary>
        /// 抄表册编号
        /// </summary>
        public string Number {
            get;
            set;
        }
        /// <summary>
        /// 抄表册名称
        /// </summary>
        public string Name {
            get;
            set;
        }
        /// <summary>
        /// 抄表员
        /// </summary>
        public string People {
            get;
            set;
        }
        /// <summary>
        /// 起抄时间
        /// </summary>
        public DateTime StartTime {
            get;
            set;
        }
        /// <summary>
        /// 止抄时间
        /// </summary>
        public DateTime EndTime {
            get;
            set;
        }
        /// <summary>
        /// 任务单完成状态0--计划;1--下达;2--完成
        /// </summary>
        public int Status {
            get;
            set;
        }
    }
}
