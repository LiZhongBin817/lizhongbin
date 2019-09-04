	//----------v_taskinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_taskinfoServices
	/// </summary>	
	public partial class v_taskinfoServices : BaseServices<v_taskinfo>, Iv_taskinfoServices
    {
	
        private readonly Iv_taskinfoRepository dal;
        public v_taskinfoServices(Iv_taskinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_taskinfo结束----------

	