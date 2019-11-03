using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpPost]
        [Route("SearchDictionary")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> SearchDictionary(int JsonData)
        {
            List<sys_parameter> data = new List<sys_parameter>();
            var list = await _ParameterServices.Query(c => c.parametertype == JsonData);
            var temp = list.Select(c => new {
                parameterkey = c.parameterkey,
                parametervalue = c.parametervalue
            });
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                data = temp
            };

        }
        #endregion

        #region 抄表员两年查询
        [HttpPost]
        [Route("SearchReaderDate")] 
        public async Task<TableModel<object>> SearchReaderDate(int readernameid)
        {
            string time,num;
            List<vrt_b_watercarryover_datainfo> data = new List<vrt_b_watercarryover_datainfo>();
            var list = await _B_Watercarryover_DatainfoServices.Query(c => c.readerid == readernameid);
            Dictionary<object, object> dic = new Dictionary<object, object>(); 
            for (int i = 0; i < list.Count; i++)
            {
                time = list[i].endtime.ToString();
                string a = time.Substring(0, 7);
                num = list[i].carrywatercount.ToString();
                string[] b = num.Split(".");
                dic.Add(a,b[0]); 
                if (i > 24)
                {
                    break;
                }
            } 
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                data = dic
            };
        }
        #endregion

    }
}