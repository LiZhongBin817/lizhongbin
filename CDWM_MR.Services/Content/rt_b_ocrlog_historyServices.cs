	//----------rt_b_ocrlog_history开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rt_b_ocrlog_historyServices
	/// </summary>	
	public partial class rt_b_ocrlog_historyServices : BaseServices<rt_b_ocrlog_history>, Irt_b_ocrlog_historyServices
    {
	
        private readonly Irt_b_ocrlog_historyRepository dal;
        public rt_b_ocrlog_historyServices(Irt_b_ocrlog_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rt_b_ocrlog_history结束----------

	