	//----------sys_user_role_mapper开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// sys_user_role_mapperRepository
	/// </summary>	
	public partial class sys_user_role_mapperRepository : BaseRepository<sys_user_role_mapper>, Isys_user_role_mapperRepository
    {
        public sys_user_role_mapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------sys_user_role_mapper结束----------
	
	