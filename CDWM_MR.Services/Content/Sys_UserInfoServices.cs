	//----------Sys_UserInfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// Sys_UserInfoServices
	/// </summary>	
	public class Sys_UserInfoServices : BaseServices<Sys_UserInfo>, ISys_UserInfoServices
    {
	
        private readonly ISys_UserInfoRepository dal;
        public Sys_UserInfoServices(ISys_UserInfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------Sys_UserInfo结束----------

	