using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services
{
    public partial class t_b_natureServices : BaseServices<t_b_nature>, It_b_natureServices
    {
        private readonly It_b_natureRepository dal;
        public t_b_natureServices(It_b_natureRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
