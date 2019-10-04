using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_rb_b_faultprocessServices:BaseServices<v_rb_b_faultprocess>, Iv_rb_b_faultprocessServices
    {
        private readonly Iv_rb_b_faultprocessRepository Dal;
        public v_rb_b_faultprocessServices(Iv_rb_b_faultprocessRepository Dal)
        {
            this.Dal = Dal;
            base.BaseDal = Dal;
        }
    }
}
