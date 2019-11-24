using CDWM_MR.IRepository;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.Models.Entitys;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services.Content
{
    public partial class v_recordpaidServices: BaseServices<v_recordpaid>, Iv_recordpaidServices
    {
        private readonly Iv_recordpaidRepository dal;
        public v_recordpaidServices(Iv_recordpaidRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }

    }
}
