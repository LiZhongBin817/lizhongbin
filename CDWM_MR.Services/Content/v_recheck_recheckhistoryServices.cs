	//----------v_recheck_recheckhistory开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_recheck_recheckhistoryServices
	/// </summary>	
	public partial class v_recheck_recheckhistoryServices : BaseServices<v_recheck_recheckhistory>, Iv_recheck_recheckhistoryServices
    {
	
        private readonly Iv_recheck_recheckhistoryRepository dal;
        public v_recheck_recheckhistoryServices(Iv_recheck_recheckhistoryRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_recheck_recheckhistory结束----------

	