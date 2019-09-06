using CDWM_MR.Common;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
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
        readonly Isys_operationRepository OperationRepository;
        readonly Isys_operationRepository sys_operationDal;
        readonly IRedisHelper redis;

        public sysManageServices(Isys_userinfoRepository userinfodal, Isys_menuRepository sysmenudal, Isys_role_menuRepository sysrolemenudal, Isys_user_role_mapperRepository sysuserroledal, Isys_roleRepository sys_roledal,Isys_operationRepository sys_operationdal,Isys_operationRepository operationRepository, IRedisHelper redisHelper)
        {
            UserinfoDal = userinfodal;
            SysMenuDal = sysmenudal;
            SysRoleMenuDal = sysrolemenudal;
            SysUserRoleDal = sysuserroledal;
            base.BaseDal = userinfodal;
            sys_roleDal = sys_roledal;
            OperationRepository = operationRepository;
            sys_operationDal = sys_operationdal;
            redis = redisHelper;
        }

        #region  菜单管理
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
                roleids = string.Join(',', userlist.Where(c => c.UserID == userid).Select(c => c.sysRole.id));
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
                var olist = Childmenu(item.id, MenuList);
                if (olist.Count == 0)
                {
                    sys_menu model = MenuList.Find(c => c.id == item.id);
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
        #endregion

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
        public async Task<int> ModifyInfo(string JsonDate, int[] roleid)
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
                updatetime = DateTime.Now,
                updatepeople = "李芊"
            }, c => c.id == Edit.id);
            int ID = Edit.id;
            //将用户角色关联表数据先删除
            await SysUserRoleDal.DeleteTable(c => c.UserID == ID);
            //编辑用户角色表信息
            List<sys_user_role_mapper> user_role = new List<sys_user_role_mapper>();
            for (int i = 0; i < roleid.Count(); i++)
            {
                sys_user_role_mapper us = new sys_user_role_mapper();
                us.UserID = ID;
                us.RoleID = roleid[i];
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
        public async Task<int> AddUserinfo(string JsonDate, int[] roleid)
        {
            sys_userinfo Add = Common.Helper.JsonHelper.GetObject<sys_userinfo>(JsonDate);
            //编号：CDWM_MR******
            var date = await UserinfoDal.Query();
            //取到最后一个用户的ID
            int id = date[date.Count() - 1].id + 1;
            string number = $"CDWM_MR{id.ToString().PadLeft(6, '0')}";
            Add.FUserNumber = number;
            Add.createtime = DateTime.Now;
            Add.createpeople = "李芊";
            //取到添加进来的用户ID
            int UserID = await UserinfoDal.Add(Add);
            //将用户角色关联进用户角色关联表
            //编辑用户角色表信息
            List<sys_user_role_mapper> user_role = new List<sys_user_role_mapper>();
            for (int i = 0; i < roleid.Count(); i++)
            {
                sys_user_role_mapper us = new sys_user_role_mapper();
                us.UserID = UserID;
                us.RoleID = roleid[i];
                us.CreatePeople = "李芊";
                us.CreateTime = DateTime.Now;
                user_role.Add(us);
            }
            //添加用户角色表
            return await SysUserRoleDal.Add(user_role);
        }
        #endregion

        #region 角色管理
        //用来存放返回数据的
        List<object> commonlist = new List<object>();
        #region 根据角色id查询菜单id
        public async Task<TableModel<object>> GetMenuID(int id)
        {
            //创建一个集合存放菜单id
            List<int> menuData = new List<int>();
            //根据角色id去角色菜单表里面查询菜单
            var data = await SysRoleMenuDal.Query(c=>c.RoleID==id);
            foreach (var item in data)
            {
                menuData.Add(item.MenuID);
            }
            return new TableModel<object>()
            {
                code = 0,
                msg = "查询成功",
                count = menuData.Count,
                data = menuData,
            };
        }
        #endregion

        #region 获取菜单列表
        /// <summary>
        /// 获得菜单树
        /// </summary>
        /// <param name="id">父级菜单的id</param>
        /// <returns></returns>
        public async Task<TableModel<object>> GetMenu()
        {
            List<sys_menu> plist = new List<sys_menu>();
            //查询菜单表里面的所有数据
            var alllist = await SysMenuDal.Query();
            List<sys_menu> query = new List<sys_menu>();
            foreach (var item in alllist)
            {
                if (item.ParentID == 0)
                {
                    query.Add(item);
                }
            }
            foreach (var item in query)
            {
                sys_menu menu = new sys_menu();
                menu.id = Convert.ToInt32(item.id);
                menu.ParentID = 0;
                menu.MenuName = item.MenuName;
                menu.MenuUrl = item.MenuUrl;
                menu.MenuLevel = item.MenuLevel;
                menu.MenuType = item.MenuType;
                menu.MenuNumber = item.MenuNumber;
                plist.Add(menu);
                var menuinfo = new { id = menu.id, pid = menu.ParentID, title = menu.MenuName, url = menu.MenuUrl ,menuType=menu.MenuType,menuNumber=menu.MenuNumber};
                commonlist.Add(menuinfo);
            }
            var data = await SysMenuDal.Query();//拿到菜单表里的所有数据
            await GetChildNode(data,plist);
            return new TableModel<object>()
            {
                code = 0,
                msg = "查询成功",
                count = commonlist.Count,
                data = commonlist
            };
        }
        /// <summary>
        /// 遍历查询子节点
        /// </summary>
        /// <param name="Plist"></param>
        /// <returns></returns>
        public async Task GetChildNode(List<sys_menu> data, List<sys_menu> Plist)
        {           
            foreach (var item in Plist)
            {
                List<sys_menu> childlist = new List<sys_menu>();
                foreach (var item1 in data)
                {

                    if (item.id == item1.ParentID)
                    {
                        childlist.Add(item1);
                    }

                }
                if (childlist.Count == 0)
                {
                    continue;
                }
                List<sys_menu> plist = new List<sys_menu>();
                foreach (var item2 in childlist)
                {
                    sys_menu menu = new sys_menu();
                    menu.id = item2.id;
                    menu.ParentID = item.id;
                    menu.MenuName = item2.MenuName;
                    menu.MenuUrl = item2.MenuUrl;
                    menu.MenuLevel = item2.MenuLevel;
                    menu.MenuType = item2.MenuType;
                    menu.MenuNumber = item2.MenuNumber;
                    plist.Add(menu);
                    var menuinfo = new { id = menu.id, pid = menu.ParentID, title = menu.MenuName, url = menu.MenuUrl,menuType=menu.MenuType, menuNumber = menu.MenuNumber };
                    commonlist.Add(menuinfo);
                }
                await GetChildNode(data,plist);

            }
        }
        #endregion

        #region 判断角色菜单
        /// 判断该角色是否存在该菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public async Task<TableModel<object>> Jude(int RoleID, int MenuID)
        {
            var data = await SysRoleMenuDal.Query(c=>c.RoleID==RoleID&&c.MenuID==MenuID);
            if (data.Count == 0)
            {
                return new TableModel<object>()
                {
                    code = 0,
                    msg = "NO",
                    count = data.Count,
                    data = data
                };
            }
            else
            {
                return new TableModel<object>()
                {
                    code = 0,
                    msg = "OK",
                    count = data.Count,
                    data = data,
                };
            }
        }
        #endregion

        #region  保存权限
        public async Task<TableModel<object>> SaveOperation(int RoleID, string MenuID)
        {
            //创建一个list集合用来存放返回渲染在复选框里面的值
            List<int> list = new List<int>();
            string[] MenuId = MenuID.Split(',');
            //存放操作用户角色表的对象
            List<sys_role_menu> commonData = new List<sys_role_menu>();
            //查询角色菜单表的所有角色与RoleID相等的数据
            var allData = await SysRoleMenuDal.Query(c => c.RoleID == RoleID);
            //将权限表的信息查出来
            var operationData = await OperationRepository.Query();           
            foreach (var item in MenuId)
            {
                //查询到菜单对应的权限
                var Data = operationData.FindAll(c => c.MenuID.ToString() == item);
                if (Data.Count == 0)
                {
                    return new TableModel<object>()
                    {
                        code = 0,
                        msg = "NO",
                        count = 0,
                        data = ""
                    };
                }
                else
                {

                    foreach (var item1 in Data)
                    {
                        sys_role_menu role_Menu = new sys_role_menu();
                        role_Menu.RoleID = RoleID;
                        role_Menu.MenuID = Convert.ToInt32(item);
                        role_Menu.OperationID = item1.id;
                        role_Menu.createtime = DateTime.Now;
                        role_Menu.createpeople = "李忠斌";
                        commonData.Add(role_Menu);
                    }
                }

            }
            //删除角色ID对应的所有数据
            foreach (var item in allData)
            {
                int ID = item.id;
                await SysRoleMenuDal.DeleteById(ID);
            }
            await SysRoleMenuDal.Add(commonData);
            //用来返回到前端时渲染在复选框里面的菜单数据
            List<sys_role_menu> menuData = await SysRoleMenuDal.Query(c => c.RoleID == RoleID);
            foreach (var item in menuData)
            {
                list.Add(item.MenuID);
            }
            return new TableModel<object>()
            {
                code = 0,
                msg = "OK",
                count = 0,
                data = list
            };
        }

        #endregion

        #region 渲染权限
        /// <summary>
        /// 通过菜单ID查询权限ID
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public async Task<TableModel<object>> GetOperation(int RoleID, int menuID)
        {
            //查出菜单id相同的数据
            var data = await SysRoleMenuDal.Query(c=>c.RoleID==RoleID&&c.MenuID==menuID);
            //查出权限表里面的所有数据
            var alldata = await OperationRepository.Query();
            //创建一个集合来存放权限id
            List<int> opID = new List<int>();
            foreach (var item in data)
            {
                foreach (var item1 in alldata)
                {
                    if (item.OperationID == item1.id)
                    {
                        sys_operation operation = new sys_operation();
                        operation.id = item1.id;
                        operation.OperationName = item1.OperationName;
                        operation.OperationType = item1.OperationType;
                        operation.remark = item.remark;
                        var operationInfo = new { opID = operation.id, opName = operation.OperationName, opType = operation.OperationType, opRemark = operation.remark };
                        commonlist.Add(operationInfo);
                    }
                }
            }
            return new TableModel<object>()
            {
                code = 0,
                msg = "查询成功",
                count = commonlist.Count,
                data = commonlist
            };
        }
        #endregion

        #region 修改权限
        public async Task<TableModel<sys_operation>> EditOperations(int RoleID, int MenuID, string OperationID)
        {
            //用于在数据库更新操作的集合
            List<sys_role_menu> commonlist1 = new List<sys_role_menu>();
            //查询权限表，将权限表中的Remark全置为0
            bool b = await SysRoleMenuDal.Update(c => new sys_role_menu
            {
                remark = "0"
            }, c => c.MenuID == MenuID && c.RoleID == RoleID);
            if (OperationID == null)
            {
                return new TableModel<sys_operation>()
                {
                    code = 0,
                    msg = "OK",
                    count = 0,
                    data = null,
                };
            }
            else
            {
                string[] OpId = OperationID.Split(',');
                if (b)
                {
                    //查出角色对应的菜单有哪些权限ID
                    List<sys_role_menu> role_Menus = await SysRoleMenuDal.Query(c => c.RoleID == RoleID && c.MenuID == MenuID);
                    //查出权限表中的所有数据
                    List<sys_operation> operations = await OperationRepository.Query();
                    foreach (var item in role_Menus)
                    {
                        foreach (var item1 in OpId)
                        {
                            if (item.OperationID.ToString() == item1)
                            {
                                sys_role_menu data = new sys_role_menu();
                                data.id = item.id;
                                data.RoleID = item.RoleID;
                                data.MenuID = item.MenuID;
                                data.OperationID = item.OperationID;
                                data.createtime = item.createtime;
                                data.createpeople = item.createpeople;
                                data.updatepeople = item.updatepeople;
                                data.updatetime = item.updatetime;
                                data.updatepeople = item.updatepeople;
                                data.remark = "1";
                                commonlist1.Add(data);
                            }
                        }
                    }
                    await SysRoleMenuDal.Updateable(commonlist1);
                }
            }

            return new TableModel<sys_operation>()
            {
                code = 0,
                msg = "OK",
                count = 0,
                data = null,
            };
        }
        #endregion

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
                var count = alllist.FindAll(m => m.ParentID == item.id);
                if (count.Count != 0)//存在子菜单
                {
                    var addchildrenlist = await GetTree(item.id);//递归遍历子类 
                    var infor = new { title = item.MenuName, id = item.id, children = addchildrenlist };
                    jsonolist.Add(infor);
                }
                else if (count.Count == 0)//无子菜单
                {
                    //查找ID菜单
                    var firstmenu = alllist.FindAll(c => c.id == item.id);
                    sys_menu sys_Menu = new sys_menu();
                    foreach (var a in firstmenu)
                    {
                        sys_Menu.MenuName = a.MenuName;
                        sys_Menu.id = a.id;
                    }
                    var menuinfo = new { title = sys_Menu.MenuName, id = sys_Menu.id };
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
                int ID = alllist[alllist.Count - 1].id + 1;
                sys_menu menu = Common.Helper.JsonHelper.GetObject<sys_menu>(json);
                var count = alllist.FindAll(c => c.MenuName == menu.MenuName);
                if (count.Count != 0)
                {
                    return false;
                }
                menu.createtime = DateTime.Now;
                menu.createpeople = "1";
                menu.MenuType = menu.MenuLevel;
                menu.MenuNumber = "M00" +ID;
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
                            updatepeople = "1",
                            updatetime = DateTime.Now,
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
                            updatepeople = "1",
                            updatetime = DateTime.Now,
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
                            updatepeople = "1",
                            updatetime = DateTime.Now,
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
                            updatepeople = "1",
                            updatetime = DateTime.Now,
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
