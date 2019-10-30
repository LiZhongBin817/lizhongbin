	//----------v_taskinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_taskinfoRepository
	/// </summary>	
	public partial class v_taskinfoRepository : BaseRepository<v_taskinfo>, Iv_taskinfoRepository
    {
        public v_taskinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_taskinfo结束----------
	
	