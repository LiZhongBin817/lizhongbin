	//----------v_b_areas_region开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_b_areas_regionRepository
	/// </summary>	
	public partial class v_b_areas_regionRepository : BaseRepository<v_b_areas_region>, Iv_b_areas_regionRepository
    {
        public v_b_areas_regionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_b_areas_region结束----------
	
	