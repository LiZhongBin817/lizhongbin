using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services
{
    public partial class t_b_installposServices : BaseServices<t_b_installpos>, It_b_installposServices
    {
        private readonly It_b_installposRepository dal;

        public t_b_installposServices(It_b_installposRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;

        }

    }
}
