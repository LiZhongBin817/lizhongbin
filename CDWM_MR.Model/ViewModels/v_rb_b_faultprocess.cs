using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.ViewModels
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_rb_b_faultprocess
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_rb_b_faultprocess()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get; set; }

        /// <summary>
        /// 抄表数据id
        /// </summary>
        public System.Int32? readdataid { get; set; }

        /// <summary>
        /// 任务账期(201909)
        /// </summary>
        public System.String taskperiodname { get; set; }

        /// <summary>
        /// 故障id(关联rt_b_faultinfo)
        /// </summary>
        public System.Int32 faultid { get; set; }

        /// <summary>
        /// 故障类型0--受理，1--处理，2--审核
        /// </summary>
        public System.Int32 faulttype { get; set; }

        /// <summary>
        /// 受理业务--派工人;处理业务--处理人
        /// </summary>
        public System.String processpreson { get; set; }

        /// <summary>
        /// 受理业务:最后处理时间;处理业务:处理时间
        /// </summary>
        public System.DateTime? processdatetime { get; set; }

        /// <summary>
        /// 备注或描述说明
        /// </summary>
        public System.String processmark { get; set; }

        /// <summary>
        /// 0--通过;1--不通过
        /// </summary>
        public System.Int32 processresult { get; set; }

        /// <summary>
        /// 处理来源:APP、后台管理系统
        /// </summary>
        public System.String processsource { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public System.DateTime createtime { get; set; }

        /// <summary>
        /// 处理人(关联sys_userinfo/mr_b_reader)
        /// </summary>
        public System.String createperson { get; set; }

        /// <summary>
        /// 用户账号信息
        /// </summary>
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 水表编号(t_b_watermeters)
        /// </summary>
        public System.String meternum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String username { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get; set; }

        /// <summary>
        /// 上报时间,故障工单上传时间
        /// </summary>
        public System.DateTime? reporttime { get; set; }

        /// <summary>
        /// 上报人
        /// </summary>
        public System.String reportpeople { get; set; }

        /// <summary>
        /// 抄表员id（mr_b_reader：:id）
        /// </summary>
        public System.Int32? readerid { get; set; }
        /// <summary>
        /// 故障编号（自动生成）
        /// </summary>
        public System.String faultnumber { get; set; }

        /// <summary>
        /// 故障信息内容
        /// </summary>
        public System.String faultcontent { get; set; }
    }

}
