	//----------v_bookexcel开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_bookexcelRepository
	/// </summary>	
	public partial class v_bookexcelRepository : BaseRepository<v_bookexcel>, Iv_bookexcelRepository
    {
        public v_bookexcelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_bookexcel结束----------
	
	