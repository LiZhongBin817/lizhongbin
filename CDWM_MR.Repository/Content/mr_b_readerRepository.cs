	//----------mr_b_reader开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// mr_b_readerRepository
	/// </summary>	
	public partial class mr_b_readerRepository : BaseRepository<mr_b_reader>, Imr_b_readerRepository
    {
        public mr_b_readerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------mr_b_reader结束----------
	
	