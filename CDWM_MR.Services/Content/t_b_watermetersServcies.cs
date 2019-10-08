using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class t_b_watermetersServcies:BaseServices<t_b_watermeters>, It_b_watermetersServices
    {
        private readonly It_b_watermetersRepository Dal;
        public t_b_watermetersServcies(It_b_watermetersRepository Dal)
        {
            this.Dal = Dal;
            this.BaseDal = Dal;
        }
    }
}
