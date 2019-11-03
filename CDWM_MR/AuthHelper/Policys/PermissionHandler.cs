using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CDWM_MR.AuthHelper
{
    /// <summary>
    /// 权限授权处理器
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }

        /// <summary>
        /// services 层注入
        /// </summary>
        public IsysManageServices sysrolemenuServices { get; set; }

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="schemes"></param>
        /// <param name="roleModulePermissionServices"></param>
        public PermissionHandler(IAuthenticationSchemeProvider schemes, IsysManageServices roleModulePermissionServices)
        {
            Schemes = schemes;
            this.sysrolemenuServices = roleModulePermissionServices;
        }

        ///重载异步处理程序
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // 将最新的角色和接口列表更新
            var data = await sysrolemenuServices.GetRoleOperation();
            var list = (from item in data
                        orderby item.id
                        select new PermissionItem
                        {
                            Url = item.Operation?.LinkUrl,
                            Role = Convert.ToInt32(item.Role?.id),
                        }).ToList();

            requirement.Permissions = list;

            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            var filterContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext);
            var httpContext = filterContext?.HttpContext;
            //请求Url
            if (httpContext != null)
            {
                var questUrl = httpContext.Request.Path.Value.ToLower();
                //判断请求是否停止
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
                {
                    if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                    {
                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        filterContext.Result = new JsonResult(new MessageModel<string> { code = 1003, msg = "很抱歉，您无权访问该接口!", data = "没有分配此接口！" });
                        context.Succeed(requirement);
                        return;
                    }
                }
                //判断请求是否拥有凭据，即有没有登录
                var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    //result?.Principal不为空即登录成功
                    if (result?.Principal != null)
                    {
                        httpContext.User = result.Principal;

                        //权限中是否存在请求的url
                        // 获取当前用户拥有的所有角色信息
                        var currentUserRoles = (from item in httpContext.User.Claims
                                                where item.Type == requirement.ClaimType
                                                select item.Value).ToList();
                        Permissions.RolesList = currentUserRoles;
                        var isMatchRole = false;
                        var permisssionRoles = requirement.Permissions.Where(w => currentUserRoles.Contains(w.Role.ToString()));
                        foreach (var item in permisssionRoles)
                        {
                            try
                            {
                                if (Regex.Match(questUrl, item.Url?.ObjToString().ToLower())?.Value == questUrl)
                                {
                                    isMatchRole = true;
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                //ignored
                            }
                        }
                        //验证权限
                        if (currentUserRoles.Count <= 0 || !isMatchRole)
                        {
                            httpContext.Response.StatusCode = StatusCodes.Status200OK;
                            filterContext.Result = new JsonResult(new MessageModel<string>{ code = 1003, msg = "很抱歉，您无权访问该接口!",data = "没有权限"});
                            context.Succeed(requirement);
                            return;
                        }
                        //判断过期时间,当过期时间过短,短到运行至此的时候，Token已经过期了
                        if ((httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) != null && DateTime.Parse(httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) >= DateTime.Now)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            httpContext.Response.StatusCode = StatusCodes.Status200OK;
                            filterContext.Result = new JsonResult(new MessageModel<string> { code = 1002, msg = "很抱歉，您的Token验证码已经失效了!", data = "Error" });
                            context.Succeed(requirement);
                            return;
                        }
                        return;
                    }
                }
                //判断没有登录时，是否访问登录的url,并且是Post请求，并且是form表单提交类型，否则为失败
                if (!questUrl.Equals(requirement.LoginPath.ToLower(), StringComparison.Ordinal) && (!httpContext.Request.Method.Equals("POST") || !httpContext.Request.HasFormContentType))
                {
                    //自定义返回数据
                    httpContext.Response.StatusCode = StatusCodes.Status200OK;
                    filterContext.Result = new JsonResult(new MessageModel<string> { code = 1002, msg = "很抱歉，您的Token验证码已经失效了!", data = "Error" });
                }
            }

            context.Succeed(requirement);
        }
    }
}
