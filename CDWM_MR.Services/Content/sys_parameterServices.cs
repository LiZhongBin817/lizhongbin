	//----------sys_parameter开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// sys_parameterServices
	/// </summary>	
	public partial class sys_parameterServices : BaseServices<sys_parameter>, Isys_parameterServices
    {
	
        private readonly Isys_parameterRepository dal;
        public sys_parameterServices(Isys_parameterRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------sys_parameter结束----------

	