using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services
{
   public partial class rt_b_photoattachmentServices:BaseServices<rt_b_photoattachment>, Irt_b_photoattachmentServices
    {
        private readonly Irt_b_photoattachmentRepository dal;
        public rt_b_photoattachmentServices(Irt_b_photoattachmentRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
