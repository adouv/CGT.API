using CGT.DDD.Config;
using CGT.DDD.Encrpty;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace CGT.Reapal.Service
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class Common
    {
        public NodeEncrpty nodeServices { get; set; }
        public Common()
        {
            IServiceCollection services = new ServiceCollection();
            var path = Directory.GetParent(Directory.GetCurrentDirectory());
            services.AddNodeServices(options =>
            {
                options.ProjectPath = path + @"\NodeEncrpty";
                options.WatchFileExtensions = new[] { ".js" };
            });
            services.AddSingleton<NodeEncrpty>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            nodeServices = serviceProvider.GetService<NodeEncrpty>();
            nodeServices.nodeServices = serviceProvider.GetService<INodeServices>();
        }

        private static string ReapalUserKey = JsonConfig.JsonRead("ReapalUserKey","Reapal");

        /// <summary>
        /// sign加密
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public string Sign(NameValueCollection contents)
        {
            var sortedContents = string.Join("&", from key in contents.AllKeys
                                                  where key != "sign" && !string.IsNullOrEmpty(contents[key]) && !key.Equals("sign_type")
                                                  orderby key
                                                  select key.ToLower() + "=" + (contents[key] ?? string.Empty));
            return nodeServices.MD5encrypt(sortedContents.Trim('&') + ReapalUserKey).Result.ToLower();
        }
        public string Sign(NameValueCollection contents, string userkey)
        {
            var sortedContents = string.Join("&", from key in contents.AllKeys
                                                  where key != "sign" && !string.IsNullOrEmpty(contents[key]) && !key.Equals("sign_type")
                                                  orderby key
                                                  select key.ToLower() + "=" + (contents[key] ?? string.Empty));
            return nodeServices.MD5encrypt(sortedContents.Trim('&') + userkey).Result.ToLower();
        }
        /// <summary>
        ///  SortedDictionary sign加密
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public string Sign(SortedDictionary<string, string> contents)
        {
            var sortedContents = string.Join("&", from key in contents.Keys
                                                  where key != "sign"
                                                  orderby key
                                                  select key.ToLower() + "=" + (contents[key] ?? string.Empty));
            return nodeServices.MD5encrypt(sortedContents.Trim('&') + ReapalUserKey).Result.ToLower();
        }
        /// <summary>
        ///  SortedDictionary sign加密
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="userKey"></param>
        /// <returns></returns>
        public string Sign(SortedDictionary<string, string> contents, string userKey)
        {
            var sortedContents = string.Join("&", from key in contents.Keys
                                                  where key != "sign"
                                                  orderby key
                                                  select key.ToLower() + "=" + (contents[key] ?? string.Empty));
            return nodeServices.MD5encrypt(sortedContents.Trim('&') + userKey).Result.ToLower();
        }
    }
}
