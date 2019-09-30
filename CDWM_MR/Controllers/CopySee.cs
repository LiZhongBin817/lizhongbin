using System;
using System.Collections.Generic;
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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers
{
    public class CopySee : Controller
    {
        readonly Iv_r_datainfoServices _v_r_datainfoServices;
        readonly Imr_b_readerServices _mr_b_readerServices;
        readonly Imr_datainfo_historyServices _mr_datainfo_historyServices;
     
        public CopySee(Iv_r_datainfoServices v_r_datainfo, Imr_b_readerServices mr_b_readerServices, Imr_datainfo_historyServices mr_datainfo_historyServices)
        {
            _v_r_datainfoServices = v_r_datainfo;
            _mr_b_readerServices = mr_b_readerServices;
            _mr_datainfo_historyServices = mr_datainfo_historyServices;
        }
        /// <summary>
        /// 实现抄表率分析
        /// </summary>
        /// <param name="mrreadername">抄表员名称</param>
        /// <param name="mrdateTime">抄表的年月</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowCopysee")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> ShowCopysee(string mrreadername, string mrdateTime, int page = 1, int limit = 5)
        {
            PageModel<mr_datainfo_history> user = new PageModel<mr_datainfo_history>();
            Expression<Func<mr_datainfo_history, bool>> wherelambda = c => true;
            string dateTime = "";
            if (string.IsNullOrEmpty(mrdateTime))
            {
                mrdateTime= (DateTime.Now.Year).ToString() + (DateTime.Now.Month).ToString().PadLeft(2, '0');
                dateTime = mrdateTime;
                wherelambda = PredicateExtensions.And<mr_datainfo_history>(wherelambda, c => c.mrdateTime == mrdateTime);
            }
            if (!string.IsNullOrEmpty(mrdateTime))
            {
                dateTime = mrdateTime;
                wherelambda = PredicateExtensions.And<mr_datainfo_history>(wherelambda, c => c.mrdateTime == mrdateTime);
            }
            if (!string.IsNullOrEmpty(mrreadername))
            {
                wherelambda = PredicateExtensions.And<mr_datainfo_history>(wherelambda, c => c.mrreadername == mrreadername);
            }
            user = await _mr_datainfo_historyServices.QueryPage(wherelambda, page, limit);
            double allyichao= user.data.FindAll(c => c.readstatus == 1).Count() + user.data.FindAll(a => a.readstatus == 0).Count();
            double allshichao= user.data.FindAll(c => c.readtype == 1).Count();
            //暂时没用9.26创建的
          //  List<mr_b_reader> name = new List<mr_b_reader>();
          //  name = await _mr_b_readerServices.Query();
            List<object> datalist = new List<object>();
        //    List<object> dataname = new List<object>();
         // for(int i=0;i<name.Count();i++)
          //  {
          //      dataname.Add(name[i].mrreadername);
          //  }
            for (int i = 0; i < user.data.Count(); i++)
            {
                var data1 = user.data.FindAll(c => c.mrreadername == user.data[i].mrreadername && c.mrdateTime == dateTime);
                //if (!(name.Contains(user.data[i].mrreadername)))判重
             
                          int yichao = user.data.FindAll(c => c.readstatus == 1&& c.mrreadername == user.data[i].mrreadername && c.mrdateTime == dateTime).Count();
                           int weichao = user.data.FindAll(c => c.readstatus == 0 && c.mrreadername == user.data[i].mrreadername && c.mrdateTime == dateTime).Count();
                           int shichao = user.data.FindAll(c => c.readtype == 1 && c.mrreadername == user.data[i].mrreadername && c.mrdateTime == dateTime).Count();
                             int yingchao = yichao + weichao;
                           string  chaojianlv = (Math.Round(Convert.ToDouble(yingchao) / Convert.ToDouble(yingchao)*100,2)).ToString();
                             chaojianlv = chaojianlv + "%";
                string shichaolv = (Math.Round(Convert.ToDouble(shichao)/1.0 / Convert.ToDouble(yingchao)*100, 2)/1.0).ToString();
                shichaolv = shichaolv + "%";
                         var data = new
                          {
                               name = user.data[i].mrreadername,
                              month = dateTime,
                               yichao = yichao,
                               weichao = weichao,
                               shichao = shichao,
                               yingchao=yingchao,
                                chaojianlv= chaojianlv,
                                shichaolv= shichaolv


                            };
                           datalist.Add(data);
                
            }
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = datalist.Count(),
                data = datalist
            };
        }
        [HttpPost]
        [Route("Serchname")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async  Task<TableModel<object>> Serchname()
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
                data  =dataname
                
            };

        }

    }
}
