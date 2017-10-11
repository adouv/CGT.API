using CGT.DDD.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.UserCenter.Service
{
    /// <summary>
    /// 商户查询
    /// </summary>
    public class MerchantsQueryProcessor : BaseProcessor<MerchantsQueryResult>
    {
        protected override string RequestAddress
        {
            get { return "pay-merchant-web/manage/merchant/merchantList"; }
        }

        protected override string ServiceAddress
        {
             get { return UserCenterApiUrl; }
        }
        private string _companyName;
        private int _status;
        private int _currentPage;
        private int _pageSize;
        private string _startDate;
        private string _endDate;

        public MerchantsQueryProcessor()
        {
        }
        /// <summary>
        /// 商户查询接口初始化
        /// </summary>
        /// <param name="companyName">商户名称</param>
        /// <param name="status">状态:全部：-1;0:有效;1:无效；默认值:-1</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="startDate">创建开始时间</param>
        /// <param name="endDate">创建结束时间</param>
        public void InitData(string companyName, int status, int currentPage, int pageSize, string startDate, string endDate)
        {
            _companyName = companyName;
            _status = status;
            _currentPage = currentPage;
            _pageSize = pageSize;
            _startDate = startDate;
            _endDate = endDate;
        }
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var result = new Dictionary<string, object>();
            result.Add("companyName", _companyName);
            result.Add("status", _status);
            result.Add("currentPage", _currentPage);
            result.Add("pageSize", _pageSize);
            result.Add("startDate", _startDate);
            result.Add("endDate", _endDate);
            return result;
        }
    }
}
