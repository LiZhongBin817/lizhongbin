	//----------v_mr_date_reader开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_mr_date_readerRepository
	/// </summary>	
	public partial class v_mr_date_readerRepository : BaseRepository<v_mr_date_reader>, Iv_mr_date_readerRepository
    {
        public v_mr_date_readerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_mr_date_reader结束----------
	
	