	//----------mr_data_check开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_data_checkServices
	/// </summary>	
	public partial class mr_data_checkServices : BaseServices<mr_data_check>, Imr_data_checkServices
    {
	
        private readonly Imr_data_checkRepository dal;
        public mr_data_checkServices(Imr_data_checkRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_data_check结束----------

	