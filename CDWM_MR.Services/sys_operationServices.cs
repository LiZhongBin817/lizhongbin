using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Services.Content
{
    public partial class sys_operationServices:BaseServices<sys_operation>,Isys_operationServices
    {

        public async Task<PageModel<sys_operation>> Getoperationlist(Expression<Func<sys_operation, bool>> whereExpression, Expression<Func<sys_operation, object>> whereExpression1, int intPageIndex, int intPageSize)
        {
            return await this.dal.Getoperationlist(whereExpression, whereExpression1, intPageIndex, intPageSize);
        }
    }
}
