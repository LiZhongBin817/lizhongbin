	//----------v_datainfo_history_ocrlog_history开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_datainfo_history_ocrlog_historyRepository
	/// </summary>	
	public partial class v_datainfo_history_ocrlog_historyRepository : BaseRepository<v_datainfo_history_ocrlog_history>, Iv_datainfo_history_ocrlog_historyRepository
    {
        public v_datainfo_history_ocrlog_historyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_datainfo_history_ocrlog_history结束----------
	
	