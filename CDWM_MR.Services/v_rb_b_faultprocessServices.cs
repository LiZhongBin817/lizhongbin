using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services
{
   public partial class v_rb_b_faultprocessServices:BaseServices<v_rb_b_faultprocess>, Iv_rb_b_faultprocessServices
    {
        private readonly Iv_rb_b_faultprocessRepository dal;
        public v_rb_b_faultprocessServices(Iv_rb_b_faultprocessRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
