using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Services.Content
{
    public partial class sys_role_menuServices:BaseServices<sys_role_menu>,Isys_role_menuServices
    {
        public async Task<List<sys_role_menu>> GetRoleOperation()
        {
            return await this.dal.GetRoleOperation();
        }
    }
}
