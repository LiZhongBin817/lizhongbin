	//----------rt_b_watercarryovarcheck_history开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rt_b_watercarryovarcheck_historyServices
	/// </summary>	
	public partial class rt_b_watercarryovarcheck_historyServices : BaseServices<rt_b_watercarryovarcheck_history>, Irt_b_watercarryovarcheck_historyServices
    {
	
        private readonly Irt_b_watercarryovarcheck_historyRepository dal;
        public rt_b_watercarryovarcheck_historyServices(Irt_b_watercarryovarcheck_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rt_b_watercarryovarcheck_history结束----------

	