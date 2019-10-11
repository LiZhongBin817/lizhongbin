using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.IRepository.UnitOfWork
{
    /// <summary>
    /// 事务提交接口
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取sqlsugarclient
        /// </summary>
        /// <returns></returns>
        ISqlSugarClient GetDbClient();

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTran();

        /// <summary>
        /// 事务提交
        /// </summary>
        void CommitTran();

        /// <summary>
        /// 事务回滚
        /// </summary>
        void RollbackTran();
    }
}
