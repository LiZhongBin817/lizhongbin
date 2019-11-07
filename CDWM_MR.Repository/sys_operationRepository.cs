using CDWM_MR.IRepository.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Repository.BASE;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Repository.Content
{
    public partial class sys_operationRepository : BaseRepository<sys_operation>, Isys_operationRepository
    {
        public async Task<PageModel<sys_operation>> Getoperationlist(Expression<Func<sys_operation, bool>> whereExpression, Expression<Func<sys_operation, object>> whereExpression1, int intPageIndex = 1, int intPageSize = 10)
        {
            int totalCount = 0;
            var list = await Task.Run(() => Db.Queryable<sys_operation>()
                .Where(whereExpression)
                .Mapper(t => t.menumodel, t => t.MenuID)
                .ToPageList(intPageIndex, intPageSize,ref totalCount));
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal())).ObjToInt();
            return new PageModel<sys_operation>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }
    }
}
