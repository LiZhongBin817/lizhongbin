using Microsoft.Extensions.Caching.Memory;
using System;

namespace CDWM_MR.Common.MemoryCache
{
    /// <summary>
    /// 实例化缓存接口ICaching
    /// </summary>
    public class MemoryCaching : ICaching
    {
        //引用Microsoft.Extensions.Caching.Memory;这个和.net 还是不一样，没有了Httpruntime了
        private readonly IMemoryCache _cache;
        //还是通过构造函数的方法，获取
        public MemoryCaching(IMemoryCache cache)
        {
            _cache = cache;
        }

        public object Get(string cacheKey)
        {
            return _cache.Get(cacheKey);
        }

        public void Set(string cacheKey, object cacheValue)
        {
            _cache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(7200));
        }

        /// <summary>
        /// 是否存在此缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            object v = null;
            return _cache.TryGetValue<object>(key, out v);
        }

        /// <summary>
        /// 取得缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetCache<T>(string key) where T : class
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            T v = null;
            _cache.TryGetValue<T>(key, out v);

            return v;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCache(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));


            object v = null;
            if (_cache.TryGetValue(key, out v))
                _cache.Remove(key);
            _cache.Set<object>(key, value);
        }

        /// <summary>
        /// 设置缓存,绝对过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationMinute">间隔分钟</param>
        /// CommonManager.CacheObj.Save<RedisCacheHelper>("test", "RedisCache works!", 30);
        public void SetCache(string key, object value, double expirationMinute)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            object v = null;
            if (_cache.TryGetValue(key, out v))
                _cache.Remove(key);
            DateTime now = DateTime.Now;
            TimeSpan ts = now.AddMinutes(expirationMinute) - DateTime.Now;
            _cache.Set<object>(key, value, ts);
        }

        /// <summary>
        /// 设置缓存,绝对过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime">DateTimeOffset 结束时间</param>
        /// CommonManager.CacheObj.Save<RedisCacheHelper>("test", "RedisCache works!", DateTimeOffset.Now.AddSeconds(30));
        public void SetCache(string key, object value, DateTimeOffset expirationTime)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            object v = null;
            if (_cache.TryGetValue(key, out v))
                _cache.Remove(key);

            _cache.Set<object>(key, value, expirationTime);
        }

        /// <summary>
        /// 设置缓存,相对过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="t"></param>
        /// CommonManager.CacheObj.SaveSlidingCache<MemoryCacheHelper>("test", "MemoryCache works!",TimeSpan.FromSeconds(30));
        public void SetSlidingCache(string key, object value, TimeSpan t)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            object v = null;
            if (_cache.TryGetValue(key, out v))
                _cache.Remove(key);

            _cache.Set(key, value, new MemoryCacheEntryOptions()
            {
                SlidingExpiration = t
            });
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void RemoveCache(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            _cache.Remove(key);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_cache != null)
                _cache.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}
