using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers
{
    public class SysManangeController : Controller
    {
        #region 相关变量
        readonly Isys_userinfoServices _sysuserinfoservices;
        readonly IsysManageServices _sysManageServices;
        readonly Isys_user_role_mapperServices _sys_user_role_mapperServices;
        readonly Isys_roleServices _sys_roleServices;
        readonly Isys_interface_infoServices _Isys_interface_infoServices;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysuserinfo"></param>
        /// <param name="sysusermanage"></param>
        /// <param name="sys_user_role_mapper"></param>
        /// <param name="sys_role"></param>
        public SysManangeController(Isys_userinfoServices sysuserinfo, IsysManageServices sysusermanage, Isys_user_role_mapperServices sys_user_role_mapper, Isys_roleServices sys_role, Isys_interface_infoServices Isys_interface_info)
        {
            _sysuserinfoservices = sysuserinfo;
            _sysManageServices = sysusermanage;
            _sys_user_role_mapperServices = sys_user_role_mapper;
            _sys_roleServices = sys_role;
            _Isys_interface_infoServices = Isys_interface_info;
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
        public async Task<TableModel<object>> ShowUserInfoDate(string FUserName, string LoginName, int page = 1, int limit = 5)
        {
            PageModel<object> user = new PageModel<object>();
            #region lambda拼接式
            Expression<Func<sys_userinfo, bool>> wherelambda = c=>c.DeleteFlag!=1;
            {
                wherelambda = PredicateExtensions.And<sys_userinfo>(wherelambda, c => c.FUserName==FUserName);
            }
            if (!string.IsNullOrEmpty(LoginName))
            {
                wherelambda = PredicateExtensions.And<sys_userinfo>(wherelambda, c => c.LoginName == LoginName);
            }
            #endregion
            Expression<Func<sys_userinfo, object>> expression = c => new
            {
                ID = c.ID,
                FUserNumber = c.FUserNumber,
                FUserName = c.FUserName,
                LoginName = c.LoginName,
                LoginPassWord = c.LoginPassWord,
                RealName = c.RealName,
                Sex = c.Sex == 1 ? "女" : "男",
                MobilePhone=c.MobilePhone,
                Adress=c.Adress,
                Email=c.Email,
                UserType=c.UserType == 0 ? "超级管理员" : (c.UserType == 1 ? "管理员" : "普通员工"),
        };
            user = await _sysuserinfoservices.QueryPage(wherelambda, expression, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = user.dataCount,
                data = user
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
            return await _sysManageServices.AddUserinfo(JsonDate, roleid);
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
        [AllowAnonymous]//允许所有都访问 
        public async Task<int> ModifyUserInfo(string JsonDate, string roleid)
        {
            return await _sysManageServices.ModifyInfo(JsonDate, roleid);
        }
        #endregion

        #endregion

        #region 接口管理
        #region 显示接口数据
        /// <summary>
        /// 显示接口数据
        /// </summary>
        /// <param name="InterfaceUrl"></param>
        /// <param name="InterfaceName"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InterfaceInfoShow")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> InterfaceInfoShow(string InterfaceUrl, string InterfaceName, int page = 1, int limit = 10)
        {
            PageModel<object> Interface = new PageModel<object>();
            #region lambda拼接式
            Expression<Func<sys_interface_info,bool>>wherelambda=c=>true;
            if(!string .IsNullOrEmpty(InterfaceUrl))
            {
                wherelambda = PredicateExtensions.And<sys_interface_info>(wherelambda, c => c.InterfaceUrl == InterfaceUrl);
            }
            if(!string.IsNullOrEmpty(InterfaceName))
            {
                wherelambda = PredicateExtensions.And<sys_interface_info>(wherelambda, c => c.InterfaceName == InterfaceName);
            }
            #endregion
            Expression<Func<sys_interface_info, object>> expression = c => new
            {
                ID = c.ID,
                InterfaceUrl = c.InterfaceUrl,
                InterfaceName = c.InterfaceName,
                OperationVersion = c.OperationVersion,
                ExternalInterface = c.ExternalInterface,
                Verify = c.Verify,
                Remark = c.Remark
            };
            Interface = await _Isys_interface_infoServices.QueryPage(wherelambda, expression, page, limit, "");
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
        [AllowAnonymous]
        public async Task<MessageModel<object>> AddInterface(string JsonData)
        {
           sys_interface_info Jsondata = Common.Helper.JsonHelper.GetObject<sys_interface_info>(JsonData);
            #region 判重
            String InterfaceName = Jsondata.InterfaceName;
            string InterfaceUrl = Jsondata.InterfaceUrl;
            var listInter= await _Isys_interface_infoServices.Query(c => c.InterfaceName == InterfaceName|| c.InterfaceUrl == InterfaceUrl);
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
        [AllowAnonymous]//允许所有都访问 
        public async Task<MessageModel<object>> ModifyInterface(string JsonData,int ID)
        {
            sys_interface_info Jsondata = Common.Helper.JsonHelper.GetObject<sys_interface_info>(JsonData);
            Jsondata.ID = ID;
            #region 判重
            Expression<Func<sys_interface_info, bool>> wherelambda = c =>c.ID!=ID&&(c.InterfaceName == Jsondata.InterfaceName|| c.InterfaceUrl == Jsondata.InterfaceUrl);
            var listQuery = await _Isys_interface_infoServices.Query(wherelambda);
            string massage = "";
            if (listQuery.Count != 0)
            {
                return new MessageModel<object>()
                {
                    data = null,
                    code = 0,
                    msg = "The same TnterfaceUrl or InterfaceName exists"
                };
            }
            #endregion
            massage = await _Isys_interface_infoServices.Update(Jsondata) == true ? "ok" : "error";
            return new MessageModel<object>()
            {
                data = null,
                code = 0,
                msg = "ok"
            };
        }
        #endregion
        #endregion
    }
}
