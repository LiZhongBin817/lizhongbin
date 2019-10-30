	//----------v_carryoverdatainfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_carryoverdatainfoServices
	/// </summary>	
	public partial class v_carryoverdatainfoServices : BaseServices<v_carryoverdatainfo>, Iv_carryoverdatainfoServices
    {
	
        private readonly Iv_carryoverdatainfoRepository dal;
        public v_carryoverdatainfoServices(Iv_carryoverdatainfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_carryoverdatainfo结束----------

	