using CDWM_MR.Common.HttpContextUser;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks.Job
{

    /// <summary>
    /// 自动结转
    /// </summary>
    public class AutoTask_AutoCarryOver : IJob
    {

        #region 相关变量
        readonly Irt_b_recheckServices _B_RecheckServices;
        readonly Irt_b_watercarryoverServices _B_WatercarryoverServices;
        readonly Imr_datainfoServices _DatainfoServices;
        readonly Iv_recheck_recheckhistoryServices _Recheck_RecheckhistoryServices;
        readonly Imr_datainfo_historyServices _Datainfo_HistoryServices;
        readonly IUser _user;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="b_RecheckServices"></param>
        /// <param name="b_WatercarryoverServices"></param>
        /// <param name="datainfoServices"></param>
        /// <param name="recheck_RecheckhistoryServices"></param>
        /// <param name="datainfo_HistoryServices"></param>
        /// <param name="user"></param>
        public AutoTask_AutoCarryOver( Irt_b_recheckServices b_RecheckServices, Irt_b_watercarryoverServices b_WatercarryoverServices, Imr_datainfoServices datainfoServices, Iv_recheck_recheckhistoryServices recheck_RecheckhistoryServices,  Imr_datainfo_historyServices datainfo_HistoryServices, IUser user)
        {
            _B_RecheckServices = b_RecheckServices;
            _B_WatercarryoverServices = b_WatercarryoverServices;
            _DatainfoServices = datainfoServices;
            _Recheck_RecheckhistoryServices = recheck_RecheckhistoryServices;
            _Datainfo_HistoryServices = datainfo_HistoryServices;
            _user = user;
        }

        /// <summary>
        /// 执行自动结转任务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            #region 连接数据库查询
            //查询结转表里面的数据
            List<rt_b_watercarryover> carryedData = await _B_WatercarryoverServices.Query();
            //查询审核表中审核状态为0（审核通过了的）的所有数据
            List<rt_b_recheck> checkPassData = await _B_RecheckServices.Query(c => c.recheckstatus == 0);
            List<v_recheck_recheckhistory> rt_B_Rechecks = await _Recheck_RecheckhistoryServices.Query();
            List<mr_datainfo_history> datainfo_Histories = await _Datainfo_HistoryServices.Query();
            List<mr_datainfo> dataInfo = await _DatainfoServices.Query();
            #endregion
            List<rt_b_watercarryover> Datalist = new List<rt_b_watercarryover>();
            List<rt_b_watercarryover> data = new List<rt_b_watercarryover>();
            if (checkPassData.Count == 0)//没有审核通过的数据
            {
                return;
            }
            foreach (var item in checkPassData)
            {
                rt_b_watercarryover addData = new rt_b_watercarryover();
                data = carryedData.FindAll(c => c.autoaccount == item.userid && c.taskperiodname == item.taskperiodname);
                if (data.Count == 0)//判重
                {
                    List<v_recheck_recheckhistory> rtRecheckData = new List<v_recheck_recheckhistory>();
                    List<mr_datainfo_history> datainfoHistory_Data = new List<mr_datainfo_history>();

                    //如果当月月份是月初，将查询上一年12月份的数据
                    if (item.taskperiodname == DateTime.Now.Year.ToString() + "01")
                    {
                        string datetime = item.taskperiodname.Insert(4, "-");
                        string ReadTime = (DateTime.Parse(datetime).AddMonths(-1)).ToString().Substring(0, 7).Replace("/", "");
                        rtRecheckData = rt_B_Rechecks.FindAll(c => c.userid == item.userid && c.taskperiodname == ReadTime);
                        datainfoHistory_Data = datainfo_Histories.FindAll(c => c.autoaccount == item.userid && c.taskperiodname == ReadTime);
                    }
                    else//不是月初
                    {
                        int number = Convert.ToInt32(item.taskperiodname);
                        rtRecheckData = rt_B_Rechecks.FindAll(c => c.userid == item.userid && c.taskperiodname == (number - 1).ToString());
                        datainfoHistory_Data = datainfo_Histories.FindAll(c => c.autoaccount == item.userid && c.taskperiodname == (number - 1).ToString());
                    }
                    addData.autoaccount = item.userid.ToString();
                    addData.taskperiodname = item.taskperiodname;
                    addData.meternum = item.meternum;
                    if (rtRecheckData.Count == 0)
                    {
                        addData.startnum = 0;
                    }
                    else
                    {
                        addData.startnum = (decimal)rtRecheckData[0].recheckdata;
                    }

                    if (datainfoHistory_Data.Count == 0)
                    {
                        addData.starttime = DateTime.Now;
                        addData.startid = 0;
                    }
                    else
                    {
                        addData.starttime = datainfoHistory_Data[0].uploadtime;
                        addData.startid = datainfoHistory_Data[0].id;
                    }
                    addData.endtime = dataInfo.FindAll(c => c.autoaccount == item.userid && c.taskperiodname == item.taskperiodname)[0].uploadtime;
                    addData.endid = dataInfo.FindAll(c => c.autoaccount == item.userid && c.taskperiodname == item.taskperiodname)[0].id;
                    addData.endnum = item.recheckdata;
                    addData.carrywatercount = addData.endnum - addData.startnum;
                    addData.adjustwatercount = 0;
                    addData.createtime = DateTime.Now;
                    addData.createperson ="系统自动结转";
                    if (addData.carrywatercount >= 0)
                    {
                        addData.carrystatus = 1;
                    }
                    else
                    {
                        addData.carrystatus = 2;
                    }
                    addData.remark = "已备注";
                    Datalist.Add(addData);
                }
            }
            if (Datalist.Count == 0)
            {
                return;
            }
            int b = await _B_WatercarryoverServices.Add(Datalist);
            return;
        }
    }
}
