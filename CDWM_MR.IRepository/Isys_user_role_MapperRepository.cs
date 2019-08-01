using CDWM_MR.IRepository.Base;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IRepository.Content
{
    public partial interface Isys_user_role_mapperRepository: IBaseRepository<sys_user_role_mapper>
    {
        Task<List<sys_user_role_mapper>> GetUserRolestr();
    }
}
