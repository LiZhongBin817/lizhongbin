using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_meterreading_recordServices:BaseServices<v_meterreading_record>, Iv_meterreading_recordServices
    {
        readonly Iv_meterreading_recordRepository dal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dal"></param>
        public v_meterreading_recordServices(Iv_meterreading_recordRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
