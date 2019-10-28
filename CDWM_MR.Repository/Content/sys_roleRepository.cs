	//----------sys_role开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// sys_roleRepository
	/// </summary>	
	public partial class sys_roleRepository : BaseRepository<sys_role>, Isys_roleRepository
    {
        public sys_roleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------sys_role结束----------
	
	