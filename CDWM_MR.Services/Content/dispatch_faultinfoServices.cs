	//----------dispatch_faultinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// dispatch_faultinfoServices
	/// </summary>	
	public partial class dispatch_faultinfoServices : BaseServices<dispatch_faultinfo>, Idispatch_faultinfoServices
    {
	
        private readonly Idispatch_faultinfoRepository dal;
        public dispatch_faultinfoServices(Idispatch_faultinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------dispatch_faultinfo结束----------

	