
using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
namespace CDWM_MR.Services.Content
{
    public partial class vrt_b_watercarryover_datainfoServices : BaseServices<vrt_b_watercarryover_datainfo>, Ivrt_b_watercarryover_datainfoServices
    {
        private readonly Ivrt_b_watercarryover_datainfoRepository dal;
        public vrt_b_watercarryover_datainfoServices(Ivrt_b_watercarryover_datainfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
