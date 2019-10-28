	//----------rt_b_faultinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rt_b_faultinfoRepository
	/// </summary>	
	public partial class rt_b_faultinfoRepository : BaseRepository<rt_b_faultinfo>, Irt_b_faultinfoRepository
    {
        public rt_b_faultinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rt_b_faultinfo结束----------
	
	