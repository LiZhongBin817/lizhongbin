using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Repository.Content
{
     public partial class v_rt_b_photoattachment_rt_b_photoattachment_histotyRepository:BaseRepository<v_rt_b_photoattachment_rt_b_photoattachment_histoty>, Iv_rt_b_photoattachment_rt_b_photoattachment_histotyRepository
    {
        public v_rt_b_photoattachment_rt_b_photoattachment_histotyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
