	//----------rt_b_watercarryover开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rt_b_watercarryoverServices
	/// </summary>	
	public partial class rt_b_watercarryoverServices : BaseServices<rt_b_watercarryover>, Irt_b_watercarryoverServices
    {
	
        private readonly Irt_b_watercarryoverRepository dal;
        public rt_b_watercarryoverServices(Irt_b_watercarryoverRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rt_b_watercarryover结束----------

	