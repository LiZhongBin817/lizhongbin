	//----------mr_taskinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// mr_taskinfoRepository
	/// </summary>	
	public partial class mr_taskinfoRepository : BaseRepository<mr_taskinfo>, Imr_taskinfoRepository
    {
        public mr_taskinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------mr_taskinfo结束----------
	
	