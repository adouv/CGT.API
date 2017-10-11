using CGT.Api.Service.Boss.TravelOrder;
using CGT.DDD.Utils;
using CGT.DDD.Config;
using CGT.Entity.CgtTravelModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CGT.DDD.Encrpty;
using System;
using Microsoft.AspNetCore.NodeServices;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using CGT.Api.DTO;
using CGT.Api.Service.Insurance;
using CGT.Api.DTO.Insurance.InsuranceOrder.Request;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace CGT.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/boss")]
    [EnableCors("AllowSameDomain")]
    public class TestController : Controller
    {
        #region 注入服务
        public GetTravelOrderListService getTravelOrderListService { get; set; }

        public ExeclHelper execl { get; set; }

        #endregion

        [Route("user")]
        [HttpGet]
        public dynamic User()
        {
            string aaa = "000";
            aaa = JsonConfig.JsonRead("cgtConnection");
            return new
            {
                aaa = aaa
            };
        }


        [Route("execl")]
        [HttpGet]
        public dynamic Execl()
        {
            FileStream file = new FileStream(@"D:\test.xlsx", FileMode.Open);
            var table = execl.ExcelImport(file);
            table.rows.Remove(table.rows.FirstOrDefault());
            foreach (var item in table.rows)
            {

                // Parallel.ForEach(table.rows, item =>
                // {
                string str = item.columns.ElementAt(3).ColumnValue.Trim().Replace('/', '-');
                string[] strs = str.Split(' ');
                string start = " 00:00:00";
                string end = " 23:59:59";
                string startDate = strs[0] + start;
                string endDate = strs[0] + end;
                RequestInsurance order = new RequestInsurance()
                {
                    AppliName = item.columns.ElementAt(0).ColumnValue.Trim(),
                    IdentifyType = "01",
                    IdentifyNumber = item.columns.ElementAt(1).ColumnValue.Trim(),
                    Mobile = "15500015943",
                    flightNo = item.columns.ElementAt(2).ColumnValue.Trim(),
                    flightDate = str,
                    UserId = "2",
                    TotalPremium = "2.0",
                    TotalAmount = "10000.00",
                    StartDate = startDate,
                    EndDate = endDate,

                    InsuredPersonList = new List<InsurancedPassengers>()
                    {
                        new InsurancedPassengers()
                        {
                            applyNum =  1,
                            IdentifyNumber =  item.columns.ElementAt(1).ColumnValue.Trim(),
                            IdentifyType = "01",
                            InsuredName = item.columns.ElementAt(0).ColumnValue.Trim(),
                            Mobile = "15500015943",
                            Relation = "01"
                        }
                    },
                };
                string json = JsonConvert.SerializeObject(order);
                var model = apiResult(json);
                apiPost("http://127.0.0.1:5002/api/Insurance/BuyInsure", model);
          //  });
          

              
                //BuyInsureService buyInsureService = new BuyInsureService();
                // buyInsureService.SetData(model);

                // DDD.Logger.LoggerFactory.Instance.Logger_Debug(result, "test");
            }
            return null;
        }

        public static string apiResult(string json)
        {
            #region 获取注入内容
            IServiceCollection services = new ServiceCollection();
            var path = Directory.GetParent(Directory.GetCurrentDirectory());
            services.AddNodeServices(options =>
            {
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
            jsonNew = JObject.Parse(json.Trim());
            jsonNew.Add("MerchantId", MerchantId);
            jsonNew.Add("TimesTamp", TimesTamp);
            jsonNew.Add("Ip", Ip);
            jsonNew.Add("Mac", Mac);
            jsonNew.Add("Sign", null);
            #region 获取Sign
            var Sign = nodeServices.MD5encrypt(string.Format(@"MerchantId={0}&TimesTamp={1}&Ip={2}&Mac={3}{4}"
                       , MerchantId
                       , TimesTamp
                       , Ip
                       , Mac
                       , "5d39980acc6e4d6f91b04720c3414ef6")).Result;
            //var Sign = nodeServices.MD5encrypt(string.Join("&", from key in dic.Keys
            //                                                    where key != "Sign" && !string.IsNullOrEmpty(key)
            //                                                    orderby key
            //                                                    select key.ToLower() + "=" + (dic[key] ?? string.Empty)) + "5d39980acc6e4d6f91b04720c3414ef6").Result.ToLower();
            // var Sign = nodeServices.MD5encrypt(JsonConvert.SerializeObject(jsonNew) + "5d39980acc6e4d6f91b04720c3414ef6").Result;
            #endregion
            jsonNew.Remove("Sign");
            jsonNew.Add("Sign", Sign);
            var aesKey = nodeServices.GenerateAESKey();
            var aesKeyBase64 = nodeServices.RSAencrypt(aesKey).Result;
            RequestModel requestModel = new RequestModel()
            {
                MerchantId = MerchantId,
                Data = nodeServices.AESencrypt(jsonNew.ToString(), aesKey).Result,
                EncryptKey = aesKeyBase64,
            };
            // return requestModel;
            return JsonConvert.SerializeObject(requestModel);
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
            webRequest.ContinueTimeout = 30000;
            System.IO.Stream newStream = webRequest.GetRequestStreamAsync().Result;
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Dispose();
            var data = "";
            HttpWebResponse response;
            try
            {
                var t = webRequest.GetResponseAsync();
                t.Wait();
                response = (HttpWebResponse)t.Result;
                data = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            }
            catch (Exception ex)
            {
                response = null;
                var msg = ex.Message;

            }
         
            return data;
        }

        [Route("insert")]
        [HttpGet]
        public dynamic insert()
        {

            var lis = new List<UserQuest>()
            {
                 new UserQuest(){
                      ID =68,
                     UserId = 123,
                      QuestAction = "123",

                 }
            };

            int num = CgtTravelDB.BulkUpdate<UserQuest>("UserQuest", lis, GetSql);

            return num;
        }

        [HttpGet]
        public string GetSql(UserQuest item)
        {
            return string.Format(" [UserId]={0},[QuestAction]='{1}' Where [Id] = {2}", item.UserId, item.QuestAction, item.ID);
        }
    }
    public class RequestInsurance
    {
        public string UserId { get; set; }
        public string OthOrderCode { get; set; }
        public string TotalAmount { get; set; }
        public string TotalPremium { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AppliName { get; set; }
        public string IdentifyType { get; set; }
        public string IdentifyNumber { get; set; }
        public string flightNo { get; set; }
        public string flightDate { get; set; }
        public string Mobile { get; set; }
        public List<InsurancedPassengers> InsuredPersonList { get; set; }
    }
    public class InsurancedPassengers
    {
        public int applyNum { get; set; }
        public string InsuredName { get; set; }
        public string IdentifyType { get; set; }
        public string IdentifyNumber { get; set; }
        public string Relation { get; set; }
        public string Mobile { get; set; }
    }
}
