	//----------v_rt_b_recheck开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_rt_b_recheckRepository
	/// </summary>	
	public partial class v_rt_b_recheckRepository : BaseRepository<v_rt_b_recheck>, Iv_rt_b_recheckRepository
    {
        public v_rt_b_recheckRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_rt_b_recheck结束----------
	
	