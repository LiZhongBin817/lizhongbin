using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class rt_b_recheckServices:BaseServices<rt_b_recheck>, Irt_b_recheckServices
    {
        private readonly Irt_b_recheckRepository Dal;
        public rt_b_recheckServices(Irt_b_recheckRepository Dal)
        {
            this.Dal = Dal;
            base.BaseDal = Dal;
        }
    }
}
