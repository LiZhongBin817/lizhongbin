	//----------mr_planinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// mr_planinfoRepository
	/// </summary>	
	public partial class mr_planinfoRepository : BaseRepository<mr_planinfo>, Imr_planinfoRepository
    {
        public mr_planinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------mr_planinfo结束----------
	
	