using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Repository.Content
{
    public partial class v_ocrlogRepository : BaseRepository<v_ocrlog>, Iv_ocrlogRepository
    {
        public v_ocrlogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
