	//----------v_recheck_recheckhistory开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_recheck_recheckhistoryRepository
	/// </summary>	
	public partial class v_recheck_recheckhistoryRepository : BaseRepository<v_recheck_recheckhistory>, Iv_recheck_recheckhistoryRepository
    {
        public v_recheck_recheckhistoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_recheck_recheckhistory结束----------
	
	