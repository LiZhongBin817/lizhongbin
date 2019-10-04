	//----------v_mr_datainfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_mr_datainfoServices
	/// </summary>	
	public partial class v_mr_datainfoServices : BaseServices<v_mr_datainfo>, Iv_mr_datainfoServices
    {
	
        private readonly Iv_mr_datainfoRepository dal;
        public v_mr_datainfoServices(Iv_mr_datainfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_mr_datainfo结束----------

	