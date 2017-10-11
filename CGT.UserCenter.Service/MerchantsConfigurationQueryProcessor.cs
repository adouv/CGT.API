using System.Collections.Generic;

namespace CGT.UserCenter.Service
{
    /// <summary>
    /// 商户配置查询
    /// </summary>
    public class MerchantsConfigurationQueryProcessor : BaseProcessor<MerchantsConfigurationQueryResult>
    {
        protected override string RequestAddress => "pay-merchant-web/manage/companyConfiguration/companyConfigurationList";
        protected override string ServiceAddress => UserCenterApiUrl;

        private string _companyName;
        private int _status;
        private int _currentPage;
        private int _pageSize;
        private string _startDate;
        private string _endDate;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="companyName">商户名称</param>
        /// <param name="status">状态：-1全部 0有效 1无效</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="startDate">创建开始时间</param>
        /// <param name="endDate">创建结束时间</param>
        public void Init(string companyName, int status, int currentPage, int pageSize, string startDate, string endDate)
        {
            _companyName = companyName;
            _status = status;
            _currentPage = currentPage;
            _pageSize = pageSize;
            _startDate = startDate;
            _endDate = endDate;
        }
        /// <summary>
        /// 请求参数赋值
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var dicResult = new Dictionary<string, object>();
            dicResult.Add("companyName", _companyName);
            dicResult.Add("status", _status);
            dicResult.Add("currentPage", _currentPage);
            dicResult.Add("pageSize", _pageSize);
            dicResult.Add("startDate", _startDate);
            dicResult.Add("endDate", _endDate);
            return dicResult;
        }
    }
}
