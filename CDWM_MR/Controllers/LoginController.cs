using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 登陆控制器--无权限控制
    /// </summary>
    [Produces("application/json")]
    [Route("api/Login")]
    [AllowAnonymous]
    public class LoginController : Controller
    {

        #region 相关变量
        readonly Isys_userinfoServices _sysuserinfoservices;
        readonly Isys_role_menuServices _sysrolemenu;
        #endregion
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="sysuserinfo"></param>
        /// <param name="sysrolemenu"></param>
        public LoginController(Isys_userinfoServices sysuserinfo,Isys_role_menuServices sysrolemenu)
        {
            _sysuserinfoservices = sysuserinfo;
            _sysrolemenu = sysrolemenu;
        }

        /// <summary>
        /// 测试第一次
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("First")]
        public async Task<int> First()
        {
            sys_userinfo addmodel = new sys_userinfo();
            addmodel.FUserName = "admin";
            addmodel.FUserNumber = "00001";
            addmodel.FUserNumber = null;
            addmodel.RealName = "张三";
            return await _sysuserinfoservices.Add(addmodel);
        }


    }
}