﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
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
        /// 修改用户联系电话接口
        /// </summary>
        /// <param name="autoaccount">用户自动编号</param>
        /// <param name="Newphone">新号码</param>
        /// <returns>成功返回1,失败返回0</returns>
        [HttpPost]
        [Route("ModifyUserPhone")]
        [AllowAnonymous]//允许所有都访问
        public async Task<MessageModel<int>> ModifyUserPhone(string autoaccount, string Newphone)
        {
            bool b = await _t_b_usersServices.OUpdate(c => new t_b_users
            {
                telephone= Newphone
            }, c => c.autoaccount == autoaccount);
            return new MessageModel<int>(){
                code = 0,
                msg = "成功",
                data = b?1:0
            };
        }
        #endregion

        #region  修改用户用水地址接口
        /// <summary>
        /// 修改用户用水地址接口
        /// </summary>
        /// <param name="autoaccount">用户自动编号</param>
        /// <param name="address">用户所在地址</param>
        /// <returns>成功返回1,失败返回0</returns>
        [HttpPost]
        [Route("ModifyUserAddress")]
        [AllowAnonymous]//允许所有都访问
        public async Task<MessageModel<int>> ModifyUserAddress(string autoaccount, string address)
        {
            bool b = await _t_b_usersServices.OUpdate(c => new t_b_users
            {
                address = address
            }, c => c.autoaccount == autoaccount);
            return new MessageModel<int>()
            {
                code = 0,
                msg = "成功",
                data = b?1:0
            };
        }
        #endregion

        /// <summary>
        /// APP接口测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AppInterfaceTest")]
        public MessageModel<string> AppInterfaceTest()
        {
            return new MessageModel<string>()
            {
                code = 0,
                msg = "成功",
                data = "测试成功,可以连接！"
            };
        }
    }
}
