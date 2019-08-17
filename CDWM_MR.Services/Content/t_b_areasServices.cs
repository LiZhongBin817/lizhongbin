using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class t_b_areasServices : BaseServices<t_b_areas>, It_b_areasServices
    {
        private readonly It_b_areasRepository dal;
        public t_b_areasServices(It_b_areasRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
