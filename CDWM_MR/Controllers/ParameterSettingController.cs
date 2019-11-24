using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.Common.HttpContextUser;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 参数管理
    /// </summary>
    [Route("api/ParameterSetting")]
    [AllowAnonymous]
    [EnableCors("LimitRequests")]
    public class ParameterSettingController : ControllerBase
    {
        #region 相关变量
        readonly Isys_parameter_settingServices _Parameter_SettingServices;
        readonly IUser _user;
        #endregion 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameter_SettingServices"></param>
        public ParameterSettingController(Isys_parameter_settingServices parameter_SettingServices,IUser user)
        {
            _Parameter_SettingServices = parameter_SettingServices;
            _user = user;
        }

        #region 参数管理
        /// <summary>
        ///显示参数信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ParameterShow")]       
        public async Task<TableModel<object>> ParameterShow(string parameterNumber,string parameterName,int parameterType,int page=1,int limit=10)
        {
            //分页信息
            PageModel<object> ParameterData = new PageModel<object>();
            #region lambda拼接
            Expression<Func<sys_parameter,bool>>wherelambda=c=>true;
            if (!string.IsNullOrEmpty(parameterNumber))
            {
                wherelambda = PredicateExtensions.And<sys_parameter>(wherelambda, c => c.parameternumber.Contains(parameterNumber));
            }
            if (!string.IsNullOrEmpty(parameterName))
            {
                wherelambda = PredicateExtensions.And<sys_parameter>(wherelambda, c => c.parametername.Contains(parameterName));
            }
            if (parameterType!=0)
            {
                wherelambda = PredicateExtensions.And<sys_parameter>(wherelambda, c => c.parametertype==parameterType);
            }
            #endregion

            Expression<Func<sys_parameter, object>> expression = c => new
            {
                ID = c.id,
                parametername = c.parametername,
                parameternumber = c.parameternumber,
                parametertype = c.parametertype,
                parametertypename = c.parametertypename,
                parameterkey = c.parameterkey,
                parametervalue = c.parametervalue,
                Remark = c.remark,
            };

            ParameterData = await _Parameter_SettingServices.QueryPage(wherelambda, expression, page, limit, "");

            return new TableModel<object>()
            {
                code = 0,
                msg = "查询成功",
                count = ParameterData.dataCount,
                data = ParameterData.data,
            };
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("AddParameter")]       
        public async Task<TableModel<object>> AddParameter(string JsonData)
        {
            //将前端传过来的值进行转换
            sys_parameter Jsondata = Common.Helper.JsonHelper.GetObject<sys_parameter>(JsonData);
            Jsondata.createpeople = _user.Name;
            Jsondata.createtime = DateTime.Now;

            //查询参数表中的最后一条数据
            var Data = await _Parameter_SettingServices.Query(c => true,1,"id desc");
            if (Data.Count <= 0)
            {
                Jsondata.parameternumber = "PN-0001";
                Jsondata.parameterkey = "PN-0001";
            }
            else 
            {
                Data[0].id++;
                Jsondata.parameternumber = $"PN-{Data[0].id.ToString("0000")}";
                Jsondata.parameterkey = Jsondata.parameternumber;
            }
            //否则进行添加
            var msg=await _Parameter_SettingServices.Add(Jsondata)>0?"OK":"error";
            return new TableModel<object>()
            {
                code=msg=="OK"?0:1001,
                msg=msg,
                data=null

            };
        }

       /// <summary>
       /// 编辑参数
       /// </summary>
       /// <param name="JsonData"></param>
       /// <param name="ID"></param>
       /// <returns></returns>
        [HttpGet]
        [Route("EditParameter")]       
        public async Task<TableModel<object>> EditParameter(string JsonData,int ID)
        {
            sys_parameter Jsondata = Common.Helper.JsonHelper.GetObject<sys_parameter>(JsonData);
            bool b = await _Parameter_SettingServices.Update(c => new sys_parameter
            {

                parametername=Jsondata.parametername,
                parameternumber=c.parameternumber,
                parametertype=Jsondata.parametertype,
                parametertypename=Jsondata.parametertypename,
                parameterkey=Jsondata.parameterkey,
                parametervalue= Jsondata.parametervalue,
                updatepeople = _user.Name,
                updatetime=DateTime.Now,
                remark=Jsondata.remark,
            }, c => c.id ==ID);
            return new TableModel<object>()
            {
                code=0,
                msg="OK",
                data=null
            };
        }
        #endregion
    }
}