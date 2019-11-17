using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.Content;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
   public partial class v_userwatermetersinfoServices:BaseServices<v_userwatermetersinfo>, Iv_userwatermetersinfoServices
    {
        private readonly Iv_userwatermetersinfoRepository dal;
        public v_userwatermetersinfoServices(Iv_userwatermetersinfoRepository dal) {
            this.dal = dal;
            base.BaseDal = dal;

        }
    }
}
