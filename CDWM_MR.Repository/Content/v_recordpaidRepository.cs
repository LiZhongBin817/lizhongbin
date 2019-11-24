using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.Models.Entitys;
using CDWM_MR.Repository.BASE;

namespace CDWM_MR.Repository.Content
{
    public partial class v_recordpaidRepository : BaseRepository<v_recordpaid>, Iv_recordpaidRepository
    {
        public v_recordpaidRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

    }
}
