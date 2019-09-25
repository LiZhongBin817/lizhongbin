using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services
{
    public partial class v_b_bookuserinfoServices : BaseServices<v_b_bookuserinfo>, Iv_b_bookuserinfoServices
    {
        private readonly Iv_b_bookuserinfoRepository dal;
        public v_b_bookuserinfoServices(Iv_b_bookuserinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
