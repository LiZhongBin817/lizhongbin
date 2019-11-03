	//----------rt_b_watercarryovarcheck开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rt_b_watercarryovarcheckRepository
	/// </summary>	
	public partial class rt_b_watercarryovarcheckRepository : BaseRepository<rt_b_watercarryovarcheck>, Irt_b_watercarryovarcheckRepository
    {
        public rt_b_watercarryovarcheckRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rt_b_watercarryovarcheck结束----------
	
	