using CGT.DDD.Domain;
using System;
using System.Linq;
using CGT.DDD.IRepositories.Commons;
using System.Linq.Expressions;

namespace CGT.DDD.IRepositories
{
    /// <summary>
    /// 提供排序功能的规范
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/8/25 14:18:26
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public interface IOrderableRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// 带排序的结果集
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderBy);

        /// <summary>
        /// 根据指定lambda表达式和排序方式,得到延时结果集
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderBy, Expression<Func<TEntity, bool>> predicate);
    }
}
