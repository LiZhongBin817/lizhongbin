	//----------v_b_datasearch_history开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_b_datasearch_historyRepository
	/// </summary>	
	public partial class v_b_datasearch_historyRepository : BaseRepository<v_b_datasearch_history>, Iv_b_datasearch_historyRepository
    {
        public v_b_datasearch_historyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_b_datasearch_history结束----------
	
	