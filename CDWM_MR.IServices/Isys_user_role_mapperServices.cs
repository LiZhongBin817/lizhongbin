using CDWM_MR.IServices.BASE;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IServices.Content
{
    public partial interface Isys_user_role_mapperServices:IBaseServices<sys_user_role_mapper>
    {
        Task<string> GetuserRole(int userid);
    }
}
