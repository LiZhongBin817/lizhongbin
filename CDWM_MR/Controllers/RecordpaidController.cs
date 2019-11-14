using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.Models.Entitys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 待缴记录
    /// </summary>
    [Route("api/Recordpaid")]
    [AllowAnonymous]
    [EnableCors("AllRequests")]

    public class RecordpaidController : ControllerBase
    {
        readonly Iv_recordpaidServices _v_recordServices;

        public RecordpaidController(Iv_recordpaidServices v_recordpaid)
        {
            _v_recordServices = v_recordpaid;
        }
        /// <summary>
        /// 显示全部数据
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Route("Waittopay")]
        [HttpGet]
        public async Task<TableModel<object>> Waittopay(string starttime, string endtime, int page = 1, int limit = 5)
        {
            PageModel<v_recordpaid> data1 = new PageModel<v_recordpaid>();
            Expression<Func<v_recordpaid, bool>> wherelambda = c => true;
            if (string.IsNullOrEmpty(starttime))
            {
                wherelambda = PredicateExtensions.And<v_recordpaid>(wherelambda, c => c.starttime.ToString() == starttime);
            }
            if (string.IsNullOrEmpty(endtime))
            {
                wherelambda = PredicateExtensions.And<v_recordpaid>(wherelambda, c => c.starttime.ToString() == endtime);
            }
            data1 = await _v_recordServices.QueryPage(wherelambda, page, limit);

            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                count = data1.dataCount,
                data = data1
            };
        }


    }


}