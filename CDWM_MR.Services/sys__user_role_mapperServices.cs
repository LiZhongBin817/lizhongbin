using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Services.Content
{
    public partial class sys_user_role_mapperServices:BaseServices<sys_user_role_mapper>, Isys_user_role_mapperServices
    {
        /// <summary>
        /// 获取用户对应所有角色的名称
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<string> GetuserRole(int userid)
        {
            var userlist = await this.dal.GetUserRolestr();
            string roleids = string.Empty;
            if (userlist != null)
            {
                roleids = string.Join(',',userlist.Where(c => c.UserID == userid).Select(c => c.sysRole.RoleName));
            }

            return roleids;
        }

    }
}
