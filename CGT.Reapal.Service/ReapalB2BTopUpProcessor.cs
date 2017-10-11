using CGT.DDD.Config;
using CGT.DDD.Enums;
using CGT.DDD.Logger;
using CGT.DDD.Utils.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CGT.Reapal.Service
{
    public class ReapalB2BTopUpProcessor : PayProcessorBase
    {
        /// <summary>
        /// 网关
        /// </summary>
        protected override string Gateway
        {
            get { return JsonConfig.JsonRead("ReapalB2BGateway","Reapal"); }
        }

        /// <summary>
        /// 同步地址
        /// </summary>
        protected override string ReturnAddress
        {
            get { return JsonConfig.JsonRead("ReapalB2BReturnAddress","Reapal"); }
        }

        /// <summary>
        /// 异步地址
        /// </summary>
        protected override string NotifyAddress
        {
            get { return JsonConfig.JsonRead("ReapalB2BNotifyAddress","Reapal"); }
        }
        private readonly string _merchant_id;
        private readonly string _seller_email;
        private readonly string _transtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        private readonly string _currency = "156";
        private readonly string _terminal_type = "mobile";
        private readonly string _terminal_info = "aaa";
        private readonly string _payment_type = "1";
        private readonly string _default_bank;

        private readonly string _pay_method = "directPay";
        private readonly string _order_no;
        private readonly string _total_fee;//元为单位
        private readonly string _title;
        private readonly string _body;
        public ReapalB2BTopUpProcessor()
        {

        }

        public ReapalB2BTopUpProcessor(string merchant_id, string seller_email,
           string default_bank, string order_no, string total_fee, string title,
           string body)
        {
            _merchant_id = merchant_id;
            _seller_email = seller_email;
            _default_bank = default_bank;
            _order_no = order_no;
            _total_fee = total_fee;
            _title = title;
            _body = body;
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
            dic.Add("terminal_type", _terminal_type);
            dic.Add("terminal_info", _terminal_info);
            dic.Add("payment_type", _payment_type);
            dic.Add("default_bank", _default_bank);
            dic.Add("member_id", _merchant_id);
            dic.Add("member_ip", "127.0.0.1");
            dic.Add("pay_method", _pay_method);
            dic.Add("order_no", _order_no);
            dic.Add("total_fee", Convert.ToString(Convert.ToInt32(Convert.ToDecimal(_total_fee) * 100)));
            dic.Add("title", _title);
            dic.Add("body", _body);
            dic.Add("return_url", ReturnAddress);
            dic.Add("notify_url", NotifyAddress);//服务器通知返回接口

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
            string encryptkey = com.nodeServices.RSAencrypt(encryptkeys, JsonConfig.JsonRead("ReapalPrivateKeyPfxUrl", "Reapal")).Result;
            var data = com.nodeServices.AESencrypt(datas, encryptkey).Result;

            LoggerFactory.Instance.Logger_Info(string.Format("ReapalB2BTopUpService-----data：{0},encryptkey:{1},merchant_id:{2},解密后的参数data:{3}", datas, encryptkeys, merchant_ids, data));
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
