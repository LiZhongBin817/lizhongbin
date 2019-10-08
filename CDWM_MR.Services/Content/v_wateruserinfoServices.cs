using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_wateruserinfoServices:BaseServices<v_wateruserinfo>, Iv_wateruserinfoServices
    {
        private readonly Iv_wateruserinfoRepository Dal;
        public v_wateruserinfoServices(Iv_wateruserinfoRepository Dal)
        {
            this.Dal = Dal;
            this.BaseDal = Dal;
        }
    }
}
