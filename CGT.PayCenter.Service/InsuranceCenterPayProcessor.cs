using CGT.DDD.Config;
using System.Collections.Generic;

namespace CGT.PayCenter.Service
{
    /// <summary>
    /// 支付
    /// </summary>
    public class InsuranceCenterPayProcessor : ProcessorBase<InsurancePayView>
    {
        protected override string RequestAddress
        {
            get{return "insur/account/transferAccount"; }
        }
        protected override string ServiceAddress
        {
            get { return PayCenterApiUrl; }
          
        }

        private readonly string _personalMerchantNo;//支付账户
        private readonly string _companyCode;//商户code
        private readonly string _amount;//总金额
        private readonly string _backRate;//返现比率
        private readonly string _toMerchangNo;//结算账户
        private readonly string _returnAmountAccount;//返钱账户
        private readonly string _incomeAmount;//收益金额
        private readonly string _sellerEmail;//支付账户邮箱
        private readonly InsuranceOrderInfo _orderInfo;
        private readonly string _accountType;

        public InsuranceCenterPayProcessor(string amount, string toMerchangNo, string returnAmountAccount, string companyCode, string backRate,
           string sellerEmail, string incomeAmount, string personalMerchantNo, InsuranceOrderInfo model ,string accountType )
        {
            _personalMerchantNo = personalMerchantNo;
            _companyCode = companyCode;
            _backRate = backRate;
            _amount = amount;
            _toMerchangNo = toMerchangNo;
            _incomeAmount = incomeAmount;
            _returnAmountAccount = returnAmountAccount;
            _sellerEmail = sellerEmail;
            _orderInfo = model;
            _accountType = accountType;
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("amount", _amount);
            request.Add("menchantNo", _toMerchangNo);
            request.Add("returnAmountAccount", _returnAmountAccount);
            request.Add("companyCode", _companyCode);
            request.Add("backRate", _backRate);
            request.Add("sellerEmail", _sellerEmail);
            request.Add("incomeAmount", _incomeAmount);
            request.Add("personalMerchantNo", _personalMerchantNo);
            request.Add("accountType", _accountType);
            request.Add("order", Newtonsoft.Json.JsonConvert.SerializeObject(_orderInfo));
            return request;
        }
    }


}
