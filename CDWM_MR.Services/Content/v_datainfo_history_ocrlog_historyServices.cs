	//----------v_datainfo_history_ocrlog_history开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_datainfo_history_ocrlog_historyServices
	/// </summary>	
	public partial class v_datainfo_history_ocrlog_historyServices : BaseServices<v_datainfo_history_ocrlog_history>, Iv_datainfo_history_ocrlog_historyServices
    {
	
        private readonly Iv_datainfo_history_ocrlog_historyRepository dal;
        public v_datainfo_history_ocrlog_historyServices(Iv_datainfo_history_ocrlog_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_datainfo_history_ocrlog_history结束----------

	