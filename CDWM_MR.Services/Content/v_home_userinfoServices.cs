using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_home_userinfoServices:BaseServices<v_home_userinfo>, Iv_home_userinfoServices
    {
        readonly Iv_home_userinfoRepository dal;
        public v_home_userinfoServices(Iv_home_userinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
