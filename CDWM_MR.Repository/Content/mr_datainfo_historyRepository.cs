	//----------mr_datainfo_history开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// mr_datainfo_historyRepository
	/// </summary>	
	public partial class mr_datainfo_historyRepository : BaseRepository<mr_datainfo_history>, Imr_datainfo_historyRepository
    {
        public mr_datainfo_historyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------mr_datainfo_history结束----------
	
	