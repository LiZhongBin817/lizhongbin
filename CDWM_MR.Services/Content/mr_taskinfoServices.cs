using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
   public partial class mr_taskinfoServices:BaseServices<mr_taskinfo>,Imr_taskinfoServices
    {
        private readonly Imr_taskinfoRepository dal;
        public mr_taskinfoServices(Imr_taskinfoRepository dal) {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
