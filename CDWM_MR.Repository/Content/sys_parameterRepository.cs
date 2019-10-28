	//----------sys_parameter开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// sys_parameterRepository
	/// </summary>	
	public partial class sys_parameterRepository : BaseRepository<sys_parameter>, Isys_parameterRepository
    {
        public sys_parameterRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------sys_parameter结束----------
	
	