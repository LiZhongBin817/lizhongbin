	//----------sys_role开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// sys_roleServices
	/// </summary>	
	public partial class sys_roleServices : BaseServices<sys_role>, Isys_roleServices
    {
	
        private readonly Isys_roleRepository dal;
        public sys_roleServices(Isys_roleRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------sys_role结束----------

	