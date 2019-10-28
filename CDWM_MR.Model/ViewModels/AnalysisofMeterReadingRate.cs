using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AnalysisofMeterReadingRate
    {
        /// <summary>
        /// 
        /// </summary>
        public string mrreadername
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string taskperiodname
        {
            get;
            set;
        }

        /// <summary>
        /// 已抄
        /// </summary>
        public int drop
        {
            get;
            set;
        }
        /// <summary>
        /// 实抄
        /// </summary>
        public int copy {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int shoudcopy {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string droprate {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string copyrate
        {
            get;
            set;
        }
    }
}
