using CDWM_MR.IServices.BASE;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IServices
{
    public interface Isys_usermanageServices:IBaseServices<sys_userinfo>
    {
        Task<List<sys_userinfo>> Showsys_userinfo(string FUserName, string LoginName);
    }
}
