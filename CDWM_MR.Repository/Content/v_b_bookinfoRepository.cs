	//----------v_b_bookinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_b_bookinfoRepository
	/// </summary>	
	public partial class v_b_bookinfoRepository : BaseRepository<v_b_bookinfo>, Iv_b_bookinfoRepository
    {
        public v_b_bookinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_b_bookinfo结束----------
	
	