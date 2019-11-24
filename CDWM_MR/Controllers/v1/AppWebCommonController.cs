﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.Common;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// Web与APP公用参数接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    [EnableCors("LimitRequests")]
    public class AppWebCommonController : ControllerBase
    {

        #region 相关变量
        readonly Isys_parameterServices _ParameterServices;
        readonly Ivrt_b_watercarryover_datainfoServices _B_Watercarryover_DatainfoServices;
        #endregion

        #region 构造函数
        public AppWebCommonController(Isys_parameterServices ParameterServices, Ivrt_b_watercarryover_datainfoServices B_Watercarryover_DatainfoServices)
        {
            _ParameterServices = ParameterServices;
            _B_Watercarryover_DatainfoServices = B_Watercarryover_DatainfoServices;
        }
        #endregion

        #region 字典查询
        /// <summary>
        /// 字典查询
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        [HttpGet("{typeid}")]
        [Caching(AbsoluteExpiration = 60)]
        public async Task<MessageModel<object>> SearchDictionary(int typeid)
        {
            List<sys_parameter> data = new List<sys_parameter>();
            var list = await _ParameterServices.Query(c => c.parametertype == typeid);
            var temp = list.Select(c => new {
                parameterkey = c.id,
                parametervalue = c.parametervalue
            });
            return new MessageModel<object>
            {
                code = 0,
                msg = "OK",
                data = temp
            };
        }
        #endregion

        #region 用户两年用水及水费查询
        /// <summary>
        /// 用户两年用水及水费查询
        /// </summary>
        /// <param name="autoaccount"></param>
        /// <returns></returns>
        [HttpGet("{autoaccount}")]
        public async Task<MessageModel<object>> SearchReaderDate01(string  autoaccount)
        {  
            List<vrt_b_watercarryover_datainfo> data = new List<vrt_b_watercarryover_datainfo>();
            var list01 = await _B_Watercarryover_DatainfoServices.Query(c => c.autoaccount == autoaccount);
            List<object> list02 = new List<object>();
            for (int i = 0; i < list01.Count; i++)
            {
                list02.Add(list01[i]);
                if (i >= 24)
                {
                    break;
                }
            }
            return new MessageModel<object>
            {
                code = 0,
                msg = "OK",
                data = list02
            };
        }
        #endregion

    }
}