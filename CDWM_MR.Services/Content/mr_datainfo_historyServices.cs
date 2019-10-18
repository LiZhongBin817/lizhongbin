	//----------mr_datainfo_history开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_datainfo_historyServices
	/// </summary>	
	public partial class mr_datainfo_historyServices : BaseServices<mr_datainfo_history>, Imr_datainfo_historyServices
    {
	
        private readonly Imr_datainfo_historyRepository dal;
        public mr_datainfo_historyServices(Imr_datainfo_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_datainfo_history结束----------

	