	//----------sys_userinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// sys_userinfoServices
	/// </summary>	
	public partial class sys_userinfoServices : BaseServices<sys_userinfo>, Isys_userinfoServices
    {
	
        private readonly Isys_userinfoRepository dal;
        public sys_userinfoServices(Isys_userinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------sys_userinfo结束----------

	