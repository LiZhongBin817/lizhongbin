	//----------v_rt_b_faultinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_rt_b_faultinfoRepository
	/// </summary>	
	public partial class v_rt_b_faultinfoRepository : BaseRepository<v_rt_b_faultinfo>, Iv_rt_b_faultinfoRepository
    {
        public v_rt_b_faultinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_rt_b_faultinfo结束----------
	
	