	//----------rt_b_watercarryovarcheck_history开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rt_b_watercarryovarcheck_historyRepository
	/// </summary>	
	public partial class rt_b_watercarryovarcheck_historyRepository : BaseRepository<rt_b_watercarryovarcheck_history>, Irt_b_watercarryovarcheck_historyRepository
    {
        public rt_b_watercarryovarcheck_historyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rt_b_watercarryovarcheck_history结束----------
	
	