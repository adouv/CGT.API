using System;
using System.Collections.Generic;
using System.Text;
using CGT.DDD.Encrpty;
using System.Linq;
using System.Net.Http;

namespace CGT.MobileMsgService
{
    public class Common
    {
        /// <summary>
        /// sign加密
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static string Sign(Dictionary<string, object> contents, string userkey)
        {
            var sortedContents = string.Join("&", from key in contents.Keys
                                                  where key != "sign" && !key.Equals("sign_type")
                                                  orderby key
                                                  select key.ToLower() + "=" + (contents[key] ?? string.Empty));
            return Encrpty.MD5Encrypt(sortedContents.Trim('&') + userkey).ToLower();
        }

        public static string HttpGet(string url)
        {
            var httpClient = new HttpClient();
            var t = httpClient.GetByteArrayAsync(url);
            t.Wait();
            var result = Encoding.UTF8.GetString(t.Result);
            return result;
        }
    }
}
