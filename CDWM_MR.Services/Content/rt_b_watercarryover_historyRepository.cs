using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class rt_b_watercarryover_historyRepository:BaseServices<rt_b_watercarryover_history>, Irt_b_watercarryover_historyServices
    {
        readonly Irt_b_watercarryover_historyRepository dal;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dal"></param>
        public rt_b_watercarryover_historyRepository(Irt_b_watercarryover_historyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
