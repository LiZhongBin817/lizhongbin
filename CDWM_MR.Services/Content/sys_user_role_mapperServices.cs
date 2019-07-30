	//----------sys_user_role_mapper开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// sys_user_role_mapperServices
	/// </summary>	
	public partial class sys_user_role_mapperServices : BaseServices<sys_user_role_mapper>, Isys_user_role_mapperServices
    {
	
        private readonly Isys_user_role_mapperRepository dal;
        public sys_user_role_mapperServices(Isys_user_role_mapperRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------sys_user_role_mapper结束----------

	