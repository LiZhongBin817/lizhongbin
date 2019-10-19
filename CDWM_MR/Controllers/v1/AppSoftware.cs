using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// 软件部分
    /// </summary>
    [Route("api/[controller]")]
    public class AppSoftware : Controller
    {
        /// <summary>
        /// 软件检查更新
        /// </summary>
        /// <param name="OldVersion">旧的版本号</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckUpdate")]
        [AllowAnonymous]//允许所有都访问
        public string CheckUpdate(string OldVersion)
        {
            string Newversion = Common.Appsettings.app(new string[] { "Version" });
            if(OldVersion!= Newversion)
            {
                return "你当前版本为" + OldVersion + "，需要更新到" + Newversion + "版本！！！";
            }
            return "当前版本已为最新版本";
        }

        /// <summary>
        /// 软件更新下载
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SoftWareUpdate")]
        [AllowAnonymous]//允许所有都访问
        public string SoftWareUpdate()
        {
            string Status = "下载失败";

            Status = "下载成功";
            return Status;
        }

        /// <summary>
        /// 技术支持
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TechnicalSupport")]
        [AllowAnonymous]//允许所有都访问
        public string TechnicalSupport()
        {
            var temp = Common.Appsettings.app(new string[] { "TechnicalSupport" });
            return temp;
        }
    }
}
