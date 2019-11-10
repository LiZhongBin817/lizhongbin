using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Repository.Content
{
   public partial class v_interfaceRepository:BaseRepository<v_interface>, Iv_interfaceRepository
    {
        public v_interfaceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
