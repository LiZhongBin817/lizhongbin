	//----------v_b_bookuserinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_b_bookuserinfoServices
	/// </summary>	
	public partial class v_b_bookuserinfoServices : BaseServices<v_b_bookuserinfo>, Iv_b_bookuserinfoServices
    {
	
        private readonly Iv_b_bookuserinfoRepository dal;
        public v_b_bookuserinfoServices(Iv_b_bookuserinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_b_bookuserinfo结束----------

	