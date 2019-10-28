	//----------v_mr_book_reader_lq开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_mr_book_reader_lqRepository
	/// </summary>	
	public partial class v_mr_book_reader_lqRepository : BaseRepository<v_mr_book_reader_lq>, Iv_mr_book_reader_lqRepository
    {
        public v_mr_book_reader_lqRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_mr_book_reader_lq结束----------
	
	