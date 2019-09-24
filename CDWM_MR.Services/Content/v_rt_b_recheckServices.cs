using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_rt_b_recheckServices:BaseServices<v_rt_b_recheck>, Iv_rt_b_recheckServices
    {
        private Iv_rt_b_recheckRepository Dal;
        public v_rt_b_recheckServices(Iv_rt_b_recheckRepository Dal)
        {
            this.Dal = Dal;
            base.BaseDal = Dal;
        }
    }
}
