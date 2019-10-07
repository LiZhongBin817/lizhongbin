using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// 抄表数据接口
    /// </summary>
    [Route("api/[controller]")]
    //或者是写[Route("api/[controller]/[action]")]，下面就不要写Route啥的了
    public class AppMeterReadingDataController : Controller
    {
        #region  相关变量
        readonly Imr_datainfoServices _mr_datainfoServices;
        readonly Iv_mr_datainfoServices _v_mr_datainfoServices;
        #endregion
        public AppMeterReadingDataController(Imr_datainfoServices mr_datainfoServices, Iv_mr_datainfoServices v_mr_datainfoServices)
        {
            _mr_datainfoServices = mr_datainfoServices;
            _v_mr_datainfoServices = v_mr_datainfoServices;
        }
        #region  上传用户抄表数据接口
        /// <summary>
        /// 上传用户抄表数据接口
        /// </summary>
        /// <param name="UserData">用户抄表数据接口对象</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateUserData")]
        [AllowAnonymous]//允许所有都访问
        public async Task<int> UpdateUserData([FromBody]List<mr_datainfo> UserData)
        {
            int Status = 0;
            await _mr_datainfoServices.Add(UserData);
            Status = UserData.Count();
            return Status;
        }
        #endregion
        #region   获取本周期已经审查的抄表数据接口
        /// <summary>
        /// 获取本周期已经审查的抄表数据接口
        /// </summary>
        /// <param name="taskperiodname">任务周期</param>
        /// <param name="taskid">任务单ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetcheckedReadingWaterDate")]
        [AllowAnonymous]//允许所有都访问
        public async Task<object> GetcheckedReadingWaterDate(string taskperiodname, int taskid)
        {
            List<v_mr_datainfo> dateinfolist = await _v_mr_datainfoServices.Query(c => c.taskid == taskid && c.taskperiodname == taskperiodname);
            List<object> datelist = new List<object>();
            for (int i = 0; i < dateinfolist.Count(); i++)
            {
                //地址=小区+家庭地址
                string address = dateinfolist[i].areaname + dateinfolist[i].address;
                var date = new
                {
                    ID = dateinfolist[i].ID,
                    CustomerNumber = dateinfolist[i].autoaccount,//用户家庭编号
                    CustomerMEterNumber = dateinfolist[i].meternum,//用户水表编号
                    CustomerName = dateinfolist[i].username,//用户名字
                    CustomerUseWaterAddress = address,//用户用水地址
                    CusWaterConsumption = dateinfolist[i].inputdata//用户用水量
                };
                datelist.Add(date);
            }
            return new JsonResult(new
            {
                code = 0,
                msg = "成功",
                data = datelist
            });
        }
        #endregion

    }
}
