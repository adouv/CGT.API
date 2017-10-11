using CGT.Api.DTO;
using CGT.DDD.Config;
using CGT.DDD.Logger;
using CGT.DDD.SOA;
using CGT.DDD.Utils.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CGT.Travel.Test.Service
{
    /// <summary>
    /// 接口基类
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class BaseProcessor<TResponse, TResult>
    {
        //设置请求参数数据格式
        private const string RequestEncodingName = "UTF-8";
        private const string ParameterEncodingName = "UTF-8";

        public BaseProcessor()
        {
            
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
        /// 执行调用api(导入)
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
                response = HttpRequest.HttpRequestUtility.SendPostRequestjson(target, request, RequestEncodingName, 360000, null, null, "application/json");
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
                LoggerFactory.Instance.Logger_Error(ex, "CGT.Travel.Test.Service");
                result = new ExecResult<TResult>
                {
                    Success = false,
                    Message = ex.Message
                };
            }

            if (reqTime.HasValue)
            {
                LoggerFactory.Instance.Logger_Debug(
                    string.Format("CGT.Travel.Test.Service----target:{1}{0}reqTime:{2:yyyy-MM-dd HH:mm:ss.fff}{0}request:{3}{0}resTime:{4:yyyy-MM-dd HH:mm:ss.fff}{0}response:{5}{0}",
                        Environment.NewLine, target, reqTime, request, resTime, response), "CGT.Travel.Test.Service");

            }
            return result;
        }
        /// <summary>
        /// 返回信息（导入）
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ExecResult<TResult> ParseResponse(string response)
        {
            var result = new ExecResult<TResult>();
            var view = JsonConvert.DeserializeObject<ResponseMessageModel>(response);

            if (typeof(TResponse) == typeof(TResult))
            {
                if (view.IsSuccess)
                {
                    result.Success = true;
                    result.Message = view.Message;
                    result.MsgCode = view.ErrorCode;
                }
                else
                {
                    result.Success = false;
                    result.Message = view.Message;
                }

            }
            else
            {
                // result.Result = ParseResponseCore(JsonConvert.DeserializeObject<TResponse>(view));
            }
            return result;
        }
        /// <summary>
        /// 返回信息（导入）
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ExecResult<TResult> ParseCenterResponse(string response)
        {
            var result = new ExecResult<TResult>();
            var view = JsonConvert.DeserializeObject<ViewBase>(response);
            if (view != null)
            {
                result.MsgCode = "0000";
                result.Message = "成功";
                result.Success = true;

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
            string data = JsonConvert.SerializeObject(parameters);

            return data;
        }

        /// <summary>
        /// Post提交
        /// </summary>
        /// <param name="requestURL">请求地址</param>
        /// <param name="requestData">请求数据</param>
        /// <returns></returns>
        public static string apiPost(string requestURL, string requestData)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(requestData);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(requestURL));
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.Timeout = 300000;
            System.IO.Stream newStream = webRequest.GetRequestStreamAsync().Result;
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Dispose();
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)webRequest.GetResponseAsync().Result;
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            var data = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            response.Close();
            return data;
        }

        #endregion
    }
    public abstract class BaseProcessor<T> : BaseProcessor<T, T>
    {
    }
}
