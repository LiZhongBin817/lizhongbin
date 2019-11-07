using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{
    public partial class v_meterdataRepository : BaseRepository<v_meterdata>, Iv_meterdataRepository
    {
        public v_meterdataRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
