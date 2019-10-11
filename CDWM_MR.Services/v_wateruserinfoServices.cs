using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services
{
    class v_wateruserinfoServices : BaseServices<v_wateruserinfo>, Iv_wateruserinfoServices
    {
        private readonly Iv_wateruserinfoRepository dal;
        public v_wateruserinfoServices(Iv_wateruserinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
