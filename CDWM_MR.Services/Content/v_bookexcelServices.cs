	//----------v_bookexcel开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_bookexcelServices
	/// </summary>	
	public partial class v_bookexcelServices : BaseServices<v_bookexcel>, Iv_bookexcelServices
    {
	
        private readonly Iv_bookexcelRepository dal;
        public v_bookexcelServices(Iv_bookexcelRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_bookexcel结束----------

	