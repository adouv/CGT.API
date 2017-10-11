using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CGT.DDD.Utils.Http
{
    /// <summary>
    /// http请求
    /// </summary>
    public static class HttpRequest
    {
        public delegate void HttpRequestCallback(string response);

        public static class HttpRequestUtility
        {
            /// <summary>
            /// 发送GET请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            public static string SendGetRequestCore(string url, string encodingName, int? timeout)
            {
                return SendGetRequestCore(url, encodingName, timeout, null, null);
            }
            /// <summary>
            /// 发送GET请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            /// <param name="cookies">请求时，附带的Cookie值</param>
            /// <param name="headers">请求时，附带的Header值</param>
            public static string SendGetRequestCore(string url, string encodingName, int? timeout, CookieCollection cookies, NameValueCollection headers)
            {
                HttpWebRequest request = null;
                try
                {
                    // 创建HTTP请求
                    request = CreateHttpRequest(url, "GET", timeout);
                    // 写入Cookie
                    WriteRequestCookie(request, cookies);
                    // 写入header值
                    WriteRequestHeader(request, headers);
                    // 接收HTTP响应
                    return ReceiveResponse(request, encodingName);
                }
                finally
                {
                    if (request != null)
                    {
                        request.Abort();
                    }
                }
            }
            /// <summary>
            /// 发送GET请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            /// <remarks>如何出错，会返回错误信息</remarks>
            public static string SendGetRequest(string url, string encodingName, int? timeout)
            {
                try
                {
                    return SendGetRequestCore(url, encodingName, timeout);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            /// <summary>
            /// 发送GET请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            /// <param name="cookies">请求时，附带的Cookie值</param>
            /// <param name="headers">请求时，附带的Header值</param>
            /// <remarks>如何出错，会返回错误信息</remarks>
            public static string SendGetRequest(string url, string encodingName, int? timeout, CookieCollection cookies, NameValueCollection headers)
            {
                try
                {
                    return SendGetRequestCore(url, encodingName, timeout, cookies, headers);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            /// <summary>
            /// 发送POST请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="content">请求内容</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            public static string SendPostRequestCore(string url, string content, string encodingName, int? timeout)
            {
                NameValueCollection collection = new NameValueCollection();
                collection.Add("HeaderContentType", "application/json;charset=UTF-8");
                return SendPostRequestCore(url, content, encodingName, timeout, null, collection);
            }

            /// <summary>
            /// 发送POST请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="content">请求内容</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            /// <param name="cookies">请求时，附带的Cookie值</param>
            /// <param name="headers">请求时，附带的Header值</param>
            public static string SendPostRequestCore(string url, string content, string encodingName, int? timeout, CookieCollection cookies, NameValueCollection headers)
            {
                return SendPostRequestCore(url, content, encodingName, timeout, cookies, headers, "application/json;charset=UTF-8");
            }
            /// <summary>
            /// 发送POST请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="content">请求内容</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            /// <param name="cookies">请求时，附带的Cookie值</param>
            /// <param name="headers">请求时，附带的Header值</param>
            /// <param name="contentType">Content-Type Header标头</param>
            public static string SendPostRequestCore(string url, string content, string encodingName, int? timeout, CookieCollection cookies, NameValueCollection headers, string contentType)
            {
                HttpWebRequest request = null;
                try
                {

                    // 创建HTTP请求
                    request = CreateHttpRequest(url, "POST", timeout);
                    // 写入Cookie
                    WriteRequestCookie(request, cookies);
                    // 写入header值
                    WriteRequestHeader(request, headers);
                    if (!string.IsNullOrWhiteSpace(contentType))
                    {
                        request.ContentType = contentType;
                    }
                    //这个在Post的时候，一定要加上，如果服务器返回错误，他还会继续再去请求，不会使用之前的错误数据，做返回数据
                    //request.ServicePoint.Expect100Continue = false;   
                    // 写入POST数据
                    WriteRequestContent(request, content, encodingName);
                    // 接收HTTP响应
                    return ReceiveResponse(request, encodingName);
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                    if (request != null)
                    {
                        request.Abort();
                    }
                    return result;
                }
                finally
                {
                    if (request != null)
                    {
                        request.Abort();
                    }
                }
            }

            public static string SendPostRequestjson(string url, string content, string encodingName, int? timeout, CookieCollection cookies, NameValueCollection headers, string contentType)
            {
                HttpWebRequest request = null;
                try
                {
                    // 创建HTTP请求
                    request = CreateHttpRequest(url, "POST", timeout);
                    // 写入Cookie
                    WriteRequestCookie(request, cookies);
                    // 写入header值
                    WriteRequestHeader(request, headers);
                    if (!string.IsNullOrWhiteSpace(contentType))
                    {
                        request.ContentType = contentType;
                    }
                    // 写入POST数据
                    WriteRequestjsonContent(request, content, encodingName);
                    // 接收HTTP响应
                    return ReceiveResponse(request, encodingName);
                }
                finally
                {
                    if (request != null)
                    {
                        request.Abort();
                    }
                }
            }
            /// <summary>
            /// 发送POST请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="content">请求内容</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            /// <remarks>如何出错，会返回错误信息</remarks>
            public static string SendPostRequest(string url, string content, string encodingName, int? timeout)
            {
                try
                {
                    return SendPostRequestCore(url, content, encodingName, timeout);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            /// <summary>
            /// 发送POST请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="content">请求内容</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            /// <param name="cookies">请求时，附带的Cookie值</param>
            /// <param name="headers">请求时，附带的Header值</param>
            /// <remarks>如何出错，会返回错误信息</remarks>
            public static string SendPostRequest(string url, string content, string encodingName, int? timeout, CookieCollection cookies, NameValueCollection headers)
            {
                try
                {
                    return SendPostRequestCore(url, content, encodingName, timeout, cookies, headers);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            /// <summary>
            /// POST二进制流到服务器
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="content">请求内容</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            /// <returns>如何出错，会返回错误信息</returns>
            public static string SendPostRequest(string url, byte[] content, string encodingName, int? timeout)
            {
                try
                {
                    // 创建HTTP请求
                    var request = CreateHttpRequest(url, "POST", timeout);
                    if (content != null && content.Length > 0)
                    {
                        request.ContentType = "application/octet-stream";
                        using (var requestSream = request.GetRequestStreamAsync().Result)
                        {
                            requestSream.Write(content, 0, content.Length);
                        }
                    }
                    // 接收HTTP响应
                    return ReceiveResponse(request, encodingName);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            /// <summary>
            /// 异步发送GET请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            /// <param name="callback">回调</param>
            public static void AsyncSendGetRequest(string url, string encodingName, int? timeout, HttpRequestCallback callback)
            {
                WebRequest request = null;
                try
                {
                    // 创建HTTP请求
                    request = CreateHttpRequest(url, "GET", timeout);
                    // 开始接收响应
                    BeginReceiveResponse(request, encodingName, callback);
                }
                finally
                {
                    if (request != null)
                    {
                        request.Abort();
                    }
                }
            }
            /// <summary>
            /// 异步发送POST请求
            /// </summary>
            /// <param name="url">请求的URL地址</param>
            /// <param name="content">请求内容</param>
            /// <param name="encodingName">编码格式</param>
            /// <param name="timeout">超时时间 以毫秒为单位</param>
            /// <param name="callback">回调</param>
            public static void AsyncSendPostRequest(string url, string content, string encodingName, int? timeout, HttpRequestCallback callback)
            {
                HttpWebRequest request = null;
                try
                {
                    // 创建HTTP请求
                    request = CreateHttpRequest(url, "POST", timeout);
                    // 写入POST数据
                    WriteRequestContent(request, content, encodingName);
                    // 开始接收响应
                    BeginReceiveResponse(request, encodingName, callback);
                }
                finally
                {
                    if (request != null)
                    {
                        request.Abort();
                    }
                }
            }

            private static HttpWebRequest CreateHttpRequest(string url, string method, int? timeout)
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = method;
                if (timeout.HasValue)
                    request.ContinueTimeout = timeout != null ? timeout.Value : 100;
                return request;
            }

            private static void WriteRequestCookie(HttpWebRequest request, CookieCollection cookies)
            {
                if (cookies != null && cookies.Count > 0)
                {
                    if (request.CookieContainer == null)
                        request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(new Uri("http://" + request.RequestUri.Host), cookies);
                }
            }
            private static void WriteRequestHeader(HttpWebRequest request, NameValueCollection headers)
            {
                if (headers != null && headers.Count > 0)
                {
                    foreach (var key in headers.AllKeys)
                    {
                        request.Headers[key] = headers.Get(key);
                    }
                }
            }
            private static void WriteRequestjsonContent(HttpWebRequest request, string content, string encodingName)
            {
                if (content != null)
                {
                    var requestData = Encoding.GetEncoding(encodingName).GetBytes(content);
                    request.ContentType = "application/json";
                    using (var requestSream = request.GetRequestStreamAsync().Result)
                    {
                        requestSream.Write(requestData, 0, requestData.Length);
                    }
                }
            }

            private static void WriteRequestContent(HttpWebRequest request, string content, string encodingName)
            {
                if (content != null)
                {
                    var requestData = Encoding.GetEncoding(encodingName).GetBytes(content);
                    request.ContentType = "application/x-www-form-urlencoded";
                    using (var requestSream = request.GetRequestStreamAsync().Result)
                    {
                        requestSream.Write(requestData, 0, requestData.Length);
                    }
                }
            }
            private static string ReceiveResponse(HttpWebRequest request, string encodingName)
            {
                WebResponse response = null;
                try
                {
                    // 获取HTTP响应
                    response = request.GetResponseAsync().Result;
                    // 接收响应数据
                    return ReceiveResponse(response, encodingName);
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
            private static void BeginReceiveResponse(WebRequest request, string encodingName, HttpRequestCallback callback)
            {
                request.BeginGetResponse(EndReceiveResponse, new HttpRequestContainer
                {
                    Request = request,
                    EncodingName = encodingName,
                    Callback = callback
                });
            }
            private static void EndReceiveResponse(IAsyncResult ar)
            {
                var requestContainer = ar.AsyncState as HttpRequestContainer;
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)requestContainer.Request.EndGetResponse(ar);
                    if (requestContainer.Callback != null)
                    {
                        requestContainer.Callback(ReceiveResponse(response, requestContainer.EncodingName));
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
            private static string ReceiveResponse(WebResponse response, string encoding)
            {
                var result = new StringBuilder();
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, Encoding.GetEncoding(!String.IsNullOrEmpty(encoding) ? encoding : "utf-8")))
                    {
                        while (reader.Peek() > 0)
                        {
                            result.Append(reader.ReadLine());
                        }
                    }
                }
                return result.ToString();
            }

            /// <summary>
            /// 获取客户IP地址
            /// </summary>
            /// <param name="context"></param>
            /// <returns></returns>
            public static string GetClientIP(HttpContext context)
            {
                var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = context.Connection.RemoteIpAddress.ToString();
                }
                return ip;
            }

            class HttpRequestContainer
            {
                public WebRequest Request { get; set; }
                public string EncodingName { get; set; }
                public HttpRequestCallback Callback { get; set; }
            }
        }
    }
}
