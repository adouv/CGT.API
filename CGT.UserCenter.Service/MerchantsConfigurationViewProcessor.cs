using System.Collections.Generic;

namespace CGT.UserCenter.Service
{
    /// <summary>
    /// 商户配置详情
    /// </summary>
    public class MerchantsConfigurationViewProcessor : BaseProcessor<MerchantsConfigurationViewResult>
    {
        protected override string RequestAddress => "pay-merchant-web/manage/companyConfiguration/viewCompanyConfiguration";
        protected override string ServiceAddress => UserCenterApiUrl;

        private string _companyCode;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="companyCode">商户Code</param>
        public void Init(string companyCode)
        {
            _companyCode = companyCode;
        }
        /// <summary>
        /// 请求参数赋值
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var dicResult = new Dictionary<string, object>();
            dicResult.Add("companyCode", _companyCode);
            return dicResult;
        }
    }
}
