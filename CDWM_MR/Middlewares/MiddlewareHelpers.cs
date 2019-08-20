using CDWM_MR.AuthHelper;
using Microsoft.AspNetCore.Builder;

namespace CDWM_MR.Middlewares
{
    /// <summary>
    /// 中间件--请求响应处理以及Jwt生成
    /// </summary>
    public static class MiddlewareHelpers
    {
        /// <summary>
        /// 生成用户Jwt令牌
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJwtTokenAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtTokenAuth>();
        }
        /// <summary>
        /// 用户请求响应日志处理
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseReuestResponseLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequRespLogMildd>();
        }
    }
}
