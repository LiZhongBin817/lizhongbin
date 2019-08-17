using CDWM_MR.IRepository.Base;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IRepository
{
    public interface Isys_usermanageRepository: IBaseRepository<sys_userinfo>
    {
        Task<List<sys_userinfo>> Showsys_userinfo(string FUserName, string LoginName);

    }
}
