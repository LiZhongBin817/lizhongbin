using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services
{
    public partial class t_b_factoryServices : BaseServices<t_b_factory>, It_b_factoryServices
    {
        private readonly It_b_factoryRepository dal;
        public t_b_factoryServices(It_b_factoryRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
