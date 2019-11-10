using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
   public partial class v_interfaceServices:BaseServices<v_interface>, Iv_interfaceServices
    {
        private readonly Iv_interfaceRepository dal;
        public v_interfaceServices(Iv_interfaceRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
