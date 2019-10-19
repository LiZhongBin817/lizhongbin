using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services
{
   public partial class rb_b_faultprocessServices:BaseServices<rb_b_faultprocess>, Irb_b_faultprocessServices
    {
        private readonly Irb_b_faultprocessRepository dal;
        public rb_b_faultprocessServices(Irb_b_faultprocessRepository dal) {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
