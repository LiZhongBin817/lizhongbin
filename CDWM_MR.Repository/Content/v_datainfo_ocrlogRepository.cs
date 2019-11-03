	//----------v_datainfo_ocrlog开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_datainfo_ocrlogRepository
	/// </summary>	
	public partial class v_datainfo_ocrlogRepository : BaseRepository<v_datainfo_ocrlog>, Iv_datainfo_ocrlogRepository
    {
        public v_datainfo_ocrlogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_datainfo_ocrlog结束----------
	
	