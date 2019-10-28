	//----------mr_book_meter开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// mr_book_meterRepository
	/// </summary>	
	public partial class mr_book_meterRepository : BaseRepository<mr_book_meter>, Imr_book_meterRepository
    {
        public mr_book_meterRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------mr_book_meter结束----------
	
	