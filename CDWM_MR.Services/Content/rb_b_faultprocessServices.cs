using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class rb_b_faultprocessServices:BaseServices<rb_b_faultprocess>,Irb_b_faultprocessServices
    {
        private readonly Irb_b_faultprocessRepository Dal;
        public rb_b_faultprocessServices(Irb_b_faultprocessRepository Dal)
        {
            this.Dal = Dal;
            base.BaseDal = Dal;
        }
    }
}
