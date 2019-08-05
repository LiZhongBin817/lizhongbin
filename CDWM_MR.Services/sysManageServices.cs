using CDWM_MR.Common;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Services
{
    public class sysManageServices:BaseServices<sys_userinfo>,IsysManageServices
    {
        readonly Isys_userinfoRepository UserinfoDal;
        readonly Isys_menuRepository SysMenuDal;
        readonly Isys_role_menuRepository SysRoleMenuDal;
        readonly Isys_user_role_mapperRepository SysUserRoleDal;


        public sysManageServices(Isys_userinfoRepository userinfodal,Isys_menuRepository sysmenudal,Isys_role_menuRepository sysrolemenudal,Isys_user_role_mapperRepository sysuserroledal)
        {
            UserinfoDal = userinfodal;
            SysMenuDal = sysmenudal;
            SysRoleMenuDal = sysrolemenudal;
            SysUserRoleDal = sysuserroledal;
            base.BaseDal = userinfodal;
        }

        /// <summary>
        /// 获取角色菜单权限表
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<sys_role_menu>> GetRoleOperation()
        {
            return await SysRoleMenuDal.GetRoleOperation();
        }

        /// <summary>
        /// 获取用户对应所有角色的名称
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<string> GetuserRole(int userid)
        {
            var userlist = await SysUserRoleDal.GetUserRolestr();
            string roleids = string.Empty;
            if (userlist != null)
            {
                roleids = string.Join(',', userlist.Where(c => c.UserID == userid).Select(c => c.sysRole.ID));
            }

            return roleids;
        }

        /// <summary>
        /// (最多支持三级菜单)
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 60)]
        public async Task<List<object>> GetMenuTree()
        {
            var allMenu = await SysMenuDal.Query();//查询出菜单表所有数据
            var menulist = Childmenu(0, allMenu);
            return menulist;
        }

        private List<object> Childmenu(int Parentid,List<sys_menu> MenuList)
        {
            List<object> rMenulist = new List<object>();
            List<sys_menu> allChildren = MenuList.FindAll(c => c.ParentID == Parentid);

            if (allChildren.Count == 0)
            {
                return rMenulist;
            }
            foreach (var item in allChildren)
            {
                var olist = Childmenu(item.ID,MenuList);
                if (olist.Count == 0)
                {
                    sys_menu model = MenuList.Find(c => c.ID == item.ID);
                    if (model.MenuType == 2)
                    {
                        var addmodel = new { title = model.MenuName, icon = model.MenuImg, jump = model.MenuUrl };
                        rMenulist.Add(addmodel);
                    }
                }
                else
                {
                    var addlist = new { title = item.MenuName, icon = item.MenuImg, list = olist };
                    rMenulist.Add(addlist);
                }
            }
            return rMenulist;
        }

    }
}
