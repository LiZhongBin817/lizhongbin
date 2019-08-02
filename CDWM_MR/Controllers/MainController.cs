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
    /// 主页面控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MainController : ControllerBase
    {

        [HttpPost]
        [Route("GetMenuData")]
        public async Task<object> GetMenuData()
        {
            return new JsonResult(new {

            });
        }
    }
}