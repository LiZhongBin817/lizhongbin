	//----------rt_b_recheck开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rt_b_recheckRepository
	/// </summary>	
	public partial class rt_b_recheckRepository : BaseRepository<rt_b_recheck>, Irt_b_recheckRepository
    {
        public rt_b_recheckRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rt_b_recheck结束----------
	
	