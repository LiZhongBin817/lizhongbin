using CDWM_MR.IRepository.Base;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDWM_MR.IRepository.Content
{
    public partial interface Isys_role_menuRepository : IBaseRepository<sys_role_menu>
    {
        Task<List<sys_role_menu>> GetRoleOperation();

        Task<List<sys_role_menu>> GetbtninfoData();
    }
}
