using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CDWM_MR.AuthHelper;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// APP登陆接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AppLoginController : ControllerBase
    {

        #region 相关变量
        readonly Imr_b_readerServices mrreader;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="reader"></param>
        public AppLoginController(Imr_b_readerServices reader)
        {
            mrreader = reader;
        }


        /// <summary>
        /// App用户登录
        /// </summary>
        /// <returns></returns>
        [Route("appuserlogin")]
        [HttpPost]
        [EnableCors("LimitRequests")]
        public async Task<object> appuserlogin([FromBody]sys_userinfo users)
        {
            var user = (await mrreader.Query(c => c.appcount == users.LoginName && c.apppassword == users.LoginPassWord && c.deleteflag == 0)).Select(c =>new {
                mrreadernumber = c.mrreadernumber,
                mrreadername = c.mrreadername,
                telephone = c.telephone,
                appcount = c.appcount,
                apppassword = c.apppassword,
                address = c.address,
                sex = c.sex,
                idcard = c.idcard,
                roles = c.roles,
                ID = c.id,
                Remark = c.remark
            }).FirstOrDefault();
            if (user != null)
            {
                return new JsonResult(new {
                    code = 0,
                    msg = "登陆成功",
                    data = user
                });
            }
            return new JsonResult(new
            {
                code = 1001,
                msg = "用户名或密码错误！",
                data = new { }
            });
        }

    }
}