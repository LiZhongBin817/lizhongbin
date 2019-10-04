using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
   public partial class mr_b_bookinfoServices:BaseServices<mr_b_bookinfo>,Imr_b_bookinfoServices
    {
        private readonly Imr_b_bookinfoRepository dal;
        public mr_b_bookinfoServices(Imr_b_bookinfoRepository dal) {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
