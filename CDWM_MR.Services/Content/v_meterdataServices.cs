using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services.Content
{
    public partial class v_meterdataServices : BaseServices<v_meterdata>, Iv_meterdataServices
    {
        private readonly Iv_meterdataRepository dal;
        public v_meterdataServices(Iv_meterdataRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
