	//----------rb_b_faultprocess_history开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rb_b_faultprocess_historyRepository
	/// </summary>	
	public partial class rb_b_faultprocess_historyRepository : BaseRepository<rb_b_faultprocess_history>, Irb_b_faultprocess_historyRepository
    {
        public rb_b_faultprocess_historyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rb_b_faultprocess_history结束----------
	
	