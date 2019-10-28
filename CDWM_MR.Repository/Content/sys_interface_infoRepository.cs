	//----------sys_interface_info开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// sys_interface_infoRepository
	/// </summary>	
	public partial class sys_interface_infoRepository : BaseRepository<sys_interface_info>, Isys_interface_infoRepository
    {
        public sys_interface_infoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------sys_interface_info结束----------
	
	