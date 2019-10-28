	//----------mr_book_reader开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// mr_book_readerRepository
	/// </summary>	
	public partial class mr_book_readerRepository : BaseRepository<mr_book_reader>, Imr_book_readerRepository
    {
        public mr_book_readerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------mr_book_reader结束----------
	
	