using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 派工单历史信息
    /// </summary>
    public class dispatch_faultinfo_history
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        #region 上传故障工单
        /// <summary>
        /// 故障编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 20)]
        public string faultnumber { get; set; }

        /// <summary>
        /// 故障类型
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 100)]
        public string faulttype { get; set; }

        /// <summary>
        /// 水表编号(t_b_watermeters)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 10)]
        public string meternum { get; set; }

        /// <summary>
        /// 用户账号信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 15)]
        public string autoaccount { get; set; }

        /// <summary>
        /// 故障信息内容
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string faultcontent { get; set; }

        /// <summary>
        /// 附件信息---至少3张照片信息
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "text")]
        public string imginfos { get; set; }

        /// <summary>
        /// 上报时间,故障工单上传时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime reporttime { get; set; }

        /// <summary>
        /// gis位置信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string gisinfo { get; set; }

        /// <summary>
        /// 抄表员编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50)]
        public string mrreadernumber { get; set; }

        /// <summary>
        /// 抄表员姓名
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string mrreadername { get; set; }

        /// <summary>
        /// 上报人姓名
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string reportpeoplename { get; set; }

        /// <summary>
        /// 上报人编号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string reportpeoplenumber { get; set; }
        #endregion

        #region 派工信息
        /// <summary>
        /// 派工人姓名(mr_b_reader)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string dispatchername { get; set; }

        /// <summary>
        /// 派工人编号(mr_b_reader)
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string dispatchernumber { get; set; }

        /// <summary>
        /// 操作派工人员姓名(sys_userinfo)
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 100)]
        public string operadispatchname { get; set; }

        /// <summary>
        /// 操作派工人员编号
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 80)]
        public string operadispatchnumber { get; set; }

        /// <summary>
        /// 派工时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime dispatchtime { get; set; }

        /// <summary>
        /// 最迟处理时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime lasthandletime { get; set; }
        #endregion

        #region 处理故障信息
        /// <summary>
        /// 故障处理时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime handletime { get; set; }

        /// <summary>
        /// 故障处理信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string handleinfo { get; set; }

        /// <summary>
        /// 处理后上传的图片
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "text")]
        public string handleimg { get; set; }

        #endregion

    }
}
