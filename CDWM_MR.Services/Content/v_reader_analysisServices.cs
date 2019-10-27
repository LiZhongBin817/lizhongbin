using System;
using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services.Content
{
    public partial class v_reader_analysisServices : BaseServices<v_reader_analysis>, Iv_reader_analysisServices
    {
        private readonly Iv_reader_analysisRepository dal;
        public v_reader_analysisServices(Iv_reader_analysisRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
