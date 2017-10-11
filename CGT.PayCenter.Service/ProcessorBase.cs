using System;
using System.Collections.Generic;
using CGT.DDD.SOA;
using CGT.DDD.Utils.Http;
using Newtonsoft.Json;
using CGT.DDD.Config;
using CGT.DDD.Logger;

namespace CGT.PayCenter.Service
{
    public abstract class ProcessorBase<TResponse, TResult>
    {
        private const string RequestEncodingName = "UTF-8";
        private const string ParameterEncodingName = "UTF-8";

        public static readonly string PayCenterApiUrl = JsonConfig.JsonRead("PayCenterApiUrl", "PayCenter");
        public static readonly string PayCenterBossApiUrl = JsonConfig.JsonRead("PayCenterBossApiUrl", "PayCenter");

        public Common com { get; set; }


        public ProcessorBase()
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
        /// 
        /// </summary>
        protected virtual bool IsBase
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 请求参数
        /// </summary>
        /// <returns></returns>
        protected abstract Dictionary<string, object> PrepareRequestCore();
        protected virtual TResult ParseResponseCore(TResponse response)
        {
            throw new InvalidOperationException();
        }
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
                response = HttpRequest.HttpRequestUtility.SendPostRequestCore(target, request, RequestEncodingName, 60000);
                resTime = DateTime.Now;


                if (IsBase)
                {
                    result = ParseResponse(response);
                }
                else
                {
                    result = ParseCenterResponse(response);
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

            if (reqTime.HasValue)
            {
                LoggerFactory.Instance.Logger_Info(
                    string.Format("PayCenterService----target:{1}{0}reqTime:{2:yyyy-MM-dd HH:mm:ss.fff}{0}request:{3}{0}resTime:{4:yyyy-MM-dd HH:mm:ss.fff}{0}response:{5}{0}",
                        Environment.NewLine, target, reqTime, request, resTime, response));
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
            var view = JsonConvert.DeserializeObject<ViewBase>(response);

            if (typeof(TResponse) == typeof(TResult))
            {
                if (view.success)
                {
                    result.Success = true;
                    result.Message = view.result_msg;
                    result.MsgCode = view.result_code;
                    result.Result = JsonConvert.DeserializeObject<TResult>(view.data);
                }
                else
                {
                    result.Success = false;
                    result.Message = view.result_msg;
                }

            }
            else
            {
                result.Result = ParseResponseCore(JsonConvert.DeserializeObject<TResponse>(view.data));
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
            var view = JsonConvert.DeserializeObject<TResult>(response);
            if (view != null)
            {
                result.MsgCode = "0000";
                result.Message = "成功";
                result.Success = true;
                result.Result = view;
            }
            else
            {
                result.MsgCode = "0001";
                result.Message = "失败";
                result.Success = false;
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
            if (IsBase)
            {
                parameters.Add("transTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                var signText = com.Sign(parameters);
                parameters.Add("sign", signText);
            }
            string data = JsonConvert.SerializeObject(parameters);

            return "requestJson=" + data;
        }
        #endregion
    }
    public abstract class ProcessorBase<T> : ProcessorBase<T, T>
    {
    }
}
