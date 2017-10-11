using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CGT.Api.DTO;
using CGT.DDD.Encrpty;
using Microsoft.AspNetCore.NodeServices;

namespace CGT.JmeterCoreWeb.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Encryption() {
            ViewData["Message"] = "ManageAPI加密";
            return View();
        }
        [HttpPost]
        public JsonResult EncryptionStr(string JsonStr) {
            try {
                var data = apiResult(JsonStr);
                return Json(new {
                    IsSuccess = true,
                    Data = data,
                    Massage = "成功",
                    ErrorCode = "0000"
                });
            }
            catch (Exception ex) {
                return Json(new {
                    IsSuccess = false,
                    Data = "",
                    Massage = ex.Message,
                    ErrorCode = "9999"
                });
            }
        }
        public IActionResult Error() {
            return View();
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
            var TimesTamp = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds).ToString();
            var Ip = "1.1.1.1";
            var Mac = "aaaaa";
            
            JObject jsonNew = new JObject();
            try {
                jsonNew = JObject.Parse(json.Trim());
            }
            catch {
                return "不是有效的JSON字符串！";
            }
            jsonNew.Add("MerchantId", MerchantId);
            jsonNew.Add("TimesTamp", TimesTamp);
            jsonNew.Add("Ip", Ip);
            jsonNew.Add("Mac", Mac);
            jsonNew.Add("Sign", null);
            #region 获取Sign
            //var Sign = nodeServices.MD5encrypt(string.Format(@"MerchantId={0}&TimesTamp={1}&Ip={2}&Mac={3}{4}"
            //           , MerchantId
            //           , TimesTamp
            //           , Ip
            //           , Mac
            //           , "5d39980acc6e4d6f91b04720c3414ef6")).Result;
            //var Sign = nodeServices.MD5encrypt(string.Join("&", from key in dic.Keys
            //                                                    where key != "Sign" && !string.IsNullOrEmpty(key)
            //                                                    orderby key
            //                                                    select key.ToLower() + "=" + (dic[key] ?? string.Empty)) + "5d39980acc6e4d6f91b04720c3414ef6").Result.ToLower();
            var Sign = nodeServices.MD5encrypt(JsonConvert.SerializeObject(jsonNew)+ "5d39980acc6e4d6f91b04720c3414ef6").Result;
            #endregion
            jsonNew.Remove("Sign");
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
