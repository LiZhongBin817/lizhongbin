
using CDWM_MR.Common.Helper;
using CDWM_MR_Common.Redis.Service;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR_Common.Redis
{
    /// <summary>
    /// Redis操作类
    /// </summary>
    public class RedisHelper:RedisBase,IRedisHelper
    {
        #region 构造函数

        /// <summary>
        /// 初始化Redis的List数据结构操作
        /// </summary>
        /// <param name="dbNum">操作的数据库索引0-64(需要在conf文件中配置)</param>
        public RedisHelper(int? dbNum = null) :
            base(dbNum)
        { }
        #endregion

        #region List集合操作异步方法
        /// <summary>
        /// 从左侧向list中添加一个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = JsonHelper.GetJson(value);
            return await base.redis.ListLeftPushAsync(key, jValue);
        }

        /// <summary>
        /// 从左侧向list中添加多个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync<T>(string key, List<T> value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = ConvertRedisValue(value.ToArray());
            return await base.redis.ListLeftPushAsync(key, valueList);
        }

        /// <summary>
        /// 从右侧向list中添加一个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = JsonHelper.GetJson(value);
            return await base.redis.ListRightPushAsync(key, jValue);
        }

        /// <summary>
        /// 从右侧向list中添加多个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string key, List<T> value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = ConvertRedisValue(value.ToArray());
            return await base.redis.ListRightPushAsync(key, valueList);
        }

        /// <summary>
        /// 从左侧向list中取出一个值并从list中删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = await base.redis.ListLeftPopAsync(key);
            return JsonHelper.GetObject<T>(rValue);
        }

        /// <summary>
        /// 从右侧向list中取出一个值并从list中删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = await base.redis.ListRightPopAsync(key);
            return JsonHelper.GetObject<T>(rValue);
        }

        /// <summary>
        /// 从key的List中右侧取出一个值，并从左侧添加到destination集合中，且返回该数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">要取出数据的List名称</param>
        /// <param name="destination">要添加到的List名称</param>
        /// <returns></returns>
        public async Task<T> ListRightPopLeftPushAsync<T>(string key, string destination)
        {
            key = AddSysCustomKey(key);
            destination = AddSysCustomKey(destination);
            var rValue = await base.redis.ListRightPopLeftPushAsync(key, destination);
            return JsonHelper.GetObject<T>(rValue);
        }

        /// <summary>
        /// 在key的List指定值pivot之后插入value，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pivot">索引值</param>
        /// <param name="value">要插入的值</param>
        /// <returns></returns>
        public async Task<long> ListInsertAfterAsync<T>(string key, T pivot, T value)
        {
            key = AddSysCustomKey(key);
            string pValue = JsonHelper.GetJson(pivot);
            string jValue = JsonHelper.GetJson(value);
            return await base.redis.ListInsertAfterAsync(key, pValue, jValue);
        }

        /// <summary>
        /// 在key的List指定值pivot之前插入value，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pivot">索引值</param>
        /// <param name="value">要插入的值</param>
        /// <returns></returns>
        public async Task<long> ListInsertBeforeAsync<T>(string key, T pivot, T value)
        {
            key = AddSysCustomKey(key);
            string pValue = JsonHelper.GetJson(pivot);
            string jValue = JsonHelper.GetJson(value);
            return await base.redis.ListInsertBeforeAsync(key, pValue, jValue);
        }

        /// <summary>
        /// 从key的list中取出所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> ListRangeAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = await base.redis.ListRangeAsync(key);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 从key的List获取指定索引的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<T> ListGetByIndexAsync<T>(string key, long index)
        {
            key = AddSysCustomKey(key);
            var rValue = await base.redis.ListGetByIndexAsync(key, index);
            return JsonHelper.GetObject<T>(rValue);
        }

        /// <summary>
        /// 获取key的list中数据个数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await base.redis.ListLengthAsync(key);
        }

        /// <summary>
        /// 从key的List中移除指定的值，返回删除个数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListRemoveAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = JsonHelper.GetJson(value);
            return await base.redis.ListRemoveAsync(key, jValue);
        }
        #endregion

        #region List集合操作同步方法
        /// <summary>
        /// 从左侧向list中添加一个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = JsonHelper.GetJson(value);
            return base.redis.ListLeftPush(key, jValue);
        }

        /// <summary>
        /// 从左侧向list中添加多个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string key, List<T> value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = ConvertRedisValue(value.ToArray());
            return base.redis.ListLeftPush(key, valueList);
        }

        /// <summary>
        /// 从右侧向list中添加一个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = JsonHelper.GetJson(value);
            return base.redis.ListRightPush(key, jValue);
        }

        /// <summary>
        /// 从右侧向list中添加多个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string key, List<T> value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = ConvertRedisValue(value.ToArray());
            return base.redis.ListRightPush(key, valueList);
        }

        /// <summary>
        /// 从左侧向list中取出一个值并从list中删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = base.redis.ListLeftPop(key);
            return JsonHelper.GetObject<T>(rValue);
        }

        /// <summary>
        /// 从右侧向list中取出一个值并从list中删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = base.redis.ListRightPop(key);
            return JsonHelper.GetObject<T>(rValue);
        }

        /// <summary>
        /// 从key的List中右侧取出一个值，并从左侧添加到destination集合中，且返回该数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">要取出数据的List名称</param>
        /// <param name="destination">要添加到的List名称</param>
        /// <returns></returns>
        public T ListRightPopLeftPush<T>(string key, string destination)
        {
            key = AddSysCustomKey(key);
            destination = AddSysCustomKey(destination);
            var rValue = base.redis.ListRightPopLeftPush(key, destination);
            return JsonHelper.GetObject<T>(rValue);
        }

        /// <summary>
        /// 在key的List指定值pivot之后插入value，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pivot">索引值</param>
        /// <param name="value">要插入的值</param>
        /// <returns></returns>
        public long ListInsertAfter<T>(string key, T pivot, T value)
        {
            key = AddSysCustomKey(key);
            string pValue = JsonHelper.GetJson(pivot);
            string jValue = JsonHelper.GetJson(value);
            return base.redis.ListInsertAfter(key, pValue, jValue);
        }

        /// <summary>
        /// 在key的List指定值pivot之前插入value，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pivot">索引值</param>
        /// <param name="value">要插入的值</param>
        /// <returns></returns>
        public long ListInsertBefore<T>(string key, T pivot, T value)
        {
            key = AddSysCustomKey(key);
            string pValue = JsonHelper.GetJson(pivot);
            string jValue = JsonHelper.GetJson(value);
            return base.redis.ListInsertBefore(key, pValue, jValue);
        }

        /// <summary>
        /// 从key的list中取出所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> ListRange<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = base.redis.ListRange(key);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 从key的List获取指定索引的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public T ListGetByIndex<T>(string key, long index)
        {
            key = AddSysCustomKey(key);
            var rValue = base.redis.ListGetByIndex(key, index);
            return JsonHelper.GetObject<T>(rValue);
        }

        /// <summary>
        /// 获取key的list中数据个数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            key = AddSysCustomKey(key);
            return base.redis.ListLength(key);
        }

        /// <summary>
        /// 从key的List中移除指定的值，返回删除个数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRemove<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = JsonHelper.GetJson(value);
            return base.redis.ListRemove(key, jValue);
        }
        #endregion

        #region Hash异步方法

        /// <summary>
        /// 异步方法 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            return await base.redis.HashExistsAsync(key, dataKey);
        }

        /// <summary>
        /// 异步方法 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string key, string dataKey, T t)
        {
            key = AddSysCustomKey(key);
            string json = JsonHelper.GetJson(t);
            return await base.redis.HashSetAsync(key, dataKey, json);
        }

        /// <summary>
        /// 异步方法 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            return await base.redis.HashDeleteAsync(key, dataKey);
        }

        /// <summary>
        /// 异步方法 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string key, params string[] dataKeys)
        {
            key = AddSysCustomKey(key);
            var newValues = dataKeys.Select(o => (RedisValue)o).ToArray();
            return await base.redis.HashDeleteAsync(key, newValues);
        }

        /// <summary>
        /// 异步方法 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            string value = await base.redis.HashGetAsync(key, dataKey);
            return JsonHelper.GetObject<T>(value);
        }

        /// <summary>
        /// 异步方法 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<double> HashIncrementAsync(string key, string dataKey, double val = 1)
        {
            key = AddSysCustomKey(key);
            return await base.redis.HashIncrementAsync(key, dataKey, val);
        }

        /// <summary>
        /// 异步方法 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<double> HashDecrementAsync(string key, string dataKey, double val = 1)
        {
            key = AddSysCustomKey(key);
            return await base.redis.HashDecrementAsync(key, dataKey, val);
        }

        /// <summary>
        /// 异步方法 获取hashkey所有key名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string[]> HashKeysAsync(string key)
        {
            key = AddSysCustomKey(key);
            RedisValue[] values = await base.redis.HashKeysAsync(key);
            return values.Select(o => o.ToString()).ToArray();
        }

        /// <summary>
        /// 获取hashkey所有key与值，必须保证Key内的所有数据类型一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, T>> HashGetAllAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var query = await base.redis.HashGetAllAsync(key);
            Dictionary<string, T> dic = new Dictionary<string, T>();
            foreach (var item in query)
            {
                dic.Add(item.Name, JsonHelper.GetObject<T>(item.Value));
            }
            return dic;
        }

        #endregion 异步方法

        #region Hash同步方法

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashExists(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            return base.redis.HashExists(key, dataKey);
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string dataKey, T t)
        {
            key = AddSysCustomKey(key);
            string json = JsonHelper.GetJson(t);
            return base.redis.HashSet(key, dataKey, json);
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            return base.redis.HashDelete(key, dataKey);
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public long HashDelete(string key, params string[] dataKeys)
        {
            key = AddSysCustomKey(key);
            var newValues = dataKeys.Select(o => (RedisValue)o).ToArray();
            return base.redis.HashDelete(key, newValues);
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string dataKey)
        {
            key = AddSysCustomKey(key);
            string value = base.redis.HashGet(key, dataKey);
            return JsonHelper.GetObject<T>(value);
        }

        /// <summary>
        /// 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public double HashIncrement(string key, string dataKey, double val = 1)
        {
            key = AddSysCustomKey(key);
            return base.redis.HashIncrement(key, dataKey, val);
        }

        /// <summary>
        /// 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public double HashDecrement(string key, string dataKey, double val = 1)
        {
            key = AddSysCustomKey(key);
            return base.redis.HashDecrement(key, dataKey, val);
        }

        /// <summary>
        /// 获取hashkey所有key名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] HashKeys(string key)
        {
            key = AddSysCustomKey(key);
            RedisValue[] values = base.redis.HashKeys(key);
            return values.Select(o => o.ToString()).ToArray();
        }

        /// <summary>
        /// 获取hashkey所有key与值，必须保证Key内的所有数据类型一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, T> HashGetAll<T>(string key)
        {
            key = AddSysCustomKey(key);
            var query = base.redis.HashGetAll(key);
            Dictionary<string, T> dic = new Dictionary<string, T>();
            foreach (var item in query)
            {
                dic.Add(item.Name, JsonHelper.GetObject<T>(item.Value));
            }
            return dic;
        }

        #endregion 同步方法

        #region 有序集合操作

        #region 异步方法
        /// <summary>
        /// 在Key集合中添加一个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> SetAddAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = JsonHelper.GetJson(value);
            return await base.redis.SetAddAsync(key, jValue);
        }
        /// <summary>
        /// 在Key集合中添加多个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值列表</param>
        /// <returns></returns>
        public async Task<long> SetAddAsync<T>(string key, List<T> value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = ConvertRedisValue(value.ToArray());
            return await base.redis.SetAddAsync(key, valueList);
        }

        /// <summary>
        /// 获取key集合值的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> SetLengthAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await base.redis.SetLengthAsync(key);
        }

        /// <summary>
        /// 判断Key集合中是否包含指定的值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value">要判断是值</param>
        /// <returns></returns>
        public async Task<bool> SetContainsAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = JsonHelper.GetJson(value);
            return await base.redis.SetContainsAsync(key, jValue);
        }

        /// <summary>
        /// 随机获取key集合中的一个值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> SetRandomMemberAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = await base.redis.SetRandomMemberAsync(key);
            return JsonHelper.GetObject<T>(rValue);
        }

        /// <summary>
        /// 获取key所有值的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> SetMembersAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = await base.redis.SetMembersAsync(key);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 删除key集合中指定的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> SetRemoveAsync<T>(string key, params T[] value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = ConvertRedisValue(value);
            return await base.redis.SetRemoveAsync(key, valueList);
        }

        /// <summary>
        /// 随机删除key集合中的一个值，并返回该值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> SetPopAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = await base.redis.SetPopAsync(key);
            return JsonHelper.GetObject<T>(rValue);
        }


        /// <summary>
        /// 获取几个集合的并集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<List<T>> SetCombineUnionAsync<T>(params string[] keys)
        {
            return await _SetCombineAsync<T>(SetOperation.Union, keys);
        }
        /// <summary>
        /// 获取几个集合的交集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<List<T>> SetCombineIntersectAsync<T>(params string[] keys)
        {
            return await _SetCombineAsync<T>(SetOperation.Intersect, keys);
        }
        /// <summary>
        /// 获取几个集合的差集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<List<T>> SetCombineDifferenceAsync<T>(params string[] keys)
        {
            return await _SetCombineAsync<T>(SetOperation.Difference, keys);
        }



        /// <summary>
        /// 获取几个集合的并集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<long> SetCombineUnionAndStoreAsync(string destination, params string[] keys)
        {
            return await _SetCombineAndStoreAsync(SetOperation.Union, destination, keys);
        }
        /// <summary>
        /// 获取几个集合的交集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<long> SetCombineIntersectAndStoreAsync(string destination, params string[] keys)
        {
            return await _SetCombineAndStoreAsync(SetOperation.Intersect, destination, keys);
        }
        /// <summary>
        /// 获取几个集合的差集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<long> SetCombineDifferenceAndStoreAsync(string destination, params string[] keys)
        {
            return await _SetCombineAndStoreAsync(SetOperation.Difference, destination, keys);
        }

        #endregion

        #region 同步方法
        /// <summary>
        /// 在Key集合中添加一个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SetAdd<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = JsonHelper.GetJson(value);
            return base.redis.SetAdd(key, jValue);
        }
        /// <summary>
        /// 在Key集合中添加多个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值列表</param>
        /// <returns></returns>
        public long SetAdd<T>(string key, List<T> value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = ConvertRedisValue(value.ToArray());
            return base.redis.SetAdd(key, valueList);
        }

        /// <summary>
        /// 获取key集合值的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SetLength(string key)
        {
            key = AddSysCustomKey(key);
            return base.redis.SetLength(key);
        }

        /// <summary>
        /// 判断Key集合中是否包含指定的值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value">要判断是值</param>
        /// <returns></returns>
        public bool SetContains<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jValue = JsonHelper.GetJson(value);
            return base.redis.SetContains(key, jValue);
        }

        /// <summary>
        /// 随机获取key集合中的一个值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T SetRandomMember<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = base.redis.SetRandomMember(key);
            return JsonHelper.GetObject<T>(rValue);
        }

        /// <summary>
        /// 获取key所有值的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> SetMembers<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = base.redis.SetMembers(key);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 删除key集合中指定的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long SetRemove<T>(string key, params T[] value)
        {
            key = AddSysCustomKey(key);
            RedisValue[] valueList = ConvertRedisValue(value);
            return base.redis.SetRemove(key, valueList);
        }

        /// <summary>
        /// 随机删除key集合中的一个值，并返回该值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T SetPop<T>(string key)
        {
            key = AddSysCustomKey(key);
            var rValue = base.redis.SetPop(key);
            return JsonHelper.GetObject<T>(rValue);
        }


        /// <summary>
        /// 获取几个集合的并集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public List<T> SetCombineUnion<T>(params string[] keys)
        {
            return _SetCombine<T>(SetOperation.Union, keys);
        }
        /// <summary>
        /// 获取几个集合的交集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public List<T> SetCombineIntersect<T>(params string[] keys)
        {
            return _SetCombine<T>(SetOperation.Intersect, keys);
        }
        /// <summary>
        /// 获取几个集合的差集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public List<T> SetCombineDifference<T>(params string[] keys)
        {
            return _SetCombine<T>(SetOperation.Difference, keys);
        }

        /// <summary>
        /// 获取几个集合的并集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public long SetCombineUnionAndStore(string destination, params string[] keys)
        {
            return _SetCombineAndStore(SetOperation.Union, destination, keys);
        }
        /// <summary>
        /// 获取几个集合的交集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public long SetCombineIntersectAndStore(string destination, params string[] keys)
        {
            return _SetCombineAndStore(SetOperation.Intersect, destination, keys);
        }
        /// <summary>
        /// 获取几个集合的差集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public long SetCombineDifferenceAndStore(string destination, params string[] keys)
        {
            return _SetCombineAndStore(SetOperation.Difference, destination, keys);
        }
        #endregion

        #region 内部辅助方法
        /// <summary>
        /// 获取几个集合的交叉并集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation">Union：并集  Intersect：交集  Difference：差集  详见 <see cref="SetOperation"/></param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        private List<T> _SetCombine<T>(SetOperation operation, params string[] keys)
        {
            RedisKey[] keyList = ConvertRedisKeysAddSysCustomKey(keys);
            var rValue = base.redis.SetCombine(operation, keyList);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 获取几个集合的交叉并集合,并保存到一个新Key中
        /// </summary>
        /// <param name="operation">Union：并集  Intersect：交集  Difference：差集  详见 <see cref="SetOperation"/></param>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        private long _SetCombineAndStore(SetOperation operation, string destination, params string[] keys)
        {
            destination = AddSysCustomKey(destination);
            RedisKey[] keyList = ConvertRedisKeysAddSysCustomKey(keys);
            return base.redis.SetCombineAndStore(operation, destination, keyList);
        }
        /// <summary>
        /// 获取几个集合的交叉并集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation">Union：并集  Intersect：交集  Difference：差集  详见 <see cref="SetOperation"/></param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        private async Task<List<T>> _SetCombineAsync<T>(SetOperation operation, params string[] keys)
        {
            RedisKey[] keyList = ConvertRedisKeysAddSysCustomKey(keys);
            var rValue = await base.redis.SetCombineAsync(operation, keyList);
            return ConvetList<T>(rValue);
        }
        /// <summary>
        /// 获取几个集合的交叉并集合,并保存到一个新Key中
        /// </summary>
        /// <param name="operation">Union：并集  Intersect：交集  Difference：差集  详见 <see cref="SetOperation"/></param>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        private async Task<long> _SetCombineAndStoreAsync(SetOperation operation, string destination, params string[] keys)
        {
            destination = AddSysCustomKey(destination);
            RedisKey[] keyList = ConvertRedisKeysAddSysCustomKey(keys);
            return await base.redis.SetCombineAndStoreAsync(operation, destination, keyList);
        }

        #endregion
        #endregion

        #region 无序集合操作
        #region 异步方法

        /// <summary>
        /// 添加一个值到Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score">排序分数，为空将获取集合中最大score加1</param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string key, T value, double? score = null)
        {
            key = AddSysCustomKey(key);
            double scoreNum = score ?? _GetScore(key);
            return await base.redis.SortedSetAddAsync(key, JsonHelper.GetJson<T>(value), scoreNum);
        }

        /// <summary>
        /// 添加一个集合到Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score">排序分数，为空将获取集合中最大score加1</param>
        /// <returns></returns>
        public async Task<long> SortedSetAddAsync<T>(string key, List<T> value, double? score = null)
        {
            key = AddSysCustomKey(key);
            double scoreNum = score ?? _GetScore(key);
            SortedSetEntry[] rValue = value.Select(o => new SortedSetEntry(JsonHelper.GetJson<T>(o), scoreNum++)).ToArray();
            return await base.redis.SortedSetAddAsync(key, rValue);
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await redis.SortedSetLengthAsync(key);
        }

        /// <summary>
        /// 获取指定起始值到结束值的集合数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthByValueAsync<T>(string key, T startValue, T endValue)
        {
            key = AddSysCustomKey(key);
            var sValue = JsonHelper.GetJson<T>(startValue);
            var eValue = JsonHelper.GetJson<T>(endValue);
            return await redis.SortedSetLengthByValueAsync(key, sValue, eValue);
        }

        /// <summary>
        /// 获取指定Key的排序Score值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<double?> SortedSetScoreAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            var rValue = JsonHelper.GetJson<T>(value);
            return await redis.SortedSetScoreAsync(key, rValue);
        }

        /// <summary>
        /// 获取指定Key中最小Score值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<double> SortedSetMinScoreAsync(string key)
        {
            key = AddSysCustomKey(key);
            double dValue = 0;
            var rValue = (await base.redis.SortedSetRangeByRankWithScoresAsync(key, 0, 0, Order.Ascending)).FirstOrDefault();
            dValue = rValue != null ? rValue.Score : 0;
            return dValue;
        }

        /// <summary>
        /// 获取指定Key中最大Score值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<double> SortedSetMaxScoreAsync(string key)
        {
            key = AddSysCustomKey(key);
            double dValue = 0;
            var rValue = (await base.redis.SortedSetRangeByRankWithScoresAsync(key, 0, 0, Order.Descending)).FirstOrDefault();
            dValue = rValue != null ? rValue.Score : 0;
            return dValue;
        }

        /// <summary>
        /// 删除Key中指定的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<long> SortedSetRemoveAsync<T>(string key, params T[] value)
        {
            key = AddSysCustomKey(key);
            var rValue = ConvertRedisValue<T>(value);
            return await base.redis.SortedSetRemoveAsync(key, rValue);
        }

        /// <summary>
        /// 删除指定起始值到结束值的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        public async Task<long> SortedSetRemoveRangeByValueAsync<T>(string key, T startValue, T endValue)
        {
            key = AddSysCustomKey(key);
            var sValue = JsonHelper.GetJson<T>(startValue);
            var eValue = JsonHelper.GetJson<T>(endValue);
            return await base.redis.SortedSetRemoveRangeByValueAsync(key, sValue, eValue);
        }

        /// <summary>
        /// 删除 从 start 开始的 stop 条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<long> SortedSetRemoveRangeByRankAsync(string key, long start, long stop)
        {
            key = AddSysCustomKey(key);
            return await base.redis.SortedSetRemoveRangeByRankAsync(key, start, stop);
        }

        /// <summary>
        /// 根据排序分数Score，删除从 start 开始的 stop 条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<long> SortedSetRemoveRangeByScoreAsync(string key, double start, double stop)
        {
            key = AddSysCustomKey(key);
            return await base.redis.SortedSetRemoveRangeByScoreAsync(key, start, stop);
        }

        /// <summary>
        /// 获取从 start 开始的 stop 条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        public async Task<List<T>> SortedSetRangeByRankAsync<T>(string key, long start = 0, long stop = -1, bool desc = false)
        {
            key = AddSysCustomKey(key);
            Order orderBy = desc ? Order.Descending : Order.Ascending;
            var rValue = await base.redis.SortedSetRangeByRankAsync(key, start, stop, orderBy);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 获取从 start 开始的 stop 条数据包含Score，返回数据格式：Key=值，Value = Score
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        public async Task<Dictionary<T, double>> SortedSetRangeByRankWithScoresAsync<T>(string key, long start = 0, long stop = -1, bool desc = false)
        {
            key = AddSysCustomKey(key);
            Order orderBy = desc ? Order.Descending : Order.Ascending;
            var rValue = await base.redis.SortedSetRangeByRankWithScoresAsync(key, start, stop, orderBy);
            Dictionary<T, double> dicList = new Dictionary<T, double>();
            foreach (var item in rValue)
            {
                dicList.Add(JsonHelper.GetObject<T>(item.Element), item.Score);
            }
            return dicList;
        }

        /// <summary>
        ///  根据Score排序 获取从 start 开始的 stop 条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        public async Task<List<T>> SortedSetRangeByScoreAsync<T>(string key, double start = 0, double stop = -1, bool desc = false)
        {
            key = AddSysCustomKey(key);
            Order orderBy = desc ? Order.Descending : Order.Ascending;
            var rValue = await base.redis.SortedSetRangeByScoreAsync(key, start, stop, Exclude.None, orderBy);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 根据Score排序  获取从 start 开始的 stop 条数据包含Score，返回数据格式：Key=值，Value = Score
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        public async Task<Dictionary<T, double>> SortedSetRangeByScoreWithScoresAsync<T>(string key, double start = 0, double stop = -1, bool desc = false)
        {
            key = AddSysCustomKey(key);
            Order orderBy = desc ? Order.Descending : Order.Ascending;
            var rValue = await base.redis.SortedSetRangeByScoreWithScoresAsync(key, start, stop, Exclude.None, orderBy);
            Dictionary<T, double> dicList = new Dictionary<T, double>();
            foreach (var item in rValue)
            {
                dicList.Add(JsonHelper.GetObject<T>(item.Element), item.Score);
            }
            return dicList;
        }

        /// <summary>
        /// 获取指定起始值到结束值的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        public async Task<List<T>> SortedSetRangeByValueAsync<T>(string key, T startValue, T endValue)
        {
            key = AddSysCustomKey(key);
            var sValue = JsonHelper.GetJson<T>(startValue);
            var eValue = JsonHelper.GetJson<T>(endValue);
            var rValue = await base.redis.SortedSetRangeByValueAsync(key, sValue, eValue);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 获取几个集合的并集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<long> SortedSetCombineUnionAndStoreAsync(string destination, params string[] keys)
        {
            return await _SortedSetCombineAndStoreAsync(SetOperation.Union, destination, keys);
        }

        /// <summary>
        /// 获取几个集合的交集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public async Task<long> SortedSetCombineIntersectAndStoreAsync(string destination, params string[] keys)
        {
            return await _SortedSetCombineAndStoreAsync(SetOperation.Intersect, destination, keys);
        }

        /// <summary>
        /// 修改指定Key和值的Scores在原值上减去scores，并返回最终Scores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        public async Task<double> SortedSetDecrementAsync<T>(string key, T value, double scores)
        {
            key = AddSysCustomKey(key);
            var rValue = JsonHelper.GetJson<T>(value);
            return await base.redis.SortedSetDecrementAsync(key, rValue, scores);
        }

        /// <summary>
        /// 修改指定Key和值的Scores在原值上增加scores，并返回最终Scores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        public async Task<double> SortedSetIncrementAsync<T>(string key, T value, double scores)
        {
            key = AddSysCustomKey(key);
            var rValue = JsonHelper.GetJson<T>(value);
            return await base.redis.SortedSetIncrementAsync(key, rValue, scores);
        }



        #endregion

        #region 同步方法

        /// <summary>
        /// 添加一个值到Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score">排序分数，为空将获取集合中最大score加1</param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string key, T value, double? score = null)
        {
            key = AddSysCustomKey(key);
            double scoreNum = score ?? _GetScore(key);
            return base.redis.SortedSetAdd(key, JsonHelper.GetJson<T>(value), scoreNum);
        }

        /// <summary>
        /// 添加一个集合到Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score">排序分数，为空将获取集合中最大score加1</param>
        /// <returns></returns>
        public long SortedSetAdd<T>(string key, List<T> value, double? score = null)
        {
            key = AddSysCustomKey(key);
            double scoreNum = score ?? _GetScore(key);
            SortedSetEntry[] rValue = value.Select(o => new SortedSetEntry(JsonHelper.GetJson<T>(o), scoreNum++)).ToArray();
            return base.redis.SortedSetAdd(key, rValue);
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SortedSetLength(string key)
        {
            key = AddSysCustomKey(key);
            return redis.SortedSetLength(key);
        }

        /// <summary>
        /// 获取指定起始值到结束值的集合数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        public long SortedSetLengthByValue<T>(string key, T startValue, T endValue)
        {
            key = AddSysCustomKey(key);
            var sValue = JsonHelper.GetJson<T>(startValue);
            var eValue = JsonHelper.GetJson<T>(endValue);
            return redis.SortedSetLengthByValue(key, sValue, eValue);
        }

        /// <summary>
        /// 获取指定Key的排序Score值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double? SortedSetScore<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            var rValue = JsonHelper.GetJson<T>(value);
            return redis.SortedSetScore(key, rValue);
        }

        /// <summary>
        /// 获取指定Key中最小Score值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public double SortedSetMinScore(string key)
        {
            key = AddSysCustomKey(key);
            double dValue = 0;
            var rValue = base.redis.SortedSetRangeByRankWithScores(key, 0, 0, Order.Ascending).FirstOrDefault();
            dValue = rValue != null ? rValue.Score : 0;
            return dValue;
        }

        /// <summary>
        /// 获取指定Key中最大Score值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public double SortedSetMaxScore(string key)
        {
            key = AddSysCustomKey(key);
            double dValue = 0;
            var rValue = base.redis.SortedSetRangeByRankWithScores(key, 0, 0, Order.Descending).FirstOrDefault();
            dValue = rValue != null ? rValue.Score : 0;
            return dValue;
        }

        /// <summary>
        /// 删除Key中指定的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public long SortedSetRemove<T>(string key, params T[] value)
        {
            key = AddSysCustomKey(key);
            var rValue = ConvertRedisValue<T>(value);
            return base.redis.SortedSetRemove(key, rValue);
        }

        /// <summary>
        /// 删除指定起始值到结束值的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        public long SortedSetRemoveRangeByValue<T>(string key, T startValue, T endValue)
        {
            key = AddSysCustomKey(key);
            var sValue = JsonHelper.GetJson<T>(startValue);
            var eValue = JsonHelper.GetJson<T>(endValue);
            return base.redis.SortedSetRemoveRangeByValue(key, sValue, eValue);
        }

        /// <summary>
        /// 删除 从 start 开始的 stop 条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public long SortedSetRemoveRangeByRank(string key, long start, long stop)
        {
            key = AddSysCustomKey(key);
            return base.redis.SortedSetRemoveRangeByRank(key, start, stop);
        }

        /// <summary>
        /// 根据排序分数Score，删除从 start 开始的 stop 条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public long SortedSetRemoveRangeByScore(string key, double start, double stop)
        {
            key = AddSysCustomKey(key);
            return base.redis.SortedSetRemoveRangeByScore(key, start, stop);
        }

        /// <summary>
        /// 获取从 start 开始的 stop 条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        public List<T> SortedSetRangeByRank<T>(string key, long start = 0, long stop = -1, bool desc = false)
        {
            key = AddSysCustomKey(key);
            Order orderBy = desc ? Order.Descending : Order.Ascending;
            var rValue = base.redis.SortedSetRangeByRank(key, start, stop, orderBy);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 获取从 start 开始的 stop 条数据包含Score，返回数据格式：Key=值，Value = Score
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        public Dictionary<T, double> SortedSetRangeByRankWithScores<T>(string key, long start = 0, long stop = -1, bool desc = false)
        {
            key = AddSysCustomKey(key);
            Order orderBy = desc ? Order.Descending : Order.Ascending;
            var rValue = base.redis.SortedSetRangeByRankWithScores(key, start, stop, orderBy);
            Dictionary<T, double> dicList = new Dictionary<T, double>();
            foreach (var item in rValue)
            {
                dicList.Add(JsonHelper.GetObject<T>(item.Element), item.Score);
            }
            return dicList;
        }

        /// <summary>
        ///  根据Score排序 获取从 start 开始的 stop 条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        public List<T> SortedSetRangeByScore<T>(string key, double start = 0, double stop = -1, bool desc = false)
        {
            key = AddSysCustomKey(key);
            Order orderBy = desc ? Order.Descending : Order.Ascending;
            var rValue = base.redis.SortedSetRangeByScore(key, start, stop, Exclude.None, orderBy);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 根据Score排序  获取从 start 开始的 stop 条数据包含Score，返回数据格式：Key=值，Value = Score
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        public Dictionary<T, double> SortedSetRangeByScoreWithScores<T>(string key, double start = 0, double stop = -1, bool desc = false)
        {
            key = AddSysCustomKey(key);
            Order orderBy = desc ? Order.Descending : Order.Ascending;
            var rValue = base.redis.SortedSetRangeByScoreWithScores(key, start, stop, Exclude.None, orderBy);
            Dictionary<T, double> dicList = new Dictionary<T, double>();
            foreach (var item in rValue)
            {
                dicList.Add(JsonHelper.GetObject<T>(item.Element), item.Score);
            }
            return dicList;
        }

        /// <summary>
        /// 获取指定起始值到结束值的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        public List<T> SortedSetRangeByValue<T>(string key, T startValue, T endValue)
        {
            key = AddSysCustomKey(key);
            var sValue = JsonHelper.GetJson<T>(startValue);
            var eValue = JsonHelper.GetJson<T>(endValue);
            var rValue = base.redis.SortedSetRangeByValue(key, sValue, eValue);
            return ConvetList<T>(rValue);
        }

        /// <summary>
        /// 获取几个集合的并集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public long SortedSetCombineUnionAndStore(string destination, params string[] keys)
        {
            return _SortedSetCombineAndStore(SetOperation.Union, destination, keys);
        }

        /// <summary>
        /// 获取几个集合的交集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        public long SortedSetCombineIntersectAndStore(string destination, params string[] keys)
        {
            return _SortedSetCombineAndStore(SetOperation.Intersect, destination, keys);
        }

        /// <summary>
        /// 修改指定Key和值的Scores在原值上减去scores，并返回最终Scores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        public double SortedSetDecrement<T>(string key, T value, double scores)
        {
            key = AddSysCustomKey(key);
            var rValue = JsonHelper.GetJson<T>(value);
            return redis.SortedSetDecrement(key, rValue, scores);
        }

        /// <summary>
        /// 修改指定Key和值的Scores在原值上增加scores，并返回最终Scores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        public double SortedSetIncrement<T>(string key, T value, double scores)
        {
            key = AddSysCustomKey(key);
            var rValue = JsonHelper.GetJson<T>(value);
            return redis.SortedSetIncrement(key, rValue, scores);
        }
        #endregion

        #region 内部辅助方法
        /// <summary>
        /// 获取指定Key中最大Score值,
        /// </summary>
        /// <param name="key">key名称，注意要先添加上Key前缀</param>
        /// <returns></returns>
        private double _GetScore(string key)
        {
            double dValue = 0;
            var rValue = base.redis.SortedSetRangeByRankWithScores(key, 0, 0, Order.Descending).FirstOrDefault();
            dValue = rValue != null ? rValue.Score : 0;
            return dValue + 1;
        }

        /// <summary>
        /// 获取几个集合的交叉并集合,并保存到一个新Key中
        /// </summary>
        /// <param name="operation">Union：并集  Intersect：交集  Difference：差集  详见 <see cref="SetOperation"/></param>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        private long _SortedSetCombineAndStore(SetOperation operation, string destination, params string[] keys)
        {

            destination = AddSysCustomKey(destination);
            RedisKey[] keyList = ConvertRedisKeysAddSysCustomKey(keys);
            var rValue = base.redis.SortedSetCombineAndStore(operation, destination, keyList);
            return rValue;

        }

        /// <summary>
        /// 获取几个集合的交叉并集合,并保存到一个新Key中
        /// </summary>
        /// <param name="operation">Union：并集  Intersect：交集  Difference：差集  详见 <see cref="SetOperation"/></param>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        private async Task<long> _SortedSetCombineAndStoreAsync(SetOperation operation, string destination, params string[] keys)
        {
            destination = AddSysCustomKey(destination);
            RedisKey[] keyList = ConvertRedisKeysAddSysCustomKey(keys);
            var rValue = await base.redis.SortedSetCombineAndStoreAsync(operation, destination, keyList);
            return rValue;
        }

        #endregion
        #endregion

        #region string操作异步方法
        /// <summary>
        /// 异步方法 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringSetAsync(key, value, expiry);
        }
        /// <summary>
        /// 异步方法 添加多个key/value
        /// </summary>
        /// <param name="valueList">key/value集合</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(Dictionary<string, string> valueList)
        {
            var newkeyValues = valueList.Select(p => new KeyValuePair<RedisKey, RedisValue>(AddSysCustomKey(p.Key), p.Value)).ToArray();
            return await base.redis.StringSetAsync(newkeyValues);
        }

        /// <summary>
        /// 异步方法 保存一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">保存的Key名称</param>
        /// <param name="obj">对象实体</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            string jsonValue = JsonHelper.GetJson(obj);
            return await base.redis.StringSetAsync(key, jsonValue, expiry);
        }

        /// <summary>
        /// 异步方法 在原有key的value值之后追加value
        /// </summary>
        /// <param name="key">追加的Key名称</param>
        /// <param name="value">追加的值</param>
        /// <returns></returns>
        public async Task<long> StringAppendAsync(string key, string value)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringAppendAsync(key, value);
        }

        /// <summary>
        /// 异步方法 获取单个key的值
        /// </summary>
        /// <param name="key">要读取的Key名称</param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringGetAsync(key);
        }

        /// <summary>
        /// 异步方法 获取多个key的value值
        /// </summary>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        public async Task<List<string>> StringGetAsync(params string[] keys)
        {
            var newKeys = ConvertRedisKeysAddSysCustomKey(keys);
            var values = await base.redis.StringGetAsync(newKeys);
            return values.Select(o => o.ToString()).ToList();
        }


        /// <summary>
        /// 异步方法 获取单个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="key">要获取值的Key集合</param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var values = await base.redis.StringGetAsync(key);
            return JsonHelper.GetObject<T>(values);
        }

        /// <summary>
        /// 异步方法 获取多个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        public async Task<List<T>> StringGetAsync<T>(params string[] keys)
        {
            var newKeys = ConvertRedisKeysAddSysCustomKey(keys);
            var values = await base.redis.StringGetAsync(newKeys);
            return ConvetList<T>(values);
        }

        /// <summary>
        /// 异步方法 获取旧值赋上新值
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        public async Task<string> StringGetSetAsync(string key, string value)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringGetSetAsync(key, value);
        }

        /// <summary>
        /// 异步方法 获取旧值赋上新值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        public async Task<T> StringGetSetAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jsonValue = JsonHelper.GetJson(value);
            var oValue = await base.redis.StringGetSetAsync(key, jsonValue);
            return JsonHelper.GetObject<T>(oValue);
        }


        /// <summary>
        /// 异步方法 获取值的长度
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <returns></returns>
        public async Task<long> StringGetLengthAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringLengthAsync(key);
        }

        /// <summary>
        /// 异步方法 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<double> StringIncrementAsync(string key, double val = 1)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringIncrementAsync(key, val);
        }

        /// <summary>
        /// 异步方法 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<double> StringDecrementAsync(string key, double val = 1)
        {
            key = AddSysCustomKey(key);
            return await base.redis.StringDecrementAsync(key, val);
        }

        #endregion

        #region String操作同步方法
        /// <summary>
        /// 添加单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            return base.redis.StringSet(key, value, expiry);
        }

        /// <summary>
        /// 添加多个key/value
        /// </summary>
        /// <param name="valueList">key/value集合</param>
        /// <returns></returns>
        public bool StringSet(Dictionary<string, string> valueList)
        {
            var newkeyValues = valueList.Select(p => new KeyValuePair<RedisKey, RedisValue>(AddSysCustomKey(p.Key), p.Value)).ToArray();
            return base.redis.StringSet(newkeyValues);
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">保存的Key名称</param>
        /// <param name="value">对象实体</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            string jsonValue = JsonHelper.GetJson(value);
            return base.redis.StringSet(key, jsonValue, expiry);
        }

        /// <summary>
        /// 在原有key的value值之后追加value
        /// </summary>
        /// <param name="key">追加的Key名称</param>
        /// <param name="value">追加的值</param>
        /// <returns></returns>
        public long StringAppend(string key, string value)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringAppend(key, value);
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">要读取的Key名称</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringGet(key);
        }

        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        public List<string> StringGet(params string[] keys)
        {
            var newKeys = ConvertRedisKeysAddSysCustomKey(keys);
            var values = base.redis.StringGet(newKeys);
            return values.Select(o => o.ToString()).ToList();
        }


        /// <summary>
        /// 获取单个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="key">要获取值的Key集合</param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            key = AddSysCustomKey(key);
            var values = base.redis.StringGet(key);
            return JsonHelper.GetObject<T>(values);
        }

        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        public List<T> StringGet<T>(params string[] keys)
        {
            var newKeys = ConvertRedisKeysAddSysCustomKey(keys);
            var values = base.redis.StringGet(newKeys);
            return ConvetList<T>(values);
        }

        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        public string StringGetSet(string key, string value)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringGetSet(key, value);
        }

        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        public T StringGetSet<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            string jsonValue = JsonHelper.GetJson(value);
            var oValue = base.redis.StringGetSet(key, jsonValue);
            return JsonHelper.GetObject<T>(oValue);
        }


        /// <summary>
        /// 获取值的长度
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <returns></returns>
        public long StringGetLength(string key)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringLength(key);
        }

        /// <summary>
        /// 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public double StringIncrement(string key, double val = 1)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringIncrement(key, val);
        }

        /// <summary>
        /// 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public double StringDecrement(string key, double val = 1)
        {
            key = AddSysCustomKey(key);
            return base.redis.StringDecrement(key, val);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 将值反系列化成对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        protected List<T> ConvetList<T>(RedisValue[] values)
        {
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = JsonHelper.GetObject<T>(item);
                result.Add(model);
            }
            return result;
        }
        /// <summary>
        /// 将string类型的Key转换成 <see cref="RedisKey"/> 型的Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        protected RedisKey[] ConvertRedisKeys(List<string> redisKeys) => redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();

        /// <summary>
        /// 将string类型的Key转换成 <see cref="RedisKey"/> 型的Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        protected RedisKey[] ConvertRedisKeys(params string[] redisKeys) => redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();

        /// <summary>
        /// 将string类型的Key转换成 <see cref="RedisKey"/> 型的Key，并添加前缀字符串
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        protected RedisKey[] ConvertRedisKeysAddSysCustomKey(params string[] redisKeys) => redisKeys.Select(redisKey => (RedisKey)AddSysCustomKey(redisKey)).ToArray();
        /// <summary>
        /// 将值集合转换成RedisValue集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisValues"></param>
        /// <returns></returns>
        protected RedisValue[] ConvertRedisValue<T>(params T[] redisValues) => redisValues.Select(o => (RedisValue)JsonHelper.GetJson<T>(o)).ToArray();
        #endregion 辅助方法
    }
}
