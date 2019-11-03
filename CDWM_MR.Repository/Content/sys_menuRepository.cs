	//----------sys_menu开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// sys_menuRepository
	/// </summary>	
	public partial class sys_menuRepository : BaseRepository<sys_menu>, Isys_menuRepository
    {
        public sys_menuRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------sys_menu结束----------
	
	