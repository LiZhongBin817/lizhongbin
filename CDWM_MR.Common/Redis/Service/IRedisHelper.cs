using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR_Common.Redis
{
    public interface IRedisHelper
    {
        Task<bool> KeyDeleteAsync(string key);
        #region List集合操作异步方法
        /// <summary>
        /// 从左侧向list中添加一个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string key, T value);

        /// <summary>
        /// 从左侧向list中添加多个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string key, List<T> value);

        /// <summary>
        /// 从右侧向list中添加一个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string key, T value);

        /// <summary>
        /// 从右侧向list中添加多个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string key, List<T> value);

        /// <summary>
        /// 从左侧向list中取出一个值并从list中删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string key);

        /// <summary>
        /// 从右侧向list中取出一个值并从list中删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string key);

        /// <summary>
        /// 从key的List中右侧取出一个值，并从左侧添加到destination集合中，且返回该数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">要取出数据的List名称</param>
        /// <param name="destination">要添加到的List名称</param>
        /// <returns></returns>
        Task<T> ListRightPopLeftPushAsync<T>(string key, string destination);

        /// <summary>
        /// 在key的List指定值pivot之后插入value，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pivot">索引值</param>
        /// <param name="value">要插入的值</param>
        /// <returns></returns>
        Task<long> ListInsertAfterAsync<T>(string key, T pivot, T value);

        /// <summary>
        /// 在key的List指定值pivot之前插入value，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pivot">索引值</param>
        /// <param name="value">要插入的值</param>
        /// <returns></returns>
        Task<long> ListInsertBeforeAsync<T>(string key, T pivot, T value);

        /// <summary>
        /// 从key的list中取出所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<T>> ListRangeAsync<T>(string key);

        /// <summary>
        /// 从key的List获取指定索引的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<T> ListGetByIndexAsync<T>(string key, long index);

        /// <summary>
        /// 获取key的list中数据个数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string key);

        /// <summary>
        /// 从key的List中移除指定的值，返回删除个数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> ListRemoveAsync<T>(string key, T value);
        #endregion

        #region List集合操作同步方法
        /// <summary>
        /// 从左侧向list中添加一个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long ListLeftPush<T>(string key, T value);

        /// <summary>
        /// 从左侧向list中添加多个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long ListLeftPush<T>(string key, List<T> value);

        /// <summary>
        /// 从右侧向list中添加一个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long ListRightPush<T>(string key, T value);

        /// <summary>
        /// 从右侧向list中添加多个值，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long ListRightPush<T>(string key, List<T> value);

        /// <summary>
        /// 从左侧向list中取出一个值并从list中删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T ListLeftPop<T>(string key);

        /// <summary>
        /// 从右侧向list中取出一个值并从list中删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T ListRightPop<T>(string key);

        /// <summary>
        /// 从key的List中右侧取出一个值，并从左侧添加到destination集合中，且返回该数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">要取出数据的List名称</param>
        /// <param name="destination">要添加到的List名称</param>
        /// <returns></returns>
        T ListRightPopLeftPush<T>(string key, string destination);

        /// <summary>
        /// 在key的List指定值pivot之后插入value，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pivot">索引值</param>
        /// <param name="value">要插入的值</param>
        /// <returns></returns>
        long ListInsertAfter<T>(string key, T pivot, T value);

        /// <summary>
        /// 在key的List指定值pivot之前插入value，返回集合总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pivot">索引值</param>
        /// <param name="value">要插入的值</param>
        /// <returns></returns>
        long ListInsertBefore<T>(string key, T pivot, T value);

        /// <summary>
        /// 从key的list中取出所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        List<T> ListRange<T>(string key);

        /// <summary>
        /// 从key的List获取指定索引的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        T ListGetByIndex<T>(string key, long index);

        /// <summary>
        /// 获取key的list中数据个数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long ListLength(string key);

        /// <summary>
        /// 从key的List中移除指定的值，返回删除个数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long ListRemove<T>(string key, T value);
        #endregion

        #region Hash异步方法

        /// <summary>
        /// 异步方法 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        Task<bool> HashExistsAsync(string key, string dataKey);

        /// <summary>
        /// 异步方法 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> HashSetAsync<T>(string key, string dataKey, T t);

        /// <summary>
        /// 异步方法 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        Task<bool> HashDeleteAsync(string key, string dataKey);

        /// <summary>
        /// 异步方法 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        Task<long> HashDeleteAsync(string key, params string[] dataKeys);

        /// <summary>
        /// 异步方法 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        Task<T> HashGetAsync<T>(string key, string dataKey);

        /// <summary>
        /// 异步方法 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        Task<double> HashIncrementAsync(string key, string dataKey, double val = 1);

        /// <summary>
        /// 异步方法 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        Task<double> HashDecrementAsync(string key, string dataKey, double val = 1);

        /// <summary>
        /// 异步方法 获取hashkey所有key名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string[]> HashKeysAsync(string key);

        /// <summary>
        /// 获取hashkey所有key与值，必须保证Key内的所有数据类型一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Dictionary<string, T>> HashGetAllAsync<T>(string key);

        #endregion

        #region Hash同步操作
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        bool HashExists(string key, string dataKey);

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        bool HashSet<T>(string key, string dataKey, T t);

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        bool HashDelete(string key, string dataKey);

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        long HashDelete(string key, params string[] dataKeys);

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        T HashGet<T>(string key, string dataKey);

        /// <summary>
        /// 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        double HashIncrement(string key, string dataKey, double val = 1);

        /// <summary>
        /// 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        double HashDecrement(string key, string dataKey, double val = 1);

        /// <summary>
        /// 获取hashkey所有key名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string[] HashKeys(string key);

        /// <summary>
        /// 获取hashkey所有key与值，必须保证Key内的所有数据类型一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Dictionary<string, T> HashGetAll<T>(string key);
        #endregion

        #region 有序集合异步操作
        /// <summary>
        /// 在Key集合中添加一个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<bool> SetAddAsync<T>(string key, T value);
        /// <summary>
        /// 在Key集合中添加多个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值列表</param>
        /// <returns></returns>
        Task<long> SetAddAsync<T>(string key, List<T> value);

        /// <summary>
        /// 获取key集合值的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> SetLengthAsync(string key);

        /// <summary>
        /// 判断Key集合中是否包含指定的值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value">要判断是值</param>
        /// <returns></returns>
        Task<bool> SetContainsAsync<T>(string key, T value);

        /// <summary>
        /// 随机获取key集合中的一个值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> SetRandomMemberAsync<T>(string key);

        /// <summary>
        /// 获取key所有值的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<T>> SetMembersAsync<T>(string key);

        /// <summary>
        /// 删除key集合中指定的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> SetRemoveAsync<T>(string key, params T[] value);

        /// <summary>
        /// 随机删除key集合中的一个值，并返回该值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> SetPopAsync<T>(string key);


        /// <summary>
        /// 获取几个集合的并集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        Task<List<T>> SetCombineUnionAsync<T>(params string[] keys);
        /// <summary>
        /// 获取几个集合的交集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        Task<List<T>> SetCombineIntersectAsync<T>(params string[] keys);
        /// <summary>
        /// 获取几个集合的差集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        Task<List<T>> SetCombineDifferenceAsync<T>(params string[] keys);

        /// <summary>
        /// 获取几个集合的并集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        Task<long> SetCombineUnionAndStoreAsync(string destination, params string[] keys);
        /// <summary>
        /// 获取几个集合的交集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        Task<long> SetCombineIntersectAndStoreAsync(string destination, params string[] keys);
        /// <summary>
        /// 获取几个集合的差集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        Task<long> SetCombineDifferenceAndStoreAsync(string destination, params string[] keys);
        #endregion

        #region 有序集合同步方法
        /// <summary>
        /// 在Key集合中添加一个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SetAdd<T>(string key, T value);
        /// <summary>
        /// 在Key集合中添加多个value值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">值列表</param>
        /// <returns></returns>
        long SetAdd<T>(string key, List<T> value);

        /// <summary>
        /// 获取key集合值的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long SetLength(string key);

        /// <summary>
        /// 判断Key集合中是否包含指定的值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value">要判断是值</param>
        /// <returns></returns>
        bool SetContains<T>(string key, T value);

        /// <summary>
        /// 随机获取key集合中的一个值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T SetRandomMember<T>(string key);

        /// <summary>
        /// 获取key所有值的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        List<T> SetMembers<T>(string key);

        /// <summary>
        /// 删除key集合中指定的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long SetRemove<T>(string key, params T[] value);

        /// <summary>
        /// 随机删除key集合中的一个值，并返回该值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T SetPop<T>(string key);


        /// <summary>
        /// 获取几个集合的并集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        List<T> SetCombineUnion<T>(params string[] keys);

        /// <summary>
        /// 获取几个集合的交集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        List<T> SetCombineIntersect<T>(params string[] keys);
        /// <summary>
        /// 获取几个集合的差集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        List<T> SetCombineDifference<T>(params string[] keys);

        /// <summary>
        /// 获取几个集合的并集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        long SetCombineUnionAndStore(string destination, params string[] keys);
        /// <summary>
        /// 获取几个集合的交集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        long SetCombineIntersectAndStore(string destination, params string[] keys);
        /// <summary>
        /// 获取几个集合的差集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        long SetCombineDifferenceAndStore(string destination, params string[] keys);
        #endregion

        #region 无序集合异步操作
        /// <summary>
        /// 添加一个值到Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score">排序分数，为空将获取集合中最大score加1</param>
        /// <returns></returns>
        Task<bool> SortedSetAddAsync<T>(string key, T value, double? score = null);

        /// <summary>
        /// 添加一个集合到Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score">排序分数，为空将获取集合中最大score加1</param>
        /// <returns></returns>
        Task<long> SortedSetAddAsync<T>(string key, List<T> value, double? score = null);

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> SortedSetLengthAsync(string key);

        /// <summary>
        /// 获取指定起始值到结束值的集合数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        Task<long> SortedSetLengthByValueAsync<T>(string key, T startValue, T endValue);

        /// <summary>
        /// 获取指定Key的排序Score值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<double?> SortedSetScoreAsync<T>(string key, T value);

        /// <summary>
        /// 获取指定Key中最小Score值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<double> SortedSetMinScoreAsync(string key);

        /// <summary>
        /// 获取指定Key中最大Score值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<double> SortedSetMaxScoreAsync(string key);

        /// <summary>
        /// 删除Key中指定的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        Task<long> SortedSetRemoveAsync<T>(string key, params T[] value);

        /// <summary>
        /// 删除指定起始值到结束值的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        Task<long> SortedSetRemoveRangeByValueAsync<T>(string key, T startValue, T endValue);

        /// <summary>
        /// 删除 从 start 开始的 stop 条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<long> SortedSetRemoveRangeByRankAsync(string key, long start, long stop);

        /// <summary>
        /// 根据排序分数Score，删除从 start 开始的 stop 条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<long> SortedSetRemoveRangeByScoreAsync(string key, double start, double stop);

        /// <summary>
        /// 获取从 start 开始的 stop 条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        Task<List<T>> SortedSetRangeByRankAsync<T>(string key, long start = 0, long stop = -1, bool desc = false);

        /// <summary>
        /// 获取从 start 开始的 stop 条数据包含Score，返回数据格式：Key=值，Value = Score
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        Task<Dictionary<T, double>> SortedSetRangeByRankWithScoresAsync<T>(string key, long start = 0, long stop = -1, bool desc = false);

        /// <summary>
        ///  根据Score排序 获取从 start 开始的 stop 条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        Task<List<T>> SortedSetRangeByScoreAsync<T>(string key, double start = 0, double stop = -1, bool desc = false);

        /// <summary>
        /// 根据Score排序  获取从 start 开始的 stop 条数据包含Score，返回数据格式：Key=值，Value = Score
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        Task<Dictionary<T, double>> SortedSetRangeByScoreWithScoresAsync<T>(string key, double start = 0, double stop = -1, bool desc = false);

        /// <summary>
        /// 获取指定起始值到结束值的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        Task<List<T>> SortedSetRangeByValueAsync<T>(string key, T startValue, T endValue);

        /// <summary>
        /// 获取几个集合的并集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        Task<long> SortedSetCombineUnionAndStoreAsync(string destination, params string[] keys);

        /// <summary>
        /// 获取几个集合的交集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        Task<long> SortedSetCombineIntersectAndStoreAsync(string destination, params string[] keys);

        /// <summary>
        /// 修改指定Key和值的Scores在原值上减去scores，并返回最终Scores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        Task<double> SortedSetDecrementAsync<T>(string key, T value, double scores);

        /// <summary>
        /// 修改指定Key和值的Scores在原值上增加scores，并返回最终Scores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        Task<double> SortedSetIncrementAsync<T>(string key, T value, double scores);
        #endregion

        #region 无序集合同步操作
        /// <summary>
        /// 添加一个值到Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score">排序分数，为空将获取集合中最大score加1</param>
        /// <returns></returns>
        bool SortedSetAdd<T>(string key, T value, double? score = null);

        /// <summary>
        /// 添加一个集合到Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score">排序分数，为空将获取集合中最大score加1</param>
        /// <returns></returns>
        long SortedSetAdd<T>(string key, List<T> value, double? score = null);

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long SortedSetLength(string key);

        /// <summary>
        /// 获取指定起始值到结束值的集合数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        long SortedSetLengthByValue<T>(string key, T startValue, T endValue);

        /// <summary>
        /// 获取指定Key的排序Score值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        double? SortedSetScore<T>(string key, T value);

        /// <summary>
        /// 获取指定Key中最小Score值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        double SortedSetMinScore(string key);

        /// <summary>
        /// 获取指定Key中最大Score值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        double SortedSetMaxScore(string key);

        /// <summary>
        /// 删除Key中指定的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        long SortedSetRemove<T>(string key, params T[] value);

        /// <summary>
        /// 删除指定起始值到结束值的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        long SortedSetRemoveRangeByValue<T>(string key, T startValue, T endValue);

        /// <summary>
        /// 删除 从 start 开始的 stop 条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        long SortedSetRemoveRangeByRank(string key, long start, long stop);

        /// <summary>
        /// 根据排序分数Score，删除从 start 开始的 stop 条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        long SortedSetRemoveRangeByScore(string key, double start, double stop);

        /// <summary>
        /// 获取从 start 开始的 stop 条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        List<T> SortedSetRangeByRank<T>(string key, long start = 0, long stop = -1, bool desc = false);

        /// <summary>
        /// 获取从 start 开始的 stop 条数据包含Score，返回数据格式：Key=值，Value = Score
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        Dictionary<T, double> SortedSetRangeByRankWithScores<T>(string key, long start = 0, long stop = -1, bool desc = false);

        /// <summary>
        ///  根据Score排序 获取从 start 开始的 stop 条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        List<T> SortedSetRangeByScore<T>(string key, double start = 0, double stop = -1, bool desc = false);

        /// <summary>
        /// 根据Score排序  获取从 start 开始的 stop 条数据包含Score，返回数据格式：Key=值，Value = Score
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">起始数</param>
        /// <param name="stop">-1表示到结束，0为1条</param>
        /// <param name="desc">是否按降序排列</param>
        /// <returns></returns>
        Dictionary<T, double> SortedSetRangeByScoreWithScores<T>(string key, double start = 0, double stop = -1, bool desc = false);

        /// <summary>
        /// 获取指定起始值到结束值的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">结束值</param>
        /// <returns></returns>
        List<T> SortedSetRangeByValue<T>(string key, T startValue, T endValue);

        /// <summary>
        /// 获取几个集合的并集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        long SortedSetCombineUnionAndStore(string destination, params string[] keys);

        /// <summary>
        /// 获取几个集合的交集,并保存到一个新Key中
        /// </summary>
        /// <param name="destination">保存的新Key名称</param>
        /// <param name="keys">要操作的Key集合</param>
        /// <returns></returns>
        long SortedSetCombineIntersectAndStore(string destination, params string[] keys);

        /// <summary>
        /// 修改指定Key和值的Scores在原值上减去scores，并返回最终Scores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        double SortedSetDecrement<T>(string key, T value, double scores);

        /// <summary>
        /// 修改指定Key和值的Scores在原值上增加scores，并返回最终Scores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        double SortedSetIncrement<T>(string key, T value, double scores);
        #endregion

        #region string操作异步方法
        /// <summary>
        /// 异步方法 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?));
        /// <summary>
        /// 异步方法 添加多个key/value
        /// </summary>
        /// <param name="valueList">key/value集合</param>
        /// <returns></returns>
        Task<bool> StringSetAsync(Dictionary<string, string> valueList);

        /// <summary>
        /// 异步方法 保存一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">保存的Key名称</param>
        /// <param name="obj">对象实体</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?));

        /// <summary>
        /// 异步方法 在原有key的value值之后追加value
        /// </summary>
        /// <param name="key">追加的Key名称</param>
        /// <param name="value">追加的值</param>
        /// <returns></returns>
        Task<long> StringAppendAsync(string key, string value);

        /// <summary>
        /// 异步方法 获取单个key的值
        /// </summary>
        /// <param name="key">要读取的Key名称</param>
        /// <returns></returns>
        Task<string> StringGetAsync(string key);

        /// <summary>
        /// 异步方法 获取多个key的value值
        /// </summary>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        Task<List<string>> StringGetAsync(params string[] keys);


        /// <summary>
        /// 异步方法 获取单个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="key">要获取值的Key集合</param>
        /// <returns></returns>
        Task<T> StringGetAsync<T>(string key);

        /// <summary>
        /// 异步方法 获取多个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        Task<List<T>> StringGetAsync<T>(params string[] keys);

        /// <summary>
        /// 异步方法 获取旧值赋上新值
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        Task<string> StringGetSetAsync(string key, string value);

        /// <summary>
        /// 异步方法 获取旧值赋上新值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        Task<T> StringGetSetAsync<T>(string key, T value);


        /// <summary>
        /// 异步方法 获取值的长度
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <returns></returns>
        Task<long> StringGetLengthAsync(string key);

        /// <summary>
        /// 异步方法 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        Task<double> StringIncrementAsync(string key, double val = 1);

        /// <summary>
        /// 异步方法 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        Task<double> StringDecrementAsync(string key, double val = 1);

        #endregion

        #region String操作同步方法
        /// <summary>
        /// 添加单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?));

        /// <summary>
        /// 添加多个key/value
        /// </summary>
        /// <param name="valueList">key/value集合</param>
        /// <returns></returns>
        bool StringSet(Dictionary<string, string> valueList);

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">保存的Key名称</param>
        /// <param name="value">对象实体</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        bool StringSet<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?));

        /// <summary>
        /// 在原有key的value值之后追加value
        /// </summary>
        /// <param name="key">追加的Key名称</param>
        /// <param name="value">追加的值</param>
        /// <returns></returns>
        long StringAppend(string key, string value);

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">要读取的Key名称</param>
        /// <returns></returns>
        string StringGet(string key);

        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        List<string> StringGet(params string[] keys);


        /// <summary>
        /// 获取单个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="key">要获取值的Key集合</param>
        /// <returns></returns>
        T StringGet<T>(string key);

        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="keys">要获取值的Key集合</param>
        /// <returns></returns>
        List<T> StringGet<T>(params string[] keys);

        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        string StringGetSet(string key, string value);

        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key名称</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        T StringGetSet<T>(string key, T value);


        /// <summary>
        /// 获取值的长度
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <returns></returns>
        long StringGetLength(string key);

        /// <summary>
        /// 数字增长val，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        double StringIncrement(string key, double val = 1);

        /// <summary>
        /// 数字减少val，返回自减少的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        double StringDecrement(string key, double val = 1);

        #endregion

    }
}
