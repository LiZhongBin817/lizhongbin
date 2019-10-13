using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Services.Content
{
    public partial class mr_taskinfoServices:BaseServices<mr_taskinfo>, Imr_taskinfoservices
    {
        /// <summary>
        /// 获取任务编号
        /// </summary>
        /// <param name="mrid"></param>
        /// <returns></returns>
        public async Task<object> Gettaskinfo(int? mrid)
        {
            var temp = await this.dal.QueryMRByid(mrid);
            var temp2 = temp.Select(c => new {
                TablesNumber = c.bookinfo.bookname,
                TablesName = c.bookinfo.bookno,
                HouseNumber = c.bookinfo,
                MeterReader = c.readerinfo.mrreadernumber,
                MeterReaderID = c.readerinfo.id,
                EndReadingTime = c.taskendtime,
                Period = c.planinfo.mplanyear + c.planinfo.mplanmonth,
            }).ToList();
            return new {
                data = temp
            };
        }
    }
}
