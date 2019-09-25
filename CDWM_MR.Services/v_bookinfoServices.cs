using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services
{
    public partial class v_bookinfoServices : BaseServices<v_bookinfo>, Iv_bookinfoServices
    {
        private readonly Iv_bookinfoRepository dal;
        public v_bookinfoServices(Iv_bookinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;

        }


    }
}
