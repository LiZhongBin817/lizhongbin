using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_mr_datainfoServices:BaseServices<v_mr_datainfo>, Iv_mr_datainfoServices
    {
        private readonly Iv_mr_datainfoRepository Dal;
        public v_mr_datainfoServices(Iv_mr_datainfoRepository Dal)
        {
            this.Dal = Dal;
            base.BaseDal = Dal;
        }
    }
}
