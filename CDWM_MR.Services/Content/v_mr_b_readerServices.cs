using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_mr_b_readerServices : BaseServices<v_mr_b_reader>, Iv_mr_b_readerServices
    {
        private readonly Iv_mr_b_readerRepository dal;
        public v_mr_b_readerServices(Iv_mr_b_readerRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
