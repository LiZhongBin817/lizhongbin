	//----------sys_interface_info开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// sys_interface_infoServices
	/// </summary>	
	public class sys_interface_infoServices : BaseServices<sys_interface_info>, Isys_interface_infoServices
    {
	
        private readonly Isys_interface_infoRepository dal;
        public sys_interface_infoServices(Isys_interface_infoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------sys_interface_info结束----------

	