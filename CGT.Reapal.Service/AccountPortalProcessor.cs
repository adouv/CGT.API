using CGT.DDD.Config;
using System;
using System.Collections.Specialized;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 网关支付
    /// </summary>
    public class AccountPortalProcessor : ProcessorBase<SamePortalView>
    {
        protected override string RequestAddress
        {
            get
            {
                //#if DEBUG
                //                return "cashieraccount/accountPortal";
                //#else
                return "cashierAccount/accountPortal";
                //#endif
            }
        }

        protected override string ServiceAddress
        {
            get
            {
                return JsonConfig.JsonRead("ReapalAccountPortalApiUrl", "Reapal");
            }
        }

        private readonly string _order_no;
        private readonly string _platform_order_info;
        private readonly string _total_fee;
        private readonly string _buyer_id;//融宝会员编码
        private readonly string _terminal_info;
        private readonly string _member_id;//采购通UserId
        private readonly string _seller_email;
        private readonly string _notify_url;
        private readonly string _title;
        private readonly string _body;
        private readonly string _protocol_no;//协议号--融宝会员编码
        private readonly string _terminal_type = "mobile";
        private readonly string _pay_method = "bankPay";
        private readonly string _payment_type = "1";
        private readonly string _currency = "156";
        private readonly string _charset = "utf-8";
        private readonly string _return_url = "1";
        private readonly string _service = "online_pay";
        private readonly string _sign_type = "MD5";
        private readonly string _merchant_id = ReapalMerchantId;



        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="order_no"></param>
        /// <param name="platform_order_info"></param>
        /// <param name="total_fee"></param>
        /// <param name="buyer_id"></param>
        /// <param name="terminal_info"></param>
        /// <param name="member_id"></param>
        /// <param name="seller_email"></param>
        /// <param name="notify_url"></param>
        /// = "支付机票金额"
        public AccountPortalProcessor(string order_no, string platform_order_info,
            string total_fee, string terminal_info, string member_id,
            string seller_email, string notify_url, string title, string body,
            string protocol_no, string buyer_id)
        {
            _order_no = order_no;
            _platform_order_info = platform_order_info;
            _total_fee = Convert.ToString(Math.Ceiling(decimal.Parse(total_fee) * 100));

            _buyer_id = buyer_id;
            _terminal_info = terminal_info;
            _member_id = member_id;
            _seller_email = seller_email;
            _notify_url = notify_url;

            _title = title;
            _body = body;
            _protocol_no = protocol_no;
        }

        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("merchant_id", _merchant_id);
            result.Add("order_no", _order_no);
            result.Add("platform_order_info", _platform_order_info);
            result.Add("total_fee", _total_fee);
            result.Add("buyer_id", _buyer_id);
            result.Add("terminal_info", _terminal_info);
            result.Add("member_id", _member_id);
            result.Add("seller_email", _seller_email);
            result.Add("notify_url", _notify_url);
            result.Add("title", _title);
            result.Add("body", _body);
            result.Add("terminal_type", _terminal_type);
            result.Add("pay_method", _pay_method);
            result.Add("payment_type", _payment_type);
            result.Add("currency", _currency);
            result.Add("charset", _charset);
            result.Add("return_url", _return_url);
            result.Add("service", _service);
            result.Add("sign_type", _sign_type);
            result.Add("business_type", "charge");
            result.Add("protocol_no", _protocol_no);
            return result;
        }


    }
}
