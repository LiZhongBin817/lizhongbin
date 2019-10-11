using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.BASE;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class rt_b_faultinfoServices:BaseServices<rt_b_faultinfo>, Irt_b_faultinfoServices
    {
        private readonly Irt_b_faultinfoRepository Dal;
        public rt_b_faultinfoServices(Irt_b_faultinfoRepository Dal)
        {
            this.Dal = Dal;
            base.BaseDal = Dal;
        }
    }
}
