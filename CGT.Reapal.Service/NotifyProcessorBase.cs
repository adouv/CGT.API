using CGT.DDD.Config;
using CGT.DDD.Logger;
using CGT.DDD.SOA;
using Newtonsoft.Json;
using System;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 融宝异步回调
    /// </summary>
    public abstract class NotifyProcessorBase<TResponse, TResult>
    {
        public Common com { get; set; }
        public NotifyProcessorBase()
        {
            com = new Common();
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public ExecResult<TResult> Execute(string data, string encryptkey, string merchant_id)
        {
            var result = new ExecResult<TResult>();

            try
            {
                //用非对称算法解密，得到key值的明文
                string decryKey = com.nodeServices.RSAdecrypt(encryptkey, JsonConfig.JsonRead("ReapalPrivateKeyPfxUrl","Reapal")).Result;

                //用对称算法解密，得到业务数据明文
                string decryData = com.nodeServices.AESencrypt(data, decryKey).Result;

                LoggerFactory.Instance.Logger_Info(string.Format("ReapalNotifyService----融宝支付异步回调解密。参数：encryptkey：{0},data:{1}，merchant_id:{2},decryData:{3}", encryptkey, data, merchant_id, decryData));

                if (typeof(TResponse) == typeof(TResult))
                {
                    result.Success = true;

                    result.Result = JsonConvert.DeserializeObject<TResult>(decryData);
                }
                else
                {
                    result.Result = ParseResponseCore(JsonConvert.DeserializeObject<TResponse>(decryData));
                }
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
            return result;
        }

        protected virtual TResult ParseResponseCore(TResponse response)
        {
            throw new InvalidOperationException();
        }

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

    public abstract class NotifyProcessorBase<T> : NotifyProcessorBase<T, T>
    {
    }
}
