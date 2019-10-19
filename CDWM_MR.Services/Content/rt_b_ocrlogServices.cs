	//----------rt_b_ocrlog开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rt_b_ocrlogServices
	/// </summary>	
	public partial class rt_b_ocrlogServices : BaseServices<rt_b_ocrlog>, Irt_b_ocrlogServices
    {
	
        private readonly Irt_b_ocrlogRepository dal;
        public rt_b_ocrlogServices(Irt_b_ocrlogRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rt_b_ocrlog结束----------

	