	//----------rt_b_ocrlog开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// rt_b_ocrlogRepository
	/// </summary>	
	public partial class rt_b_ocrlogRepository : BaseRepository<rt_b_ocrlog>, Irt_b_ocrlogRepository
    {
        public rt_b_ocrlogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------rt_b_ocrlog结束----------
	
	