using CDWM_MR.IRepository;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Repository.Content
{
    public partial class mr_taskinfoRepository:BaseRepository<mr_taskinfo>, Imr_taskinfoRepository
    {

        /// <summary>
        /// 根据抄表员ID查询抄表册
        /// </summary>
        /// <param name="mrid"></param>
        /// <returns></returns>
        public async Task<List<mr_taskinfo>> QueryMRByid(int? mrid)
        {
            return await Task.Run(() => Db.Queryable<mr_taskinfo>()
            .Where(c => c.readerid == mrid)
            .Mapper(t => t.planinfo,t => t.planid)
            .Mapper(t => t.bookinfo,t => t.bookid)
            .ToList());
        }
    }
}
