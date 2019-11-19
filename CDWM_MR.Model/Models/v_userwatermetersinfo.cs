using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
   public class v_userwatermetersinfo
    {
        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
        /// </summary>
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String username { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        public System.String meternum { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get; set; }

        public static implicit operator v_userwatermetersinfo(List<v_userwatermetersinfo> v)
        {
            throw new NotImplementedException();
        }
    }
}
