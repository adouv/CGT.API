namespace CGT.Api.DTO.Boss.UserCenter.Request
{
    /// <summary>
    /// 商户配置查询实体
    /// </summary>
    public class RequestMerchantsConfigurationModel : RequestBaseModel
    {
        /// <summary>
        /// 商户名称
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 状态：
        /// -1全部 0有效 1无效
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int currentPage { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 创建开始时间
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 创建结束时间
        /// </summary>
        public string endDate { get; set; }
    }
}
