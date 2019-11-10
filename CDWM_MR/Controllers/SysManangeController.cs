using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.Common.HttpContextUser;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 系统管理
    /// </summary>
    [Route("api/SysManange")]
    [EnableCors("LimitRequests")]
    [AllowAnonymous]
    public class SysManangeController : Controller
    {
        #region 相关变量
        readonly Isys_userinfoServices _sysuserinfoservices;
        readonly IsysManageServices _sysManageServices;
        readonly Isys_user_role_mapperServices _sys_user_role_mapperServices;
        readonly Isys_roleServices _sys_roleServices;
        readonly Isys_interface_infoServices _Isys_interface_infoServices;
        readonly Isys_operationServices _sys_OperationServices;
        readonly Isys_role_menuServices _Role_MenuServices;
        readonly IUser _user;
        readonly Iv_interfaceServices _InterfaceServices;
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysuserinfo"></param>
        /// <param name="sysusermanage"></param>
        /// <param name="sys_user_role_mapper"></param>
        /// <param name="sys_role"></param>
        /// <param name="Isys_interface_info"></param>
        /// <param name="sysrolemenu"></param>
        /// <param name="sys_OperationServices"></param>
        /// <param name="user"></param>
        /// <param name="iv_InterfaceServices"></param>
        public SysManangeController(Isys_userinfoServices sysuserinfo, IsysManageServices sysusermanage, Isys_user_role_mapperServices sys_user_role_mapper, Isys_roleServices sys_role, Isys_interface_infoServices Isys_interface_info,Isys_role_menuServices sysrolemenu,Isys_operationServices sys_OperationServices, IUser user, Iv_interfaceServices iv_InterfaceServices)
        {
            _sysuserinfoservices = sysuserinfo;
            _sysManageServices = sysusermanage;
            _sys_user_role_mapperServices = sys_user_role_mapper;
            _sys_roleServices = sys_role;
            _Isys_interface_infoServices = Isys_interface_info;
            _sys_OperationServices = sys_OperationServices;
            _Role_MenuServices = sysrolemenu;
            _user = user;
            _InterfaceServices = iv_InterfaceServices;
        }

        #region  用户管理

        #region 显示
        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="FUserName">用户名称</param>
        /// <param name="LoginName">登录名</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页显示数量</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowUserInfoDate")]       
        public async Task<TableModel<object>> ShowUserInfoDate(string FUserName, string LoginName, int page = 1, int limit = 5)
        {
            PageModel<object> user = new PageModel<object>();
            #region lambda拼接式
            Expression<Func<sys_userinfo, bool>> wherelambda = c => c.DeleteFlag != 1;
            if (!string.IsNullOrEmpty(FUserName))
            {
                wherelambda = PredicateExtensions.And<sys_userinfo>(wherelambda, c => c.FUserName .Contains( FUserName));
            }
            if (!string.IsNullOrEmpty(LoginName))
            {
                wherelambda = PredicateExtensions.And<sys_userinfo>(wherelambda, c => c.LoginName.Contains( LoginName));
            }
            #endregion
            Expression<Func<sys_userinfo, object>> expression = c => new
            {
                ID = c.id,
                FUserNumber = c.FUserNumber,
                FUserName = c.FUserName,
                LoginName = c.LoginName,
                LoginPassWord = c.LoginPassWord,
                RealName = c.RealName,
                Sex = c.Sex == 1 ? "女" : "男",
                MobilePhone = c.MobilePhone,
                Adress = c.Adress,
                Email = c.Email,
                UserType = c.UserType
            };
            user = await _sysuserinfoservices.QueryPage(wherelambda, expression, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = user.dataCount,
                data = user.data
            };
        }
        #endregion

        #region  添加用户信息
        /// <summary>
        /// 给添加界面的角色选择复选框传值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("roleDate")]       
        public async Task<TableModel<List<sys_role>>> roleDate()
        {
            //用户角色表
            List<sys_role> role = await _sys_roleServices.Query();
            return new TableModel<List<sys_role>>
            {
                code = 0,
                msg = "ok",
                count = 1,
                data = role
            };
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="JsonDate"></param>
        /// <param name="roleid">用户角色ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUser")]     
        public async Task<TableModel<object>> AddUser(string JsonDate, int[] roleid)
        {
            await _sysManageServices.AddUserinfo(JsonDate, roleid);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                count = 0,
                data = null
            };
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteUser")]      
        public async Task<TableModel<object>> DeleteUser(int ID)
        {
            sys_userinfo user = await _sysuserinfoservices.QueryById(ID);
            user.DeleteFlag = 1;
            await _sysuserinfoservices.Update(user);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                count = 0,
                data = null
            };
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteUsers")]      
        public async Task<TableModel<object>> DeleteUsers(string ids)
        {
            object[] IDs = ids.Split(',');
            List<sys_userinfo> users = await _sysuserinfoservices.QueryByIDs(IDs);
            for (int i = 0; i < users.Count(); i++)
            {
                users[i].DeleteFlag = 1;
            }
            await _sysuserinfoservices.Updateable(users);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                count = 0,
                data = null
            };
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 向编辑界面传参
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ModifyData")]
        
        public async Task<TableModel<object>> ModifyData(int ID)
        {
            return await _sysManageServices.Modify(ID);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="JsonDate"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyUserInfo")]
       
        public async Task<TableModel<object>> ModifyUserInfo(string JsonDate, int[] roleid)
        {
            await _sysManageServices.ModifyInfo(JsonDate, roleid);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                count = 0,
                data = null
            };
        }
        #endregion

        #endregion

        #region 接口管理
        #region 显示接口数据
        /// <summary>
        /// 显示接口数据
        /// </summary>
        /// <param name="InterfaceName"></param>
        /// <param name="menuid"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InterfaceInfoShow")]        
        public async Task<TableModel<object>> InterfaceInfoShow(string InterfaceName, int menuid, int page = 1, int limit = 10)
        {
            PageModel<object> Interface = new PageModel<object>();
            #region lambda拼接式
            Expression<Func<v_interface, bool>>wherelambda=c=>true;
            if(!string .IsNullOrEmpty(InterfaceName))
            {
                wherelambda = PredicateExtensions.And<v_interface>(wherelambda, c => c.InterfaceName.Contains(InterfaceName));
            }
            if(menuid!=0)
            {
                wherelambda = PredicateExtensions.And<v_interface>(wherelambda, c => c.menuid==menuid);
            }
            #endregion
            Expression<Func<v_interface, object>> expression = c => new
            {
                ID = c.id,
                InterfaceUrl = c.InterfaceUrl,
                InterfaceName = c.InterfaceName,
                OperationVersion = c.OperationVersion,
                MenuName=c.MenuName,
                MenuID=c.menuid,
                ExternalInterface = c.ExternalInterface,
                Verify = c.Verify,
                Remark = c.Remark
            };
            Interface = await _InterfaceServices.QueryPage(wherelambda, expression, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = Interface.dataCount,
                data = Interface.data
            };
        }
        #endregion

        #region 添加接口
        /// <summary>
        /// 添加接口
        /// </summary>
        /// <param name="JsonData">前台传来的Json对象</param>
        /// <returns>总数目</returns>
        [HttpPost]
        [Route("AddInterface")]      
        public async Task<MessageModel<object>> AddInterface(string JsonData)
        {
           sys_interface_info Jsondata = Common.Helper.JsonHelper.GetObject<sys_interface_info>(JsonData);
            #region 判重
            String InterfaceName = Jsondata.InterfaceName;
            string InterfaceUrl = Jsondata.InterfaceUrl;
            var listInter= await _Isys_interface_infoServices.Query(c => c.InterfaceName.Contains(InterfaceName)|| c.InterfaceUrl.Contains(InterfaceUrl));
            var msg = "";
            if (listInter.Count()>0)
            {
                return new MessageModel<object>()
                {
                    code = 0,
                    data = null,
                    msg = "error"
                };
            }
            #endregion
            msg= await _Isys_interface_infoServices.Add(Jsondata)>0?"ok":"error";
            return new MessageModel<object>()
            {
                code = 0,
                data = null,
                msg = msg
            };
        }
        #endregion


        #region 编辑接口
        /// <summary>
        /// 修改接口信息
        /// </summary>
        /// <param name="JsonData">修改后的接口对象</param>
        /// <param name="ID">编辑的ID</param>
        /// <returns>返回是否成功</returns>
        [HttpPost]
        [Route("ModifyInterface")]       
        public async Task<MessageModel<object>> ModifyInterface(string JsonData,int ID)
        {
            sys_interface_info Jsondata = Common.Helper.JsonHelper.GetObject<sys_interface_info>(JsonData);
            Jsondata.ID = ID;
            string massage = await _Isys_interface_infoServices.Update(Jsondata) == true ? "ok" : "error";
            return new MessageModel<object>()
            {
                data = null,
                code = 0,
                msg = "ok"
            };
        }
        #endregion
        #endregion

        #region 菜单管理

        #region 生成菜单树
        /// <summary>
        /// 生成菜单树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTrees")]        
        public async Task<MessageModel<object>> GetTrees()
        {
            var data=await _sysManageServices.GetTree(0);
            return new MessageModel<object>() {
                code=0,
                msg="ok",
                data=data
            };
           
        }
        #endregion

        #region 显示菜单信息
        /// <summary>
        /// 展示点击菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowInfo")]       
        public async Task<TableModel<object>> ShowInfo(int id)
        {
            var data = await _sysManageServices.GetMenuInfo(id);
            return new TableModel<object>()
            {
               code=0,
               msg="ok",
               data=data
            };
        }
        #endregion

        #region 添加菜单
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveMenu")]       
        public async Task<TableModel<object>> SaveMenu(string json)
        {
            if (await _sysManageServices.AddMenu(json))
            {
                return new TableModel<object>
                {
                    code = 0,
                    msg = "添加成功",
                    data=null

                };
            }
                return new TableModel<object>
                {
                    code = 1,
                    msg = "添加失败",
                    data=null
                    
                };
        }
        #endregion

        #region 删除菜单
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleMenu")]        
        public async Task<TableModel<object>> DeleMenu(int id)
        {
            if (await _sysManageServices.DelMenu(id))
            {
                return new TableModel<object>
                {
                    code = 0,
                    msg="删除成功",
                    data=null
                };
            }
                return new TableModel<object>
                {
                    code = 0,
                    msg = "删除失败",
                    data=null
                };
        }
        #endregion

        #region 权限分配
        /// <summary>
        /// 菜单权限分配
        /// </summary>
        /// <param name="adddata"></param>
        /// <param name="deldata"></param>
        /// <param name="modifdata"></param>
        /// <param name="seedata"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AuthorityManagement")]       
        public async Task<TableModel<object>> AuthorityManagement(string adddata,string deldata,string modifdata,string seedata,int id)
        {
            if (await _sysManageServices.Power(adddata,deldata,modifdata,seedata,id))
            {
                return new TableModel<object>
                {
                    code = 0,
                    msg = "分配成功",
                    data=null
                };
            }
                return new TableModel<object>
                {
                    code = 1,
                    msg = "分配失败",
                    data=null
                };
            
        }
        #endregion

        #region 权限信息
        /// <summary>
        /// 菜单权限信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInfo")]       
        public async Task<TableModel<object>> GetInfo(int id)
        {
            try
            {
                var alllist = await _sys_OperationServices.Query(c => c.MenuID == id);
                var addlist = alllist.FindAll(c => c.OperationType == 0);
                var dellist = alllist.FindAll(c => c.OperationType == 1);
                var modlist = alllist.FindAll(c => c.OperationType == 2);
                var seelist = alllist.FindAll(c => c.OperationType == 3);
                return new TableModel<object>
                {
                    code = 0,
                    msg="ok",
                    data = new { addstr = addlist, delstr = dellist, modifstr = modlist, seestr = seelist }
                };
            }
            catch
            {

                return new TableModel<object>
                {
                    code = 1,
                    msg="false",
                    data=null
                };
            }
            

        }
        #endregion
        #endregion

        #region 角色管理

        #region 添加角色
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="RoleName">角色名称</param>
        /// <returns></returns>
        [HttpGet]
        [Route("AddRole")]      
        public async Task<MessageModel<object>> AddRole( string RoleName)
        {
            sys_role role = new sys_role();
            #region 自动生成编号
            //查询表中最后一条数据
             var data = await _sys_roleServices.Query();
            string RoleNumber = data[data.Count - 1].RoleNumber;
            int NewRoleNumber = Convert.ToInt32(RoleNumber.Substring(RoleNumber.Length - 1, 1))+1;
            #endregion
            role.RoleNumber = "RN-000"+NewRoleNumber.ToString();
            role.RoleName = RoleName;
            role.createpeople =_user.Name;
            role.createtime = DateTime.Now;
            role.DeleteFlag = 0;
            foreach (var item in data)
            {
                if (item.RoleName == RoleName)
                {
                    return new MessageModel<object>()
                    {
                        code = 0,
                        msg = "该角色已经存在",
                        data = ""

                    };
                }
            }
            var id = await _sys_roleServices.Add(role);
            return new MessageModel<object>()
            {
                code = 0,
                msg = "添加成功",
                data = new
                {
                    RoleID = id.ObjToString(),
                }
            };
        }
        #endregion

        #region 删除角色
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleName">删除的角色名称</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteRole")]        
        public async Task<MessageModel<object>> DeleteRole(string RoleName)
        {
            //寻找到RoleName相等的数据对应的ID
            List<sys_role> list = await _sys_roleServices.Query(t => t.RoleName == RoleName);
            bool a = false;
            bool b = false;
            bool c = false;
            int SID = list[0].id;//存放角色ID
            a = await _sys_roleServices.DeleteById(SID); //删除角色表中对应的id数据
            //在表sys_role_menu中根据角色id找到对应的进行删除
            b = await _Role_MenuServices.DeleteTable(t => t.RoleID == SID);
            c = await _sys_user_role_mapperServices.DeleteTable(t => t.RoleID == SID);
            var data = new MessageModel<object>();
            if (a == true)
            {
                return new MessageModel<object>()
                {
                    code = 0,
                    msg = "删除成功",
                    data = ""
                };
            }
            else
            {
                return new MessageModel<object>()
                {
                    code = 0,
                    msg = "该角色名称不存在",
                    data = ""
                };
            }
        }
        #endregion

        #region 编辑角色
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="RoleName">旧角色名称</param>
        /// <param name="NewRoleName">新角色名称</param>
        /// <returns></returns>
        [HttpGet]
        [Route("EditRole")]      
        public async Task<MessageModel<object>> EditRole(string RoleName, string NewRoleName)
        {
            //查找到角色表中的所有数据
            List<sys_role> list = await _sys_roleServices.Query(c => c.RoleName == NewRoleName);
            if (list.Count > 0)
            {
                return new MessageModel<object>()
                {
                    code = 1001,
                    msg = "重复",
                    data = NewRoleName
                };
            }
            var b = await _sys_roleServices.Update(c => new sys_role
            {
                RoleName = NewRoleName
            }, c => c.RoleName == RoleName);
            return new MessageModel<object>()
            {
                code = 0,
                msg = "OK",
                data = NewRoleName
            };
        }
        #endregion

        #region 展示角色名称
        /// <summary>
        /// 显示角色数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowRole")]       
        public async Task<TableModel<object>> ShowRole()
        {
            //查询角色表中的所有数据
            List<sys_role> list = await _sys_roleServices.Query();
            //存放角色
            List<int> roleID = new List<int>();
            //声明一个list集合用来装所有的角色名称
            List<string> roleName = new List<string>();
            foreach (var item in list)
            {
                roleName.Add(item.RoleName);
                roleID.Add(item.id);
            }
            return new TableModel<object>()
            {
                code = 0,
                msg = "查询成功",
                count = roleName.Count,
                data = new
                {
                    roleID = roleID,
                    roleName = roleName,
                },


            };

        }
        #endregion

        #region 根据角色id查询菜单id
        /// <summary>
        /// 根据角色id查询菜单id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenuID")]      
        public async Task<TableModel<object>> GetMenuID(int id)
        {
            return await _sysManageServices.GetMenuID(id);
        }
        #endregion

        #region 获得菜单列表
        /// <summary>
        /// 获得菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenu")]       
        public async Task<TableModel<object>> GetMenu()
        {
            return await _sysManageServices.GetMenu();
        }
        #endregion

        #region 判断角色菜单
        /// <summary>
        /// 判断角色菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Jude")]        
        public async Task<TableModel<object>> Jude(int RoleID, int MenuID)
        {
            return await _sysManageServices.Jude(RoleID, MenuID);
        }
        #endregion

        #region 保存权限
        /// <summary>
        /// 为角色分配菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SaveOperation")]      
        public async Task<TableModel<object>> SaveOperation(int RoleID, string MenuID)
        {
            return await _sysManageServices.SaveOperation(RoleID, MenuID);
        }
        #endregion

        #region 渲染权限
        /// <summary>
        /// 渲染权限
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="menuID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOperation")]        
        public async Task<TableModel<object>> GetOperation(int RoleID, int menuID,int judgetype)
        {
            return await _sysManageServices.GetOperation(RoleID, menuID, judgetype);
        }
        #endregion

        #region 权限修改
        /// <summary>
        /// 权限修改
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="MenuID"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("EditOperations")]        
        public async Task<TableModel<sys_operation>> EditOperations(int RoleID, int MenuID, string OperationID, int judgetype)
        {
            return await _sysManageServices.EditOperations(RoleID, MenuID, OperationID, judgetype);
        }
        #endregion
        #endregion
    }
}
