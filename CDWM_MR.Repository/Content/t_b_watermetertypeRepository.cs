using CDWM_MR.IRepository;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;

namespace CDWM_MR.Repository
{
    public partial class t_b_watermetertypeRepository : BaseRepository<t_b_watermetertype>, It_b_watermetertypeRepository
    {
        public t_b_watermetertypeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
