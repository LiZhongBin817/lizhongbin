using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 故障处理记录历史表
    /// </summary>
    public class rb_b_faultprocess_history
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public int id { get; set; }

        /// <summary>
        /// 故障id(关联rt_b_faultinfo)
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "故障id(关联rt_b_faultinfo)")]
        public int faultid { get; set; }

        /// <summary>
        /// 故障类型
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "故障类型0--受理，1--处理，2--审核")]
        public int faulttype { get; set; }

        /// <summary>
        /// 受理业务--派工人;处理业务--处理人
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 20, ColumnDescription = "受理业务--派工人;处理业务--处理人")]
        public string processpreson { get; set; }

        /// <summary>
        /// 受理业务:最后处理时间;处理业务:处理时间
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "受理业务:最后处理时间;处理业务:处理时间")]
        public DateTime processdatetime { get; set; }

        /// <summary>
        /// 备注或描述说明
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "备注或描述说明")]
        public string processmark { get; set; }

        /// <summary>
        /// 处理结果(0--通过;1--不通过)
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "0--通过;1--不通过")]
        public int processresult { get; set; }

        /// <summary>
        /// 处理来源:APP、后台管理系统
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 20, ColumnDescription = "处理来源:APP、后台管理系统")]
        public string processsource { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "处理时间")]
        public DateTime createtime { get; set; }

        /// <summary>
        /// 处理人(关联sys_userinfo)
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 20, ColumnDescription = "处理人(关联sys_userinfo/mr_b_reader)")]
        public string createperson { get; set; }

        /// <summary>
        /// 水表编号
        /// </summary>
        public string meternum { get; set; }

        /// <summary>
        /// 任务周期
        /// </summary>
        public string taskperiodname { get; set; }

    }
}
