using CDWM_MR.IServices.BASE;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IServices.Content
{
    public partial interface Isys_role_menuServices:IBaseServices<sys_role_menu>
    {
        Task<List<sys_role_menu>> GetRoleOperation();
    }
}
