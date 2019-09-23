using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
   public partial class mr_planinfoServices:BaseServices<mr_planinfo>,Imr_planinfoServices
    {
        private readonly Imr_planinfoRepository dal;

        public mr_planinfoServices(Imr_planinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
