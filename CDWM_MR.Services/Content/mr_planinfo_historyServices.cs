	//----------mr_planinfo_history开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_planinfo_historyServices
	/// </summary>	
	public partial class mr_planinfo_historyServices : BaseServices<mr_planinfo_history>, Imr_planinfo_historyServices
    {
	
        private readonly Imr_planinfo_historyRepository dal;
        public mr_planinfo_historyServices(Imr_planinfo_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_planinfo_history结束----------

	