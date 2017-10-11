using CGT.DDD.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.PayCenter.Service {
    public class RefundProcessor : ProcessorBase<PayView> {
        protected override string RequestAddress {
            get { return "member/account/refundAmount"; }
        }

        protected override string ServiceAddress {
            get { return PayCenterApiUrl; }
        }


        private readonly string _companyCode;//商户code
        private readonly string _amount;//订单总金额
        private readonly string _accountType;//账户类型
        private readonly string _sellerEmail;//支付账户
        private readonly string _returnAmountAccount;//返现账户
        private readonly int isFull = 1;//是否全额

        private readonly OrderInfo _orderInfo;

        public RefundProcessor(string companyCode, string amount, string accountType, string sellerEmail, string returnAmountAccount, OrderInfo model) {
            _companyCode = companyCode;
            _amount = amount;
            _accountType = accountType;
            _sellerEmail = sellerEmail;
            _orderInfo = model;
            _returnAmountAccount = returnAmountAccount;
        }

        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            request.Add("companyCode", _companyCode);
            request.Add("amount", _amount);
            request.Add("accountType", _accountType);
            request.Add("sellerEmail", _sellerEmail);
            request.Add("returnAmountAccount", _returnAmountAccount);
            request.Add("isFull", isFull);
            request.Add("order", Newtonsoft.Json.JsonConvert.SerializeObject(_orderInfo));
            return request;
        }
    }
}
