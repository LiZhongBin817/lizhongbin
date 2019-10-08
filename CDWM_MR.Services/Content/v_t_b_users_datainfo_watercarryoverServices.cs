	//----------v_t_b_users_datainfo_watercarryover开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_t_b_users_datainfo_watercarryoverServices
	/// </summary>	
	public partial class v_t_b_users_datainfo_watercarryoverServices : BaseServices<v_t_b_users_datainfo_watercarryover>, Iv_t_b_users_datainfo_watercarryoverServices
    {
	
        private readonly Iv_t_b_users_datainfo_watercarryoverRepository dal;
        public v_t_b_users_datainfo_watercarryoverServices(Iv_t_b_users_datainfo_watercarryoverRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_t_b_users_datainfo_watercarryover结束----------

	