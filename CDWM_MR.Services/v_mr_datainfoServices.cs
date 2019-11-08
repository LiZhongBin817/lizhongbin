	//----------v_mr_datainfo开始----------
    

using System;
using CDWM_MR.IServices.Content;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using CDWM_MR.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using CDWM_MR.Common.Helper;

namespace CDWM_MR.Services.Content
{	
	/// <summary>
	/// v_mr_datainfoServices
	/// </summary>	
	public partial class v_mr_datainfoServices : BaseServices<v_mr_datainfo>, Iv_mr_datainfoServices
    {
        /// <summary>
        /// 显示抄表路径
        /// </summary>
        /// <param name="month"></param>
        /// <param name="date"></param>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<TableModel<object>> ShowMRPath(string month, string date, string name, int page = 1, int limit = 5)
        {
            string Month = DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString();//为了默认是查询当前周期的
            List<string> uploadGPS = new List<string>();
            List<object> ReturnData = new List<object>();
            PageModel<v_mr_datainfo> pageModel = new PageModel<v_mr_datainfo>();
            #region lambda拼接式
            Expression<Func<v_mr_datainfo, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(month))
            {
                Month = month;
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.taskperiodname == month);
            }
            if (string.IsNullOrEmpty(month))
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.taskperiodname == Month);
            }
            if (!string.IsNullOrEmpty(name))
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.mrreadername == name);
            }
            if (!string.IsNullOrEmpty(date))
            {
                wherelambda = PredicateExtensions.And<v_mr_datainfo>(wherelambda, c => c.uploadtime.ToString().Substring(0, 10) == date);
            }
            #endregion
            pageModel = await dal.QueryPage(wherelambda, page, limit);
            if (pageModel.dataCount == 0)
            {
                return new TableModel<object>
                {
                    code = 0,
                    msg = "NO",
                    data = ReturnData,
                    count = pageModel.dataCount,
                };
            }
            int Counts = (await dal.Query(c => c.taskperiodname == Month && c.mrreadername == name)).Count;//一个抄表员在本周期的总抄表数
            var counts = new
            {
                date = date,
                Counts= Counts,
                SumCounts= pageModel.dataCount,
            };
            for (int i = 0; i < pageModel.dataCount; i++)
            {
                uploadGPS.Add(pageModel.data[i].uploadgisplace);
               
                var Data = new
                {
                    account = pageModel.data[i].account,
                    username = pageModel.data[i].username,
                    mrreadername = pageModel.data[i].mrreadername,
                    taskperiodname = pageModel.data[i].taskperiodname,
                    uploadtime = pageModel.data[i].uploadtime,
                    uploadGPS = uploadGPS,
                    counts= counts
                };
                ReturnData.Add(Data);
            }
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                data = ReturnData,
                count = pageModel.dataCount,
            };
        }
    }
}

	//----------v_mr_datainfo结束----------

	