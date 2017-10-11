using CGT.DDD.Config;
using System;
using System.Collections.Specialized;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 企业版转账
    /// </summary>
    public class EnterPrisPayProcessor : ProcessorBase<EnterprisePayView>
    {
        protected override string RequestAddress
        {
            get { return "account/transferaccount"; }
        }

        protected override string ServiceAddress
        {
            get
            {
                return JsonConfig.JsonRead("ReapalEnterApiUrl","Reapal");
            }
        }


        private readonly string _merchant_id = ReapalMerchantId;
        private readonly string _from_merchantid;
        private readonly string _to_merchantid;
        private readonly string _trans_no;
        private readonly string _amount;
        private readonly string _trans_reason;
        private readonly string _member_ip;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="member_ip"></param>
        /// <param name="member_no"></param>
        public EnterPrisPayProcessor(string from_merchantid, string to_merchantid, string trans_no, string amount, string trans_reason, string member_ip)
        {

            _from_merchantid = from_merchantid;
            _to_merchantid = to_merchantid;
            _trans_no = trans_no;
            _amount = Convert.ToString(Math.Ceiling(decimal.Parse(amount) * 100));
            _trans_reason = trans_reason;
            _member_ip = member_ip;
        }
        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("from_merchantid", _from_merchantid);
            result.Add("to_merchantid", _to_merchantid);
            result.Add("trans_no", _trans_no);
            result.Add("amount", _amount);
            result.Add("trans_reason", _trans_reason);
            result.Add("member_ip", _member_ip);
            result.Add("merchant_id", _merchant_id);
            return result;
        }
    }
}
