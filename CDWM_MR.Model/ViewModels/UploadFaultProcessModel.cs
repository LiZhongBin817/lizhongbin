using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.ViewModels
{
    /// <summary>
    /// 上传抄表处理对象
    /// </summary>
    public class UploadFaultProcessModel
    {
        /// <summary>
        /// 任务账期(201909)
        /// </summary>
        public string taskperiodname { get; set; }

        /// <summary>
        /// 故障id(关联rt_b_faultinfo)
        /// </summary>
        public int faultid { get; set; }

        /// <summary>
        /// 受理业务--派工人;处理业务--处理人
        /// </summary>
        public string processpreson { get; set; }

        /// <summary>
        /// 受理业务:最后处理时间;处理业务:处理时间
        /// </summary>
        public DateTime processdatetime { get; set; }

        /// <summary>
        /// 备注或描述说明
        /// </summary>
        public string processmark { get; set; }

        /// <summary>
        /// 处理结果(0--通过;1--不通过)
        /// </summary>
        public int processresult { get; set; }

        /// <summary>
        /// 数据创建人
        /// </summary>
        public string createperson { get; set; }

        /// <summary>
        /// 水表编号
        /// </summary>
        public string meternum { get; set; }

        /// <summary>
        /// 是否上传过故障处理图片0--未上传;1--已上传
        /// </summary>
        public int isupdateimg { get; set; }
    }
}
