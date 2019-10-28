	//----------rt_b_photoattachment开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rt_b_photoattachmentRepository
	/// </summary>	
	public partial class rt_b_photoattachmentRepository : BaseRepository<rt_b_photoattachment>, Irt_b_photoattachmentRepository
    {
        public rt_b_photoattachmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rt_b_photoattachment结束----------
	
	