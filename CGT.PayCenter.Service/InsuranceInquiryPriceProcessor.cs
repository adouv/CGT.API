using CGT.DDD.Config;
using System.Collections.Generic;

namespace CGT.PayCenter.Service
{
    /// <summary>
    /// 保险询价
    /// </summary>
    public class InsuranceInquiryPriceProcessor : ProcessorBase<InquiryPriceView>
    {
        protected override string RequestAddress
        {
            get { return "insur/merchant/queryBalance"; }
        }
        protected override string ServiceAddress
        {
            get { return PayCenterApiUrl; }
        }
        private readonly string _menchantNo;
        private readonly string _companyCode;
        private readonly string _insuranceCode;
        private readonly string _amount;
        private readonly string _backRate;

        public InsuranceInquiryPriceProcessor(string toMerchangNo,
            string companyCode, string insuranceCode, string amount, string backRate)
        {
            _menchantNo = toMerchangNo;
            _companyCode = companyCode;
            _insuranceCode = insuranceCode;
            _amount = amount;
            _backRate = backRate;
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var result = new Dictionary<string, object>();
            result.Add("menchantNo", _menchantNo);
            result.Add("companyCode", _companyCode);
            result.Add("insuranceCode", _insuranceCode);
            result.Add("amount", _amount);
            result.Add("backRate", _backRate);
            return result;
        }
    }
}
