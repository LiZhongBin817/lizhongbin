using CDWM_MR.IRepository.Content;
using CDWM_MR.IRepository.UnitOfWork;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Repository.Content
{
    public partial class sys_menuRepository:BaseRepository<sys_menu>,Isys_menuRepository
    {
        /// <summary>
        /// 获取单个菜单信息
        /// </summary>
        /// <returns></returns>
        public async Task<object> Getsinglemenuinfo(int sid)
        {
            return await Db.Queryable<sys_menu, sys_menu>((s, p) => new object[] {
               JoinType.Left,s.ParentID==p.id})
               .Where(s => s.id == sid)
               .Select((s, p) => new { id = s.id, order = s.MenuOrder, MenuUrl = s.MenuUrl, Remark = s.remark, MenuName = p.MenuName,ParentID=s.ParentID }).ToListAsync();
        }
    }
}
