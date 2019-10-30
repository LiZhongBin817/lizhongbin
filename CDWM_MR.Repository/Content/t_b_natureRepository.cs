using CDWM_MR.IRepository;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;

namespace CDWM_MR.Repository
{
    public partial class t_b_natureRepository : BaseRepository<t_b_nature>, It_b_natureRepository
    {
        public t_b_natureRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
