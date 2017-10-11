using CGT.Api.DTO;
using CGT.DDD.Encrpty;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace CGT.JmeterCore {
    public class Program {
        static void Main(string[] args) {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine(Encoding.GetEncoding("GB2312"));
            Console.WriteLine("请填写需要加密的JSON串:");
            Console.OutputEncoding = Encoding.GetEncoding(936);
            while (true) {
                Stream inputStream = Console.OpenStandardInput();
                Console.SetIn(new StreamReader(inputStream));
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input)) {
                    var result = apiResult(input);
                    Console.WriteLine(result);
                    Console.WriteLine("请填写需要加密的JSON串:");
                }
            }
        }
        public static string apiResult(string json) {
            #region 获取注入内容
            IServiceCollection services = new ServiceCollection();
            var path = Directory.GetParent(Directory.GetCurrentDirectory());
            services.AddNodeServices(options => {
                options.ProjectPath = path + @"\NodeEncrpty";
                options.WatchFileExtensions = new[] { ".js" };
            });
            services.AddSingleton<NodeEncrpty>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            NodeEncrpty nodeServices = serviceProvider.GetService<NodeEncrpty>();
            nodeServices.nodeServices = serviceProvider.GetService<INodeServices>();
            #endregion

            var MerchantId = "CS01";
            var TimesTamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var Ip = "1.1.1.1";
            var Mac = "aaaaa";

            #region 获取Sign
            var Sign = nodeServices.MD5encrypt(string.Format(@"MerchantId={0}&TimesTamp={1}&Ip={2}&Mac={3}{4}"
                       , MerchantId
                       , TimesTamp
                       , Ip
                       , Mac
                       , "5d39980acc6e4d6f91b04720c3414ef6")).Result;
            ;
            #endregion
            JObject jsonNew = new JObject();
            try {
                jsonNew = JObject.Parse(json);
            }
            catch {
                return "不是有效的JSON字符串！";
            }
            jsonNew.Add("MerchantId", MerchantId);
            jsonNew.Add("TimesTamp", TimesTamp);
            jsonNew.Add("Ip", Ip);
            jsonNew.Add("Mac", Mac);
            jsonNew.Add("Sign", Sign);

            var aesKey = nodeServices.GenerateAESKey();
            var aesKeyBase64 = nodeServices.RSAencrypt(aesKey).Result;
            RequestModel requestModel = new RequestModel() {
                MerchantId = MerchantId,
                Data = nodeServices.AESencrypt(jsonNew.ToString(), aesKey).Result,
                EncryptKey = aesKeyBase64,
            };
            return JsonConvert.SerializeObject(requestModel);
        }
    }
}