
using CGT.DDD.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.PayCenter.Service {
    public class RegisteredMerchantProcessor : ProcessorBase<RegisteredMerchantView> {
        private readonly string _loginName;
        private readonly string _password;
        private readonly string _companyCode;
        private readonly string _companyName;
        private readonly string _grade;
        private readonly string _parentCode;
        private readonly string _returnAmountAccount;
        private readonly string _receivablesAccount;
        private readonly string _merchantNo;
        private readonly string _telphone;
        private readonly string _address;
        private readonly string _remark;

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="companyCode"></param>
        /// <param name="companyName"></param>
        /// <param name="grade"></param>
        /// <param name="parentCode"></param>
        /// <param name="returnAmountAccount"></param>
        /// <param name="receivablesAccount"></param>
        /// <param name="merchantNo"></param>
        /// <param name="telphone"></param>
        /// <param name="address"></param>
        /// <param name="remark"></param>
        public RegisteredMerchantProcessor(string loginName, string password, string companyCode, string companyName, string grade,
            string parentCode, string returnAmountAccount, string receivablesAccount, string merchantNo, string telphone, string address, string remark) {
            _loginName = loginName;
            _password = password;
            _companyCode = companyCode;
            _companyName = companyName;
            _grade = grade;
            _parentCode = parentCode;
            _returnAmountAccount = returnAmountAccount;
            _receivablesAccount = receivablesAccount;
            _merchantNo = merchantNo;
            _telphone = telphone;
            _address = address;
            _remark = remark;
        }
        protected override bool IsBase {
            get {
                return false;
            }
        }
        protected override string RequestAddress {
            get { return "company/saveMerchant"; }
        }
        protected override string ServiceAddress {
            get { return PayCenterBossApiUrl; }
        }
        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            request.Add("loginName", _loginName);
            request.Add("password", _password);
            request.Add("companyCode", _companyCode);
            request.Add("companyName", _companyName);
            request.Add("grade", _grade);
            request.Add("parentCode", _parentCode);
            request.Add("returnAmountAccount", _returnAmountAccount);
            request.Add("receivablesAccount", _receivablesAccount);
            request.Add("merchantNo", _merchantNo);
            request.Add("telphone", _telphone);
            request.Add("address", _address);
            request.Add("remark", _remark);
            return request;
        }
    }
}
