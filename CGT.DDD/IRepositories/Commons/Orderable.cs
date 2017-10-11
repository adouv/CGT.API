using System;
using System.Linq;
using System.Linq.Expressions;

namespace CGT.DDD.IRepositories.Commons
{
    /// <summary>
    /// 架构里对集合排序实现
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/8/25 14:23:55
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class Orderable<T> : IOrderable<T>
    {
        private IQueryable<T> _queryable;

        /// <summary>
        /// 排序后的结果集
        /// </summary>
        /// <param name="enumerable"></param>
        public Orderable(IQueryable<T> enumerable)
        {
            _queryable = enumerable;
        }

        /// <summary>
        /// 排序之后的结果集
        /// </summary>
        public IQueryable<T> Queryable
        {
            get { return _queryable; }
        }
        /// <summary>
        /// 递增
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> Asc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<T>)
                .OrderBy(keySelector);
            return this;
        }
        /// <summary>
        /// 然后递增
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> ThenAsc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<T>)
                .ThenBy(keySelector);
            return this;
        }
        /// <summary>
        /// 递减
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> Desc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = _queryable
                .OrderByDescending(keySelector);
            return this;
        }
        /// <summary>
        /// 然后递减
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> ThenDesc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<T>)
                .ThenByDescending(keySelector);
            return this;
        }
    }
}
