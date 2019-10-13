using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_b_datasearch_historyServices : BaseServices<v_b_datasearch_history>, Iv_b_datasearch_historyServices
    {
        private readonly Iv_b_datasearch_historyRepository dal;
        public v_b_datasearch_historyServices(Iv_b_datasearch_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
