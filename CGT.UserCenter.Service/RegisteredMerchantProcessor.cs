using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace CGT.UserCenter.Service
{
    public class RegisteredMerchantProcessor : BaseProcessor<MerchantsModifyResult>
    {
        private string _loginName;
        private string _password;
        private string _companyCode;
        private string _companyName;
        private string _receivablesAccount;
        private string _merchantNo;
        private string _telphone;
        private string _address;
        private string _email;
        private string _contactPerson;
        private string _payAccountName;
        private string _payAccountNo;
        private string _reapalMerchantPwd;
        protected override string RequestAddress { get { return "pay-merchant-web/manage/merchant/registerMerchant"; } }
        protected override string ServiceAddress { get { return UserCenterApiUrl; } }

        /// <summary>
        ///注册
        /// </summary>
        /// <param name="loginName">登入名称</param>
        /// <param name="password">密码</param>
        /// <param name="companyCode">商户</param>
        /// <param name="companyName">商户名称</param>
        /// <param name="contactPerson">联系人</param>
        /// <param name="payAccountName">支付账户</param>
        /// <param name="payAccountNo">支付账户号</param>
        /// <param name="receivablesAccount">结算账户</param>
        /// <param name="merchantNo">结算账户号</param>
        /// <param name="telphone">电话</param>
        /// <param name="address">地址</param>
        /// <param name="email">邮箱</param>
        /// <param name="reapalMerchantPwd">融宝密码</param>
        public void InitData(string loginName, string password, string companyCode, string companyName, string contactPerson,
            string payAccountName, string payAccountNo, string receivablesAccount, string merchantNo, string telphone,
            string address, string email, string reapalMerchantPwd)
        {
            _loginName = loginName;
            _password = password;
            _companyCode = companyCode;
            _companyName = companyName;
            _receivablesAccount = receivablesAccount;
            _merchantNo = merchantNo;
            _telphone = telphone;
            _address = address;

            _email = email;
            _reapalMerchantPwd = reapalMerchantPwd;
            _contactPerson = contactPerson;
            _payAccountName = payAccountName;
            _payAccountNo = payAccountNo;
        }

        public RegisteredMerchantProcessor()
        {
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("loginName", _loginName);
            request.Add("password", _password);
            request.Add("companyCode", _companyCode);
            request.Add("companyName", _companyName);
            request.Add("receivablesAccount", _receivablesAccount);
            request.Add("merchantNo", _merchantNo);
            request.Add("telphone", _telphone);
            request.Add("address", _address);

            request.Add("email", _email);
            request.Add("reapalPassword", _reapalMerchantPwd);
            request.Add("contactPerson", _contactPerson);
            request.Add("payAccountName", _payAccountName);
            request.Add("payAccountNo", _payAccountNo);
            return request;
        }
    }
}
