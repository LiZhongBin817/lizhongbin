	//----------mr_b_bookinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// mr_b_bookinfoRepository
	/// </summary>	
	public partial class mr_b_bookinfoRepository : BaseRepository<mr_b_bookinfo>, Imr_b_bookinfoRepository
    {
        public mr_b_bookinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------mr_b_bookinfo结束----------
	
	