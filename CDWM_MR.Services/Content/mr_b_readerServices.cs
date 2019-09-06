
	//----------mr_b_reader开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_b_readerServices
	/// </summary>	
	public partial class mr_b_readerServices : BaseServices<mr_b_reader>, Imr_b_readerServices
    {
	
        private readonly Imr_b_readerRepository dal;
        public mr_b_readerServices(Imr_b_readerRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_b_reader结束----------

