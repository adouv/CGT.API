using CGT.DDD.Config;
using CGT.DDD.Extension;
using CGT.DDD.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace CGT.Reapal.Service
{
    public class PaidPayProcessor : ProcessorBase<PaidPayView>
    {
        public Common com { get; set; }

        public PaidPayProcessor()
        {
            com = new Common();
        }

        private const string ParameterEncodingName = "UTF-8";
        protected override string RequestAddress
        {
            get { return "agentpay/pay"; }

        }

        protected override string ServiceAddress
        { 
            get { return JsonConfig.JsonRead("ReapalPaidPayUrl","Reapal"); }
        }
        private readonly string _merchant_id;//付款账号
        private readonly string _batch_amount;//付款金额
        private readonly string _batch_count = "1";//提现条数
        private readonly string _batch_no;//提现批次号
        private readonly string _content; //订单号
        private readonly string _notify_url;//异步地址
        private readonly string _pay_type = "1";//记账方式 
        private readonly string _charset = "utf-8";

        /// 构造方法
        /// </summary>
        /// <param name="member_ip"></param>
        /// <param name="member_no"></param>
        public PaidPayProcessor(string merchant_id, string batch_amount, string batch_no, string content, string notify_url)
        {
            _merchant_id = merchant_id;
            _batch_amount = batch_amount;
            _batch_no = batch_no;
            _content = content;
            _notify_url = notify_url;
        }
        protected override NameValueCollection PrepareRequestCore()
        {
            var result = new NameValueCollection();
            result.Add("merchant_id", _merchant_id);
            result.Add("batch_amount", _batch_amount);
            result.Add("batch_count", _batch_count);
            result.Add("batch_no", _batch_no);
            result.Add("content", _content);
            result.Add("notify_url", _notify_url);
            result.Add("pay_type", _pay_type);
            result.Add("charset", _charset);
            return result;
        }
        public override string PrepareRequest(string userkey = "")
        {
            var parameters = PrepareRequestCore();
            var merchant_id = parameters["merchant_id"].ToString();
            if (RequireTimeStamp)
            {
                parameters.Add("transtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            parameters.Remove("merchant_id");
            var p = parameters.AllKeys.Join("&", item => item + "=" + parameters[item]);
            string signText = string.Empty;
            if (!string.IsNullOrWhiteSpace(userkey))
            {

                signText = com.Sign(parameters, userkey);
            }
            else
            {
                signText = com.Sign(parameters);
            }

            parameters.Add("sign", signText);


            //排序
            var obj = NameValueCollectionExtension.ConvNameValueCollectionToDictionary(parameters);

            //序列化json
            string json = JsonConvert.SerializeObject(obj);

            LoggerFactory.Instance.Logger_Info(string.Format("ReapalService----提交参数：{0}", json));
            //加密业务数据--用AES对称加密算法
            string AESKey = com.nodeServices.GenerateAESKey();
            string strData = com.nodeServices.AESencrypt(json, AESKey).Result;

            //加密AESKey--用RSA非对称加密算法
            string strKey = com.nodeServices.RSAencrypt(AESKey, JsonConfig.JsonRead("ReapalPublicKeyCerUrl","Reapal")).Result;

            NameValueCollection dic = new NameValueCollection();
            dic.Add("data", strData);
            dic.Add("encryptkey", strKey);
            if (!string.IsNullOrWhiteSpace(merchant_id))
            {
                dic.Add("merchant_id", merchant_id);
            }
            else
            {
                dic.Add("merchant_id", ReapalMerchantId);
            }
            dic.Add("version", "1.0");
            var encoding = Encoding.GetEncoding(ParameterEncodingName);
            return dic.AllKeys.Join("&", item => item + "=" + WebUtility.UrlEncode(dic[item] ?? string.Empty));
        }
    }
}
