	//----------rt_b_photoattachment_history开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rt_b_photoattachment_historyRepository
	/// </summary>	
	public partial class rt_b_photoattachment_historyRepository : BaseRepository<rt_b_photoattachment_history>, Irt_b_photoattachment_historyRepository
    {
        public rt_b_photoattachment_historyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rt_b_photoattachment_history结束----------
	
	