using CGT.DDD.Config;
using System.Collections.Generic;

namespace CGT.PayCenter.Service
{
    /// <summary>
    /// 支付
    /// </summary>
    public class PayProcessor : ProcessorBase<PayView>
    {
        protected override string RequestAddress
        {
            get { return "member/account/transferAccount"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterApiUrl; }
        }

        private readonly string _personalMerchantNo;//个人商户号
        private readonly string _companyCode;//商户code
        private readonly string _startDate;//起飞时间
        private readonly string _amount;//订单总金额
        private readonly string _accountType;//账户类型
        private readonly string _sellerEmail;//支付账户
        private readonly string _returnAmountAccount;//返钱账户
        private readonly string _changeBackRate;//退改签比例
        private readonly string _feeType;//手续费类型
        private readonly OrderInfo _orderInfo;

        public PayProcessor(string personalMerchantNo, string companyCode, string startDate, string amount, string accountType, string sellerEmail, string returnAmountAccount, OrderInfo model, string changeBackRate, string feeType)
        {
            _personalMerchantNo = personalMerchantNo;
            _companyCode = companyCode;
            _startDate = startDate;
            _amount = amount;
            _accountType = accountType;
            _sellerEmail = sellerEmail;
            _returnAmountAccount = returnAmountAccount;
            _orderInfo = model;
            _changeBackRate = changeBackRate;
            _feeType = feeType;
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("personalMerchantNo", _personalMerchantNo);
            request.Add("companyCode", _companyCode);
            request.Add("startDate", _startDate);
            request.Add("amount", _amount);
            request.Add("accountType", _accountType);
            request.Add("sellerEmail", _sellerEmail);
            request.Add("returnAmountAccount", _returnAmountAccount);
            request.Add("changeBackRate", _changeBackRate);
            request.Add("feeType", _feeType);
            request.Add("order", Newtonsoft.Json.JsonConvert.SerializeObject(_orderInfo));
            return request;
        }
    }


}
