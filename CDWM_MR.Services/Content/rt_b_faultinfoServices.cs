	//----------rt_b_faultinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rt_b_faultinfoServices
	/// </summary>	
	public partial class rt_b_faultinfoServices : BaseServices<rt_b_faultinfo>, Irt_b_faultinfoServices
    {
	
        private readonly Irt_b_faultinfoRepository dal;
        public rt_b_faultinfoServices(Irt_b_faultinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rt_b_faultinfo结束----------

	