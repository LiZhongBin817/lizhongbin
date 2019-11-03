using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System.Collections.Generic;
using System.Text;
using CDWM_MR.IRepository.UnitOfWork;

namespace CDWM_MR.Repository.Content
{
    public partial class vrt_b_watercarryover_datainfoRepository : BaseRepository<vrt_b_watercarryover_datainfo>, Ivrt_b_watercarryover_datainfoRepository
    {
        public vrt_b_watercarryover_datainfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
