	//----------sys_userinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// sys_userinfoRepository
	/// </summary>	
	public partial class sys_userinfoRepository : BaseRepository<sys_userinfo>, Isys_userinfoRepository
    {
        public sys_userinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------sys_userinfo结束----------
	
	