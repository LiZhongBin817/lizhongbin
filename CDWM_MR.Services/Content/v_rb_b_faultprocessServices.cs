	//----------v_rb_b_faultprocess开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_rb_b_faultprocessServices
	/// </summary>	
	public partial class v_rb_b_faultprocessServices : BaseServices<v_rb_b_faultprocess>, Iv_rb_b_faultprocessServices
    {
	
        private readonly Iv_rb_b_faultprocessRepository dal;
        public v_rb_b_faultprocessServices(Iv_rb_b_faultprocessRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_rb_b_faultprocess结束----------

	