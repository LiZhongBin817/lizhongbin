	//----------mr_b_bookinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_b_bookinfoServices
	/// </summary>	
	public partial class mr_b_bookinfoServices : BaseServices<mr_b_bookinfo>, Imr_b_bookinfoServices
    {
	
        private readonly Imr_b_bookinfoRepository dal;
        public mr_b_bookinfoServices(Imr_b_bookinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_b_bookinfo结束----------

	