using CDWM_MR.IRepository.Base;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IRepository.Content
{
    public partial interface Isys_role_menuRepository:IBaseRepository<sys_role_menu>
    {
        Task<List<sys_role_menu>> GetRoleOperation();
    }
}
