using CGT.DDD.Config;
/**************************************************************
*项目名称* CGT.PayCenter.Service
*项目描述*
*类 名 称* FinancingRemoneyProcessor
*命名空间* CGT.PayCenter.Service
*创 建 人* tonglei
*创建时间* 2016/12/15 9:17:24
*创建描述* 
*修 改 人* 
*修改时间* 
*修改描述* 

**************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.PayCenter.Service {
    /// <summary>
    /// 保理返现
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/12/15 9:17:24
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class FinancingRemoneyProcessor : ProcessorBase<FinancingRemoneyView> {

        protected override string RequestAddress {
            get { return "factoring/account/loanAmount"; }
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
        private readonly string _cashBackAmount;
        private readonly FinancingOrder _financingOrder;
        private readonly string _remark;
        private readonly string _ip;
        private readonly string _billDateTime;
        private readonly string _corporateCode;
        private readonly string _billDays;
        private readonly string _accountName;
        private readonly string _companyName;
        private readonly string _firmName;
        /// <summary>
        /// 构造方法
        /// </summary>
        public FinancingRemoneyProcessor(string companyCode, string companyEmail, string companyReapalNo,
            string cashBackCode, string cashBackEmial, string cashBackReapalNo, string cashBackAmount,
            FinancingOrder financingOrder, string remark, string ip, string billDateTime, string corporateCode, string billDays, string accountName, string companyName, string firmName) {
            _companyCode = companyCode;
            _companyEmail = companyEmail;
            _companyReapalNo = companyReapalNo;
            _cashBackCode = cashBackCode;
            _cashBackEmial = cashBackEmial;
            _cashBackReapalNo = cashBackReapalNo;
            _cashBackAmount = cashBackAmount;
            _financingOrder = financingOrder;
            _remark = remark;
            _ip = ip;
            _billDateTime = billDateTime;
            _corporateCode = corporateCode;
            _billDays = billDays;
            _accountName = accountName;
            _companyName = companyName;
            _firmName = firmName;
        }

        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            request.Add("companyCode", _companyCode);
            request.Add("companyEmail", _companyEmail);
            request.Add("companyReapalNo", _companyReapalNo);
            request.Add("cashBackCode", _cashBackCode);
            request.Add("cashBackEmial", _cashBackEmial);
            request.Add("cashBackReapalNo", _cashBackReapalNo);
            request.Add("cashBackAmount", _cashBackAmount);
            request.Add("remark", _remark);
            request.Add("ip", _ip);
            request.Add("billDateTime", _billDateTime);
            request.Add("corporateCode", _corporateCode);
            request.Add("billDays", _billDays);
            request.Add("accountName", _accountName);
            request.Add("companyName", _companyName);
            request.Add("firmName", _firmName);
            request.Add("financingOrder", Newtonsoft.Json.JsonConvert.SerializeObject(_financingOrder));
            return request;
        }
    }
}
