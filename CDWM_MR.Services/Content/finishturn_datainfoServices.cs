	//----------finishturn_datainfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// finishturn_datainfoServices
	/// </summary>	
	public partial class finishturn_datainfoServices : BaseServices<finishturn_datainfo>, Ifinishturn_datainfoServices
    {
	
        private readonly Ifinishturn_datainfoRepository dal;
        public finishturn_datainfoServices(Ifinishturn_datainfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------finishturn_datainfo结束----------

	