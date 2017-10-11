using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System;

namespace CGT.DDD.Config
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class JsonConfig
    {
        private static string env =Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ;
        
        /// <summary>
        /// 根据节点名称和属性获取值
        /// </summary>
        /// <param name="settings">使用Debug版本不用添加Debug后缀</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string JsonRead(string key, string settings = null)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            if (String.IsNullOrEmpty(env) || env == "Development")
            {
                builder.AddJsonFile("appsettings.json");
            }
            else
            {
                builder.AddJsonFile("appsettings."+env.ToLower()+".json");
            }
            var config = builder.Build();
            if (String.IsNullOrEmpty(settings))
            {
                settings = "CGTSettings";
            }
            return config.GetSection(settings + ":" + key).Value;
        }
    }
}
