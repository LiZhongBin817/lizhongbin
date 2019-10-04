	//----------rb_b_faultprocess开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rb_b_faultprocessServices
	/// </summary>	
	public partial class rb_b_faultprocessServices : BaseServices<rb_b_faultprocess>, Irb_b_faultprocessServices
    {
	
        private readonly Irb_b_faultprocessRepository dal;
        public rb_b_faultprocessServices(Irb_b_faultprocessRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rb_b_faultprocess结束----------

	