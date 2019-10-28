	//----------v_user_water_bookinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_user_water_bookinfoServices
	/// </summary>	
	public partial class v_user_water_bookinfoServices : BaseServices<v_user_water_bookinfo>, Iv_user_water_bookinfoServices
    {
	
        private readonly Iv_user_water_bookinfoRepository dal;
        public v_user_water_bookinfoServices(Iv_user_water_bookinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_user_water_bookinfo结束----------

	