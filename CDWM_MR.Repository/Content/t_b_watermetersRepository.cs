using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Repository.Content
{
    public partial class t_b_watermetersRepository:BaseRepository<t_b_watermeters>, It_b_watermetersRepository
    {
        public t_b_watermetersRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
