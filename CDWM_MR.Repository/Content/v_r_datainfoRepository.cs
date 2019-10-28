	//----------v_r_datainfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_r_datainfoRepository
	/// </summary>	
	public partial class v_r_datainfoRepository : BaseRepository<v_r_datainfo>, Iv_r_datainfoRepository
    {
        public v_r_datainfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_r_datainfo结束----------
	
	