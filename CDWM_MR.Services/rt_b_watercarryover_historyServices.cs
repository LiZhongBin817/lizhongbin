using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services
{
    public partial class rt_b_watercarryover_historyServices : BaseServices<rt_b_watercarryover_history>, Irt_b_watercarryover_historyServices
    {
        private readonly Irt_b_watercarryover_historyRpository dal;
        public rt_b_watercarryover_historyServices(rt_b_watercarryover_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
