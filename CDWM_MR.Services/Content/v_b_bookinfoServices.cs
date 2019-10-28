	//----------v_b_bookinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_b_bookinfoServices
	/// </summary>	
	public partial class v_b_bookinfoServices : BaseServices<v_b_bookinfo>, Iv_b_bookinfoServices
    {
	
        private readonly Iv_b_bookinfoRepository dal;
        public v_b_bookinfoServices(Iv_b_bookinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_b_bookinfo结束----------

	