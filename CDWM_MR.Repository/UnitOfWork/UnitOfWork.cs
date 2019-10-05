using CDWM_MR.IRepository.UnitOfWork;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Repository.UnitOfWork
{
    /// <summary>
    /// 单元事务类
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient _sqlsugarclient;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="sqlSugarClient"></param>
        public UnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            _sqlsugarclient = sqlSugarClient;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            GetDbClient().Ado.BeginTran();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            GetDbClient().Ado.CommitTran();
        }

        /// <summary>
        /// 获取操作对象
        /// </summary>
        /// <returns></returns>
        public ISqlSugarClient GetDbClient()
        {
            return _sqlsugarclient;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTran()
        {
            GetDbClient().Ado.RollbackTran();
        }
    }
}
