	//----------sys_user_operation开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// sys_user_operationRepository
	/// </summary>	
	public partial class sys_user_operationRepository : BaseRepository<sys_user_operation>, Isys_user_operationRepository
    {
        public sys_user_operationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------sys_user_operation结束----------
	
	