using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace CGT.UserCenter.Service
{
    /// <summary>
    /// 注册分销
    /// </summary>
    public class UpdateMerchantProcessor : BaseProcessor<MerchantsModifyResult>
    {
        private string _loginName;
        private string _companyCode;
        private string _payAccountName;
        private string _payAccountNo;
        private string _cashCompanyName;
        private string _cashCompanyCode;
        protected override string RequestAddress { get { return "pay-merchant-web/manage/merchant/updateMerchant"; } }
        protected override string ServiceAddress { get { return UserCenterApiUrl; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName">登入名</param>
        /// <param name="companyCode">代理code</param>
        /// <param name="payAccountName">支付账户</param>
        /// <param name="payAccountNo">支付账户号</param>
        /// <param name="cashCompanyName">分销名称</param>
        /// <param name="cashCompanyCode">分销code</param>

        public void InitData(string loginName,  string companyCode, string payAccountName, string payAccountNo,string cashCompanyName,string cashCompanyCode)
        {
            _loginName = loginName;
            _companyCode = companyCode;
            _payAccountName = payAccountName;
            _payAccountNo = payAccountNo;
            _cashCompanyName = cashCompanyName;
            _cashCompanyCode = cashCompanyCode;
        }

        public UpdateMerchantProcessor()
        {
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("loginName", _loginName);
            request.Add("companyCode", _companyCode);
            request.Add("payAccountName", _payAccountName);
            request.Add("payAccountNo", _payAccountNo);
            request.Add("cashCompanyName", _cashCompanyName);
            request.Add("cashCompanyCode", _cashCompanyCode);
            return request;
        }
    }
}
