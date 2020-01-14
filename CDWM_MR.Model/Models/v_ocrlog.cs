using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_ocrlog
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_ocrlog()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get; set; }

        /// <summary>
        /// 业务编号如抄表:关联抄表id,故障:关联故障id,0--默认不关联
        /// </summary>
        public System.Int32 billid { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public System.String createpeople { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime createtime { get; set; }

        /// <summary>
        /// 水表编号
        /// </summary>
        public System.String metercode { get; set; }

        /// <summary>
        /// 照片编号
        /// </summary>
        public System.String photocode { get; set; }

        /// <summary>
        /// 文件名后缀
        /// </summary>
        public System.String photoext { get; set; }

        /// <summary>
        /// 照片名称
        /// </summary>
        public System.String photonname { get; set; }

        /// <summary>
        /// 拍照时间
        /// </summary>
        public System.DateTime phototime { get; set; }

        /// <summary>
        /// 照片类型(0--其他类型;1--表盘抄表;2--现场照片;3--故障处理后(故障);4--故障照片)
        /// </summary>
        public System.Int32? phototype { get; set; }

        /// <summary>
        /// 服务器存储路径
        /// </summary>
        public System.String photourl { get; set; }

        /// <summary>
        /// 抄表员编号
        /// </summary>
        public System.String readercode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String remark { get; set; }

        /// <summary>
        /// 任务账期(201909)
        /// </summary>
        public System.String taskperiodname { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public System.String updatepeople { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? updatetime { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public System.String usercode { get; set; }

        /// <summary>
        ///识别标识 0---未识别 1---已识别
        /// </summary>
        public System.Int32 temp { get; set; }
        /// <summary>
        /// 抄表数据id
        /// </summary>
        public int readdataid { get; set; }
    }
}
