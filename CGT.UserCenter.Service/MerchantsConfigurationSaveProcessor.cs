using System;
using System.Collections.Generic;

namespace CGT.UserCenter.Service
{
    /// <summary>
    /// 商户配置修改
    /// </summary>
    public class MerchantsConfigurationSaveProcessor : BaseProcessor<MerchantsConfigurationSaveResult>
    {
        protected override string RequestAddress => "pay-merchant-web/manage/companyConfiguration/savaCompanyConfiguration";

        protected override string ServiceAddress => UserCenterApiUrl;

        private string _loginName;
        private string _companyCode;
        private string _accountCode;
        private int _accountType;
        private int _accountBusiType;
        private int _busiType;
        private string _creditAmount;
        private int _billDays;
        private string _totalCreditAmount;
        private string _travelRate;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="loginName">当前登录名称</param>
        /// <param name="companyCode">商户号</param>
        /// <param name="accountCode">金主号</param>
        /// <param name="accountType">金主账户类型</param>
        /// <param name="accountBusiType">金主业务类型</param>
        /// <param name="busiType">商户业务类型</param>
        /// <param name="creditAmount">授信额度</param>
        /// <param name="billDays">账期</param>
        /// <param name="totalCreditAmount">分销商总限额</param>
        /// <param name="travelRate">差旅费率</param>
        public void Init(string loginName, string companyCode, string accountCode, int accountType, int accountBusiType, int busiType, string creditAmount, int billDays, string totalCreditAmount, string travelRate)
        {
            _loginName = loginName;
            _companyCode = companyCode;
            _accountCode = accountCode;
            _accountType = accountType;
            _accountBusiType = accountBusiType;
            _busiType = busiType;
            _creditAmount = creditAmount;
            _billDays = billDays;
            _totalCreditAmount = totalCreditAmount;
            _travelRate = travelRate;
        }
        /// <summary>
        /// 请求参数赋值
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var dicResult = new Dictionary<string, object>();
            dicResult.Add("loginName", _loginName);
            dicResult.Add("companyCode", _companyCode);
            dicResult.Add("accountCode", _accountCode);
            dicResult.Add("accountType", _accountType);
            dicResult.Add("accountBusiType", _accountBusiType);
            dicResult.Add("busiType", _busiType);
            dicResult.Add("creditAmount", _creditAmount);
            dicResult.Add("billDays", _billDays);
            dicResult.Add("totalCreditAmount", _totalCreditAmount);
            dicResult.Add("travelRate", _travelRate);
            return dicResult;
        }
    }
}
