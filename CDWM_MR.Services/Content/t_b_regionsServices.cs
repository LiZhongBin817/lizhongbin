using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class t_b_regionsServices:BaseServices<t_b_regions>, It_b_regionsServices
    {
        private readonly It_b_regionsRepository dal;
        public t_b_regionsServices(It_b_regionsRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
