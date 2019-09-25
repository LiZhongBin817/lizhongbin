using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.ViewModels
{
    public class MeterReadingPlan
    {
        //MRID = c.ID,
        //        MRNumber = c.tasknumber,
        //        MRName = c.taskname,
        //        MRPeople =,
        //        MRStartTime = c.taskstarttime,
        //        MREndTime = c.taskendtime,
        //        MRTaskStatus=c.taskstatus
        //序号
        public int ID{
            get;
            set;
        }
        //抄表册编号
        public string Number {
            get;
            set;
        }
        //抄表册名称
        public string Name {
            get;
            set;
        }
        //抄表员
        public string People {
            get;
            set;
        }
        //起抄时间
        public DateTime StartTime {
            get;
            set;
        }
        //止抄时间
        public DateTime EndTime {
            get;
            set;
        }
        //任务单完成状态0--计划;1--下达;2--完成
        public int Status {
            get;
            set;
        }
    }
}
