using CDWM_MR.Common;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using CDWM_MR_Common.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Services
{
    public class sysManageServices : BaseServices<sys_userinfo>, IsysManageServices
    {
        readonly Isys_userinfoRepository UserinfoDal;
        readonly Isys_menuRepository SysMenuDal;
        readonly Isys_role_menuRepository SysRoleMenuDal;
        readonly Isys_user_role_mapperRepository SysUserRoleDal;
        readonly Isys_roleRepository sys_roleDal;
        readonly Isys_operationRepository sys_operationDal;
        readonly IRedisHelper redis;

        public sysManageServices(Isys_userinfoRepository userinfodal, Isys_menuRepository sysmenudal, Isys_role_menuRepository sysrolemenudal, Isys_user_role_mapperRepository sysuserroledal, Isys_roleRepository sys_roledal,Isys_operationRepository sys_operationdal, IRedisHelper redisHelper)
        {
            UserinfoDal = userinfodal;
            SysMenuDal = sysmenudal;
            SysRoleMenuDal = sysrolemenudal;
            SysUserRoleDal = sysuserroledal;
            base.BaseDal = userinfodal;
            sys_roleDal = sys_roledal;
            sys_operationDal = sys_operationdal;
            redis = redisHelper;
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
        public async Task<List<sys_menu>> GetMenuTree()
        {
            var allMenu = await SysMenuDal.Query();//查询出菜单表所有数据
            //var menulist = Childmenu(0, allMenu);
            return allMenu;
        }

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="Parentid"></param>
        /// <param name="MenuList"></param>
        /// <returns></returns>
        public List<object> Childmenu(int Parentid, List<sys_menu> MenuList)
        {
            List<object> rMenulist = new List<object>();
            List<sys_menu> allChildren = MenuList.FindAll(c => c.ParentID == Parentid);

            if (allChildren.Count == 0)
            {
                return rMenulist;
            }
            foreach (var item in allChildren)
            {
                var olist = Childmenu(item.ID, MenuList);
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

        #region 用户管理
        /// <summary>
        /// 给编辑界面传值
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<TableModel<object>> Modify(int ID)
        {
            //查出编辑人员
            sys_userinfo Modify = await UserinfoDal.QueryById(ID);
            //用户角色表
            List<sys_role> rolelist = await sys_roleDal.Query();
            //用户关联的数据
            string roleid = await GetuserRole(ID);
            string[] Roles = roleid.Split(',');
            List<int> roles = new List<int>();
            for (int i = 0; i < Roles.Count(); i++)
            {
                roles.Add(Convert.ToInt32(Roles[i]));
            }
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = 1,
                data = new
                {
                    modifydate = Modify,
                    roles = rolelist,
                    role_mapper = roles
                }
            };
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="JsonDate"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public async Task<int> ModifyInfo(string JsonDate, string roleid)
        {
            //将数据转换成JSON对象
            sys_userinfo Edit = Common.Helper.JsonHelper.GetObject<sys_userinfo>(JsonDate);
            await UserinfoDal.Update(c => new sys_userinfo
            {
                FUserName = Edit.FUserName,
                LoginName = Edit.LoginName,
                RealName = Edit.RealName,
                Sex = Edit.Sex,
                MobilePhone = Edit.MobilePhone,
                Adress = Edit.Adress,
                Email = Edit.Email,
                UserType = Edit.UserType,
                UpdateTime = DateTime.Now,
                UpdatePeople = "李芊"
            }, c => c.ID == Edit.ID);
            int ID = Edit.ID;
            //将用户角色关联表数据先删除
            await SysUserRoleDal.DeleteTable(c => c.UserID == ID);
            //编辑用户角色表信息
            string[] RoleName = roleid.Split(',');
            List<sys_user_role_mapper> user_role = new List<sys_user_role_mapper>();
            for (int i = 0; i < RoleName.Count() - 1; i++)
            {
                sys_user_role_mapper us = new sys_user_role_mapper();
                us.UserID = ID;
                us.RoleID = Convert.ToInt32(RoleName[i]);
                us.CreatePeople = "李芊";
                us.CreateTime = DateTime.Now;
                user_role.Add(us);
            }
            //添加用户角色表
            return await SysUserRoleDal.Add(user_role);
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="JsonDate"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public async Task<int> AddUserinfo(string JsonDate, string roleid)
        {
            sys_userinfo Add = Common.Helper.JsonHelper.GetObject<sys_userinfo>(JsonDate);
            //编号：CDWM_MR******
            var date = await UserinfoDal.Query();
            //取到最后一个用户的ID
            int id = date[date.Count() - 1].ID + 1;
            string number = $"CDWM_MR{id.ToString().PadLeft(6, '0')}";
            Add.FUserNumber = number;
            Add.CreateTime = DateTime.Now;
            Add.CreatePeople = "李芊";
            //取到添加进来的用户ID
            int UserID = await UserinfoDal.Add(Add);
            //将用户角色关联进用户角色关联表
            //编辑用户角色表信息
            string[] RoleName = roleid.Split(',');
            List<sys_user_role_mapper> user_role = new List<sys_user_role_mapper>();
            for (int i = 0; i < RoleName.Count() - 1; i++)
            {
                sys_user_role_mapper us = new sys_user_role_mapper();
                us.UserID = UserID;
                us.RoleID = Convert.ToInt32(RoleName[i]);
                us.CreatePeople = "李芊";
                us.CreateTime = DateTime.Now;
                user_role.Add(us);
            }
            //添加用户角色表
            return await SysUserRoleDal.Add(user_role);
        }
        #endregion

        #region 菜单管理
        /// <summary>
        /// 生成菜单树,仅三层
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<object>> GetTree(int id)
        {
            //创建一个集合
            List<object> jsonolist = new List<object>();
            //查询所有的数据
            var alllist = await SysMenuDal.Query();
            //查询id
            var treelist = alllist.FindAll(c => c.ParentID == id);
            foreach (var item in treelist)
            {
                //查询该条数据是否拥有子类
                var count = alllist.FindAll(m => m.ParentID == item.ID);
                if (count.Count != 0)//存在子菜单
                {
                    var addchildrenlist = await GetTree(item.ID);//递归遍历子类 
                    var infor = new { title = item.MenuName, id = item.ID, children = addchildrenlist };
                    jsonolist.Add(infor);
                }
                else if (count.Count == 0)//无子菜单
                {
                    //查找ID菜单
                    var firstmenu = alllist.FindAll(c => c.ID == item.ID);
                    sys_menu sys_Menu = new sys_menu();
                    foreach (var a in firstmenu)
                    {
                        sys_Menu.MenuName = a.MenuName;
                        sys_Menu.ID = a.ID;
                    }
                    var menuinfo = new { title = sys_Menu.MenuName, id = sys_Menu.ID };
                    jsonolist.Add(menuinfo);
                }
            }
            return jsonolist;
        }
        /// <summary>
        /// 获取点击菜单信息
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <returns></returns>
        public async Task<object> GetMenuInfo(int id)
        {
            return await SysMenuDal.Getsinglemenuinfo(id);           
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public async Task<bool> AddMenu(string json)
        {
            try
            {
                await redis.KeyDeleteAsync("CDWM_MR_sysManageServices:GetMenuTree");
                var alllist = await SysMenuDal.Query();
                sys_menu menu = Common.Helper.JsonHelper.GetObject<sys_menu>(json);
                var count = alllist.FindAll(c => c.MenuName == menu.MenuName);
                if (count.Count != 0)
                {
                    return false;
                }
                menu.CreateTime = DateTime.Now;
                menu.CreatePeople = "1";
                menu.MenuType = menu.MenuLevel;
                menu.MenuNumber = "M0" + menu.ID;
                await SysMenuDal.Add(menu);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        /// <summary>
        /// 根据MenuID删除菜单及sys_role_menu中的role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DelMenu(int id)
        {
            if (await SysMenuDal.DeleteById(id))
            {
                var list = await SysRoleMenuDal.Query(c => c.MenuID == id);
                await SysRoleMenuDal.DeleteTable(c=>c.MenuID==id);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 权限分配
        /// </summary>
        /// <param name="adddata"></param>
        /// <param name="deldata"></param>
        /// <param name="modifdata"></param>
        /// <param name="seedata"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Power(string adddata, string deldata, string modifdata, string seedata, int id)
        {
            try
            {
                //先将MenuID=id的权限MenuID改为0
                await sys_operationDal.Update(c => new sys_operation { MenuID = 0 }, c => c.MenuID == id);
                List<sys_operation> PageList = new List<sys_operation>();
                if (!string.IsNullOrEmpty(adddata))
                {
                    string[] AddArry = adddata.Split(',');
                    foreach (var item in AddArry)
                    {
                        var LinkUrl = item.Split('+')[0];
                        var operationname = item.Split('+')[1];
                        await sys_operationDal.Update(c => new sys_operation
                        {
                            LinkUrl = LinkUrl,
                            OperationName = operationname,
                            UpdatePeople = "1",
                            UpdateTime = DateTime.Now,
                            MenuID = id,
                            OperationType = 0
                        }, c => c.LinkUrl == LinkUrl);
                    }
                }
                if (!string.IsNullOrEmpty(deldata))
                {
                    string[] DelArry = deldata.Split(',');
                    foreach (var item in DelArry)
                    {
                        var LinkUrl = item.Split('+')[0];
                        var operationname = item.Split('+')[1];
                        await sys_operationDal.Update(c => new sys_operation
                        {
                            LinkUrl = LinkUrl,
                            OperationName = operationname,
                            UpdatePeople = "1",
                            UpdateTime = DateTime.Now,
                            MenuID = id,
                            OperationType = 1
                        }, c => c.LinkUrl == LinkUrl);
                    }
                }
                if (!string.IsNullOrEmpty(modifdata))
                {
                    string[] ModifiedArry = modifdata.Split(',');
                    foreach (var item in ModifiedArry)
                    {
                        var LinkUrl = item.Split('+')[0];
                        var operationname = item.Split('+')[1];
                        await sys_operationDal.Update(c => new sys_operation
                        {
                            LinkUrl = LinkUrl,
                            OperationName = operationname,
                            UpdatePeople = "1",
                            UpdateTime = DateTime.Now,
                            MenuID = id,
                            OperationType = 2
                        }, c => c.LinkUrl == LinkUrl);
                    }
                }
                if (!string.IsNullOrEmpty(seedata))
                {
                    string[] SelArry = seedata.Split(',');
                    foreach (var item in SelArry)
                    {
                        var LinkUrl = item.Split('+')[0];
                        var operationname = item.Split('+')[1];
                        await sys_operationDal.Update(c => new sys_operation
                        {
                            LinkUrl = LinkUrl,
                            OperationName = operationname,
                            UpdatePeople = "1",
                            UpdateTime = DateTime.Now,
                            MenuID = id,
                            OperationType = 3
                        }, c => c.LinkUrl == LinkUrl);

                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

    }
}
