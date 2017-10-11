using System;
using System.Linq.Expressions;

namespace CGT.DDD.IRepositories.Commons
{
    /// <summary>
    /// 排序规范
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/8/25 14:20:00
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public interface IOrderable<T>
    {
        /// <summary>
        /// 递增
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        IOrderable<T> Asc<TKey>(Expression<Func<T, TKey>> keySelector);
        /// <summary>
        /// 然后递增
        /// </summary>
        /// <typeparam name="TKey1"></typeparam>
        /// <typeparam name="TKey2"></typeparam>
        /// <param name="keySelector1"></param>
        /// <returns></returns>
        IOrderable<T> ThenAsc<TKey>(Expression<Func<T, TKey>> keySelector);
        /// <summary>
        /// 递减
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        IOrderable<T> Desc<TKey>(Expression<Func<T, TKey>> keySelector);
        /// <summary>
        /// 然后递减
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        IOrderable<T> ThenDesc<TKey>(Expression<Func<T, TKey>> keySelector);
        /// <summary>
        /// 排序后的结果集
        /// </summary>
        global::System.Linq.IQueryable<T> Queryable { get; }
    }
}
