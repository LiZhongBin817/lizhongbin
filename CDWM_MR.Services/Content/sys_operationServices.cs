//----------sys_operation开始----------


using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using CDWM_MR.Model;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// sys_operationServices
	/// </summary>	
	public partial class sys_operationServices : BaseServices<sys_operation>, Isys_operationServices
    {
	
        private readonly Isys_operationRepository dal;
        public sys_operationServices(Isys_operationRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}

	//----------sys_operation结束----------

	