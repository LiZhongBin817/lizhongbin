	//----------rt_b_recheck开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rt_b_recheckServices
	/// </summary>	
	public partial class rt_b_recheckServices : BaseServices<rt_b_recheck>, Irt_b_recheckServices
    {
	
        private readonly Irt_b_recheckRepository dal;
        public rt_b_recheckServices(Irt_b_recheckRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rt_b_recheck结束----------

	