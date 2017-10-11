using CGT.DDD.Domain;
using System.Collections.Generic;

namespace CGT.DDD.UoW
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/8/25 15:37:55
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public interface IUnitOfWorkRepository
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="item"></param>
        void UoWInsert(IEntity item);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="item"></param>
        void UoWUpdate(IEntity item);
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="item"></param>
        void UoWDelete(IEntity item);
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="item"></param>
        void UoWInsert(IEnumerable<IEntity> list);
        /// <summary>
        /// 更新集合
        /// </summary>
        /// <param name="item"></param>
        void UoWUpdate(IEnumerable<IEntity> list);
        /// <summary>
        /// 移除集合
        /// </summary>
        /// <param name="item"></param>
        void UoWDelete(IEnumerable<IEntity> list);
    }
}
