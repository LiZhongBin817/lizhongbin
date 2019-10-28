	//----------v_rt_b_recheck开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_rt_b_recheckServices
	/// </summary>	
	public partial class v_rt_b_recheckServices : BaseServices<v_rt_b_recheck>, Iv_rt_b_recheckServices
    {
	
        private readonly Iv_rt_b_recheckRepository dal;
        public v_rt_b_recheckServices(Iv_rt_b_recheckRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_rt_b_recheck结束----------

	