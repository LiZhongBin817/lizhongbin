using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class mr_b_readerServices:BaseServices<mr_b_reader>,Imr_b_readerServices
    {
        readonly Imr_b_readerRepository dal;
        /// <summary>
        /// 构造函数
        /// </summary>
        public mr_b_readerServices(Imr_b_readerRepository dal)
        {
            this.dal= dal;
            this.BaseDal = dal;
        }
    }
}
