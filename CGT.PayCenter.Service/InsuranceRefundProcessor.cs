using CGT.DDD.Config;
using System.Collections.Generic;

namespace CGT.PayCenter.Service
{
    public class InsuranceRefundProcessor : ProcessorBase<PayView>
    {
        protected override string RequestAddress
        {
            get { return "insur/account/refundAmount"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterApiUrl; }
        }

        private readonly string _amount;//退款金额
        private readonly string _companyCode;//商户code
        private readonly string _sellerEmail;//邮箱
        private readonly int _isFull;//是否全退
        private readonly InsuranceRefundOrder _order;//订单信息

        public InsuranceRefundProcessor(string amount, string companyCode, string sellerEmail, int isFull, InsuranceRefundOrder order)
        {
            _amount = amount;
            _companyCode = companyCode;
            _sellerEmail = sellerEmail;
            _isFull = isFull;
            _order = order;
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("amount", _amount);
            request.Add("companyCode", _companyCode);
            request.Add("sellerEmail", _sellerEmail);
            request.Add("isFull", _isFull);
            request.Add("order", Newtonsoft.Json.JsonConvert.SerializeObject(_order));
            return request;
        }
    }
}
