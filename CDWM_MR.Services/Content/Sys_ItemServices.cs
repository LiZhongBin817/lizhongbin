	//----------Sys_Item开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// Sys_ItemServices
	/// </summary>	
	public class Sys_ItemServices : BaseServices<Sys_Item>, ISys_ItemServices
    {
	
        private readonly ISys_ItemRepository dal;
        public Sys_ItemServices(ISys_ItemRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------Sys_Item结束----------

	