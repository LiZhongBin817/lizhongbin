	//----------v_user_water_bookinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_user_water_bookinfoRepository
	/// </summary>	
	public partial class v_user_water_bookinfoRepository : BaseRepository<v_user_water_bookinfo>, Iv_user_water_bookinfoRepository
    {
        public v_user_water_bookinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_user_water_bookinfo结束----------
	
	