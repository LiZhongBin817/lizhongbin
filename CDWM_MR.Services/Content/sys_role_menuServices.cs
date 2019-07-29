	//----------sys_role_menu开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// sys_role_menuServices
	/// </summary>	
	public class sys_role_menuServices : BaseServices<sys_role_menu>, Isys_role_menuServices
    {
	
        private readonly Isys_role_menuRepository dal;
        public sys_role_menuServices(Isys_role_menuRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------sys_role_menu结束----------

	