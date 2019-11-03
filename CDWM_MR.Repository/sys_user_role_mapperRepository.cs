using CDWM_MR.IRepository;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Repository.Content
{

    public partial class sys_user_role_mapperRepository : BaseRepository<sys_user_role_mapper>, Isys_user_role_mapperRepository
    {
        /// <summary>
        /// 获取用户下所有的角色
        /// </summary>
        /// <returns></returns>
        public async Task<List<sys_user_role_mapper>> GetUserRolestr()
        {
            return await Task.Run(() => Db.Queryable<sys_user_role_mapper>()
            .Mapper(t => t.sysUserInfo,t => t.UserID)
            .Mapper(t => t.sysRole,t => t.RoleID).ToList());
        }
    }
}
