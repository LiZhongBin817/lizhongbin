	//----------mr_taskinfo_history开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// mr_taskinfo_historyRepository
	/// </summary>	
	public partial class mr_taskinfo_historyRepository : BaseRepository<mr_taskinfo_history>, Imr_taskinfo_historyRepository
    {
        public mr_taskinfo_historyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------mr_taskinfo_history结束----------
	
	