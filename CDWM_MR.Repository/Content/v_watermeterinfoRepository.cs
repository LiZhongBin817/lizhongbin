	//----------v_watermeterinfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_watermeterinfoRepository
	/// </summary>	
	public partial class v_watermeterinfoRepository : BaseRepository<v_watermeterinfo>, Iv_watermeterinfoRepository
    {
        public v_watermeterinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_watermeterinfo结束----------
	
	