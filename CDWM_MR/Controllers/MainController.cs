using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.IServices;
using CDWM_MR.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 主页面控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("LimitRequests")]
    [Authorize("Permission")]
    public class MainController : BaseController
    {
        readonly IsysManageServices SysManage;
        
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="sysmanage"></param>
        public MainController(IsysManageServices sysmanage)
        {
            SysManage = sysmanage;
        }

        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenuData")]
        public async Task<MessageModel<List<object>>> GetMenuData()
        {
            var menulist = await SysManage.GetMenuTree();
            return new MessageModel<List<object>> {
                msg = "成功",
                data = menulist
            };
        }
    }
}