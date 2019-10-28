	//----------v_carryoverdatainfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_carryoverdatainfoRepository
	/// </summary>	
	public partial class v_carryoverdatainfoRepository : BaseRepository<v_carryoverdatainfo>, Iv_carryoverdatainfoRepository
    {
        public v_carryoverdatainfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_carryoverdatainfo结束----------
	
	