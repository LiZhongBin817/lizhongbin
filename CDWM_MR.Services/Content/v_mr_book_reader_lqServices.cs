	//----------v_mr_book_reader_lq开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_mr_book_reader_lqServices
	/// </summary>	
	public partial class v_mr_book_reader_lqServices : BaseServices<v_mr_book_reader_lq>, Iv_mr_book_reader_lqServices
    {
	
        private readonly Iv_mr_book_reader_lqRepository dal;
        public v_mr_book_reader_lqServices(Iv_mr_book_reader_lqRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_mr_book_reader_lq结束----------

	