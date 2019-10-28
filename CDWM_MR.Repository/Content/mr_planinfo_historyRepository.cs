	//----------mr_planinfo_history开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// mr_planinfo_historyRepository
	/// </summary>	
	public partial class mr_planinfo_historyRepository : BaseRepository<mr_planinfo_history>, Imr_planinfo_historyRepository
    {
        public mr_planinfo_historyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------mr_planinfo_history结束----------
	
	