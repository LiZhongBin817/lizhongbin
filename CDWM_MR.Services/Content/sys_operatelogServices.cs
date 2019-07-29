	//----------sys_operatelog开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// sys_operatelogServices
	/// </summary>	
	public class sys_operatelogServices : BaseServices<sys_operatelog>, Isys_operatelogServices
    {
	
        private readonly Isys_operatelogRepository dal;
        public sys_operatelogServices(Isys_operatelogRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------sys_operatelog结束----------

	