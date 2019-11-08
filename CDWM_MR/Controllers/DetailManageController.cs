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
    /// <summary>
    /// 应抄明细管理
    /// </summary>
    [Route("api/DetailManage")]
    [AllowAnonymous]
    [EnableCors("LimitRequests")]
    public class DetailManageController : ControllerBase
    {
        readonly Iv_t_b_users_datainfo_watercarryoverServices _T_B_Users_Datainfo_WatercarryoverServices;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="t_B_Users_Datainfo_WatercarryoverServices"></param>
        public DetailManageController(Iv_t_b_users_datainfo_watercarryoverServices t_B_Users_Datainfo_WatercarryoverServices)
        {
            _T_B_Users_Datainfo_WatercarryoverServices = t_B_Users_Datainfo_WatercarryoverServices;
        }

        /// <summary>
        /// 展示应抄明细数据
        /// </summary>
        /// <param name="readtype"></param>
        /// <param name="ReaderName"></param>
        /// <param name="bookno"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowDetailInfo")]      
        public async Task<TableModel<object>> ShowDetailInfo(string ReaderName, string bookno, int readtype, int page = 1, int limit = 20)
        {
            PageModel<object> pageModel = new PageModel<object>();
            #region lambda拼接式
            Expression<Func<v_t_b_users_datainfo_watercarryover, bool>> wherelambda = c => true;
            if ((readtype != 4&&readtype!=0))
            {
                if (readtype == 5)//前台传过来的数字5表示抄表状态为正常
                {
                    wherelambda = PredicateExtensions.And<v_t_b_users_datainfo_watercarryover>(wherelambda, c => c.readtype == 0);
                }
                else
                {
                    wherelambda = PredicateExtensions.And<v_t_b_users_datainfo_watercarryover>(wherelambda, c => c.readtype == readtype);
                }       
            }
            if (readtype==4)//表示抄表状态为未抄
            {
                wherelambda = PredicateExtensions.And<v_t_b_users_datainfo_watercarryover>(wherelambda, c => c.inputdata==null);
            }
            if (!string.IsNullOrEmpty(ReaderName))
            {
                wherelambda = PredicateExtensions.And<v_t_b_users_datainfo_watercarryover>(wherelambda, c => c.mrreadername .Contains( ReaderName));
            }
            if (!string.IsNullOrEmpty(bookno))
            {
                wherelambda = PredicateExtensions.And<v_t_b_users_datainfo_watercarryover>(wherelambda, c => c.bookno.Contains( bookno));
            }
            #endregion

            Expression<Func<v_t_b_users_datainfo_watercarryover, object>> expression = c => new
            {
                account=c.account,
                username=c.username,
                regionname=c.regionname,
                areaname=c.areaname,
                address=c.address,
                mrreadername = c.mrreadername,
                bookno=c.bookno,
                startnum=c.startnum,
                inputdata=c.inputdata,
                readtype=c.readtype,
                carrystatus=c.carrystatus,
            };
            pageModel = await _T_B_Users_Datainfo_WatercarryoverServices.QueryPage(wherelambda, expression, page, limit, "");
            return new TableModel<object>
            {
                code=0,
                msg="OK",
                count= pageModel.dataCount,
                data = pageModel.data,
            };
        }
    }
}