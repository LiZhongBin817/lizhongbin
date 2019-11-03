	//----------v_b_datasearch_history开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_b_datasearch_historyServices
	/// </summary>	
	public partial class v_b_datasearch_historyServices : BaseServices<v_b_datasearch_history>, Iv_b_datasearch_historyServices
    {
	
        private readonly Iv_b_datasearch_historyRepository dal;
        public v_b_datasearch_historyServices(Iv_b_datasearch_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_b_datasearch_history结束----------

	