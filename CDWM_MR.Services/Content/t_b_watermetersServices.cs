using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;

namespace CDWM_MR.Services.Content
{
    public partial class t_b_watermetersServices : BaseServices<t_b_watermeters>, It_b_watermetersServices
    {
        private readonly It_b_watermetersRepository dal;
        public t_b_watermetersServices(It_b_watermetersRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
