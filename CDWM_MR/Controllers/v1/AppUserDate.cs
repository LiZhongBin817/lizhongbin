using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    ///用户数据
    /// </summary>
    [Route("api/[controller]")]
    public class AppUserDate : Controller
    {
        #region  相关变量
        readonly It_b_usersServices _t_b_usersServices;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="t_b_usersServices"></param>
        public AppUserDate(It_b_usersServices t_b_usersServices)
        {
            _t_b_usersServices = t_b_usersServices;
        }

        #region  修改用户联系电话接口
        /// <summary>
        /// 
        /// </summary>
        /// <param name="autoaccount">用户自动编号</param>
        /// <param name="Newphone">新号码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyUserPhone")]
        [AllowAnonymous]//允许所有都访问
        public async Task<int> ModifyUserPhone(string autoaccount, string Newphone)
        {
            int Status=0;
            await _t_b_usersServices.OUpdate(c => new t_b_users
            {
                telephone= Newphone
            }, c => c.autoaccount == autoaccount);
            Status = 1;
            return Status;
        }
        #endregion

        #region  修改用户用水地址接口
        /// <summary>
        /// 修改用户用水地址接口
        /// </summary>
        /// <param name="autoaccount">用户自动编号</param>
        /// <param name="areano">用户所在小区</param>
        /// <param name="address">用户所在地址</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyUserAddress")]
        [AllowAnonymous]//允许所有都访问
        public async Task<int> ModifyUserAddress(string autoaccount, string areano, string address)
        {
            int Status = 0;
            await _t_b_usersServices.OUpdate(c => new t_b_users
            {
                areano = areano,
                address = address
            }, c => c.autoaccount == autoaccount);
            Status = 1;
            return Status;
        }
        #endregion
    }
}
