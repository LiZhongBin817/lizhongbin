using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model
{
    public class AnalysisofMeterReadingRate
    {
        public string mrreadername
        {
            get;
            set;
        }
        public string taskperiodname
        {
            get;
            set;
        }
        
       //已抄
        public int drop
        {
            get;
            set;
        }
        //实抄
        public int copy {
            get;
            set;
        }
        public int shoudcopy {
            get;
            set;
        }
        public string droprate {
            get;
            set;
        }
        public string copyrate
        {
            get;
            set;
        }
    }
}
