using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 用户一站式管理之换表记录和地理位置
    /// </summary>
    [Route("api/OneUserManagement")]
    [ApiController]
    [AllowAnonymous]
    public class OneUserManagementController : Controller
    {
        #region 参数名称
        readonly Iv_watermeterinfoServices _v_watermeterinfoServices;
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="B_WatermetersServices"></param>
        public OneUserManagementController(Iv_watermeterinfoServices v_watermeterinfoServices)
        {
            _v_watermeterinfoServices = v_watermeterinfoServices;
        }
        #endregion

        #region
        /// <summary>
        /// 用户换表记录
        /// </summary>
        /// <param name="autoaccount"></param>
        /// <returns></returns> 
        [HttpPost]
        [Route("changewater")]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> changewater(string autoaccount)
        { 
            var data01 = await _v_watermeterinfoServices.Query(c => c.autoaccount == autoaccount);//查询对应用户的信息  
            List<object> list01 = new List<object>();
          
            for (int i = 0; i < data01.Count; i++)
            {
                var t = new {
                    autoaccount = data01[i].autoaccount,//用户id
                   meternum = data01[i].meternum,//水表编号
                caliber = data01[i].caliber,//口径
                bwcode = data01[i].bwcode,//初始底数
                    posname = data01[i].posname,//安装位置
                lastwaternum = data01[i].lastwaternum,//截止底数
                meterstate = data01[i].meterstate,//状态
                installtime = data01[i].installtime,//安装时间
                 readername = data01[i].readername,//安装人
                remark = data01[i].remark,//换表原因 备注
                updatemetertime = data01[i].updatemetertime,//更换时间
                GISPlace = data01[i].GISPlace,//Gis位置 
                processpreson = data01[i].processpreson//换表人 
                };
                list01.Add(t);

            }
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                data = list01
            };
        }
        #endregion
        #region
        /// <summary>
        /// 地理位置
        /// </summary>
        /// <param name="autoaccount"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("geograpposition")]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> geograpposition(string autoaccount)
        {

            List<string> list01 = new List<string>();
            var data01 = await _v_watermeterinfoServices.Query(c => c.autoaccount == autoaccount&&c.meterstate==1);//查询对应用户的信息  
            foreach (var item in data01)
            {
                list01.Add(item.GISPlace);
            } 
            return new TableModel<object>
            {
                code = 0,
                msg = "OK",
                data = list01
            };
        }
        #endregion
    }
}