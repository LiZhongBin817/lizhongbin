	//----------v_wateruserinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_wateruserinfoServices
	/// </summary>	
	public partial class v_wateruserinfoServices : BaseServices<v_wateruserinfo>, Iv_wateruserinfoServices
    {
	
        private readonly Iv_wateruserinfoRepository dal;
        public v_wateruserinfoServices(Iv_wateruserinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_wateruserinfo结束----------

	