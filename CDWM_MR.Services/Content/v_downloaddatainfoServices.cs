using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_downloaddatainfoServices:BaseServices<v_downloaddatainfo>, Iv_downloaddatainfoServices
    {
        private readonly Iv_downloaddatainfoRepository dal;
        public v_downloaddatainfoServices(Iv_downloaddatainfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
