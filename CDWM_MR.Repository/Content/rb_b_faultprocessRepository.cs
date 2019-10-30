	//----------rb_b_faultprocess开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rb_b_faultprocessRepository
	/// </summary>	
	public partial class rb_b_faultprocessRepository : BaseRepository<rb_b_faultprocess>, Irb_b_faultprocessRepository
    {
        public rb_b_faultprocessRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rb_b_faultprocess结束----------
	
	