   //-------------sys_parameter开始------------------
using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class sys_parameter_settingServices:BaseServices<sys_parameter>,Isys_parameter_settingServices
    {
        private readonly Isys_parameter_settingRepository dal;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dal"></param>
        public sys_parameter_settingServices(Isys_parameter_settingRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
   //----------------sys_parameter结束-------------------