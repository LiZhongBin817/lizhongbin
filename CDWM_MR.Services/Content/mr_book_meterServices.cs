	//----------mr_book_meter开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_book_meterServices
	/// </summary>	
	public partial class mr_book_meterServices : BaseServices<mr_book_meter>, Imr_book_meterServices
    {
	
        private readonly Imr_book_meterRepository dal;
        public mr_book_meterServices(Imr_book_meterRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_book_meter结束----------

	