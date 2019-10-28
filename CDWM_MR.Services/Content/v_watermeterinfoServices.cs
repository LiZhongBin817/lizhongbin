	//----------v_watermeterinfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_watermeterinfoServices
	/// </summary>	
	public partial class v_watermeterinfoServices : BaseServices<v_watermeterinfo>, Iv_watermeterinfoServices
    {
	
        private readonly Iv_watermeterinfoRepository dal;
        public v_watermeterinfoServices(Iv_watermeterinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_watermeterinfo结束----------

	