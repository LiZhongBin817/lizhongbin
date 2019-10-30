	//----------sys_operation开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// sys_operationRepository
	/// </summary>	
	public partial class sys_operationRepository : BaseRepository<sys_operation>, Isys_operationRepository
    {
        public sys_operationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------sys_operation结束----------
	
	