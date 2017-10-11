using CGT.DDD.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.PayCenter.Service {
    public class BatchRechargeProcessor : ProcessorBase<PayView> {
        protected override string RequestAddress {
            get { return "member/account/returnAmount"; }
        }

        protected override string ServiceAddress {
            get { return PayCenterApiUrl; }
        }
        private readonly string _personalMerchantNo;//个人商户号
        private readonly string _companyCode;
        private readonly OrderInfo _orderInfo;
        private readonly string _amount;
        private readonly string _accountType;
        private readonly string _sellerEmail;
        private readonly string _returnAmountAccount;//返钱账户
        private readonly string _startBillDateTime;//开始账期 
        private readonly string _endBillDateTime;//结束账期 
        private readonly string _billId;
        private readonly string _allBillInterest; //总利息

        public BatchRechargeProcessor(string personalMerchantNo, string companyCode, string amount, OrderInfo orderInfo, string accountType, string sellerEmail, string startBillDateTime, string endBillDateTime, string billId, string allBillInterest) {
            _personalMerchantNo = personalMerchantNo;
            _companyCode = companyCode;
            _amount = amount;
            _orderInfo = orderInfo;
            _accountType = accountType;
            _sellerEmail = sellerEmail;
            _returnAmountAccount = sellerEmail;
            _startBillDateTime = startBillDateTime;
            _endBillDateTime = endBillDateTime;
            _billId = billId;
            _allBillInterest = allBillInterest;
        }
        protected override Dictionary<string, object> PrepareRequestCore() {
            var result = new Dictionary<string, object>();
            result.Add("personalMerchantNo", _personalMerchantNo);
            result.Add("companyCode", _companyCode);
            result.Add("startBillDateTime", _startBillDateTime);
            result.Add("endBillDateTime", _endBillDateTime);
            result.Add("amount", _amount);
            result.Add("accountType", _accountType);
            result.Add("sellerEmail", _sellerEmail);
            result.Add("returnAmountAccount", _returnAmountAccount);
            result.Add("billId", _billId);
            result.Add("order", Newtonsoft.Json.JsonConvert.SerializeObject(_orderInfo));
            result.Add("allBillInterest", _allBillInterest);
            return result;
        }
    }
}
