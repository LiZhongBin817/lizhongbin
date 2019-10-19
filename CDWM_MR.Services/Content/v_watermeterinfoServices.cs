using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_watermeterinfoServices:BaseServices<v_watermeterinfo>, Iv_watermeterinfoServices
    {
        private readonly Iv_watermeterinfoRepository Dal;
        public v_watermeterinfoServices(Iv_watermeterinfoRepository Dal)
        {
            this.Dal = Dal;
            this.BaseDal = Dal;
        }
    }
}
