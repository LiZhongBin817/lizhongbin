using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CDWM_MR.AuthHelper;
using CDWM_MR.AuthHelper.OverWrite;
using CDWM_MR.Common;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR_Common.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        readonly PermissionRequirement _requirement;
        readonly IRedisHelper _redishelper;
        readonly Isys_user_role_mapperServices _userrole;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="sysuserinfo"></param>
        /// <param name="sysrolemenu"></param>
        /// <param name="userrole"></param>
        /// <param name="addredis"></param>
        /// <param name="requirement"></param>
        public LoginController(Isys_userinfoServices sysuserinfo,Isys_role_menuServices sysrolemenu,Isys_user_role_mapperServices userrole,IRedisHelper addredis, PermissionRequirement requirement)
        {
            _redishelper = addredis;
            _sysuserinfoservices = sysuserinfo;
            _sysrolemenu = sysrolemenu;
            _requirement = requirement;
            _userrole = userrole;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowCode")]
        public object ShowCode()
        {
            Common.ValidateCode valcode = new Common.ValidateCode();
            string Code;
            byte[] buffer = valcode.GetVerifyCode(out Code);//将验证码画到画布上
            //TimeSpan timeSetting = new TimeSpan(1000 * 60 * 30);
            _redishelper.StringSet("Code", Code);
            return File(buffer, "image/jpeg");
        }

        /// <summary>
        /// 完成用户登录后生成Token
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <param name="VerCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("UserLogin")]
        [EnableCors("LimitRequests")]
        public async Task<object> UserLogin(string UserName, string PassWord, string VerCode)
        {
            //检验验证码
            string checkCode = _redishelper.StringGet("Code");
            if (string.IsNullOrEmpty(checkCode))
            {
                return new JsonResult(new {
                    code = 1001,
                    msg = "验证码错误！",
                    data = new { }
                });
            }
            if (VerCode != checkCode)
            {
                return new JsonResult(new {
                    code = 1001,
                    msg = "验证码错误！",
                    data = new { }
                });
            }
            var md5 = MD5Helper.MD5Encrypt32(PassWord);//MD5加密
            var user = (await _sysuserinfoservices.Query(c => c.LoginName == UserName && c.LoginPassWord == md5 && c.UseStatus == 0)).FirstOrDefault();
            if (user != null)
            {
                TimeSpan timeSetting = new TimeSpan(1000*60*30);
                //将登陆的用户信息存入Redis缓存
                var t = await _redishelper.StringSetAsync("UserInfo", user,timeSetting);
                var h = _redishelper.StringGet("UserInfo");
                var rolestr = await _userrole.GetuserRole(user.ID);//角色的组合

                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, user.ID.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
                claims.AddRange(rolestr.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                return new JsonResult(new {
                    code = 0,
                    msg = "登入成功",
                    data = new { access_token = token }
                });
            }
            return new JsonResult(new {
                code = 1001,
                msg = "用户名或密码错误！",
                data = new { }
            });
        }

        /// <summary>
        /// 根据旧Token换取新Token
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RefreshToken")]
        public async Task<object> RefreshToken(string Token = "")
        {
            string jwtStr = string.Empty;
            if (string.IsNullOrEmpty(Token))
            {
                return new JsonResult(new {
                    Status = false,
                    message = "Token无效,请重新登陆！"
                });
            }
            var tokenmodel = JwtHelper.SerializeJwt(Token);//Token解析
            if (tokenmodel != null&& tokenmodel.Uid > 0)
            {
                var user = await _sysuserinfoservices.QueryById(tokenmodel.Uid);
                if (user != null)
                {
                    var userRoles = await _userrole.GetuserRole(Convert.ToInt32(tokenmodel.Uid));
                    //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                    var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.LoginName),
                    new Claim(JwtRegisteredClaimNames.Jti, tokenmodel.Uid.ObjToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
                    claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

                    //用户标识
                    var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                    identity.AddClaims(claims);

                    var refreshToken = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                    return new JsonResult(refreshToken);
                }
            }

            return new JsonResult(new {
                Success = false,
                message = "认证失败"
            });

        }

        /// <summary>
        /// 测试MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MD5PassWord")]
        public string MD5PassWord(string password = "")
        {
            return MD5Helper.MD5Encrypt32(password);
        }

    }
}