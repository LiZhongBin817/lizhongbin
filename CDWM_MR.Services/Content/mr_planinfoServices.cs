	//----------mr_planinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_planinfoServices
	/// </summary>	
	public partial class mr_planinfoServices : BaseServices<mr_planinfo>, Imr_planinfoServices
    {
	
        private readonly Imr_planinfoRepository dal;
        public mr_planinfoServices(Imr_planinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_planinfo结束----------

	