	//----------rt_b_wateradjust开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rt_b_wateradjustServices
	/// </summary>	
	public partial class rt_b_wateradjustServices : BaseServices<rt_b_wateradjust>, Irt_b_wateradjustServices
    {
	
        private readonly Irt_b_wateradjustRepository dal;
        public rt_b_wateradjustServices(Irt_b_wateradjustRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rt_b_wateradjust结束----------

	