using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_b_areas_regionServices : BaseServices<v_b_areas_region>, Iv_b_areas_regionServices
    {
        private readonly Iv_b_areas_regionRepository dal;
        public v_b_areas_regionServices(Iv_b_areas_regionRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
