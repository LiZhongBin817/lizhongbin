using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository;
using Microsoft.AspNetCore.Authorization;
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
        #endregion
        public UserManageController(Isys_userinfoServices sysuserinfo, Isys_usermanageServices sysusermanage, Isys_user_role_mapperServices sys_user_role_mapper)
        {
            _sysuserinfoservices = sysuserinfo;
            _sys_usermanageServices = sysusermanage;
            _sys_user_role_mapperServices = sys_user_role_mapper;
        }
        #region  用户管理
        /// <summary>
        /// 显示用户数据
        /// </summary>
        /// <param name="FUserName"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowUserInfo")]
        [AllowAnonymous]//允许所有都访问
        public async Task<List<sys_userinfo>> ShowUserInfo(string FUserName, string LoginName)
        {
            return await _sys_usermanageServices.Showsys_userinfo(FUserName,LoginName);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="FUserNumber">用户编号</param>
        /// <param name="FUserName">用户名称</param>
        /// <param name="LoginName">登录账号</param>
        /// <param name="LoginPassWord">登录密码,默认密码为12345</param>
        /// <param name="RealName">真实姓名</param>
        /// <param name="Sex">性别，默认为男</param>
        /// <param name="MobilePhone">手机号</param>
        /// <param name="Adress">地址</param>
        /// <param name="Email">邮箱</param>
        /// <param name="UserType">登陆用户类型0--超级管理员;1--管理员;2--普通用户(默认)</param>
        /// <param name="roleid">用户角色ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("AddUser")]
        [AllowAnonymous]//允许所有都访问
        public  async Task<int> AddUser(string FUserNumber, string FUserName, string LoginName, string LoginPassWord, string RealName, short Sex, string MobilePhone, string Adress, string Email, short UserType,string roleid)
        {
            //将用户基本信息存入用户表
            sys_userinfo user = new sys_userinfo();
            user.FUserNumber = FUserNumber;
            user.FUserName = FUserName;
            user.LoginName = LoginName;
            user.LoginPassWord = LoginPassWord;
            user.RealName = RealName;
            user.Sex = Sex;
            user.MobilePhone = MobilePhone;
            user.Adress = Adress;
            user.Email = Email;
            user.UseStatus = 0;//正常
            user.UserType = UserType;
            user.CreateTime = DateTime.Now;
            user.CreatePeople = "李芊";
            //取到添加进来的用户ID
            int UserID= await _sysuserinfoservices.Add(user);
            //将用户角色关联进用户角色关联表
            string[] Roleid = roleid.Split(',');
            List<int> RoleID = new List<int>();
            for(int i=0;i< Roleid.Count();i++)
            {
                RoleID.Add(Convert.ToInt32(Roleid[i]));
            }
            List<sys_user_role_mapper> role_mapper = new List<sys_user_role_mapper>();
            for(int i=0;i< RoleID.Count();i++)
            {
                sys_user_role_mapper mapper = new sys_user_role_mapper();
                mapper.UserID = UserID;
                mapper.RoleID = RoleID[i];
                mapper.CreateTime = DateTime.Now;
                mapper.CreatePeople = "李芊";
                role_mapper.Add(mapper);
            }
            //批量添加
            return await _sys_user_role_mapperServices.Add(role_mapper);
        }

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
            sys_userinfo user =await _sysuserinfoservices.QueryById(ID);
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
            for(int i=0;i< users.Count();i++)
            {
                users[i].DeleteFlag = 1;
            }
            return await _sysuserinfoservices.Updateable(users);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="FUserNumber">用户编号</param>
        /// <param name="FUserName">用户名称</param>
        /// <param name="LoginName">登录账号</param>
        /// <param name="LoginPassWord">登录密码(默认密码为12345)</param>
        /// <param name="RealName">真实姓名</param>
        /// <param name="Sex">性别，默认为男</param>
        /// <param name="MobilePhone">手机号</param>
        /// <param name="Adress">地址</param>
        /// <param name="Email">邮箱</param>
        /// <param name="UserType">登陆用户类型0--超级管理员;1--管理员;2--普通用户(默认)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ModifyUserInfo")]
        [AllowAnonymous]//允许所有都访问
        public async Task<bool> ModifyUserInfo(int ID,string FUserNumber, string FUserName, string LoginName, string LoginPassWord, string RealName, short Sex, string MobilePhone, string Adress, string Email, short UserType)
        {
            sys_userinfo user = await _sysuserinfoservices.QueryById(ID);
            user.FUserNumber = FUserNumber;
            user.FUserName = FUserName;
            user.LoginName = LoginName;
            user.LoginPassWord = LoginPassWord;
            user.RealName = RealName;
            user.Sex = Sex;
            user.MobilePhone = MobilePhone;
            user.Adress = Adress;
            user.Email = Email;
            user.UserType = UserType;
            user.CreateTime = DateTime.Now;
            user.CreatePeople = "李芊";
            return await _sysuserinfoservices.Update(user);
        }
        #endregion
    }
}

