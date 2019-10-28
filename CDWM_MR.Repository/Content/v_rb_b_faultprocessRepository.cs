	//----------v_rb_b_faultprocess开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_rb_b_faultprocessRepository
	/// </summary>	
	public partial class v_rb_b_faultprocessRepository : BaseRepository<v_rb_b_faultprocess>, Iv_rb_b_faultprocessRepository
    {
        public v_rb_b_faultprocessRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_rb_b_faultprocess结束----------
	
	