	//----------dispatch_fault_handleinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// dispatch_fault_handleinfoServices
	/// </summary>	
	public partial class dispatch_fault_handleinfoServices : BaseServices<dispatch_fault_handleinfo>, Idispatch_fault_handleinfoServices
    {
	
        private readonly Idispatch_fault_handleinfoRepository dal;
        public dispatch_fault_handleinfoServices(Idispatch_fault_handleinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------dispatch_fault_handleinfo结束----------

	