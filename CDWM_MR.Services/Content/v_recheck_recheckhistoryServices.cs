using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_recheck_recheckhistoryServices:BaseServices<v_recheck_recheckhistory>, Iv_recheck_recheckhistoryServices
    {
        readonly Iv_recheck_recheckhistoryRepository dal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dal"></param>
        public v_recheck_recheckhistoryServices(Iv_recheck_recheckhistoryRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
