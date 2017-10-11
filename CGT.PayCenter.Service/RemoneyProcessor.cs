using CGT.DDD.Config;
using System.Collections.Generic;

namespace CGT.PayCenter.Service {
    public class RemoneyProcessor : ProcessorBase<PayView> {
        protected override string RequestAddress {
            get { return "member/account/loanAmount"; }
        }

        protected override string ServiceAddress {
            get { return PayCenterApiUrl; }
        }

        private readonly string _personalMerchantNo;//个人商户号
        private readonly string _companyCode;//商户code
        private readonly string _startDate;//起飞时间
        private readonly string _amount;//返现金额
        private readonly string _accountType;//账户类型
        private readonly string _returnAmountAccount;//返钱账户
        private readonly string _billDateTime;//账期时间
        private readonly string _changeBackRate;//退改签比例
        private readonly ReMoney _reMoney;

        public RemoneyProcessor(string personalMerchantNo, string companyCode, string startDate, string amount, string accountType, ReMoney remoney, string returnAmountAccount, string billDateTime, string changeBackRate) {
            _personalMerchantNo = personalMerchantNo;
            _companyCode = companyCode;
            _startDate = startDate;
            _amount = amount;
            _accountType = accountType;
            _returnAmountAccount = returnAmountAccount;
            _billDateTime = billDateTime;
            _changeBackRate = changeBackRate;
            _reMoney = remoney;
        }

        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            request.Add("personalMerchantNo", _personalMerchantNo);
            request.Add("companyCode", _companyCode);
            request.Add("startDate", _startDate);
            request.Add("amount", _amount);
            request.Add("accountType", _accountType);
            request.Add("returnAmountAccount", _returnAmountAccount);
            request.Add("billDateTime", _billDateTime);
            request.Add("changeBackRate", _changeBackRate);
            request.Add("order", Newtonsoft.Json.JsonConvert.SerializeObject(_reMoney));
            return request;
        }
    }
}
