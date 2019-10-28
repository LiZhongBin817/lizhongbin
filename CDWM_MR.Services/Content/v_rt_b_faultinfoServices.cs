	//----------v_rt_b_faultinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_rt_b_faultinfoServices
	/// </summary>	
	public partial class v_rt_b_faultinfoServices : BaseServices<v_rt_b_faultinfo>, Iv_rt_b_faultinfoServices
    {
	
        private readonly Iv_rt_b_faultinfoRepository dal;
        public v_rt_b_faultinfoServices(Iv_rt_b_faultinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_rt_b_faultinfo结束----------

	