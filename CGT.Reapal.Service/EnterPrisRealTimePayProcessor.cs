/**************************************************************
*项目名称* CGT.Reapal.Service
*项目描述*
*类 名 称* EnterPrisRealTimePayProcessor
*命名空间* CGT.Reapal.Service
*创 建 人* tonglei
*创建时间* 2017/1/3 17:19:38
*创建描述* 
*修 改 人* 
*修改时间* 
*修改描述* 

**************************************************************/
using CGT.DDD.Config;
using System;
using System.Collections.Specialized;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 实时到账企业转账解决
    /// </summary>
    /// <remarks>
    /// 创 建 人 :  
    /// 创建时间 : 2017/1/3 17:19:38
    /// 修 改 人 :
    /// 修改时间 :
    /// 修改描述 :
    /// </remarks>
    public class EnterPrisRealTimePayProcessor : ProcessorBase<EnterprisePayView>
    {

        protected override string RequestAddress
        {
#if DEBUG
            get { return "cashierAccount/accountPortal"; }
#else
            get{ return "account/portal";}
#endif
        }

        protected override string ServiceAddress
        {
            get
            {
                return JsonConfig.JsonRead("ReapalEnterRealTimeApiUrl","Reapal");
            }
        }



        private readonly string _merchant_id = ReapalMerchantId;//付款账户
        private readonly string _from_merchant_id;//付款账户
        private readonly string _to_merchant_id;//收款账户
        private readonly string _order_no; //订单号
        private readonly string _total_fee;//支付金额
        private readonly string _member_ip;//ip
        private readonly string _terminal_info = "fc:64:ba:5c:b8:ef";//mac地址
        private readonly string _notify_url;//异步地址
        private readonly string _title;// 商品名称
        private readonly string _body;//商品描述
        private readonly string _terminal_type = "web";//terminal_type
        private readonly string _currency = "156";
        private readonly string _return_url;//同步回调地址
        private readonly string _seller_email;//商户邮箱
        private readonly string _protocol_no;//
        /// 构造方法
        /// </summary>
        /// <param name="member_ip"></param>
        /// <param name="member_no"></param>
        public EnterPrisRealTimePayProcessor(string from_merchant_id, string to_merchant_id, string order_no, string total_fee, string member_ip, string notify_url, string title, string body, string return_url, string seller_email)
        {

            _from_merchant_id = from_merchant_id;
            _to_merchant_id = to_merchant_id;
            _order_no = order_no;
            _total_fee = Convert.ToString(Math.Ceiling(decimal.Parse(total_fee) * 100));
            _member_ip = member_ip;
            _notify_url = notify_url;
            _title = title;
            _body = body;
            _return_url = return_url;
            _seller_email = seller_email;
            _protocol_no = from_merchant_id;
        }
        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("from_merchant_id", _from_merchant_id);
            result.Add("to_merchant_id", _to_merchant_id);
            result.Add("order_no", _order_no);
            result.Add("total_fee", _total_fee);
            result.Add("notify_url", _notify_url);
            result.Add("member_ip", _member_ip);
            result.Add("title", _title);
            result.Add("body", _body);
            result.Add("return_url", _return_url);
            result.Add("seller_email", _seller_email);
            result.Add("terminal_info", _terminal_info);
            result.Add("terminal_type", _terminal_type);
            result.Add("currency", _currency);
            result.Add("merchant_id", _merchant_id);
            result.Add("protocol_no", _protocol_no);
            return result;
        }

    }
}
