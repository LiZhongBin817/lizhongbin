	//----------mr_datainfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_datainfoServices
	/// </summary>	
	public partial class mr_datainfoServices : BaseServices<mr_datainfo>, Imr_datainfoServices
    {
	
        private readonly Imr_datainfoRepository dal;
        public mr_datainfoServices(Imr_datainfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_datainfo结束----------

	