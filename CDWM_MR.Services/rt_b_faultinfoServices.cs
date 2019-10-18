using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services
{
    public partial class rt_b_faultinfoServices:BaseServices<rt_b_faultinfo>, Irt_b_faultinfoServices
    {
        private readonly Irt_b_faultinfoRepository dal;
        public rt_b_faultinfoServices(Irt_b_faultinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
