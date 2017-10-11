using CGT.DDD.Encrpty;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CGT.UserCenter.Service
{
    public class Common
    {
        private static readonly string userAgen = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36";
        /// <summary>
        /// sign加密
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public string Sign(Dictionary<string, object> contents)
        {
            var sortedContents = string.Join("&", from key in contents.Keys
                                                  where key != "sign" && !key.Equals("sign_type")
                                                  orderby key
                                                  select key.ToLower() + "=" + (contents[key] ?? string.Empty));
            return Encrpty.MD5Encrypt(sortedContents.Trim('&') + "cgt").ToLower();
        }

        public string Post(string url, string postData)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                HttpContent httpContent = new StringContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                Task<HttpResponseMessage> taskResponse = httpClient.PostAsync(url, httpContent);
                taskResponse.Wait();
                response = taskResponse.Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string result = reader.ReadToEnd();
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
               throw ex;
                //return null;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }

            }
        }
    }
}
