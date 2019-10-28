using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Repository.Content
{
    public partial class t_b_usersRepository:BaseRepository<t_b_users>, It_b_usersRepository
    {
        public t_b_usersRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
