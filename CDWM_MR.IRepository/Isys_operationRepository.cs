using CDWM_MR.IRepository.Base;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IRepository.Content
{
    public partial interface Isys_operationRepository : IBaseRepository<sys_operation>
    {
        Task<PageModel<sys_operation>> Getoperationlist(Expression<Func<sys_operation, bool>> whereExpression, Expression<Func<sys_operation, object>> whereExpression1, int intPageIndex = 1, int intPageSize = 10);
    }
}
