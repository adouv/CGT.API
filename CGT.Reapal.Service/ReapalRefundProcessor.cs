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
    /// 还款充值
    /// </summary>
    public class ReapalRefundProcessor : PayProcessorBase
    {
        /// <summary>
        /// 网关
        /// </summary>
        protected override string Gateway
        {
            get { return JsonConfig.JsonRead("ReapalGateway","Reapal"); }
        }

        /// <summary>
        /// 同步地址
        /// </summary>
        protected override string ReturnAddress
        {
            get { return _reapalReturnAddress; }
        }

        /// <summary>
        /// 异步地址
        /// </summary>
        protected override string NotifyAddress
        {
            get { return _reapalReturnAddress; }
        }

        private readonly string _merchant_id = ReapalMerchantId;
        private readonly string _seller_email;
        private readonly string _transtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        private readonly string _currency = "156";
        private readonly string _charset = "utf-8";
        private readonly string _terminal_type = "mobile";
        private readonly string _terminal_info;
        private readonly string _payment_type = "1";
        private readonly string _default_bank;
        private readonly string _service = "online_pay";

        private readonly string _pay_method = "directPay";
        private readonly string _order_no;
        private readonly string _total_fee;//元为单位
        private readonly string _title;
        private readonly string _body;
        private readonly string _business_type = "charge";
        private readonly string _member_id;
        private readonly string _protocol_no;

        private readonly string _reapalReturnAddress;
        private readonly string _reapalNotifyAddress;
        /// <summary>
        /// 交易类型
        /// </summary>
        private readonly EnumHelper.TradeType _tradetype;

        public ReapalRefundProcessor(string reapalReturnAddress, string reapalNotifyAddress)
        {
            _reapalNotifyAddress = reapalNotifyAddress;
            _reapalReturnAddress = reapalReturnAddress;
        }

        public ReapalRefundProcessor(string seller_email, string terminal_info,
            string default_bank, string order_no, string total_fee, string title,
            string body, string member_id, string protocol_no, EnumHelper.TradeType tradetype, string reapalReturnAddress, string reapalNotifyAddress)
        {
            _seller_email = seller_email;
            _terminal_info = terminal_info;
            _default_bank = default_bank;
            _order_no = order_no;
            _total_fee = total_fee;
            _title = title;
            _body = body;
            _member_id = member_id;
            _protocol_no = protocol_no;
            _tradetype = tradetype;

            _reapalNotifyAddress = reapalNotifyAddress;
            _reapalReturnAddress = reapalReturnAddress;
        }

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
            dic.Add("charset", _charset);
            dic.Add("terminal_type", _terminal_type);
            dic.Add("terminal_info", _terminal_info);
            dic.Add("payment_type", _payment_type);
            dic.Add("default_bank", _default_bank);
            dic.Add("service", _service);
            dic.Add("pay_method", _pay_method);
            dic.Add("order_no", _order_no);
            dic.Add("total_fee", Convert.ToString(Convert.ToInt32(Convert.ToDecimal(_total_fee) * 100)));
            dic.Add("title", _title);
            dic.Add("body", _body);

            //充值
            if (_tradetype == EnumHelper.TradeType.充值)
            {
                dic.Add("member_id", _member_id);
                dic.Add("business_type", _business_type);
                dic.Add("protocol_no", _protocol_no);
            }
            dic.Add("return_url", _reapalReturnAddress);
            dic.Add("notify_url", _reapalNotifyAddress);//服务器通知返回接口

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
            //var get_post = httpMethod == EnumHelper.HttpMethod.Get ? Request.QueryString : Request.Form;

            string datas = Request.Form["data"];
            string encryptkeys = Request.Form["encryptkey"];
            string merchant_ids = Request.Form["merchant_id"];

            //解密 
            string encryptkey =com.nodeServices.RSAencrypt(encryptkeys, JsonConfig.JsonRead("ReapalPrivateKeyPfxUrl", "Reapal")).Result;
            var data =com.nodeServices.AESencrypt(datas, encryptkey).Result;

            LoggerFactory.Instance.Logger_Info(string.Format("ReapalRefundService----data：{0},encryptkey:{1},merchant_id:{2},解密后的参数data:{3}", datas, encryptkeys, merchant_ids, data));
            var samePortal = JsonConvert.DeserializeObject<SamePortalView>(data);

            var model = new PayEventArgs()
            {
                order_no = samePortal.order_no,
                trade_no = samePortal.trade_no,
                total_fee = samePortal.total_fee,
                status = samePortal.status,
                merchant_id = samePortal.merchant_id,
                result_code = samePortal.result_code,
                result_msg = samePortal.result_msg
            };

            var returnModel = new PayEventModel();

            //成功
            if (samePortal.status.ToUpper() == "TRADE_FINISHED")
            {
                OnSuccess(model);

                returnModel.Code = "0000";
                returnModel.IsSuccess = true;
                returnModel.Msg = "成功";
                returnModel.OrderId = model.order_no;
            }
            else
            {
                OnFail(model);

                returnModel.Code = "0001";
                returnModel.IsSuccess = false;
                returnModel.Msg = "失败";
            }

            return returnModel;
        }
    }
}
