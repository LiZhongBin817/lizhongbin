	//----------dispatchsheet_info开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// dispatchsheet_infoServices
	/// </summary>	
	public partial class dispatchsheet_infoServices : BaseServices<dispatchsheet_info>, Idispatchsheet_infoServices
    {
	
        private readonly Idispatchsheet_infoRepository dal;
        public dispatchsheet_infoServices(Idispatchsheet_infoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------dispatchsheet_info结束----------

	