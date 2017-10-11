using CGT.DDD.Config;
/**************************************************************
*项目名称* CGT.PayCenter.Service
*项目描述*
*类 名 称* 
*命名空间* CGT.PayCenter.Service
*创 建 人* tonglei
*创建时间* 2016/11/23 16:53:02
*创建描述* 
*修 改 人* 
*修改时间* 
*修改描述* 

**************************************************************/
using System;
using System.Collections.Generic;

namespace CGT.PayCenter.Service {
    /// <summary>
    /// 充值查询
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2016/11/23 16:53:02
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class RechargeInquiryProcessor : ProcessorBase<RechargeInquiryView> {

        private readonly string _accountCode;
        private readonly string _amount;
        private readonly string _companyCode;
        private readonly string _bankNum;
        private readonly string _bankType;

        private readonly string _type;
        private readonly string _remark;
        private readonly string _loginName;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountCode">用户Code</param>
        /// <param name="amount">金额</param>
        /// <param name="bankNum">银行卡号</param>
        /// <param name="companyCode"></param>
        /// <param name="bankType">金主类型1融宝  2廊坊银行</param>
        /// <param name="type">充值类型0普通 1兜底 3利息</param>
        /// <param name="remark"></param>
        /// <param name="loginName">登录名</param>
        public RechargeInquiryProcessor(string accountCode, string amount, string bankNum, string companyCode, string bankType, string type,
            string remark, string loginName)
        {
            _accountCode = accountCode;
            _amount = amount;
            _companyCode = companyCode;
            _bankNum = bankNum;
            _bankType = bankType;
            _type = type;
            _remark = remark;
            _loginName = loginName;

        }
        protected override bool IsBase {
            get {
                return false;
            }
        }

        protected override string RequestAddress {
            get { return "funds/saveReveal"; }
        }

        protected override string ServiceAddress {
            get { return PayCenterBossApiUrl; }
        }


        protected override Dictionary<string, object> PrepareRequestCore() {
            var request = new Dictionary<string, object>();
            request.Add("accountCode", _accountCode);
            request.Add("amount", _amount);
            request.Add("bankNum", _bankNum);
            request.Add("bankType", _bankType);
            request.Add("companyCode", _companyCode);
            request.Add("type", _type);
            request.Add("remark", _remark);
            request.Add("loginName", _loginName);

            return request;
        }
    }
}
