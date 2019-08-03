using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Services
{
    public class sys_usermanageServices:BaseServices<sys_userinfo>,Isys_usermanageServices
    {
        private readonly Isys_usermanageRepository dal;
        public sys_usermanageServices(Isys_usermanageRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }

        public async Task<List<sys_userinfo>> Showsys_userinfo(string FUserName, string LoginName)
        {
            return await this.dal.Showsys_userinfo(FUserName, LoginName);
        }
    }
}
