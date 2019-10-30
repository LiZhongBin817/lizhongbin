	//----------v_downloaddatainfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_downloaddatainfoServices
	/// </summary>	
	public partial class v_downloaddatainfoServices : BaseServices<v_downloaddatainfo>, Iv_downloaddatainfoServices
    {
	
        private readonly Iv_downloaddatainfoRepository dal;
        public v_downloaddatainfoServices(Iv_downloaddatainfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_downloaddatainfo结束----------

	