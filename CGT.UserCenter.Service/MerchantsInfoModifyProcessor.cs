using CGT.DDD.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.UserCenter.Service
{
    /// <summary>
    /// 商户信息修改
    /// </summary>
    public class MerchantsInfoModifyProcessor : BaseProcessor<MerchantsModifyResult>
    {
        protected override string RequestAddress
        {
            get { return "pay-merchant-web/manage/merchant/updateMerchant"; }
        }

        protected override string ServiceAddress
        {
             get { return UserCenterApiUrl; }
           
        }
        private string _loginName;
        private string _password;
        private string _companyCode;
        private string _companyName;
        private string _receivablesAccount;
        private string _merchantNo;
        private string _telphone;
        private string _address;
        private string _email;
        private string _reapalPassword;
        private string _contactPerson;

        public MerchantsInfoModifyProcessor()
        {
        }
        /// <summary>
        /// 商户信息修改初始化
        /// </summary>
        /// <param name="loginName">登入名称</param>
        /// <param name="password">密码</param>
        /// <param name="companyCode">商户code</param>
        /// <param name="companyName">商户名称</param>
        /// <param name="receivablesAccount">结算账户</param>
        /// <param name="merchantNo">结算商户号</param>
        /// <param name="telphone">手机号</param>
        /// <param name="address">地址</param>
        /// <param name="email">邮件</param>
        /// <param name="reapalPassword">融宝密码</param>
        /// <param name="contactPerson">联系人
        /// </param>
        public void InitData(string loginName, string password, string companyCode, string companyName, string receivablesAccount, string merchantNo
        , string telphone, string address, string email, string reapalPassword, string contactPerson)
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
            _reapalPassword = reapalPassword;
            _contactPerson = contactPerson;
        }
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var result = new Dictionary<string, object>();
            result.Add("loginName", _loginName);
            result.Add("password", _password);
            result.Add("companyCode", _companyCode);
            result.Add("companyName", _companyName);
            result.Add("receivablesAccount", _receivablesAccount);
            result.Add("merchantNo", _merchantNo);
            result.Add("telphone", _telphone);
            result.Add("address", _address);
            result.Add("email", _email);
            result.Add("reapalPassword", _reapalPassword);
            result.Add("contactPerson", _contactPerson);
            return result;
        }
    }
}
