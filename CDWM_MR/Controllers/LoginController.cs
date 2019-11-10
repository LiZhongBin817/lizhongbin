using CDWM_MR.AuthHelper;
using CDWM_MR.AuthHelper.OverWrite;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR_Common.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 登陆控制器--无权限控制
    /// </summary>
    [Produces("application/json")]
    [Route("api/Login")]
    [AllowAnonymous]
    [EnableCors("AllRequests")]
    public class LoginController : Controller
    {

        #region 相关变量
        readonly PermissionRequirement _requirement;
        readonly IRedisHelper _redishelper;
        readonly IsysManageServices _SysManage;
        readonly Isys_userinfoServices _SysUserinfo;
        readonly IHttpContextAccessor _accessor;
        #endregion


        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="sysuserinfo"></param>
        /// <param name="sysManage"></param>
        /// <param name="addredis"></param>
        /// <param name="requirement"></param>
        /// <param name="accessor"></param>
        public LoginController(Isys_userinfoServices sysuserinfo, IsysManageServices sysManage, IRedisHelper addredis, PermissionRequirement requirement, IHttpContextAccessor accessor)
        {

            _redishelper = addredis;
            _requirement = requirement;
            _SysManage = sysManage;
            _SysUserinfo = sysuserinfo;
            _accessor = accessor;
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
            string ipadress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            _redishelper.StringSet(ipadress, Code, TimeSpan.FromSeconds(180));
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
        public async Task<object> UserLogin(string UserName, string PassWord, string VerCode)
        {
            //检验验证码
            string checkCode = _redishelper.StringGet(_accessor.HttpContext.Connection.RemoteIpAddress.ToString());
            if (string.IsNullOrEmpty(checkCode))
            {
                return new JsonResult(new
                {
                    code = 1001,
                    msg = "验证码错误！",
                    data = new { }
                });
            }
            if (VerCode != checkCode)
            {
                return new JsonResult(new
                {
                    code = 1001,
                    msg = "验证码错误！",
                    data = new { }
                });
            }
            var md5 = MD5Helper.MD5Encrypt32(PassWord);//MD5加密
            var user = (await _SysManage.Query(c => c.LoginName == UserName && c.LoginPassWord == md5 && c.UseStatus == 0)).FirstOrDefault();
            if (user != null)
            {
                Permissions.UersName = user.FUserName;
                //将登陆的用户信息存入Redis缓存
                await _redishelper.StringSetAsync($"UserInfo{user.id}", user, TimeSpan.FromMinutes(60*60));
                var rolestr = await _SysManage.GetuserRole(user.id);//角色的组合
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.FUserName),
                    new Claim(JwtRegisteredClaimNames.Jti, user.id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
                claims.AddRange(rolestr.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                return new JsonResult(token);
            }
            return new JsonResult(new
            {
                code = 1000,
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
                return new JsonResult(new
                {
                    Status = false,
                    message = "Token无效,请重新登陆！"
                });
            }
            var tokenmodel = JwtHelper.SerializeJwt(Token);//Token解析
            if (tokenmodel != null && tokenmodel.Uid > 0)
            {
                var user = await _SysManage.QueryById(tokenmodel.Uid);
                if (user != null)
                {
                    var userRoles = await _SysManage.GetuserRole(Convert.ToInt32(tokenmodel.Uid));
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

            return new JsonResult(new
            {
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