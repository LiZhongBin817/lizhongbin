 using CDWM_MR.IRepository;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Repository.Content
{
    public class sys_usermanageRepository : BaseRepository<sys_userinfo>, Isys_usermanageRepository
    {
        /// <summary>
        /// 显示用户信息数据
        /// </summary>
        /// <param name="FUserName">昵称</param>
        /// <param name="LoginName">登录名</param>
        /// <returns></returns>
        public async Task<List<sys_userinfo>> Showsys_userinfo(string FUserName,string LoginName)
        {
            return await Db.Queryable<sys_userinfo>().WhereIF(!string.IsNullOrEmpty(FUserName), c => c.FUserName == FUserName)
                .WhereIF(!string.IsNullOrEmpty(LoginName), c => c.LoginName == LoginName).Where(c=>c.DeleteFlag!=1).ToListAsync();
        }

        
    }
}
