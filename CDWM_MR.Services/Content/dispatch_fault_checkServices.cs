	//----------dispatch_fault_check开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// dispatch_fault_checkServices
	/// </summary>	
	public partial class dispatch_fault_checkServices : BaseServices<dispatch_fault_check>, Idispatch_fault_checkServices
    {
	
        private readonly Idispatch_fault_checkRepository dal;
        public dispatch_fault_checkServices(Idispatch_fault_checkRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------dispatch_fault_check结束----------

	