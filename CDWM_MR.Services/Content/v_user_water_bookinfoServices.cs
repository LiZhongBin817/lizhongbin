using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using CDWM_MR.Model.Models.Entitys;

namespace CDWM_MR.Services.Content
{
    public partial class v_user_water_bookinfoServices : BaseServices<v_user_water_bookinfo>, Iv_user_water_bookinfoServices
    {

        private readonly Iv_user_water_bookinfoRepository dal;
        public v_user_water_bookinfoServices(Iv_user_water_bookinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }

    }
}

