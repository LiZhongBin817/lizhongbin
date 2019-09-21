	//----------mr_taskinfo_history开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_taskinfo_historyServices
	/// </summary>	
	public partial class mr_taskinfo_historyServices : BaseServices<mr_taskinfo_history>, Imr_taskinfo_historyServices
    {
	
        private readonly Imr_taskinfo_historyRepository dal;
        public mr_taskinfo_historyServices(Imr_taskinfo_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_taskinfo_history结束----------

	