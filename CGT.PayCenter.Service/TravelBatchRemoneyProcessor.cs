using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.PayCenter.Service {
    /// <summary>
    ///差旅批量支付
    /// </summary>
    public class TravelBatchRemoneyProcessor : ProcessorBase<FinancingRemoneyView> {
        protected override string RequestAddress {
            get { return "factoring/account/batchTransferAccount"; }
        }

        protected override string ServiceAddress {
            get { return PayCenterApiUrl; }
        }

        private readonly string _companyCode;//金主（保理）
        private readonly string _companyEmail;
        private readonly string _companyReapalNo;
        private readonly string _cashBackCode;//分销
        private readonly string _cashBackEmial;
        private readonly string _cashBackReapalNo;
        private readonly List<FinancingOrder> _financingOrderList;
        private readonly string _remark;
        private readonly string _ip;
        private readonly string _billDateTime;
        private readonly string _corporateCode;
        private readonly string _billDays;
        private readonly string _accountName;
        private readonly string _companyName;
        private readonly string _firmName;
        private readonly string _batchNum;
        private readonly string _returnUrl;

        /// <summary>
        /// 构造方法
        /// </summary>
        public TravelBatchRemoneyProcessor(string companyCode, string companyEmail, string companyReapalNo,
            string cashBackCode, string cashBackEmial, string cashBackReapalNo,
            List<FinancingOrder> financingOrderList, string remark, string ip, string billDateTime, string corporateCode, string billDays, string accountName, string companyName, string firmName, string batchNum, string returnUrl) {
            _companyCode = companyCode;
            _companyEmail = companyEmail;
            _companyReapalNo = companyReapalNo;
            _cashBackCode = cashBackCode;
            _cashBackEmial = cashBackEmial;
            _cashBackReapalNo = cashBackReapalNo;
            _financingOrderList = financingOrderList;
            _remark = remark;
            _ip = ip;
            _billDateTime = billDateTime;
            _corporateCode = corporateCode;
            _billDays = billDays;
            _accountName = accountName;
            _companyName = companyName;
            _firmName = firmName;
            _batchNum = batchNum;
            _returnUrl = returnUrl;
        }
        /// <summary>
        /// 组织参数
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            request.Add("companyCode", _companyCode);
            request.Add("companyEmail", _companyEmail);
            request.Add("companyReapalNo", _companyReapalNo);
            request.Add("cashBackCode", _cashBackCode);
            request.Add("cashBackEmial", _cashBackEmial);
            request.Add("cashBackReapalNo", _cashBackReapalNo);
            request.Add("remark", _remark);
            request.Add("ip", _ip);
            request.Add("billDateTime", _billDateTime);
            request.Add("corporateCode", _corporateCode);
            request.Add("billDays", _billDays);
            request.Add("accountName", _accountName);
            request.Add("companyName", _companyName);
            request.Add("firmName", _firmName);
            request.Add("batchNum", _batchNum);
            request.Add("returnUrl", _returnUrl);
            request.Add("financingOrder", Newtonsoft.Json.JsonConvert.SerializeObject(_financingOrderList));
            return request;
        }
    }
}
