using CGT.DDD.Config;
using System.Collections.Generic;

namespace CGT.PayCenter.Service
{
    /// <summary>
    /// 询价
    /// </summary>
    public class InquiryPriceProcessor : ProcessorBase<InquiryPriceView>
    {
        protected override string RequestAddress
        {
            get { return "member/merchant/queryBalance"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterApiUrl; }
        }
        private readonly string _companyCode;
        private readonly string _startDate;
        private readonly string _amount;
        private readonly string _changeBackRate;
        private readonly string _returnAmountAccount;

        public InquiryPriceProcessor(string companyCode, string startDate, string amount, string changeBackRate, string returnAmountAccount)
        {
            _companyCode = companyCode;
            _startDate = startDate;
            _amount = amount;
            _changeBackRate = changeBackRate;
            _returnAmountAccount = returnAmountAccount;
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var result = new Dictionary<string, object>();
            result.Add("companyCode", _companyCode);
            result.Add("startDate", _startDate);
            result.Add("amount", _amount);
            result.Add("changeBackRate", _changeBackRate);
            result.Add("returnAmountAccount", _returnAmountAccount);
            return result;
        }
    }
}
