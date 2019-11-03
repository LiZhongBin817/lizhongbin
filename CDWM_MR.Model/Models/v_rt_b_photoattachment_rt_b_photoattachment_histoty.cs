using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
     public class v_rt_b_photoattachment_rt_b_photoattachment_histoty
    {
        /// <summary>
        /// 图片表(图片历史表)
        /// </summary>
        public v_rt_b_photoattachment_rt_b_photoattachment_histoty()
        {
        }

        private System.Int32 _id;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get { return this._id; } set { this._id = value; } }

        private System.String _usercode;
        /// <summary>
        /// 用户编号
        /// </summary>
        public System.String usercode { get { return this._usercode; } set { this._usercode = value; } }

        private System.String _taskperiodname;
        /// <summary>
        /// 任务账期(201909)
        /// </summary>
        public System.String taskperiodname { get { return this._taskperiodname; } set { this._taskperiodname = value; } }

        private System.String _photourl;
        /// <summary>
        /// 服务器存储路径
        /// </summary>
        public System.String photourl { get { return this._photourl; } set { this._photourl = value; } }
    }
}

