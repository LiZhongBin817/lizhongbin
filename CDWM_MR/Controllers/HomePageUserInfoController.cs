using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers
{
    [Route("api/HomePageUserInfo")]
    [AllowAnonymous]
    [EnableCors("LimitRequests")]
    public class HomePageUserInfoController : ControllerBase
    {
        #region 相关变量
        readonly Iv_home_userinfoServices Home_UserinfoServices;
        readonly It_b_regionsServices B_RegionsServices;
        readonly It_b_areasServices B_AreasServices;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="home_UserinfoServices"></param>
        /// <param name="b_RegionsServices"></param>
        /// <param name="b_AreasServices"></param>
        public HomePageUserInfoController(Iv_home_userinfoServices home_UserinfoServices, It_b_regionsServices b_RegionsServices, It_b_areasServices b_AreasServices)
        {
            Home_UserinfoServices = home_UserinfoServices;
            B_RegionsServices = b_RegionsServices;
            B_AreasServices = b_AreasServices;

        }

        /// <summary>
        /// 渲染区域下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("RegionSelectRender")]
        public async Task<TableModel<object>> RegionSelectRender()
        {
            var data = await B_RegionsServices.OQuery(c=>true);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                data = data,
                count = data.Count
            };
        }

        /// <summary>
        /// 渲染小区下拉框
        /// </summary>
        /// <param name="regionno"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AreaSelectRender")]
        public async Task<TableModel<object>> AreaSelectRender(string regionno)
        {
            var data = await B_AreasServices.OQuery(c => c.regionno == regionno);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                data = data,
                count = data.Count
            };
        }

        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <param name="meternum"></param>
        /// <param name="address"></param>
        /// <param name="telephone"></param>
        /// <param name="region"></param>
        /// <param name="area"></param>
        /// <param name="bookno"></param>
        /// <param name="mrreadername"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserInfoSearch")]
        public async Task<TableModel<object>> UserInfoSearch(string account,string username,string meternum,string address,string telephone,string region,string area,string bookno,string mrreadername, int page = 1, int limit =20)
       {
            PageModel<v_home_userinfo> pageModel = new PageModel<v_home_userinfo>();
            #region lambda拼接式 
            Expression<Func<v_home_userinfo, bool>> wherelambda = c => true;      
            if (!string.IsNullOrEmpty(username))
            {
                wherelambda = PredicateExtensions.And<v_home_userinfo>(wherelambda, c => c.username.Contains(username));
            }
            if (!string.IsNullOrEmpty(account))
            {
                wherelambda = PredicateExtensions.And<v_home_userinfo>(wherelambda, c => c.account.Contains(account));
            }
            if (!string.IsNullOrEmpty(meternum))
            {
                wherelambda = PredicateExtensions.And<v_home_userinfo>(wherelambda, c => c.meternum.Contains(meternum));
            }
            if (!string.IsNullOrEmpty(address))
            {
                wherelambda = PredicateExtensions.And<v_home_userinfo>(wherelambda, c => c.address.Contains(address));
            }
            if (!string.IsNullOrEmpty(mrreadername))
            {
                wherelambda = PredicateExtensions.And<v_home_userinfo>(wherelambda, c => c.mrreadername.Contains(mrreadername));
            }
            if (!string.IsNullOrEmpty(bookno))
            {
                wherelambda = PredicateExtensions.And<v_home_userinfo>(wherelambda, c => c.bookno.Contains(bookno));
            }
            #endregion
            pageModel = await Home_UserinfoServices.QueryPage(wherelambda, page, limit, "");
            if (pageModel.dataCount == 0)
            {
                return new TableModel<object>
                {
                    code = 0,
                    msg = "NO",
                    data = pageModel.data,
                    count = pageModel.dataCount
                };
            }
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                data = pageModel.data,
                count = pageModel.dataCount
            };
        }

        /// <summary>
        /// 用户信息显示
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UserInfoShow")]
        public async Task<TableModel<object>> UserInfoShow(string autoaccount)
        {
            var data = await Home_UserinfoServices.Query(c=>c.autoaccount== autoaccount);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                data = data,
                count = data.Count
            };
        }
    }
}