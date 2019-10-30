using CDWM_MR.IRepository;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;

namespace CDWM_MR.Repository
{
    public partial class t_b_watermodelRepository : BaseRepository<t_b_watermodel>, It_b_watermodelRepository
    {
        public t_b_watermodelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
