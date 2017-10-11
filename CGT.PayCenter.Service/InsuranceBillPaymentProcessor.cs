
using CGT.DDD.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.PayCenter.Service
{
    public class InsuranceBillPaymentProcessor : ProcessorBase<InsurancePayView>
    {
        protected override string RequestAddress
        {
            get { return "insur/account/repayAmount"; }
        }

        protected override string ServiceAddress
        {
            get { return PayCenterApiUrl; }
        }
        private readonly string _billId;
        private readonly string _companyCode;
        private readonly string _returnType;
        private readonly string _accountType;
        private readonly string _amount;
        private readonly string _penaltyAmount;
        private readonly string _sellerEmail;
        private readonly string _personalMerchantNo;
        private readonly InsuranceBillPayOrderInfo _orderInfo;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="billId">账单ID</param>
        /// <param name="companyCode">商户code</param>
        /// <param name="returnType">还款类型暂时1</param>
        /// <param name="accountType">账户类型</param>
        /// <param name="amount">还款总金额</param>
        /// <param name="penaltyAmount">罚息总金额</param>
        /// <param name="sellerEmail">邮箱</param>
        /// <param name="model"></param>
        public InsuranceBillPaymentProcessor(string billId, string companyCode, string returnType,
          string accountType, string amount, string penaltyAmount, string sellerEmail, string personalMerchantNo, InsuranceBillPayOrderInfo model)
        {
            _billId = billId;
            _companyCode = companyCode;
            _returnType = returnType;
            _accountType = accountType;
            _amount = amount;
            _penaltyAmount = penaltyAmount;
            _sellerEmail = sellerEmail;
            _personalMerchantNo = personalMerchantNo;
            _orderInfo = model;
        }
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var request = new Dictionary<string, object>();
            request.Add("billId", _billId);
            request.Add("companyCode", _companyCode);
            request.Add("returnType", _returnType);
            request.Add("accountType", _accountType);
            request.Add("amount", _amount);
            request.Add("penaltyAmount", _penaltyAmount);
            request.Add("sellerEmail", _sellerEmail);
            request.Add("personalMerchantNo", _personalMerchantNo);
            request.Add("order", Newtonsoft.Json.JsonConvert.SerializeObject(_orderInfo));
            return request;
        }


    }
}
