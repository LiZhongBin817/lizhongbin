using System;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;

namespace CDWM_MR.Repository.Content
{
    /// <summary>
    /// t_b_areasRepository
    /// </summary>
    public partial class t_b_areasRepository : BaseRepository<t_b_areas>, It_b_areasRepository
    {
        public t_b_areasRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
