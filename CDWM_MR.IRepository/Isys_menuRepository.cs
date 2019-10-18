using CDWM_MR.IRepository.Base;
using CDWM_MR.Model.Models;
using System.Threading.Tasks;

namespace CDWM_MR.IRepository.Content
{
    public partial interface Isys_menuRepository : IBaseRepository<sys_menu>
    {
        Task<object> Getsinglemenuinfo(int sid);
    }
}
