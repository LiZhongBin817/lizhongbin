using CDWM_MR.IRepository.Base;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IRepository.Content
{
    public partial interface Imr_taskinfoRepository:IBaseRepository<mr_taskinfo>
    {
        /// <summary>
        /// 根据抄表员ID查询
        /// </summary>
        /// <param name="mrid"></param>
        /// <returns></returns>
        Task<List<mr_taskinfo>> QueryMRByid(int? mrid);
    }
}
