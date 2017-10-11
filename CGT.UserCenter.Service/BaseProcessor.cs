using System;
using System.Collections.Generic;
using CGT.DDD.SOA;
using CGT.DDD.Utils.Http;
using Newtonsoft.Json;
using CGT.DDD.Config;
using CGT.DDD.Logger;

namespace CGT.UserCenter.Service
{
    public abstract class BaseProcessor<TResult>
    {

        public static readonly string UserCenterApiUrl = JsonConfig.JsonRead("UserCenterApiUrl", "UserCenter");
        public Common com { get; set; }


        public BaseProcessor()
        {
            com = new Common();
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
        /// 请求参数
        /// </summary>
        /// <returns></returns>
        protected abstract Dictionary<string, object> PrepareRequestCore();

        /// <summary>
        /// 执行调用api
        /// </summary>
        /// <returns></returns>
        /// 
        public ExecResult<TResult> Execute()
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
                request = PrepareRequest();
                reqTime = DateTime.Now;
               response = com.Post(target, request);
               // response = HttpRequest.HttpRequestUtility.SendPostRequestCore(target, request, RequestEncodingName, 60000);
                resTime = DateTime.Now;
              
                result = ParseCenterResponse(response);
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
                   string.Format("UserCenterService----target:{1}{0}reqTime:{2:yyyy-MM-dd HH:mm:ss.fff}{0}request:{3}{0}resTime:{4:yyyy-MM-dd HH:mm:ss.fff}{0}response:{5}{0}",
                       Environment.NewLine, target, reqTime, request, resTime, response));
            }
            return result;
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ExecResult<TResult> ParseCenterResponse(string response)
        {
            var result = new ExecResult<TResult>();
            if (!string.IsNullOrWhiteSpace(response))
            {
                var view = JsonConvert.DeserializeObject<ViewBase<TResult>>(response);
                if (view.errorCode == "0000")
                {
                    result.Success = true;
                    result.Message = view.message;
                    result.MsgCode = view.errorCode;
                    result.Result = view.data;
                }
                else
                {
                    result.Success = false;
                    result.Message = view.message;
                    result.MsgCode = view.errorCode;
                }
            }
            else
            {
                result.Success = false;
                result.Message = "请求错误";
                result.MsgCode = "0001";
            }
            return result;
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
            var param = new
            {
                sign = com.Sign(parameters),
                data=parameters
            };
            return JsonConvert.SerializeObject(param);
        }
        #endregion
    }
}
