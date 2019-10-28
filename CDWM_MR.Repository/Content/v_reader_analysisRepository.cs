	//----------v_reader_analysis开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_reader_analysisRepository
	/// </summary>	
	public partial class v_reader_analysisRepository : BaseRepository<v_reader_analysis>, Iv_reader_analysisRepository
    {
        public v_reader_analysisRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_reader_analysis结束----------
	
	