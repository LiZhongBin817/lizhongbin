	//----------mr_book_reader开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// mr_book_readerServices
	/// </summary>	
	public partial class mr_book_readerServices : BaseServices<mr_book_reader>, Imr_book_readerServices
    {
	
        private readonly Imr_book_readerRepository dal;
        public mr_book_readerServices(Imr_book_readerRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------mr_book_reader结束----------

	