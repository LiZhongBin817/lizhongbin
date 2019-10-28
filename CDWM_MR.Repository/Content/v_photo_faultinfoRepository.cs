	//----------v_photo_faultinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_photo_faultinfoRepository
	/// </summary>	
	public partial class v_photo_faultinfoRepository : BaseRepository<v_photo_faultinfo>, Iv_photo_faultinfoRepository
    {
        public v_photo_faultinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_photo_faultinfo结束----------
	
	