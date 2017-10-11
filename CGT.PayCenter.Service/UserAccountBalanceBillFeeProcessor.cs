using CGT.DDD.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.PayCenter.Service
{
    public class UserAccountBalanceBillFeeProcessor : ProcessorBase<UserAccountBalanceBillFeeView>
    {
        protected override string RequestAddress
        {
            get { return "member/merchant/queryRate"; }

        }

        protected override string ServiceAddress
        {
            get { return PayCenterApiUrl; }
        }

        private readonly string _merchantCode;//商户号


        public UserAccountBalanceBillFeeProcessor(string merchantCode)
        {
            _merchantCode = merchantCode;
            
        }

        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("companyCode", _merchantCode);
            return request;
        }

    }
}
