	//----------rb_b_faultprocess_history开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rb_b_faultprocess_historyServices
	/// </summary>	
	public partial class rb_b_faultprocess_historyServices : BaseServices<rb_b_faultprocess_history>, Irb_b_faultprocess_historyServices
    {
	
        private readonly Irb_b_faultprocess_historyRepository dal;
        public rb_b_faultprocess_historyServices(Irb_b_faultprocess_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rb_b_faultprocess_history结束----------

	