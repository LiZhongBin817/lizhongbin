using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
    /// 
    /// </summary>
    public class CarryOverDataManageController : ControllerBase
    {
        #region 相关变量
        readonly Iv_carryoverdatainfoServices _CarryoverdatainfoServices;
        readonly Irt_b_wateradjustServices _B_WateradjustServices;
        readonly Irt_b_watercarryovarcheckServices _B_WatercarryovarcheckServices;
        readonly Irt_b_watercarryoverServices _B_WatercarryoverServices;
        readonly It_b_usersServices _B_UsersServices;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="carryoverdatainfoServices"></param>
        /// <param name="b_WateradjustServices"></param>
        /// <param name="b_WatercarryovarcheckServices"></param>
        /// <param name="b_WatercarryoverServices"></param>
        /// <param name="b_UsersServices"></param>
        public CarryOverDataManageController(Iv_carryoverdatainfoServices carryoverdatainfoServices, Irt_b_wateradjustServices b_WateradjustServices, Irt_b_watercarryovarcheckServices b_WatercarryovarcheckServices, Irt_b_watercarryoverServices b_WatercarryoverServices, It_b_usersServices b_UsersServices)
        {
            _CarryoverdatainfoServices = carryoverdatainfoServices;
            _B_WateradjustServices = b_WateradjustServices;
            _B_WatercarryovarcheckServices = b_WatercarryovarcheckServices;
            _B_WatercarryoverServices = b_WatercarryoverServices;
            _B_UsersServices = b_UsersServices;
        }

        /// <summary>
        /// 结转数据管理数据展示
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowCarriedData")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> ShowCarriedData(int page=1,int limit=10)
        {
            PageModel<v_carryoverdatainfo> pageModel = new PageModel<v_carryoverdatainfo>();
            Expression<Func<v_carryoverdatainfo, bool>> wherelambda = c => true;
            pageModel = await _CarryoverdatainfoServices.QueryPage(wherelambda, page, limit, "");
            return new TableModel<object>
            {
                code=0,
                msg="ok",
                data= pageModel.data,
                count =pageModel.dataCount,
            };
        }
        /// <summary>
        /// 调整用量
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeCarryCounts")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> ChangeCarryCounts(string[] accounts,string JsonData)
        {
            rt_b_wateradjust JustData = Common.Helper.JsonHelper.GetObject<rt_b_wateradjust>(JsonData);
 
            var CarryInfo = await _B_WatercarryoverServices.Query();
            List<t_b_users> UserInfo = await _B_UsersServices.OQuery(c=>true);
            List<rt_b_wateradjust> AddData = new List<rt_b_wateradjust>();
            for (int i = 0; i < accounts.Length; i++)
            {
                rt_b_wateradjust b_Wateradjust = new rt_b_wateradjust();
                var atuoaccount = UserInfo.FindAll(c => c.account == accounts[i])[0].autoaccount;
                b_Wateradjust.carryoverid= CarryInfo.FindAll(c=>c.autoaccount== atuoaccount)[0].id;
                b_Wateradjust.adjustwatercount = JustData.adjustwatercount;
                b_Wateradjust.adjustperson = JustData.adjustperson;
                b_Wateradjust.adjusttime = JustData.adjusttime;
                b_Wateradjust.adjustremark = JustData.adjustremark;
                b_Wateradjust.createperson = Permissions.UersName;
                b_Wateradjust.createtime = DateTime.Now;
                AddData.Add(b_Wateradjust);
            }
            int b = await _B_WateradjustServices.Add(AddData);
            return new TableModel<object>
            {
                code = b!=0?0:1001,
                msg = b!=0?"ok":"No",
                data = AddData,
                count = AddData.Count,
            };
        }

        /// <summary>
        /// 重新结转
        /// </summary>
        /// <param name="account"></param>
        /// <param name="JsonData"></param>
        /// <param name="meternum"></param>
        /// <param name="taskperiodname"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReCarryOver")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> ReCarryOver(string account,string JsonData,string meternum,string taskperiodname)
        {
            rt_b_watercarryovarcheck b_Watercarryovarcheck = Common.Helper.JsonHelper.GetObject<rt_b_watercarryovarcheck>(JsonData);
            List<rt_b_watercarryovarcheck> InsertData = new List<rt_b_watercarryovarcheck>();
          rt_b_watercarryovarcheck addData = new rt_b_watercarryovarcheck();
            //用户编号对应自动编号（如同ID）
            string autoaccount = (await _B_UsersServices.OQuery(c => c.account== account))[0].autoaccount;
            int CarryID =( await _B_WatercarryoverServices.Query(c=>c.autoaccount== autoaccount))[0].id;
            if (b_Watercarryovarcheck.finishturnstatus == 0)
            {
                bool b = await _B_WatercarryoverServices.Update(c => new rt_b_watercarryover
                {
                    carrystatus = 1,
                }, c => c.autoaccount == autoaccount);
            }            
            addData.carryoverid = CarryID;
            addData.userid =autoaccount;
            addData.meternum = meternum;
            addData.taskperiodname = taskperiodname;
            addData.turndatainfo = b_Watercarryovarcheck.turndatainfo;
            addData.turndate = b_Watercarryovarcheck.turndate;
            addData.finishturnstatus = b_Watercarryovarcheck.finishturnstatus;
            InsertData.Add(addData);
            int a = await _B_WatercarryovarcheckServices.Add(InsertData);
            return new TableModel<object>
            {
                code = a == 1? 0 : 1001,
                msg =  a == 1 ? "ok" : "NO",
                data = "",
            };
        }

    }
}