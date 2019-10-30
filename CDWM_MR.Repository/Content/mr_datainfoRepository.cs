	//----------mr_datainfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// mr_datainfoRepository
	/// </summary>	
	public partial class mr_datainfoRepository : BaseRepository<mr_datainfo>, Imr_datainfoRepository
    {
        public mr_datainfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------mr_datainfo结束----------
	
	