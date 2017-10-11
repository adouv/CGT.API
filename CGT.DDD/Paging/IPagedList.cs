namespace CGT.DDD.Paging
{
    /// <summary>
    /// 分页接口类
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/8/21 22:08:57
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public interface IPagedList
    {
        /// <summary>
        /// 记录数
        /// </summary>
        int TotalCount { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        int TotalPages { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 是否上一页
        /// </summary>
        bool IsPreviousPage { get; }
        /// <summary>
        /// 是否下一页
        /// </summary>
        bool IsNextPage { get; }
    }
}
