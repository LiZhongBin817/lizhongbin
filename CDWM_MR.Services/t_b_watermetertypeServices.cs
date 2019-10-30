using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services
{
    public partial class t_b_watermetertypeServices : BaseServices<t_b_watermetertype>, It_b_watermetertypeServices
    {
        private readonly It_b_watermetertypeRepository dal;
        public t_b_watermetertypeServices(It_b_watermetertypeRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
