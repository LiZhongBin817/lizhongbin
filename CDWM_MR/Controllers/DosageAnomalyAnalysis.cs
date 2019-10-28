using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CDWM_MR.Controllers
{
    /// <summary>
   /// 用量异常分析
    /// </summary>
    public class DosageAnomalyAnalysis : ControllerBase
    {
        readonly Iv_user_water_bookinfoServices _v_user_water_bookinfoServices;
        readonly Imr_b_readerServices _mr_b_readerServices;
        readonly Imr_b_bookinfoServices _mr_b_bookinfoServices;
      
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="v_user_water_bookinfoServices"></param>
        /// <param name="mr_b_readerServices"></param>
        /// <param name="mr_b_bookinfoServices"></param>
        public DosageAnomalyAnalysis(Iv_user_water_bookinfoServices v_user_water_bookinfoServices, Imr_b_readerServices mr_b_readerServices, Imr_b_bookinfoServices mr_b_bookinfoServices)
        {
            _v_user_water_bookinfoServices = v_user_water_bookinfoServices;
            _mr_b_readerServices = mr_b_readerServices;
            _mr_b_bookinfoServices = mr_b_bookinfoServices;
      
        }
        /// <summary>
        /// 用量异常分析
        /// </summary>
        /// <param name="taskperiodname"></param>
        /// <param name="readname"></param>
        /// <param name="bookno"></param>
        /// <param name="meternum"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowDosageAnomalyAnalysis")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> ShowDosageAnomalyAnalysis(string taskperiodname, string readname, string bookno,string meternum, int page = 1, int limit = 5)
        {
            

            PageModel<v_user_water_bookinfo> user = new PageModel<v_user_water_bookinfo>();
            Expression<Func<v_user_water_bookinfo, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(taskperiodname))
            {
                wherelambda = PredicateExtensions.And<v_user_water_bookinfo>(wherelambda, c => c.taskperiodname == taskperiodname);
            }

            //string dateTime = "";
            //if (string.IsNullOrEmpty(endtime))
            //{
            //    endtime = (DateTime.Now.Year).ToString() + (DateTime.Now.Month).ToString().PadLeft(2, '0');
            //    dateTime = endtime;
            //    wherelambda = PredicateExtensions.And<v_user_water_bookinfo>(wherelambda, c => c.endtime.ToString() == endtime);
            //}
            if (!string.IsNullOrEmpty(readname))
            {
                wherelambda = PredicateExtensions.And<v_user_water_bookinfo>(wherelambda, c => c.readname == readname);
            }
            if(!string.IsNullOrEmpty(bookno))
            {
                wherelambda = PredicateExtensions.And<v_user_water_bookinfo>(wherelambda, c => c.bookno == bookno);
            }
            if(!string.IsNullOrEmpty(meternum))
            {
                wherelambda = PredicateExtensions.And<v_user_water_bookinfo>(wherelambda, c => c.meternum == meternum);
            }
          
            user = await _v_user_water_bookinfoServices.QueryPage(wherelambda, page, limit);

            List<object> datalist = new List<object>();
        
           //想用再次查询时间来
            for (int i = 0; i < user.data.Count(); i++)
            {
                double waterdifference;
                 string waterdifferencerate;
                waterdifference = System.Math.Abs(Convert.ToDouble(user.data[i].lastwaternum) - Convert.ToDouble(user.data[i].carrywatercount));

                waterdifferencerate = Convert.ToDouble(user.data[i].lastwaternum) / waterdifference + "%";
              
                var data = new
                {
                    autoaccount = user.data[i].autoaccount,
                    username = user.data[i].username,
                    meternum = user.data[i].meternum,
                    address = user.data[i].address,
                    lastwaternum = user.data[i].lastwaternum,
                    carrywatercount = user.data[i].carrywatercount,
                    taskperiodname = user.data[i].taskperiodname,
                    startnum = user.data[i].startnum,
                    endnum = user.data[i].endnum,
                    readname = user.data[i].readname,
                    waterdifference = waterdifference,
                    waterdifferencerate = waterdifferencerate,
                    uploadgisplace = user.data[i].uploadgisplace

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
        /// 给抄表员下拉框赋值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Serchmrreader")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> Serchmrreader()
        {


            List<mr_b_reader> name = new List<mr_b_reader>();
            name = await _mr_b_readerServices.Query();
            List<object> dataname = new List<object>();
            for (int i = 0; i < name.Count(); i++)
            {
                dataname.Add(name[i].mrreadername);
            }
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = dataname.Count(),
                data = dataname

            };

        }

        /// <summary>
        /// 给抄表册传值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Serchbookno")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> Serchbookno()
        {
            List<mr_b_bookinfo> booknober = new List<mr_b_bookinfo>();
            booknober = await _mr_b_bookinfoServices.Query();
            List<object> datano = new List<object>();
            for (int i = 0; i < booknober.Count(); i++)
            {
                datano.Add(booknober[i].bookno);
            }
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = datano.Count(),
                data = datano

            };

        }




    }
}
