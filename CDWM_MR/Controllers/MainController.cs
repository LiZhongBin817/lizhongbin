using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.AuthHelper;
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
            var UserRoles = Permissions.RolesList;//当前用户所有角色
            var data = await SysManage.GetRoleOperation();//当前所有的角色对应的菜单权限信息
            List<int> Menulist = new List<int>();
            UserRoles.ForEach(c => {
                var t = Convert.ToInt32(c);
                Menulist.AddRange(data.FindAll(s => s.RoleID == t).Select(d => d.MenuID));
            });
            var temp = Menulist.Distinct().ToList();//不更新原集合--去重
            var temp1 = menulist.Where(c => temp.Contains(c.id)).ToList();//不更新原集合--查找出对应的集合
            var rMenuList = SysManage.Childmenu(0, temp1);
            return new MessageModel<List<object>> {
                msg = "成功",
                data = rMenuList
            };
        }


    }
}