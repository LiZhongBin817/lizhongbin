	//----------Sys_Item开始----------
    

using System;
using CDWM_MR_IServices.Content;
using CDWM_MR_IRepository.Content;
using CDWM_MR_Model.Model;
using CDWM_MR_Services.BASE;
namespace CDWM_MR_Services.Content
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

	