	//----------rt_b_wateradjust开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rt_b_wateradjustRepository
	/// </summary>	
	public partial class rt_b_wateradjustRepository : BaseRepository<rt_b_wateradjust>, Irt_b_wateradjustRepository
    {
        public rt_b_wateradjustRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rt_b_wateradjust结束----------
	
	