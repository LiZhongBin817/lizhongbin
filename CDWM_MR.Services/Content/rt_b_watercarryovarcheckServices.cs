	//----------rt_b_watercarryovarcheck开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// rt_b_watercarryovarcheckServices
	/// </summary>	
	public partial class rt_b_watercarryovarcheckServices : BaseServices<rt_b_watercarryovarcheck>, Irt_b_watercarryovarcheckServices
    {
	
        private readonly Irt_b_watercarryovarcheckRepository dal;
        public rt_b_watercarryovarcheckServices(Irt_b_watercarryovarcheckRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------rt_b_watercarryovarcheck结束----------

	