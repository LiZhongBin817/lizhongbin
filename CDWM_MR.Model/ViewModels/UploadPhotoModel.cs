using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CDWM_MR.Model.ViewModels
{
    /// <summary>
    /// 接受上传图片
    /// </summary>
    public class UploadPhotoModel
    {
        /// <summary>
        /// 任务单编号
        /// </summary>
        public int taskid { get; set; }

        /// <summary>
        ///读数是否上传标识符(0--未上传;1--已上传)(APP)
        /// </summary>
        public int isreadupdate { get; set; }

        /// <summary>
        /// 照片编号(APP)
        /// </summary>
        public string photocode { get; set; }

        /// <summary>
        /// 照片名称(APP)
        /// </summary>
        public string photonname { get; set; }

        /// <summary>
        /// 照片类型(0--其他类型;1--表盘抄表;2--现场表况;3--故障处理后(故障);4--其他照片)(APP)
        /// </summary>
        public int phototype { get; set; } = 0;

        /// <summary>
        /// 业务编号如抄表关联id,故障关联故障id,0--默认不关联
        /// </summary>
        public int billid { get; set; } = 0;

        /// <summary>
        /// 服务器存储路径
        /// </summary>
        public string photourl { get; set; }

        /// <summary>
        /// 文件名后缀
        /// </summary>
        public string photoext { get; set; }

        /// <summary>
        /// 任务账期(201909)(APP)
        /// </summary>
        public string taskperiodname { get; set; }

        /// <summary>
        /// 抄表员编号(APP)
        /// </summary>
        public string readercode { get; set; }

        /// <summary>
        /// 水表编号(APP)
        /// </summary>
        public string metercode { get; set; }

        /// <summary>
        /// 用户编号(APP)
        /// </summary>
        public string usercode { get; set; }

        /// <summary>
        /// 拍照时间（APP）
        /// </summary>
        public DateTime phototime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime createtime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        public string createpeople { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime updatetime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新人
        /// </summary>
        public string updatepeople { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

    }
}
