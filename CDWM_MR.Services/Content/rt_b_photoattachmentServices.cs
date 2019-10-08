	//----------rt_b_photoattachment开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rt_b_photoattachmentServices
	/// </summary>	
	public partial class rt_b_photoattachmentServices : BaseServices<rt_b_photoattachment>, Irt_b_photoattachmentServices
    {
	
        private readonly Irt_b_photoattachmentRepository dal;
        public rt_b_photoattachmentServices(Irt_b_photoattachmentRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rt_b_photoattachment结束----------

	