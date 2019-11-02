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
    [Route("api/[controller]/[action]")]
    public class AppSoftware : Controller
    {
        
        /// <summary>
        /// 软件更新下载
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SoftWareUpdate")]
        [AllowAnonymous]//允许所有都访问
        public object SoftWareUpdate()
        {           
            int VersionCode = Common.Appsettings.app(new string[] { "APKSetting" , "VersionCode" }).ObjToInt();
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
                msg="获取成功",
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
            var Technicaltelephone= Common.Appsettings.app(new string[] { "AppAboutus", "Technicaltelephone" });
            var temp = new
            {
                TechnicalSupport = TechnicalSupport,
                Technicaltelephone= Technicaltelephone,
                Servicestelephone = Servicestelephone,
            };
            return temp;
        }
    }
}
