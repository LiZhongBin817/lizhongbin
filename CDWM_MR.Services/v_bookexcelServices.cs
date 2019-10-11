using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services
{
    public partial class v_bookexcelServices : BaseServices<v_bookexcel>, Iv_bookexcelServices
    {
        private readonly Iv_bookexcelRepository dal;
        public v_bookexcelServices(Iv_bookexcelRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
