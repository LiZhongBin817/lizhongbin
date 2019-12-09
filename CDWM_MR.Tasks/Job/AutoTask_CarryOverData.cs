using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks.Job
{
    /// <summary>
    /// 结转历史表数据
    /// </summary>
    public class AutoTask_CarryOverData : IJob
    {
        #region 相关变量
        private readonly Imr_datainfoServices _mrdatainfoservices;
        private readonly Imr_planinfoServices _planinfoservices;
        private readonly Imr_taskinfoServices _mrtaskinfoservices;
        private readonly Imr_datainfoServices _mr_datainfoservices;
        private readonly Irb_b_faultprocessServices _rb_b_faultprocessservices;
        private readonly Irt_b_faultinfoServices _rt_b_faultinfoservices;
        private readonly Irt_b_ocrlogServices _rt_b_ocrlogservices;
        private readonly Irt_b_photoattachmentServices _rt_b_photoattachmentservices;
        private readonly Irt_b_recheckServices _rt_b_recheckservices;
        private readonly Irt_b_wateradjustServices _rt_b_wateradjustservice;
        private readonly Irt_b_watercarryovarcheckServices _rt_b_watercarryovarcheckservices;
        private readonly Irt_b_watercarryoverServices _rt_b_watercarryoverservices;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mrdatainfoservices"></param>
        public AutoTask_CarryOverData(Imr_datainfoServices mrdatainfoservices, Imr_planinfoServices planinfoservices, Imr_taskinfoServices mrtaskinfoservices, Imr_datainfoServices mr_datainfoservices, Irt_b_faultinfoServices rt_b_faultinfoservices, Irt_b_ocrlogServices rt_b_ocrlogservices, Irt_b_photoattachmentServices rt_b_photoattachmentservices, Irt_b_recheckServices rt_b_recheckservices, Irt_b_wateradjustServices rt_b_wateradjustservices, Irt_b_watercarryovarcheckServices rt_b_watercarryovarcheckservices, Irt_b_watercarryoverServices rt_b_watercarryoverservices, Irb_b_faultprocessServices b_FaultprocessServices)
        {
            _mrdatainfoservices = mrdatainfoservices;
            _planinfoservices = planinfoservices;
            _mrtaskinfoservices = mrtaskinfoservices;
            _mr_datainfoservices = mr_datainfoservices;
            _rt_b_faultinfoservices = rt_b_faultinfoservices;
            _rt_b_ocrlogservices = rt_b_ocrlogservices;
            _rt_b_photoattachmentservices = rt_b_photoattachmentservices;
            _rt_b_recheckservices = rt_b_recheckservices;
            _rt_b_wateradjustservice = rt_b_wateradjustservices;
            _rt_b_watercarryovarcheckservices = rt_b_watercarryovarcheckservices;
            _rt_b_watercarryoverservices = rt_b_watercarryoverservices;
            _rb_b_faultprocessservices = b_FaultprocessServices;
        }

        /// <summary>
        /// 执行结转任务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            #region 将mr_planinfo转存进入历史表

            var taknumber = await _planinfoservices.Query(c => c.finishstatus == 2);
            if (taknumber.Count <= 0) return;//没有找到对应要转的信息
            string tasknumber = taknumber[0].mplanyear + taknumber[0].mplanmonth;
            await _planinfoservices.ExecutePro("insert into mr_planinfo_history(id,mplannumber,mplanname,mplanyear,mplanmonth,planstarttime,planendtime,createtime,createpeople,remark) select id,mplannumber,mplanname,mplanyear,mplanmonth,planstarttime,planendtime,createtime,createpeople,remark from mr_planinfo b where b.finishstatus=2", new { maxpalnnumber = tasknumber });
            bool delplaninfo = await _planinfoservices.DeleteTable(c => c.mplannumber.Equals(tasknumber));

            #endregion

            #region 将mr_taskinfo转存到历史表
            await _mrtaskinfoservices.ExecutePro("insert into mr_taskinfo_history(id,tasknumber,taskname,bookno,mrreadernumber,mplannumber,taskstarttime,taskendtime,downloadstarttime,downloadendtime,createtime,createpeople,remark,taskperiodname) select taskid as id,tasknumber,taskname,bookno,mrreadernumber,mplannumber,taskstarttime,taskendtime,downloadstarttime,downloadendtime,createtime,createpeople,remark,taskperiodname from v_taskinfo b where b.taskperiodname=@taskperiodname", new { taskperiodname = tasknumber });
            bool deltaskinfo = await _mrtaskinfoservices.DeleteTable(c => c.taskperiodname.Equals(tasknumber));
            #endregion

            #region 将mr_datainfo转存到历史表
            await _mr_datainfoservices.ExecutePro("insert into mr_datainfo_history( id,meternum,autoaccount,username,address,telephone,meterbookname,meterbooknumber,mrreadernumber,mrreadername,areano,areaname,regionno,regionname,taskperiodname, lastmonthdata,inputdata,usewaternum,readstatus,readDateTime,uploadtime,readtype,meterstatus,readcheckdata,recheckresult,remark,readerid) select ID,meternum,autoaccount,username,address,telephone,bookname as meterbookname,bookno as meterbooknumber,mrreadernumber,mrreadername,areano,areaname,regionno,regionname,taskperiodname,nowmonthdata as lastmonthdata, readcheckdata as inputdata,carrywatercount as usewaternum,readstatus,readDateTime,uploadtime,readtype,meterstatus,readcheckdata,recheckresult,remark,ID as readerid from v_mr_datainfo b where b.taskperiodname=@taskperiodname", new { taskperiodname = tasknumber });
            bool deldatainfo = await _mr_datainfoservices.DeleteTable(c => c.taskperiodname.Equals(tasknumber));
            #endregion

            #region 将rb_b_faultprocess转存到历史表
            await _rb_b_faultprocessservices.ExecutePro("insert into rb_b_faultprocess_history(id,faultid,faulttype,processpreson,processdatetime,processmark,processresult,processsource,createtime,createperson,meternum,taskperiodname) select id,faultid,faulttype,processpreson,processdatetime,processmark,processresult,processsource,createtime,createperson,meternum,taskperiodname from rb_b_faultprocess b where b.taskperiodname=@taskperiodname", new { taskperiodname = tasknumber });
            bool delrb_b_faultprocess = await _rb_b_faultprocessservices.DeleteTable(c => c.taskperiodname.Equals(tasknumber));
            #endregion

            #region 将rt_b_faultinfo转存到历史表
            await _rt_b_faultinfoservices.ExecutePro("insert into rt_b_faultinfo_history(id,readdataid,faulttype,faultnumber,meternum,taskperiodname,autoaccount,faultcontent,reporttime,gisinfo,meterstatus,readerid,reportpeople,faultstatus) select id,readdataid,faulttype,faultnumber,meternum,taskperiodname,autoaccount,faultcontent,reporttime,gisinfo,meterstatus,readerid,reportpeople,faultstatus from rt_b_faultinfo b where b.taskperiodname=@taskperiodname", new { taskperiodname = tasknumber });
            bool delrt_b_faultinfo = await _rt_b_faultinfoservices.DeleteTable(c => c.taskperiodname.Equals(tasknumber));
            #endregion

            #region 将rt_b_ocrlog转存到历史表
            await _rt_b_ocrlogservices.ExecutePro("insert into rt_b_ocrlog_history(id,readdataid,photoid,ocrdata,ocrtime,ocrstatus,ocrusesecond,createtime,createpeople,updatetime,updatepeople,remark,taskperiodname) select id,readdataid,photoid,ocrdata,ocrtime,ocrstatus,ocrusesecond,createtime,createpeople,updatetime,updatepeople,remark,taskperiodname from rt_b_ocrlog b where b.taskperiodname=@taskperiodname", new { taskperiodname = tasknumber });
            bool delrt_b_ocrlog = await _rt_b_ocrlogservices.DeleteTable(c => c.taskperiodname.Equals(tasknumber));
            #endregion

            #region 将rt_b_photoattachment转存到历史表
            await _rt_b_photoattachmentservices.ExecutePro("insert into rt_b_photoattachment_history(id,photocode,phototype,billid,photourl,photoext,taskperiodname,readercode,metercode,usercode,phototime,createtime,createpeople,remark) select id,photocode,phototype,billid,photourl,photoext,taskperiodname,readercode,metercode,usercode,phototime,createtime,createpeople,remark from rt_b_photoattachment b where b.taskperiodname=@taskperiodname", new { taskperiodname = tasknumber });
            bool delrt_b_photoattachment = await _rt_b_photoattachmentservices.DeleteTable(c => c.taskperiodname.Equals(tasknumber));
            #endregion

            #region 将rt_b_recheck转存到历史表
            await _rt_b_recheckservices.ExecutePro("insert into rt_b_recheck_history(id,readdataid,meternum,userid,taskperiodname,recheckstatus,recheckdata,recheckresult,checksuccesstime,checkor,createtime,createpeople) select id,readdataid,meternum,userid,taskperiodname,recheckstatus,recheckdata,recheckresult,checksuccesstime,checkor,createtime,createpeople from rt_b_recheck b where b.taskperiodname=@taskperiodname", new { taskperiodname = tasknumber });
            bool delrt_b_recheck = await _rt_b_recheckservices.DeleteTable(c => c.taskperiodname.Equals(tasknumber));
            #endregion

            #region 将rt_b_wateradjust转存到历史表
            await _rt_b_wateradjustservice.ExecutePro("insert into rt_b_wateradjust_history(id,carryoverid,adjustwatercount,adjustperson,adjusttime,adjustremark,createtime,createperson,taskperiodname) select id,carryoverid,adjustwatercount,adjustperson,adjusttime,adjustremark,createtime,createperson,taskperiodname from rt_b_wateradjust b where b.taskperiodname=@taskperiodname", new { taskperiodname = tasknumber });
            bool delrt_b_wateradjust = await _rt_b_wateradjustservice.DeleteTable(c => c.taskperiodname.Equals(tasknumber));
            #endregion

            #region 将rt_b_watercarryovarcheck转存到历史表
            await _rt_b_watercarryovarcheckservices.ExecutePro("insert into rt_b_watercarryovarcheck_history(id,carryoverid,userid,meternum,taskperiodname,turndatainfo,turndate,finishturnstatus) select id,carryoverid,userid,meternum,taskperiodname,turndatainfo,turndate,finishturnstatus from rt_b_watercarryovarcheck b where b.taskperiodname=@taskperiodname", new { taskperiodname = tasknumber });
            bool delrt_b_watercarryovarcheck = await _rt_b_watercarryovarcheckservices.DeleteTable(c => c.taskperiodname.Equals(tasknumber));
            #endregion

            #region 将rt_b_watercarryover转存到历史表
            await _rt_b_watercarryoverservices.ExecutePro("insert into rt_b_watercarryover_history(id,autoaccount,taskperiodname,meternum,startnum,starttime,startid,endnum,endtime,endid,carrywatercount,bookkeepingcount,adjustwatercount,createtime,createperson,carrystatus,remark) select id,autoaccount,taskperiodname,meternum,startnum,starttime,startid,endnum,endtime,endid,carrywatercount,bookkeepingcount,adjustwatercount,createtime,createperson,carrystatus,remark from rt_b_watercarryover b where b.taskperiodname=@taskperiodname", new { taskperiodname = tasknumber });
            bool delrt_b_watercarryover = await _rt_b_watercarryoverservices.DeleteTable(c => c.taskperiodname.Equals(tasknumber));
            #endregion
        }
    }
}
