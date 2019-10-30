	//----------mr_taskinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_taskinfoServices
	/// </summary>	
	public partial class mr_taskinfoServices : BaseServices<mr_taskinfo>, Imr_taskinfoServices
    {
	
        private readonly Imr_taskinfoRepository dal;
        public mr_taskinfoServices(Imr_taskinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_taskinfo结束----------

	