	//----------v_b_region开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_b_regionRepository
	/// </summary>	
	public partial class v_b_regionRepository : BaseRepository<v_b_region>, Iv_b_regionRepository
    {
        public v_b_regionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_b_region结束----------
	
	