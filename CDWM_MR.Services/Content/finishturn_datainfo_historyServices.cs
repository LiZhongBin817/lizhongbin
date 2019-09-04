	//----------finishturn_datainfo_history开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// finishturn_datainfo_historyServices
	/// </summary>	
	public partial class finishturn_datainfo_historyServices : BaseServices<finishturn_datainfo_history>, Ifinishturn_datainfo_historyServices
    {
	
        private readonly Ifinishturn_datainfo_historyRepository dal;
        public finishturn_datainfo_historyServices(Ifinishturn_datainfo_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------finishturn_datainfo_history结束----------

	