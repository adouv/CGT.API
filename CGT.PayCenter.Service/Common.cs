using CGT.DDD.Encrpty;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CGT.PayCenter.Service
{
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

        /// <summary>
        /// sign加密
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        //public string Sign(Dictionary<string, object> contents)
        //{
        //    var sortedContents = string.Join("&", from key in contents.Keys
        //                                          where key != "sign" && !key.Equals("sign_type")
        //                                          orderby key
        //                                          select key.ToLower() + "=" + (contents[key] ?? string.Empty));
        //    return nodeServices.MD5encrypt(sortedContents.Trim('&') + "cgt").Result.ToLower();
        //}
        /// <summary>
        /// sign加密
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public string Sign(Dictionary<string, object> contents) {
            var sortedContents = string.Join("&", from key in contents.Keys
                                                  where key != "sign" && !key.Equals("sign_type")
                                                  orderby key
                                                  select key.ToLower() + "=" + (contents[key] ?? string.Empty));
            return Encrpty.MD5Encrypt(sortedContents.Trim('&') + "cgt").ToLower();
        }
    }
}
