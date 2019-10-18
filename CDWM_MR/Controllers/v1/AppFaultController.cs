using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// 故障工单
    /// </summary>
    [Route("api/[controller]")]
    [AllowAnonymous]
    //或者是写[Route("api/[controller]/[action]")]，下面就不要写Route啥的了
    public class AppFaultController : ControllerBase
    {
        #region 相关变量
        readonly Irt_b_faultinfoServices _rt_b_faultinfoServices;
        readonly Iv_rb_b_faultprocessServices _v_rb_b_faultprocessServices;
        readonly Irb_b_faultprocessServices _rb_b_faultprocessServices;
        private readonly Iv_rt_b_faultinfoServices _v_rt_b_faultinfoServices;
        private readonly Irt_b_photoattachmentServices _rt_b_photoservices;
        private readonly IMapper _mapper;
        #endregion 

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="rt_b_faultinfoServices"></param>
        /// <param name="v_rb_b_faultprocessServices"></param>
        /// <param name="rb_b_faultprocessServices"></param>
        /// <param name="vrtbfaultservices"></param>
        /// <param name="mapper"></param>
        /// <param name="photoservices"></param>
        public AppFaultController(Irt_b_faultinfoServices rt_b_faultinfoServices, Iv_rb_b_faultprocessServices v_rb_b_faultprocessServices, Irb_b_faultprocessServices rb_b_faultprocessServices, Iv_rt_b_faultinfoServices vrtbfaultservices,IMapper mapper, Irt_b_photoattachmentServices photoservices)
        {
            _rt_b_faultinfoServices = rt_b_faultinfoServices;
            _v_rb_b_faultprocessServices = v_rb_b_faultprocessServices;
            _rb_b_faultprocessServices = rb_b_faultprocessServices;
            _v_rt_b_faultinfoServices = vrtbfaultservices;
            _rt_b_photoservices = photoservices;
            _mapper = mapper;
        }

        #region  提交用户故障工单接口
        /// <summary>
        /// 提交用户故障工单接口
        /// </summary>
        /// <param name="faultDate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateFaultWorkOrder")]
        public async Task<MessageModel<int>> UpdateFaultWorkOrder([FromBody] List<rt_b_faultinfo> faultDate)
        {
            var data = new MessageModel<int>();
            try
            {
                string faultnumber = $"{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString().PadLeft(2, '0')}{DateTime.Now.Day.ToString().PadLeft(2, '0')}";
                List<rt_b_faultinfo> dateist = await _rt_b_faultinfoServices.Query(c => c.faultnumber.Contains(faultnumber));
                for (int i = 0; i < faultDate.Count; i++)
                {
                    faultnumber += (dateist.Count() + 1).ToString().PadLeft(3, '0');
                    int a = await _rt_b_faultinfoServices.Add(faultDate[i]);
                    string tasknumber = faultDate[i].taskperiodname,meternum = faultDate[i].meternum;
                    await _rt_b_photoservices.Update(s => new rt_b_photoattachment() {billid = a },c => c.taskperiodname == tasknumber && c.metercode == meternum && c.phototype == 2);
                }
            }
            catch (Exception ex) 
            {
                data.code = 1001;
                data.msg = ex.ObjToString();
                data.data = 0;
                return data;
            }
            data.code = 0;
            data.msg = "成功！";
            data.data = faultDate.Count;
            return data;
        }
        #endregion

        #region  获取本周期有异常的数据的接口
        /// <summary>
        /// 获取本周期有异常的数据的接口
        /// </summary>
        /// <param name="readerid">抄表员ID</param>
        /// <param name="taskperiodname">任务账单</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetFaultWorkOrder")]
        public async Task<object> GetFaultWorkOrder(int readerid, string taskperiodname)
        {
            #region lambda拼接式
            Expression<Func<v_rb_b_faultprocess, bool>> wherelambda = c => true;
            if (readerid!=0)
            {
                wherelambda = PredicateExtensions.And<v_rb_b_faultprocess>(wherelambda, c => c.readerid == readerid);
            }
            if (!string.IsNullOrEmpty(taskperiodname))
            {
                wherelambda = PredicateExtensions.And<v_rb_b_faultprocess>(wherelambda, c => c.taskperiodname == taskperiodname);
            }
            #endregion
            List<v_rb_b_faultprocess> datelist = await _v_rb_b_faultprocessServices.Query(wherelambda);
            var data= datelist.Select(c => new
            {
                CustomerNumber = c.autoaccount,//用户家庭编号
                CustomerMeterNumber = c.meternum,//用户水表编号
                CustomerName = c.username,//用户名字
                CustomerUseWaterAddress = c.areaname + c.address,//用户用水地址
                CustomerMeterStatus=c.faulttype,//故障上报
                AcceptPerson=c.processpreson,//受理人的名字
                AcceptTime=c.processdatetime,//后台受理时间
                WorkDisposeName=c.reportpeople,//工单接收人
                EndDisposeTime=c.reporttime,//最迟处理时间
            });
            return new JsonResult(new
            {
                code = 0,
                msg = "成功",
                data = data
            });
        }
        #endregion

        #region 故障处理接口
        /// <summary>
        /// 故障处理接口
        /// </summary>
        /// <param name="FaultHandlinglist"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FaultHandling")]
        public async Task<MessageModel<int>> FaultHandling([FromBody]List<rb_b_faultprocess> FaultHandlinglist)
        {
            var data = new MessageModel<int>();
            if (FaultHandlinglist == null || FaultHandlinglist?.Count == 0)
            {
                data.code = 1001;
                data.msg = "无上传数据！";
                data.data = 0;
                return data;
            }
            try
            {
                for (int i = 0; i < FaultHandlinglist.Count; i++)
                {
                    int a = await _rb_b_faultprocessServices.Add(FaultHandlinglist[i]);
                    string meternum = FaultHandlinglist[i].meternum, tasknumber = FaultHandlinglist[i].taskperiodname;
                    await _rt_b_photoservices.Update(c => new rt_b_photoattachment() { billid = a},c => c.metercode == meternum && c.taskperiodname == tasknumber);//修改图片表billid
                }
            }
            catch (Exception ex)
            {
                data.code = 1001;
                data.msg = ex.ObjToString();
                data.data = 0;
                return data;
            }
            data.code = 0;
            data.msg = "成功";
            data.data = FaultHandlinglist.Count;
            return data;
        }
        #endregion


        #region 获取表册内的故障信息
        /// <summary>
        /// 获取表册内的故障信息
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetbookFault")]
        public async Task<MessageModel<List<vfaultinfo>>> GetbookFault(int? taskid)
        {
            var data = new MessageModel<List<vfaultinfo>>();
            var temp = await _v_rt_b_faultinfoServices.Query(c => c.taskid == taskid);
            if (temp == null || temp?.Count <= 0)
            {
                data.code = 1001;
                data.msg = "没有对应的故障信息！";
                return data;
            }
            List<vfaultinfo> rlist = new List<vfaultinfo>();
            foreach (var item in temp)
            {
                rlist.Add(_mapper.Map<vfaultinfo>(item));
            }
            data.code = 0;
            data.msg = "成功！";
            data.data = rlist;
            return data;
        }
        #endregion

        #region 获取单个故障详细信息
        /// <summary>
        /// 获取单个故障详细信息
        /// </summary>
        /// <param name="faultid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSingleFaultinfo")]
        public async Task<MessageModel<string>> GetSingleFaultinfo(int? faultid)
        {
            var data = new MessageModel<string>();
            var rdata = await _rt_b_faultinfoServices.Query(c => c.id == faultid);
            return data;
        }
        #endregion

    }
}
