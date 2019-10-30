	//----------rt_b_watercarryover开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rt_b_watercarryoverRepository
	/// </summary>	
	public partial class rt_b_watercarryoverRepository : BaseRepository<rt_b_watercarryover>, Irt_b_watercarryoverRepository
    {
        public rt_b_watercarryoverRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rt_b_watercarryover结束----------
	
	