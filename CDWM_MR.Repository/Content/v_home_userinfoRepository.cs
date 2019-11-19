using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Repository.Content
{
     public partial class v_home_userinfoRepository:BaseRepository<v_home_userinfo>, Iv_home_userinfoRepository
    {
        public v_home_userinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
