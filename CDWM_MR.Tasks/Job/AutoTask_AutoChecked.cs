using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks.Job
{
    /// <summary>
    ///自动审核任务引擎
    /// </summary>
    public class AutoTask_AutoChecked:IJob
    {
        #region 相关变量
         readonly Iv_mr_datainfoServices _Mr_DatainfoServices;
         readonly Irt_b_recheckServices _B_RecheckServices;
         readonly Imr_datainfoServices _DatainfoServices;
        #endregion

        public AutoTask_AutoChecked(Iv_mr_datainfoServices mr_DatainfoServices,Irt_b_recheckServices b_RecheckServices, Imr_datainfoServices datainfoServices)
        {
            _Mr_DatainfoServices = mr_DatainfoServices;
            _B_RecheckServices = b_RecheckServices;
            _DatainfoServices = datainfoServices;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            List<rt_b_recheck> AddData = new List<rt_b_recheck>();
            //查出还未审核的数据
            var data = await _Mr_DatainfoServices.Query(c=>c.recheckstatus!=0&&c.recheckstatus!=1);
            decimal? inputdata = 0;//人工输入的数据
            decimal? orcdata = 0;//图像识别出来的读数
            foreach (var item in data)
            {
                inputdata =item.inputdata;
                orcdata = item.ocrdata;
                //声明一个审核表的一个对象用于插入或者修改
                rt_b_recheck b_Recheck = new rt_b_recheck();
                if (orcdata>=inputdata-5&&orcdata<inputdata+5)
                {
                    b_Recheck.readdataid= item.ID;
                    b_Recheck.meternum = item.meternum;
                    b_Recheck.userid = item.autoaccount;
                    b_Recheck.taskperiodname = item.taskperiodname;
                    b_Recheck.recheckstatus = 0;
                    b_Recheck.recheckdata =(decimal)orcdata;
                    b_Recheck.checksuccesstime = DateTime.Now;
                    b_Recheck.checkor = "0";//0表示系统自动审核
                    b_Recheck.createtime = DateTime.Now;
                    b_Recheck.createpeople = "系统自动创建";                   
                }
                else//不在规定范围内将不进行自动审核
                {
                    continue;
                }
                AddData.Add(b_Recheck);            
            }
            int b = await _B_RecheckServices.Add(AddData);
            var autocheckeddata = await _B_RecheckServices.Query(c => c.checkor == "0");//查出系统自动审核的数据
            foreach (var item1 in autocheckeddata)
            {
                //将抄表数据表中的审核状态改为了已审
                await _DatainfoServices.Update(c => new mr_datainfo
                {
                    readcheckdata = item1.recheckdata,
                    recheckstatus = 1,
                    readtype = 1,
                    readstatus = 3,
                    recheckresult = "系统自动审核",
                }, c => c.autoaccount == item1.userid && c.taskperiodname == item1.taskperiodname);
            }
        }
    }
}
