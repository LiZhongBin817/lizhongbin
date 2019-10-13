	//----------v_datainfo_ocrlog开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_datainfo_ocrlogServices
	/// </summary>	
	public partial class v_datainfo_ocrlogServices : BaseServices<v_datainfo_ocrlog>, Iv_datainfo_ocrlogServices
    {
	
        private readonly Iv_datainfo_ocrlogRepository dal;
        public v_datainfo_ocrlogServices(Iv_datainfo_ocrlogRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------v_datainfo_ocrlog结束----------

	