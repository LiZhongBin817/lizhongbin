using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//用户管理控制器
namespace CDWM_MR.Controllers
{
    public class UserManageController : Controller
    {
        #region 相关变量
        readonly Isys_userinfoServices _sysuserinfoservices;
        readonly Isys_usermanageServices _sys_usermanageServices;
        readonly Isys_user_role_mapperServices _sys_user_role_mapperServices;
        readonly Isys_roleServices _sys_roleServices;
        #endregion

        //构造函数
        public UserManageController(Isys_userinfoServices sysuserinfo, Isys_usermanageServices sysusermanage, Isys_user_role_mapperServices sys_user_role_mapper, Isys_roleServices sys_role)
        {
            _sysuserinfoservices = sysuserinfo;
            _sys_usermanageServices = sysusermanage;
            _sys_user_role_mapperServices = sys_user_role_mapper;
            _sys_roleServices = sys_role;
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
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<List<ShowUser>>> ShowUserInfoDate(string FUserName, string LoginName, int page=1, int limit=5)
        {
            PageModel<sys_userinfo> user = new PageModel<sys_userinfo>();
            if (!string.IsNullOrEmpty(FUserName) && !string.IsNullOrEmpty(LoginName))
            {
                user = await _sysuserinfoservices.QueryPage(c => c.FUserName == FUserName && c.LoginName == LoginName && c.DeleteFlag != 1, page, limit, "");
            }
            else if (string.IsNullOrEmpty(FUserName) && !string.IsNullOrEmpty(LoginName))
            {
                user = await _sysuserinfoservices.QueryPage(c => c.LoginName == LoginName && c.DeleteFlag != 1, page, limit, "");
            }
            else if (!string.IsNullOrEmpty(FUserName) && string.IsNullOrEmpty(LoginName))
            {
                user = await _sysuserinfoservices.QueryPage(c => c.FUserName == FUserName && c.DeleteFlag != 1, page, limit, "");
            }
            else
            {
                user = await _sysuserinfoservices.QueryPage(c => c.DeleteFlag != 1, page, limit, "");
            }
            List<ShowUser> userlist = new List<ShowUser>();
            for (int i = 0; i < user.data.Count(); i++)
            {
                ShowUser users = new ShowUser();
                users.ID = user.data[i].ID;
                users.FUserNumber = user.data[i].FUserNumber;
                users.FUserName = user.data[i].FUserName;
                users.LoginName = user.data[i].LoginName;
                users.LoginPassWord = user.data[i].LoginPassWord;
                users.RealName = user.data[i].RealName;
                if (user.data[i].Sex == 1)
                {
                    users.Sex = "女";
                }
                else
                {
                    users.Sex = "男";
                }
                users.MobilePhone = user.data[i].MobilePhone;
                users.Adress = user.data[i].Adress;
                users.Email = user.data[i].Email;
                users.UserType = user.data[i].UserType == 0 ? "超级管理员" : (user.data[i].UserType == 1 ? "管理员" : "普通员工");
                users.page = user.page;//当前页标
                users.PageSize = user.PageSize;//每页大小
                userlist.Add(users);
            }
            return new TableModel<List<ShowUser>>()
            {
                code = 0,
                msg = "ok",
                count = user.dataCount,
                data = userlist
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
        [AllowAnonymous]//允许所有都访问
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
        [AllowAnonymous]//允许所有都访问
        public async Task<int> AddUser(string JsonDate, string roleid)
        {
            sys_userinfo Add = Common.Helper.JsonHelper.GetObject<sys_userinfo>(JsonDate);
            //编号：CDWM_MR******
            
            var date = await _sysuserinfoservices.Query();
            //取到最后一个用户的ID
            int id = date[date.Count() - 1].ID + 1;
            string number = $"CDWM_MR{id.ToString().PadLeft(6, '0')}";
            Add.FUserNumber = number;
            Add.CreateTime = DateTime.Now;
            Add.CreatePeople = "李芊";
            //取到添加进来的用户ID
            int UserID = await _sysuserinfoservices.Add(Add);
            //将用户角色关联进用户角色关联表
            //编辑用户角色表信息
            string[] RoleName = roleid.Split(',');
            List<int> RoleID = new List<int>();
            for (int i = 0; i < RoleName.Count()-1; i++)
            {
                RoleID.Add(Convert.ToInt32(RoleName[i]));
            }
            List<sys_user_role_mapper> user_role = new List<sys_user_role_mapper>();
            for (int i = 0; i < RoleID.Count(); i++)
            {
                sys_user_role_mapper us = new sys_user_role_mapper();
                us.UserID = UserID;
                us.RoleID = RoleID[i];
                us.CreatePeople = "李芊";
                us.CreateTime = DateTime.Now;
                user_role.Add(us);
            }
            //添加用户角色表
            return await _sys_user_role_mapperServices.Add(user_role);
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
        [AllowAnonymous]//允许所有都访问
        public async Task<bool> DeleteUser(int ID)
        {
            sys_userinfo user = await _sysuserinfoservices.QueryById(ID);
            user.DeleteFlag = 1;
            return await _sysuserinfoservices.Update(user);
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteUsers")]
        [AllowAnonymous]//允许所有都访问
        public async Task<bool> DeleteUsers(string ids)
        {
            object[] IDs = ids.Split(',');
            List<sys_userinfo> users = await _sysuserinfoservices.QueryByIDs(IDs);
            for (int i = 0; i < users.Count(); i++)
            {
                users[i].DeleteFlag = 1;
            }
            return await _sysuserinfoservices.Updateable(users);
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
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<sys_user_role_mapper>> ModifyData(int ID)
        {
            //sys_userinfo Modify = await _sysuserinfoservices.QueryById(ID);
            //用户角色表
            //List<sys_role> role = await _sys_roleServices.Query();
            //用户关联的数据
            sys_user_role_mapper role_mapper = await _sys_user_role_mapperServices.Query(ID);
            //role_mapper.sysUserInfo.FUserName;

            //EditDate edit = new EditDate();
            //edit.FUserName = Modify.FUserName;
            //edit.LoginName = Modify.LoginName;
            //edit.RealName = Modify.RealName;
            //edit.Sex = Modify.Sex;
            //edit.MobilePhone = Modify.MobilePhone;
            //edit.Adress = Modify.Adress;
            //edit.Email = Modify.Email;
            //edit.UserType = Modify.UserType;
            //edit.roles = ;
            //edit.role_mapper = role_mapper;
            return new TableModel<sys_user_role_mapper>()
            {
                code = 0,
                msg = "ok",
                count = 1,
                data = role_mapper
            };
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="JsonDate"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyUserInfo")]
        [AllowAnonymous]//允许所有都访问 
        public async Task<int> ModifyUserInfo(string JsonDate, string roleid)
        {
            //将数据转换成JSON对象
            sys_userinfo Edit = Common.Helper.JsonHelper.GetObject<sys_userinfo>(JsonDate);
            await _sysuserinfoservices.Update(c => new sys_userinfo {
                FUserName = Edit.FUserName,
                LoginName=Edit.LoginName,
                RealName=Edit.RealName,
                Sex=Edit.Sex,
                MobilePhone=Edit.MobilePhone,
                Adress=Edit.Adress,
                Email=Edit.Email,
                UserType=Edit.UserType,
                UpdateTime=DateTime.Now,
                UpdatePeople="李芊"
            }, c => c.ID == Edit.ID);
            int ID = Edit.ID;
            //将用户角色关联表数据先删除
            await _sys_user_role_mapperServices.DeleteTable(c => c.UserID == ID);
            //编辑用户角色表信息
            string[] RoleName = roleid.Split(',');
            List<int> RoleID = new List<int>();
            for (int i = 0; i < RoleName.Count()-1;i++)
            {
                RoleID.Add(Convert.ToInt32(RoleName[i]));
            }
            List<sys_user_role_mapper> user_role = new List<sys_user_role_mapper>();
            for (int i = 0; i < RoleID.Count(); i++)
            {
                sys_user_role_mapper us = new sys_user_role_mapper();
                us.UserID = ID;
                us.RoleID = RoleID[i];
                us.CreatePeople = "李芊";
                us.CreateTime = DateTime.Now;
                user_role.Add(us);
            }
            //添加用户角色表
            return await _sys_user_role_mapperServices.Add(user_role);
        }
        #endregion

        #endregion
    }
}

