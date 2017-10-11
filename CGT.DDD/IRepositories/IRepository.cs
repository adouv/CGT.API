using CGT.DDD.Domain;
using CGT.DDD.UoW;

namespace CGT.DDD.IRepositories
{
    /// <summary>
    /// 基本仓储操作接口
    /// out表示类型的协变,可以进行子类与父类的返回类型转换,in表示逆变
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/8/25 14:17:33
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public interface IRepository<TEntity> : IUnitOfWorkRepository where TEntity : class, IEntity
    {
        /// <summary>
        /// 通过主键拿一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(params object[] id);
        /// <summary>
        /// 拿到可查询结果集
        /// </summary>
        /// <returns></returns>
        System.Linq.IQueryable<TEntity> GetModel();
        /// <summary>
        /// 设置当前数据库上下文对象，与具体ORM无关，上下文应该继承Lind.DDD.UnitOfWork.IDataContext
        /// </summary>
        void SetDataContext(object db);
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="item"></param>
        void Insert(TEntity item);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="item"></param>
        void Update(TEntity item);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="item"></param>
        void Delete(TEntity item);
    }
}
