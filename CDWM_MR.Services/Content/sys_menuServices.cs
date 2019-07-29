	//----------sys_menu开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// sys_menuServices
	/// </summary>	
	public class sys_menuServices : BaseServices<sys_menu>, Isys_menuServices
    {
	
        private readonly Isys_menuRepository dal;
        public sys_menuServices(Isys_menuRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------sys_menu结束----------

	