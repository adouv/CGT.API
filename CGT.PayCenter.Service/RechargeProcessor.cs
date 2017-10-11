using CGT.DDD.Config;
using System.Collections.Generic;

namespace CGT.PayCenter.Service
{
    /// <summary>
    /// 还款
    /// </summary>
    public class RechargeProcessor : ProcessorBase<PayView>
    {
        protected override string RequestAddress
        {
            get { return "member/account/returnAmount"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterApiUrl; }//WebConfig.GetWebConfig("PayCenterApiUrl"); 
        }
        private readonly string _personalMerchantNo;//个人商户号
        private readonly string _companyCode;
        private readonly OrderInfo _orderInfo;
        private readonly string _amount;
        private readonly string _accountType;
        private readonly string _sellerEmail;
        private readonly string _startDate;
        private readonly string _billId;
        private readonly string _penaltyAmount;
        private readonly string _returnAmountAccount;//返钱账户
        private readonly string _allBillInterest;    //未还款账单总利息
        public RechargeProcessor(string personalMerchantNo, string companyCode, string startDate, string amount, OrderInfo orderInfo, string accountType, string sellerEmail, string billId, string penaltyAmount, string allBillInterest)
        {
            _personalMerchantNo = personalMerchantNo;
            _companyCode = companyCode;
            _amount = amount;
            _orderInfo = orderInfo;
            _accountType = accountType;
            _sellerEmail = sellerEmail;
            _startDate = startDate;
            _billId = billId;
            _penaltyAmount = penaltyAmount;
            _returnAmountAccount = sellerEmail;
            _allBillInterest = allBillInterest;
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var result = new Dictionary<string, object>();
            result.Add("personalMerchantNo", _personalMerchantNo);
            result.Add("companyCode", _companyCode);
            result.Add("startDate", _startDate);
            result.Add("amount", _amount);
            result.Add("accountType", _accountType);
            result.Add("sellerEmail", _sellerEmail);
            result.Add("billId", _billId);
            result.Add("penaltyAmount", _penaltyAmount);
            result.Add("returnAmountAccount", _returnAmountAccount);
            result.Add("order", Newtonsoft.Json.JsonConvert.SerializeObject(_orderInfo));
            result.Add("allBillInterest",_allBillInterest);
            return result;
        }
    }

}
