using CDWM_MR_Common.Redis.Init;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR_Common.Redis.Service
{
    public class RedisBase:IDisposable
    {
        #region 属性字段
        /// <summary>
        /// 网站Redis 链接字符串
        /// </summary>
        protected readonly ConnectionMultiplexer _conn;
        /// <summary>
        /// Redis操作对象
        /// </summary>
        protected readonly IDatabase redis = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化Redis操作方法基础类
        /// </summary>
        /// <param name="dbNum">操作的数据库索引0-64(需要在文件中配置)</param>
        protected RedisBase(int? dbNum = null)
        {
            _conn = RedisManager.Instance;
            if (_conn != null)
            {
                redis = _conn.GetDatabase(dbNum ?? RedisManager.RedisDataBaseIndex);
            }
            else
            {
                throw new ArgumentNullException("Redis连接初始化失败");
            }
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _conn.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion 构造函数

        #region 异步方法

        /// <summary>
        /// 组合缓存Key名称
        /// </summary>
        /// <param name="oldKey"></param>
        /// <returns></returns>
        public string AddSysCustomKey(string oldKey) => $"CDWM_MR_{oldKey}";

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">要删除的key</param>
        /// <returns>是否删除成功</returns>
        public async Task<bool> KeyDeleteAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await redis.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">要删除的key集合</param>
        /// <returns>成功删除的个数</returns>
        public async Task<long> KeyDeleteAsync(params string[] keys)
        {
            RedisKey[] newKeys = keys.Select(o => (RedisKey)AddSysCustomKey(o)).ToArray();
            return await redis.KeyDeleteAsync(newKeys);
        }

        /// <summary>
        /// 清空当前DataBase中所有Key
        /// </summary>
        public async Task KeyFulshAsync()
        {
            //直接执行清除命令
            await redis.ExecuteAsync("FLUSHDB");
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">要判断的key</param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await redis.KeyExistsAsync(key);
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">就的redis key</param>
        /// <param name="newKey">新的redis key</param>
        /// <returns></returns>
        public async Task<bool> KeyRenameAsync(string key, string newKey)
        {
            key = AddSysCustomKey(key);
            newKey = AddSysCustomKey(newKey);
            return await redis.KeyRenameAsync(key, newKey);
        }

        /// <summary>
        /// 设置Key的过期时间
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            return await redis.KeyExpireAsync(key, expiry);
        }
        #endregion
    }
}
