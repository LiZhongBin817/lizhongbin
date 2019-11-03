	//----------v_b_bookuserinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_b_bookuserinfoRepository
	/// </summary>	
	public partial class v_b_bookuserinfoRepository : BaseRepository<v_b_bookuserinfo>, Iv_b_bookuserinfoRepository
    {
        public v_b_bookuserinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_b_bookuserinfo结束----------
	
	