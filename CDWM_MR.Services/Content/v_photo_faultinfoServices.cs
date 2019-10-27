using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_photo_faultinfoServices:BaseServices<v_photo_faultinfo>, Iv_photo_faultinfoServices
    {
        private readonly Iv_photo_faultinfoRepository dal;
        public v_photo_faultinfoServices(Iv_photo_faultinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
