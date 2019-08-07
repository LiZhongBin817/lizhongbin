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
        /// 显示用户数据
        /// </summary>
        /// <param name="FUserName"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowUserInfoDate")]
        [AllowAnonymous]//允许所有都访问
        [EnableCors("LimitRequests")]
        public async Task<TableModel<List<ShowUser>>> ShowUserInfoDate(string FUserName, string LoginName)
        {
            var temp = await _sys_usermanageServices.Showsys_userinfo(FUserName, LoginName);
            List<ShowUser> users = new List<ShowUser>();
            //用户角色表
            List<sys_role> role = await _sys_roleServices.Query();
            for (int i = 0; i < temp.Count(); i++)
            {
                ShowUser user = new ShowUser();
                user.ID = temp[i].ID;
                user.FUserNumber = temp[i].FUserNumber;
                user.FUserName = temp[i].FUserName;
                user.LoginName = temp[i].LoginName;
                user.LoginPassWord = temp[i].LoginPassWord;
                user.RealName = temp[i].RealName;
                if (temp[i].Sex == 1)
                {
                    user.Sex = "女";
                }
                else
                {
                    user.Sex = "男";
                }
                user.MobilePhone = temp[i].MobilePhone;
                user.Adress = temp[i].Adress;
                user.Email = temp[i].Email;
                if (temp[i].UserType == 0)
                {
                    user.UserType = "超级管理员";
                }
                else if (temp[i].UserType == 1)
                {
                    user.UserType = "管理员";
                }
                else if (temp[i].UserType == 2)
                {
                    user.UserType = "普通员工";
                }
                user.roles = role;
                users.Add(user);
            }
            return new TableModel<List<ShowUser>>()
            {
                code = 0,
                msg = "ok",
                count = 10,
                data = users
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
        [HttpGet]
        [Route("AddUser")]
        [AllowAnonymous]//允许所有都访问
        public async Task<int> AddUser(string JsonDate, string roleid)
        {
            AddUser Add = Common.Helper.JsonHelper.GetObject<AddUser>(JsonDate);
            //将用户基本信息存入用户表
            sys_userinfo user = new sys_userinfo();
            user.FUserNumber = Add.FUserNumber;
            user.FUserName = Add.FUserName;
            user.LoginName = Add.LoginName;
            user.LoginPassWord = Add.LoginPassWord;
            user.RealName = Add.RealName;
            user.Sex = Add.Sex;
            user.MobilePhone = Add.MobilePhone;
            user.Adress = Add.Adress;
            user.Email = Add.Email;
            user.UseStatus = 0;//正常
            user.UserType = Add.UserType;
            user.CreateTime = DateTime.Now;
            user.CreatePeople = "李芊";
            //取到添加进来的用户ID
            int UserID = await _sysuserinfoservices.Add(user);
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
        [HttpPost]
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
        /// <param name="IDs"></param>
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
        public async Task<TableModel<EditDate>> ModifyData(int ID)
        {
            sys_userinfo Modify = await _sysuserinfoservices.QueryById(ID);
            //用户角色表
            List<sys_role> role = await _sys_roleServices.Query();
            //用户关联的数据
            List<sys_user_role_mapper> role_mapper = await _sys_user_role_mapperServices.Query(c => c.UserID == ID);
            EditDate edit = new EditDate();
            edit.FUserNumber = Modify.FUserNumber;
            edit.FUserName = Modify.FUserName;
            edit.LoginName = Modify.LoginName;
            edit.RealName = Modify.RealName;
            edit.Sex = Modify.Sex;
            edit.MobilePhone = Modify.MobilePhone;
            edit.Adress = Modify.Adress;
            edit.Email = Modify.Email;
            edit.UserType = Modify.UserType;
            edit.roles = role;
            edit.role_mapper = role_mapper;
            return new TableModel<EditDate>()
            {
                code = 0,
                msg = "ok",
                count = 1,
                data = edit
            };
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="JsonDate"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ModifyUserInfo")]
        [AllowAnonymous]//允许所有都访问 
        public async Task<int> ModifyUserInfo(string JsonDate, string roleid)
        {
            //将数据转换成JSON对象
            EditUser Edit = Common.Helper.JsonHelper.GetObject<EditUser>(JsonDate);
            //用ID查出要编辑的数据
            int ID = Edit.ID;
            sys_userinfo user = await _sysuserinfoservices.QueryById(ID);
            user.FUserNumber = Edit.FUserNumber;
            user.FUserName = Edit.FUserName;
            user.LoginName = Edit.LoginName;
            user.RealName = Edit.RealName;
            user.Sex = Edit.Sex;
            user.MobilePhone = Edit.MobilePhone;
            user.Adress = Edit.Adress;
            user.Email = Edit.Email;
            user.UserType = Edit.UserType;
            user.CreateTime = DateTime.Now;
            user.CreatePeople = "李芊";
            //编辑用户表信息
            await _sysuserinfoservices.Update(user);
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

