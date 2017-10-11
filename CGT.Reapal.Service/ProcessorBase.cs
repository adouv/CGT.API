using CGT.DDD.Config;
using CGT.DDD.Extension;
using CGT.DDD.Logger;
using CGT.DDD.SOA;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 融宝接口基类
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ProcessorBase<TResponse, TResult>
    {
        private const string RequestEncodingName = "UTF-8";
        private const string ParameterEncodingName = "UTF-8";

        public static readonly string ReapalMerchantId = JsonConfig.JsonRead("ReapalMerchantId", "Reapal");

        public Common com { get; set; }
        public ProcessorBase()
        {
            com = new Common();
        }

        /// <summary>
        /// 执行调用api
        /// </summary>
        /// <returns></returns>
        public ExecResult<TResult> Execute(string userkey = "")
        {
            var result = new ExecResult<TResult>();
            var target = string.Empty;
            var request = string.Empty;
            var response = string.Empty;
            DateTime? reqTime = null;
            DateTime? resTime = null;
            try
            {
                target = GetRequestUrl();
                request = (string.IsNullOrWhiteSpace(userkey)) ? PrepareRequest() : PrepareRequest(userkey);
                reqTime = DateTime.Now;
                response = DDD.Utils.Http.HttpRequest.HttpRequestUtility.SendPostRequestCore(target, request, RequestEncodingName, null);
                resTime = DateTime.Now;
                result = ParseResponse(response);
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
                result = new ExecResult<TResult>
                {
                    Success = false,
                    Message = ex.Message
                };
            }

            if (reqTime.HasValue)
            {
                LoggerFactory.Instance.Logger_Info(
                    string.Format("ProcessorBaseInfo --- target:{1}{0}reqTime:{2:yyyy-MM-dd HH:mm:ss.fff}{0}request:{3}{0}resTime:{4:yyyy-MM-dd HH:mm:ss.fff}{0}response:{5}{0}",
                        Environment.NewLine, target, reqTime, request, resTime, response)
                );
            }
            return result;
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ExecResult<TResult> ParseResponse(string response)
        {
            var result = new ExecResult<TResult>();
            var view = JsonConvert.DeserializeObject<ResponseView>(response);

            //解密
            string encryptkey = com.nodeServices.RSAencrypt(view.encryptkey, JsonConfig.JsonRead("ReapalPrivateKeyPfxUrl", "Reapal")).Result;
            var data = com.nodeServices.AESencrypt(view.data, encryptkey).Result;

            LoggerFactory.Instance.Logger_Info(string.Format("response：{0},data:{1}", response, data));

            if (typeof(TResponse) == typeof(TResult))
            {
                var sData = JsonConvert.DeserializeObject<ResponseView>(data);
                if (sData.result_code.Equals("0000"))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = sData.result_msg;
                }
                result.Result = JsonConvert.DeserializeObject<TResult>(data);
            }
            else
            {
                result.Result = ParseResponseCore(JsonConvert.DeserializeObject<TResponse>(data));
            }
            return result;
        }
        /// <summary>
        /// 接口名称
        /// </summary>
        protected abstract string RequestAddress { get; }

        /// <summary>
        /// 接口地址
        /// </summary>
        protected abstract string ServiceAddress { get; }

        /// <summary>
        /// 是否添加时间戳
        /// </summary>
        protected virtual bool RequireTimeStamp
        {
            get { return true; }
        }

        /// <summary>
        /// 请求参数
        /// </summary>
        /// <returns></returns>
        protected abstract NameValueCollection PrepareRequestCore();

        protected virtual TResult ParseResponseCore(TResponse response)
        {
            throw new InvalidOperationException();
        }

        #region 方法
        /// <summary>
        /// 获取请求url
        /// </summary>
        /// <returns></returns>
        private string GetRequestUrl()
        {
            return ServiceAddress + "/" + RequestAddress;
        }


        private string PrepareRequest()
        {
            var parameters = PrepareRequestCore();
            if (RequireTimeStamp)
            {
                parameters.Add("transtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }


            var signText = com.Sign(parameters);
            parameters.Add("sign", signText);


            //排序
            var obj = NameValueCollectionExtension.ConvNameValueCollectionToDictionary(parameters);

            //序列化json
            string json = JsonConvert.SerializeObject(obj);

            LoggerFactory.Instance.Logger_Info(string.Format("ReapalService --- 提交参数：{0}", json));
            //加密业务数据--用AES对称加密算法
            string AESKey = com.nodeServices.GenerateAESKey();
            string strData = com.nodeServices.AESencrypt(json, AESKey).Result;

            //加密AESKey--用RSA非对称加密算法
            string strKey = com.nodeServices.RSAencrypt(AESKey, JsonConfig.JsonRead("ReapalPublicKeyCerUrl", "Reapal")).Result;

            NameValueCollection dic = new NameValueCollection();
            dic.Add("data", strData);
            dic.Add("encryptkey", strKey);
            dic.Add("merchant_id", ReapalMerchantId);

            var encoding = System.Text.Encoding.GetEncoding(ParameterEncodingName);
            return dic.AllKeys.Join("&", item => item + "=" + WebUtility.UrlEncode(dic[item] ?? string.Empty));
        }
        public virtual string PrepareRequest(string userkey = "")
        {
            var parameters = PrepareRequestCore();
            if (RequireTimeStamp)
            {
                parameters.Add("transtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
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

            LoggerFactory.Instance.Logger_Info(string.Format("ReapalService --- 提交参数：{0}", json));
            //加密业务数据--用AES对称加密算法
            string AESKey = com.nodeServices.GenerateAESKey();
            string strData = com.nodeServices.AESencrypt(json, AESKey).Result;

            //加密AESKey--用RSA非对称加密算法
            string strKey = com.nodeServices.RSAencrypt(AESKey, JsonConfig.JsonRead("ReapalPublicKeyCerUrl", "Reapal")).Result;

            NameValueCollection dic = new NameValueCollection();
            dic.Add("data", strData);
            dic.Add("encryptkey", strKey);
            if (!string.IsNullOrWhiteSpace(parameters["merchant_id"].ToString()))
            {
                dic.Add("merchant_id", parameters["merchant_id"].ToString());
            }
            else
            {
                dic.Add("merchant_id", ReapalMerchantId);
            }
            dic.Add("version", "1.0");
            var encoding = System.Text.Encoding.GetEncoding(ParameterEncodingName);
            return dic.AllKeys.Join("&", item => item + "=" + WebUtility.UrlEncode(dic[item] ?? string.Empty));
        }

        #endregion

        #region 返回信息实体
        /// <summary>
        /// 返回实体类
        /// </summary>
        class ResponseView
        {
            public string encryptkey { get; set; }
            public string data { get; set; }
            public string merchant_id { get; set; }
            public string result_code { get; set; }
            public string result_msg { get; set; }
        }
        #endregion
    }
    public abstract class ProcessorBase<T> : ProcessorBase<T, T>
    {
    }
}
