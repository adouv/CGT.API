using CGT.DDD.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.PayCenter.Service
{
    public class InsuranceRemoneyProcessor : ProcessorBase<InsurancePayView>
    {
        protected override string RequestAddress
        {
            get { return "insur/account/loan"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterApiUrl; }
        }

        private readonly string _personalMerchantNo;//个人商户号
        private readonly string _companyCode;//商户code
        private readonly string _billDate;//起飞时间
        private readonly string _amount;//返现金额
        private readonly string _accountType;//账户类型
        private readonly string _returnAmountAccount;//返钱账户
        private readonly string _insuranceNo;//保单号
        private readonly string _remoneyRate;//返现比率

        private readonly InsuranceOrderInfo _reMoney;

        public InsuranceRemoneyProcessor(string personalMerchantNo, string companyCode, string billDate, string amount, string accountType, InsuranceOrderInfo remoney, string returnAmountAccount, string insuranceNo, string remoneyRate)
        {
            _personalMerchantNo = personalMerchantNo;
            _companyCode = companyCode;
            _billDate = billDate;
            _amount = amount;
            _accountType = accountType;
            _returnAmountAccount = returnAmountAccount;
            _insuranceNo = insuranceNo;
            _remoneyRate = remoneyRate;
            _reMoney = remoney;
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("returnAmountAccount", _returnAmountAccount);
            request.Add("order", Newtonsoft.Json.JsonConvert.SerializeObject(_reMoney));
            request.Add("companyCode", _companyCode);
            request.Add("accountDate", _billDate);
            request.Add("insuranceNo", _insuranceNo);
            request.Add("sellerEmail", _returnAmountAccount);
            request.Add("personalMerchantNo", _personalMerchantNo);
            request.Add("amount", _amount);
            request.Add("backRate", _remoneyRate);
            request.Add("accountType", _accountType);
          
            return request;
        }
    }
}
