	//----------v_downloaddatainfo开始----------
    

using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{	
	/// <summary>
	/// v_downloaddatainfoRepository
	/// </summary>	
	public partial class v_downloaddatainfoRepository : BaseRepository<v_downloaddatainfo>, Iv_downloaddatainfoRepository
    {
        public v_downloaddatainfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
          
        }
       
    }
}

	//----------v_downloaddatainfo结束----------
	
	