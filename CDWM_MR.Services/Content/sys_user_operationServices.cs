	//----------sys_user_operation开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// sys_user_operationServices
	/// </summary>	
	public class sys_user_operationServices : BaseServices<sys_user_operation>, Isys_user_operationServices
    {
	
        private readonly Isys_user_operationRepository dal;
        public sys_user_operationServices(Isys_user_operationRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------sys_user_operation结束----------

	