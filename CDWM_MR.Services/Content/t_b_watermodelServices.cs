using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services
{
    public partial class t_b_watermodelServices : BaseServices<t_b_watermodel>, It_b_watermodelServices
    {
        private readonly It_b_watermodelRepository dal;
        public t_b_watermodelServices(It_b_watermodelRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
