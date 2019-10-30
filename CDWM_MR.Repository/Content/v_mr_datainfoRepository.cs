	//----------v_mr_datainfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_mr_datainfoRepository
	/// </summary>	
	public partial class v_mr_datainfoRepository : BaseRepository<v_mr_datainfo>, Iv_mr_datainfoRepository
    {
        public v_mr_datainfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_mr_datainfo结束----------
	
	