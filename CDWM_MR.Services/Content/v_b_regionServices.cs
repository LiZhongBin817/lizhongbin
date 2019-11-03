	//----------v_b_region开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_b_regionServices
	/// </summary>	
	public partial class v_b_regionServices : BaseServices<v_b_region>, Iv_b_regionServices
    {
	
        private readonly Iv_b_regionRepository dal;
        public v_b_regionServices(Iv_b_regionRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_b_region结束----------

	