using CGT.DDD.Config;
/**************************************************************
*项目名称* CGT.PayCenter.Service
*项目描述*
*类 名 称* FinancingRepaymentProcessor
*命名空间* CGT.PayCenter.Service
*创 建 人* tonglei
*创建时间* 2016/12/15 10:24:22
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
    /// 保理还款
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/12/15 10:24:22
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class FinancingRepaymentProcessor : ProcessorBase<FinancingRepaymentView> {

        protected override string RequestAddress {
            get { return "factoring/account/returnAmount"; }
        }

        protected override string ServiceAddress {
            get { return PayCenterApiUrl; }
        }

        private readonly string _refundCode;//分销子code
        private readonly string _refundEmail;//分销账号
        private readonly string _refundReapalNo;//分销商户号
        private readonly string _billAmount;//账单金额
        private readonly string _refundAmount;//还款金额
        private readonly string _billDateTime;//账期时间
        private readonly string _ticketSumNo;//票量
        private readonly string _refundType;//还款类型（0全额，1部分）
        private readonly string _remark;//备注
        private readonly string _ip;
        private readonly string _billId;//账单号
        private readonly string _corporateCode;//企业编号
        private readonly string _billDays;//账期
        private readonly string _startTime;
        private readonly string _endTime;
        /// <summary>
        /// 构造方法
        /// </summary>
        public FinancingRepaymentProcessor(string refundCode, string refundEmail, string refundReapalNo,
            string billAmount, string refundAmount, string billDateTime, string ticketSumNo, string refundType,
            string remark, string ip, string billId, string corporateCode, string billDays, string startTime="",string endTime="") {
            _refundCode = refundCode;
            _refundEmail = refundEmail;
            _refundReapalNo = refundReapalNo;
            _billAmount = billAmount;
            _refundAmount = refundAmount;
            _billDateTime = billDateTime;
            _ticketSumNo = ticketSumNo;
            _refundType = refundType;
            _remark = remark;
            _ip = ip;
            _billId = billId;
            _corporateCode = corporateCode;
            _billDays = billDays;
            _startTime = startTime;
            _endTime = endTime;
        }

        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(_startTime))
            {
                request.Add("startTime", _startTime);
            }
            if (!string.IsNullOrEmpty(_endTime))
            {
                request.Add("endTime", _endTime);
            }
            request.Add("refundCode", _refundCode);
            request.Add("refundEmail", _refundEmail);
            request.Add("refundReapalNo", _refundReapalNo);
            request.Add("billAmount", _billAmount);
            request.Add("refundAmount", _refundAmount);
            request.Add("billDateTime", _billDateTime);
            request.Add("ticketSumNo", _ticketSumNo);
            request.Add("refundType", _refundType);
            request.Add("remark", _remark);
            request.Add("ip", _ip);
            request.Add("billId", _billId);
            request.Add("corporateCode", _corporateCode);
            request.Add("billDays", _billDays);
            return request;
        }
    }
}
