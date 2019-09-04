	//----------dispatch_faultinfo_history开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// dispatch_faultinfo_historyServices
	/// </summary>	
	public partial class dispatch_faultinfo_historyServices : BaseServices<dispatch_faultinfo_history>, Idispatch_faultinfo_historyServices
    {
	
        private readonly Idispatch_faultinfo_historyRepository dal;
        public dispatch_faultinfo_historyServices(Idispatch_faultinfo_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------dispatch_faultinfo_history结束----------

	