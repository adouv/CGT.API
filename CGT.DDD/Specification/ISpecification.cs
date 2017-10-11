using System;
using System.Linq.Expressions;

namespace CGT.DDD.Specification
{
    /// <summary>
    /// 规范
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/8/25 15:36:02
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Check if this specification is satisfied by a 
        /// specific expression lambda
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> SatisfiedBy();
    }
}
