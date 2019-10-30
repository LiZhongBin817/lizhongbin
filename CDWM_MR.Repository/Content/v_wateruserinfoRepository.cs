	//----------v_wateruserinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_wateruserinfoRepository
	/// </summary>	
	public partial class v_wateruserinfoRepository : BaseRepository<v_wateruserinfo>, Iv_wateruserinfoRepository
    {
        public v_wateruserinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_wateruserinfo结束----------
	
	