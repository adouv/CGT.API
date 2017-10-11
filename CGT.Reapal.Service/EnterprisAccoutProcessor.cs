using CGT.DDD.Config;
using System.Collections.Specialized;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 商户余额查询
    /// </summary>
    public class EnterprisAccoutProcessor : ProcessorBase<MemberAccountView>
    {

        protected override string RequestAddress
        {
            get { return "merchant/qrymerchantbalance"; }
        }

        protected override string ServiceAddress
        {
            get
            {
                return JsonConfig.JsonRead("ReapalEnterApiUrl","Reapal");
            }
        }


        private readonly string _qry_merchant_id;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="member_ip"></param>
        /// <param name="member_no"></param>
        public EnterprisAccoutProcessor(string qry_merchant_id)
        {
            _qry_merchant_id = qry_merchant_id;

        }
        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("qry_merchant_id", _qry_merchant_id);
            return result;
        }
    }
}
