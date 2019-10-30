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
        public string CheckUpdate(int OldVersion)
        {
            string Newversion = Common.Appsettings.app(new string[] { "APKSetting", "VersionCode" });
            if(OldVersion<Convert.ToInt32( Newversion))
            {
                return "您的版本过低！请更新版本！"+"旧版本号是"+ OldVersion +",最新版本号是"+ Newversion ;
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
        public object SoftWareUpdate()
        {           
            string VersionCode = Common.Appsettings.app(new string[] { "APKSetting" , "VersionCode" });
            string VersionName= Common.Appsettings.app(new string[] { "APKSetting", "VersionName" });
            string APKName = Common.Appsettings.app(new string[] { "APKSetting", "APKName" });
            string Dowloadurl= Common.Appsettings.app(new string[] { "APKSetting", "Dowloadurl" });
            string UpdateInfo= Common.Appsettings.app(new string[] { "APKSetting", "UpdateInfo" });
            var data =new
            {
                VersionCode= VersionCode,
                VersionName= VersionName,
                APKName= APKName,
                Dowloadurl= Dowloadurl,
                UpdateInfo= UpdateInfo,
            };
            return new JsonResult(new {
                code=0,
                msg="下载成功",
                data= data
            });
            
        }

        /// <summary>
        /// 技术支持
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TechnicalSupport")]
        [AllowAnonymous]//允许所有都访问
        public object TechnicalSupport()
        {
            
            var TechnicalSupport = Common.Appsettings.app(new string[] { "AppAboutus", "TechnicalSupport" });
            var Servicestelephone = Common.Appsettings.app(new string[] { "AppAboutus", "Servicestelephone" });
            var temp = new
            {
                TechnicalSupport = TechnicalSupport,
                Servicestelephone = Servicestelephone,
            };
            return temp;
        }
    }
}
