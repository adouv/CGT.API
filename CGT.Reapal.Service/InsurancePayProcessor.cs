using CGT.DDD.Config;
using CGT.DDD.Enums;
using CGT.DDD.Logger;
using CGT.DDD.Utils.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CGT.Reapal.Service
{


    /// <summary>
    /// 保险支付
    /// </summary>
    public class InsurancePayProcessor : PayProcessorBase
    {
        /// <summary>
        /// 网关
        /// </summary>
        protected override string Gateway
        {
            get { return JsonConfig.JsonRead("ReapalH5Gateway","Reapal"); }
        }
        /// <summary>
        /// 同步地址
        /// </summary>
        protected override string ReturnAddress
        {
            get { return JsonConfig.JsonRead("ReapalH5ReturnAddress","Reapal"); }
        }
        /// <summary>
        /// 异步地址
        /// </summary>
        protected override string NotifyAddress
        {
            get { return JsonConfig.JsonRead("ReapalH5NotifyAddress","Reapal"); }
        }
        //XmlConfig.ReapalMerchantId
        #region 构造函数
        // private readonly string _merchant_id = "100000000071531";  //商户Id
        private readonly string _merchant_id;  //商户Id

        private readonly string _seller_email;                        //商户邮箱
        private readonly string _transtime = DateTime.Now.ToString("yyyyMMddHHmmss");      //
        private readonly string _currency = "156";                                         //币种  156人民币
        private readonly string _terminal_type = "mobile";                                 //终端类型
        private readonly string _terminal_info;//终端设备信息 手机IMEI地址、MAC地址、UUID 
        private readonly string _member_ip; //用户Ip地址
        private readonly string _payment_type = "2";
        private readonly string _pay_method = "bankPay";
        private readonly string _notify_url = JsonConfig.JsonRead("ReapalH5NotifyAddress","Reapal");//服务器通知返回接口
        private readonly string _return_url = JsonConfig.JsonRead("ReapalH5ReturnAddress","Reapal");
        private readonly string _order_no;    //请与贵网站订单系统中的唯一订单号匹配
        private readonly string _total_fee;  //金额转换为分//订单总金额，显示在融宝支付收银台里的“应付总额”里  
        private readonly string _title;//订单名称，显示在融宝支付收银台里的“商品名称”里，显示在融宝支付的交易管理的“商品名称的列表里。 
        private readonly string _body;//订单描述、订单详细、订单备注，显示在融宝支付收银台里的“商品描述”里
        private readonly string _member_id; //商户会员编号
        public InsurancePayProcessor()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="seller_email">商户</param>
        /// <param name="terminal_info">终端设备信息 手机IMEI地址、MAC地址、UUID </param>
        /// <param name="member_ip">/用户Ip地址</param>
        /// <param name="payment_type"></param>
        /// <param name="pay_method"></param>
        /// <param name="order_no">/请与贵网站订单系统中的唯一订单号匹配</param>
        /// <param name="total_fee">金额转换为分//订单总金额，显示在融宝支付收银台里的“应付总额”里  </param>
        /// <param name="title">订单名称</param>
        /// <param name="body">订单描述</param>
        /// <param name="member_id">会员id</param>
        public InsurancePayProcessor(string merchantCode, string seller_email, string terminal_info, string member_ip,
            string order_no, string total_fee, string title, string body, string member_id)
        {
            _merchant_id = merchantCode;
            _seller_email = seller_email;
            _terminal_info = terminal_info;
            _member_ip = member_ip;
            _order_no = order_no;
            _total_fee = total_fee;
            _title = title;
            _body = body;
            _member_id = member_id;
        }
        #endregion

        /// <summary>
        /// 组装数据
        /// </summary>
        /// <returns></returns>
        protected override SortedDictionary<string, string> PrepareRequestCore()
        {
            var dic = new SortedDictionary<string, string>();
            dic.Add("merchant_id", _merchant_id);
            dic.Add("seller_email", _seller_email);
            dic.Add("transtime", _transtime);
            dic.Add("currency", _currency);
            dic.Add("terminal_type", _terminal_type);
            dic.Add("terminal_info", _terminal_info);
            dic.Add("payment_type", _payment_type);
            dic.Add("pay_method", _pay_method);
            dic.Add("notify_url", _notify_url);
            dic.Add("return_url", _return_url);
            dic.Add("order_no", _order_no);
            dic.Add("member_ip", _member_ip);
            dic.Add("total_fee", Convert.ToString(Math.Ceiling(Convert.ToDouble(_total_fee) * 100)));
            dic.Add("title", _title);
            dic.Add("body", _body);
            dic.Add("member_id", _member_id);
            dic.Add("version", "4.0.1");
            return dic;
        }

        /// <summary>
        /// 回调处理
        /// </summary>
        /// <param name="httpMethod"></param>
        public override PayEventModel Notify_Return(EnumHelper.HttpMethod httpMethod)
        {
            var Request = CGTHttpContext.Current.Request;
            var Response = CGTHttpContext.Current.Response;
            //var get_post = httpMethod == EnumHelper.HttpMethod.Get ? Request.QueryString.Value : Request.Form[""].ToString();
            string datas = Request.Form["data"].ToString();
            string encryptkeys = Request.Form["encryptkey"].ToString();
            string merchant_ids = Request.Form["merchant_id"].ToString();
            //解密 
            string encryptkey = com.nodeServices.RSAencrypt(encryptkeys, JsonConfig.JsonRead("ReapalPrivateKeyPfxUrl","Reapal")).Result;
            var data = com.nodeServices.AESencrypt(datas, encryptkey).Result;

            LoggerFactory.Instance.Logger_Info(string.Format("InsurancePayProcessor -----data：{0},encryptkey:{1},merchant_id:{2},解密后的参数data:{3}", datas, encryptkeys, merchant_ids, data));

            var samePortal = JsonConvert.DeserializeObject<SamePortalView>(data);
            var model = new PayEventArgs()
            {
                order_no = samePortal.order_no,
                trade_no = samePortal.trade_no,
                total_fee = samePortal.total_fee,
                status = samePortal.status,
                merchant_id = samePortal.merchant_id,
                result_code = samePortal.result_code,
                result_msg = samePortal.result_msg,
                fee_amount = string.IsNullOrEmpty(samePortal.fee_amount) ? "0" : samePortal.fee_amount
            };
            var returnModel = new PayEventModel();
            if (samePortal.status.ToUpper() == "TRADE_FINISHED")
            {
                OnSuccess(model);
                returnModel.Code = "0000";
                returnModel.IsSuccess = true;
                returnModel.Msg = "成功";
                returnModel.OrderId = model.order_no;
                returnModel.FeeAmount = model.fee_amount;
            }
            else
            {
                OnFail(model);
                returnModel.Code = "1001";
                returnModel.IsSuccess = true;
                returnModel.Msg = "失败";
            }
            return returnModel;
        }
    }
}
