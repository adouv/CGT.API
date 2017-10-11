using CGT.DDD.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGT.UserCenter.Service
{
    /// <summary>
    /// 商户状态修改
    /// </summary>
    public class MerchantsStatusModifyProcessor : BaseProcessor<MerchantsModifyResult>
    {
        protected override string RequestAddress
        {
            get { return "pay-merchant-web/manage/merchant/enabledMerchant"; }
        }

        protected override string ServiceAddress
        {
             get { return UserCenterApiUrl; }
        }
        private int _status;
        private string _companyCode;

        public MerchantsStatusModifyProcessor()
        {
        }
        /// <summary>
        /// 商户状态修改初始化
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="companyCode">商户code</param>
   
        /// </param>
        public void InitData( int status, string companyCode)
        {
            _status = status;
            _companyCode = companyCode;
        }
        protected override Dictionary<string, object> PrepareRequestCore()
        {
            var result = new Dictionary<string, object>();
            result.Add("status", _status);
            result.Add("companyCode", _companyCode);
            return result;
        }
    }
}
