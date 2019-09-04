	//----------finishturn_check开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// finishturn_checkServices
	/// </summary>	
	public partial class finishturn_checkServices : BaseServices<finishturn_check>, Ifinishturn_checkServices
    {
	
        private readonly Ifinishturn_checkRepository dal;
        public finishturn_checkServices(Ifinishturn_checkRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------finishturn_check结束----------

	