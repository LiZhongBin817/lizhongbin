using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Repository.Content
{
    public partial class sys_role_menuRepository : BaseRepository<sys_role_menu>, Isys_role_menuRepository
    {
        /// <summary>
        /// 获取角色菜单权限表
        /// </summary>
        /// <returns></returns>
        public async Task<List<sys_role_menu>> GetRoleOperation()
        {
            var temp= await Task.Run(() => Db.Queryable<sys_role_menu>().Where(c => c.judgetype == 1)
            .Mapper(t => t.Role,t => t.RoleID)
            .Mapper(t => t.interfaceinfo, t => t.OperationID)
            .Mapper(t => t.Menu,t => t.MenuID).ToList());
            return temp;
        }

        /// <summary>
        /// 获取按钮信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<sys_role_menu>> GetbtninfoData()
        {
            return await Task.Run(() => Db.Queryable<sys_role_menu>().Where(c => c.judgetype == 0)
                .Mapper(t => t.Operation, t => t.OperationID).ToList());
        }
    }
}
