	//----------v_mr_date_reader开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_mr_date_readerServices
	/// </summary>	
	public partial class v_mr_date_readerServices : BaseServices<v_mr_date_reader>, Iv_mr_date_readerServices
    {
	
        private readonly Iv_mr_date_readerRepository dal;
        public v_mr_date_readerServices(Iv_mr_date_readerRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_mr_date_reader结束----------

	