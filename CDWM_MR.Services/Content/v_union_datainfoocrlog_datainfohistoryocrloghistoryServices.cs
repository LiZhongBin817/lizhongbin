using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
   public partial  class v_union_datainfoocrlog_datainfohistoryocrloghistoryServices:BaseServices<v_union_datainfoocrlog_datainfohistoryocrloghistory>, Iv_union_datainfoocrlog_datainfohistoryocrloghistoryServices
    {
        readonly Iv_union_datainfoocrlog_datainfohistoryocrloghistoryRepository dal;
        public v_union_datainfoocrlog_datainfohistoryocrloghistoryServices(Iv_union_datainfoocrlog_datainfohistoryocrloghistoryRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
