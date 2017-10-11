using CGT.DDD.SOA;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CGT.MobileMsgService
{
    public abstract class ProcessorBase<TResponse, TResult>
    {
        private const string userkey = "48958gg3a25eeabg5fdgb4d95g93d4a4gfeb92c4g02ef276518da56cb9c7a809";
        /// <summary>
        /// 接口名称
        /// </summary>
        protected abstract string ServiceAddress { get; }


        /// <summary>
        /// 接口地址
        /// </summary>
        public abstract string RequestAddress { get; }
        /// <summary>
        /// 获取请求url
        /// </summary>
        /// <returns></returns>
        private string GetRequestUrl()
        {
            return ServiceAddress + "/" + RequestAddress;
        }

        protected abstract Dictionary<string, object> PrepareRequestCore();
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
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
                response = Common.HttpGet(target + request);
                resTime = DateTime.Now;
                result = ParseResponse(response);
            }
            catch (Exception ex)
            {
                CGT.DDD.Logger.LoggerFactory.Instance.Logger_Error(ex);
                result = new ExecResult<TResult>
                {
                    Success = false,
                    Message = ex.Message
                };
            }

            if (reqTime.HasValue)
            {
                CGT.DDD.Logger.LoggerFactory.Instance.Logger_Info(
                    string.Format(
                        "target:{1}{0}reqTime:{2:yyyy-MM-dd HH:mm:ss.fff}{0}request:{3}{0}resTime:{4:yyyy-MM-dd HH:mm:ss.fff}{0}response:{5}{0}",
                        Environment.NewLine, target, reqTime, request, resTime, response), "MobileMsgService");
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
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response);
            XmlNode node = doc.SelectSingleNode("");
            if (typeof(TResponse) == typeof(TResult))
            {
                if (node.InnerText.Equals("0000"))
                    result.Success = true;
                else
                    result.Success = false;
                    result.Success = false;
            }
            return result;
        }
        /// <summary>
        /// 请求参数
        /// </summary>
        /// <returns></returns>
        public string PrepareRequest()
        {
            string prepareRequest = string.Empty;
            var parameters = PrepareRequestCore();
            string sign = Common.Sign(parameters, "userkey");
            var i = 0;
            foreach (var p in parameters)
            {
                if (i == 0)
                {
                    prepareRequest += p.Key + "=" + p.Value;
                }
                else
                {
                    prepareRequest += "&" + p.Key + "=" + p.Value;
                }
                i++;
            }
            return prepareRequest + sign;

        }
    }
    public abstract class ProcessorBase<T> : ProcessorBase<T, T>
    {

    }
}
