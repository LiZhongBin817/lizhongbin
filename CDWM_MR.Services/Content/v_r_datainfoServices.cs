using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services.Content
{
    public partial class v_r_datainfoServices : BaseServices<v_r_datainfo>, Iv_r_datainfoServices
    {
        private readonly Iv_r_datainfoRepository dal;
        public v_r_datainfoServices(Iv_r_datainfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
