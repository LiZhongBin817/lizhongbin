using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Repository.Content
{
    public partial class v_meterreading_recordRepository:BaseRepository<v_meterreading_record>, Iv_meterreading_recordRepository
    {
        public v_meterreading_recordRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
