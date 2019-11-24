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
    /// 账单记录
    /// </summary>
    [Route("api/Billrecord")]
    [AllowAnonymous]
    [EnableCors("AllRequests")]
    public class BillrecordController : ControllerBase
    {

        readonly Iv_recordpaidServices _v_recordServices;

        public BillrecordController(Iv_recordpaidServices v_recordpaid)
        {
            _v_recordServices = v_recordpaid;
        }
        /// <summary>
        /// 显示全部数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Route("billread")]
        [HttpGet]
        public async Task<TableModel<object>> billread(string autoaccount, int page = 1, int limit = 5)
        {
            PageModel<v_recordpaid> data1 = new PageModel<v_recordpaid>();
            List<object> datalist = new List<object>();
            Expression<Func<v_recordpaid, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(autoaccount))
            {
                wherelambda = PredicateExtensions.And<v_recordpaid>(wherelambda, c => c.autoaccount == autoaccount);
            }

            data1 = await _v_recordServices.QueryPage(wherelambda, page, limit);

            for(int i=0;i<data1.data.Count();i++)
            {
               
                    var data = new
                    {
                        payseq=data1.data[i].payseq,
                        waterfee= data1.data[i].waterfee,
                        taskperiodname= data1.data[i].taskperiodname,
                        startnum= data1.data[i].startnum,
                        endnum= data1.data[i].endnum,
                        lastwaternum= data1.data[i].lastwaternum,
                        carrywatercount= data1.data[i].carrywatercount,
                        starttime= data1.data[i].starttime,
                        endtime= data1.data[i].endtime,
                        bmttype= data1.data[i].bmttype,
                        cbalance= data1.data[i].cbalance,
                        autoaccount= data1.data[i].autoaccount,
                        username= data1.data[i].username,
                        address= data1.data[i].address
                    };
                    datalist.Add(data);
                      
            }

            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                count = datalist.Count(),
                data = datalist
            };
        }


        /// <summary>
        /// 传值时间回来查询
        /// </summary>
        /// <param name="autoaccount"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Route("Lookbillread")]
        [HttpGet]
        public async Task<TableModel<object>> Lookbillread(string autoaccount, string starttime, string endtime, int page = 1, int limit = 5)
        {
            PageModel<v_recordpaid> data1 = new PageModel<v_recordpaid>();
            Expression<Func<v_recordpaid, bool>> wherelambda = c => true;
            List<object> datalist = new List<object>();
            if (!string.IsNullOrEmpty(autoaccount))
            {
                wherelambda = PredicateExtensions.And<v_recordpaid>(wherelambda, c => c.autoaccount == autoaccount);
            }

            data1 = await _v_recordServices.QueryPage(wherelambda, page, limit);

            for (int i = 0; i < data1.data.Count; i++)
            {
                if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                {
                    if (Convert.ToInt32(data1.data[i].starttime) >= Convert.ToInt32(starttime) && Convert.ToInt32(data1.data[i].endtime) <= Convert.ToInt32(endtime))
                    {

                        var data = new
                        {
                            payseq = data1.data[i].payseq,
                            waterfee = data1.data[i].waterfee,
                            taskperiodname = data1.data[i].taskperiodname,
                            startnum = data1.data[i].startnum,
                            endnum = data1.data[i].endnum,
                            lastwaternum = data1.data[i].lastwaternum,
                            carrywatercount = data1.data[i].carrywatercount,
                            starttime = data1.data[i].starttime,
                            endtime = data1.data[i].endtime,
                            bmttype = data1.data[i].bmttype,
                            cbalance = data1.data[i].cbalance,
                            autoaccount = data1.data[i].autoaccount,
                            username = data1.data[i].username,
                            address = data1.data[i].address
                        };
                        datalist.Add(data);
                    }
                }
            }

            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                count = datalist.Count(),
                data = datalist
            };
        }


    }

}
