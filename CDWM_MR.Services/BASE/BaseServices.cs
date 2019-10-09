using CDWM_MR.IRepository.Base;
using CDWM_MR.IServices.BASE;
using CDWM_MR.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Services.BASE
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        //public IBaseRepository<TEntity> baseDal = new BaseRepository<TEntity>();
        public IBaseRepository<TEntity> BaseDal;//通过在子类的构造函数中注入，这里是基类，不用构造函数

        public async Task<TEntity> QueryById(object objId)
        {
            return await BaseDal.QueryById(objId);
        }
        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await BaseDal.QueryById(objId, blnUseCache);
        }

        /// <summary>
        /// 功能描述:根据ID查询数据
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await BaseDal.QueryByIDs(lstIds);
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            return await BaseDal.Add(entity);
        }

        /// <summary>
        /// 批量写入实体数据
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        public async Task<int> Add(List<TEntity> listEntity)
        {
            return await BaseDal.Add(listEntity);
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await BaseDal.Update(entity);
        }
        public async Task<bool> Updateable(List<TEntity> entity)
        {
            return await BaseDal.Updateable(entity);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="wherelambda"></param>
        /// <returns></returns>
        public async Task<bool> Update(Expression<Func<TEntity, TEntity>> expression, Expression<Func<TEntity, bool>> wherelambda)
        {
            return await BaseDal.Update(expression, wherelambda);
        }

        /// <summary>
        /// 按条件批量其他更新
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="wherelambda"></param>
        /// <returns></returns>
        public async Task<bool> OUpdate(Expression<Func<TEntity, TEntity>> expression, Expression<Func<TEntity, bool>> wherelambda)
        {
            return await BaseDal.OUpdate(expression, wherelambda);
        }
        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await BaseDal.Update(entity, strWhere);
        }

        public async Task<bool> Update(
         TEntity entity,
         List<string> lstColumns = null,
         List<string> lstIgnoreColumns = null,
         string strWhere = ""
            )
        {
            return await BaseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }


        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            return await BaseDal.Delete(entity);
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await BaseDal.DeleteById(id);
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await BaseDal.DeleteByIds(ids);
        }

        public async Task<bool> DeleteTable(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseDal.DeleteTable(whereExpression);
        }

        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await BaseDal.Query();
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await BaseDal.Query(strWhere);
        }

        public async Task<List<TEntity>> Queryfield(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> sellambda)
        {
            return await BaseDal.Queryfield(whereExpression,sellambda);
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseDal.Query(whereExpression);
        }
        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await BaseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await BaseDal.Query(whereExpression, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await BaseDal.Query(strWhere, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await BaseDal.Query(whereExpression, intTop, strOrderByFileds);
        }
        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            string strWhere,
            int intTop,
            string strOrderByFileds)
        {
            return await BaseDal.Query(strWhere, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFileds)
        {
            return await BaseDal.Query(
              whereExpression,
              intPageIndex,
              intPageSize,
              strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:AZLinli.CDWM_MR
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
          string strWhere,
          int intPageIndex,
          int intPageSize,
          string strOrderByFileds)
        {
            return await BaseDal.Query(
            strWhere,
            intPageIndex,
            intPageSize,
            strOrderByFileds);
        }

        // <summary>
        /// 分页查询不加Select方法连接其他数据库
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<PageModel<TEntity>> OQueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await BaseDal.OQueryPage(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }
        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression,int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await BaseDal.QueryPage(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }

        public async Task<PageModel<object>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> whereExpression1, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await BaseDal.QueryPage(whereExpression, whereExpression1, intPageIndex, intPageSize, strOrderByFileds);
        }
        public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        {
            return await BaseDal.QueryMuch(joinExpression, selectExpression, whereLambda);
        }
        public async Task<List<TEntity>> OQuery(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseDal.OQuery(whereExpression);
        }
        /// <summary>
        /// 更新其他数据库实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> OUpdate(TEntity entity)
        {
            return await BaseDal.OUpdate(entity);
        }
        /// <summary>
        /// 写入实体数据在其他数据库
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> OAdd(TEntity entity)
        {
            return await BaseDal.OAdd(entity);
        }
    }
}
