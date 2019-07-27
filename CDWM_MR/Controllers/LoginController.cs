using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 登陆控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/Login")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        /// <summary>
        /// 测试第一次
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("First")]
        public int First()
        {
            int a = 2;
            int e = 4;
            return a + e;
        }
    }
}