	//----------v_union_datainfoocrlog_datainfohistoryocrloghistory开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_union_datainfoocrlog_datainfohistoryocrloghistoryServices
	/// </summary>	
	public partial class v_union_datainfoocrlog_datainfohistoryocrloghistoryServices : BaseServices<v_union_datainfoocrlog_datainfohistoryocrloghistory>, Iv_union_datainfoocrlog_datainfohistoryocrloghistoryServices
    {
	
        private readonly Iv_union_datainfoocrlog_datainfohistoryocrloghistoryRepository dal;
        public v_union_datainfoocrlog_datainfohistoryocrloghistoryServices(Iv_union_datainfoocrlog_datainfohistoryocrloghistoryRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_union_datainfoocrlog_datainfohistoryocrloghistory结束----------

	