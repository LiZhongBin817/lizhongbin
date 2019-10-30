	//----------sys_role_menu开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// sys_role_menuRepository
	/// </summary>	
	public partial class sys_role_menuRepository : BaseRepository<sys_role_menu>, Isys_role_menuRepository
    {
        public sys_role_menuRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------sys_role_menu结束----------
	
	