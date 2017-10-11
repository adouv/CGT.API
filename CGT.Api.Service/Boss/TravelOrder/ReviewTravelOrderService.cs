using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelOrder;
using CGT.Api.DTO.Boss.TravelOrder.MiddleModel;
using CGT.DDD.Config;
using CGT.DDD.Encrpty;
using CGT.DDD.Logger;
using CGT.Entity.CgtModel;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.PetaPoco.Repositories.CgtTravel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CGT.Api.Service.Boss.TravelOrder {
    /// <summary>
    /// 差旅订单审核操作
    /// </summary>
    public class ReviewTravelOrderService : ApiBase<RequestReviweTravelOrder> {
        #region 注入服务
        public EnterpriseOrderRiskRep enterpriseOrderRiskRep { get; set; }
        public EnterpriseOrderRep enterpriseOrderRep { get; set; }

        public UserAccountRep userAccountRep { get; set; }
        #endregion
        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json) {
            base.SetData(json);
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate() {
            base.Validate();
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod() {
            var rescount = 0;
            var EOrderIds = new List<string>();
            EOrderIds = Parameter.EOrderIds.Split(',').ToList();
            if (!EOrderIds.Any()) {
                throw new AggregateException("差旅订单号不存在或错误！");
            }
            foreach (var EOrderId in EOrderIds) {
                //获取对应订单号数据
                var enterpriseOrder = enterpriseOrderRep.GetEnterpriseOrder(EOrderId);
                if (enterpriseOrder == null) {
                    throw new AggregateException("差旅订单不存在！");
                }
                //判断是否返现
                if (enterpriseOrder.BackStatus == 1) {
                    throw new AggregateException("此订单已返现，无法审核！");
                }
                //判断分销是否存在
                var userAccount = new UserAccount() {
                    PayCenterCode = enterpriseOrder.PayCenterCode
                };
                var userAccountInfo = userAccountRep.GetUserAccount(userAccount);
                if (userAccountInfo == null) {
                    throw new AggregateException("分销不存在！");
                }
                //判断代理是否存在
                var interfaceAccount = new InterfaceAccount() {
                    MerchantCode = userAccountInfo.MerchantCode
                };
                var interfaceAccountlist = interfaceAccountRep.GetInterfaceAccount(interfaceAccount);
                if (interfaceAccountlist == null) {
                    throw new AggregateException("代理CODE不存在！");
                }
                //订单实体赋值
                var person = new Person() {
                    PersonName = enterpriseOrder.PassengerName,
                    CardType = 0,
                    IdNumber = enterpriseOrder.PassengerNo,
                    PersonType = 0,
                    TicketId = enterpriseOrder.TicketNo,
                    BrithDay = DateTime.Now
                };
                var PayModel = new PayRemoneyImportModel() {
                    AirCompanyCode = enterpriseOrder.Airline,
                    AirFee = 50,
                    ArriveCode = enterpriseOrder.ReachCity,
                    ArriveTime = (DateTime)enterpriseOrder.DepartureTime,
                    Cabin = "Y",
                    CallBackUrl = "",
                    CompanyCode = interfaceAccountlist.MerchantCode,
                    DepartCode = enterpriseOrder.DepartureCity,
                    DepartureTime = (DateTime)enterpriseOrder.DepartureTime,
                    EnterpriseID = enterpriseOrder.EnterpriseWhiteListID.ToString(),
                    FlightNo = enterpriseOrder.FlightNo,
                    FuelFee = 0,
                    Ip = "1.1.1.1",
                    IsRemoney = 1,
                    IsValid = true,
                    Mac = "127.1.1.1",
                    OfficeNo = "PEK474",
                    OrderPrice = (decimal)enterpriseOrder.TicketAmount,
                    PassWord = "123456",
                    PayAccount = enterpriseOrder.CashBackEmial,
                    PlateCode = enterpriseOrder.OrderId,
                    PNR = enterpriseOrder.Pnr,
                    Rebate = 0,
                    RemoneyAccount = enterpriseOrder.CashBackEmial,
                    SomeRebate = 0,
                    StartDate = (DateTime)enterpriseOrder.DepartureTime,
                    TicketPrice = (decimal)enterpriseOrder.TicketAmount,
                    TicketTime = (DateTime)enterpriseOrder.TicketTime,
                    TicketType = 0,
                    TimelyUrl = "",
                    TimesTamp = DateTime.Now.ToString(),
                    UserKey = interfaceAccountlist.UserKey,
                    person = person,

                };
                if (Parameter.ReviewState == 1) {
                    //调用返现
                    var orderPay = SubmitTravelPayRemoney(PayModel, userAccountInfo, interfaceAccountlist.CertAddress.Split('|')[0], interfaceAccountlist.CertPassword, interfaceAccountlist.MerchantCode, interfaceAccountlist.UserKey, JsonConfig.JsonRead("CgtPayApiUrl", "CgtPayApi"));
                    if (orderPay.Status != 100) {
                        throw new AggregateException(orderPay.Message);
                    }
                }
                //修改订单审核状态
                var res = enterpriseOrderRiskRep.UpdateEnterpriseOrderRiskReviewState(EOrderId, Parameter.ReviewState, Parameter.RefuseReason, Parameter.ReviewUserId);
                if (res > 0) {
                    rescount++;
                }
            }
            this.Result.Data = rescount;
        }
        /// <summary>
        /// 提交差旅支付返现
        /// </summary>
        /// <param name="List">订单信息</param>
        /// <param name="ServiceUrl">请求接口地址</param>
        /// <param name="Path"></param>
        /// <param name="Pwd"></param>
        /// <param name="MerchantId"></param>
        /// <param name="UserKey"></param>
        /// <param name="BaoLiAPIUrl"></param>
        /// <param name="Message"></param>
        /// <param name="OrderMessage"></param>
        public ResponsePayAPIMessage SubmitTravelPayRemoney(PayRemoneyImportModel payRemony, UserAccount userAccount, string Path, string Pwd, string MerchantId, string UserKey, string BaoLiAPIUrl) {
            ResponsePayModel payModel = new ResponsePayModel();
            ResponsePayAPIMessage orderPay = BaoLiOrderPayMoney(payRemony, userAccount, BaoLiAPIUrl, Path, Pwd, MerchantId, UserKey, ref payModel);
            return orderPay;
        }
        /// <summary>
        /// 调用保理订单支付返现
        /// </summary>
        /// <param name="payRemony">数据实体</param>
        /// <param name="BaoLiServiceUrl">保理接口地址</param>
        /// <param name="Path">证书路径</param>
        /// <param name="Pwd">密码</param>
        /// <param name="MerchantId">商户编号</param>
        /// <param name="UserKey">key值</param>
        /// <param name="respons">返回支付返现信息</param>
        /// <returns>接口返回信息</returns>
        public ResponsePayAPIMessage BaoLiOrderPayMoney(PayRemoneyImportModel payRemony, UserAccount userAccount, string BaoLiServiceUrl, string Path, string Pwd, string MerchantId, string UserKey, ref ResponsePayModel respons) {
            respons = new ResponsePayModel();
            ResponsePayAPIMessage result = new ResponsePayAPIMessage();

            try {
                #region 乘机人信息
                TravelPassenger pmodel = new TravelPassenger();
                pmodel.PassengerName = payRemony.person.PersonName;
                pmodel.PassengerType = payRemony.person.PersonType;
                pmodel.CertificateNumber = payRemony.person.IdNumber;
                pmodel.CertificateType = payRemony.person.CardType;
                pmodel.Birthday = payRemony.person.BrithDay.ToString("yyyy-MM-dd");
                pmodel.AirTicketNo = payRemony.person.TicketId;
                #endregion

                #region 航段
                Voyage vmodel = new Voyage();

                vmodel.Departure = payRemony.DepartCode;
                vmodel.Arrival = payRemony.ArriveCode;
                vmodel.DepartureTime = payRemony.DepartureTime.ToString();
                vmodel.ArrivalTime = payRemony.ArriveTime.ToString();
                vmodel.Bunk = payRemony.Cabin;
                vmodel.Airline = payRemony.AirCompanyCode;
                vmodel.FlightNo = payRemony.FlightNo;

                #endregion

                var item = new RequestBaoLiPayAPIModel {
                    PayUserName = payRemony.PayAccount,
                    IsRemoney = payRemony.IsRemoney.ToString(),
                    UserName = payRemony.RemoneyAccount,
                    OrderId = payRemony.PlateCode,
                    MerchantCode = payRemony.CompanyCode,
                    StartDate = payRemony.StartDate.ToString("yyyy-MM-dd"),
                    TicketTime = payRemony.TicketTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    NotifyUrl = payRemony.CallBackUrl,
                    ReturnUrl = payRemony.TimelyUrl,
                    Rebate = Convert.ToInt32(payRemony.Rebate),
                    RetMoney = Convert.ToInt32(payRemony.SomeRebate),
                    Pnr = payRemony.PNR,
                    OrderPrice = payRemony.OrderPrice.ToString("#0.00"),
                    TicketPrice = payRemony.TicketPrice.ToString("#0.00"),
                    AirPortTax = Convert.ToInt32(payRemony.AirFee),
                    FuelTax = Convert.ToInt32(payRemony.FuelFee),
                    Passenger = pmodel,
                    Voyage = vmodel,
                    TimesTamp = payRemony.TimesTamp,
                    Ip = payRemony.Ip,
                    Mac = payRemony.Mac,
                    UserKey = payRemony.UserKey,
                    EnterpriseID = Convert.ToInt32(payRemony.EnterpriseID),
                    IsValid = true
                };

                PayRequestBaseModel baseModel = apiResult<RequestBaoLiPayAPIModel>(item, Path, Pwd, MerchantId, UserKey);
                string url = BaoLiServiceUrl + "api/Travel/Pay";
                var jsonData = JsonConvert.SerializeObject(baseModel);
                //var post = HttpRequest.HttpRequestUtility.SendPostRequestCore(url, jsonData, "UTF-8", null);
                var post = apiPost(url, jsonData);
                result = JsonConvert.DeserializeObject<ResponsePayAPIMessage>(post);
                if (result.Status == 100) {
                    var _EnterpriseOrder = new EnterpriseOrder() {
                        OrderId = item.OrderId,
                        UserInterestRate = userAccount.FactoringInterestRate,
                        UserInterest = decimal.Parse(item.TicketPrice) * userAccount.FactoringInterestRate
                    };
                    enterpriseOrderRep.UpdateEnterpriseOrderBackState(_EnterpriseOrder);
                }
                if (!string.IsNullOrEmpty(result.Result)) {
                    respons = JsonConvert.DeserializeObject<ResponsePayModel>(result.Result);
                }
                return result;
            }
            catch (Exception ex) {
                LoggerFactory.Instance.Logger_Error(ex, "ReviewTravelOrderError");
                return result;
            }
        }

        #region 数据加密
        /// <summary>
        /// 数据加密密
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns>RequestBaseModel</returns>
        public PayRequestBaseModel apiResult<T>(T model, string Path, string Pwd, string MerchantId, string UserKey) where T : RequestBase, new() {
            PayRequestBaseModel baseModel = new PayRequestBaseModel();

            string certAdd = @"" + Path;
            model.UserKey = UserKey;
            baseModel.MerchantId = MerchantId;

            string certPwd = Pwd;

            model.Ip = "127.0.0.1";
            model.Mac = "F4-6D-04-35-B5-A1";
            model.TimesTamp = DateTime.Now.ToString("yyyy-MM-dd HH:ss:dd");

            string jsonData = JsonConvert.SerializeObject(model);

            model.Sign = Encrpty.MD5Encrypt(jsonData + model.UserKey);

            string aesKey = Cryptor.GenerateAESKey();
            string d = Encrpty.AESEecrypt(JsonConvert.SerializeObject(model), aesKey);
            aesKey = Encrpty.RSAEcrypt(aesKey, certAdd);
            baseModel.Data = d;
            baseModel.EncryptKey = aesKey;

            return baseModel;
        }
        #endregion

        /// <summary>
        /// Post提交
        /// </summary>
        /// <param name="requestURL">请求地址</param>
        /// <param name="requestData">请求数据</param>
        /// <returns></returns>
        public static string apiPost(string requestURL, string requestData) {
            byte[] byteArray = Encoding.UTF8.GetBytes(requestData);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(requestURL));
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.Timeout = 300000;
            System.IO.Stream newStream = webRequest.GetRequestStreamAsync().Result;
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Dispose();
            HttpWebResponse response;
            try {
                response = (HttpWebResponse)webRequest.GetResponseAsync().Result;
            }
            catch (WebException ex) {
                response = (HttpWebResponse)ex.Response;
            }
            var data = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            response.Close();
            return data;
        }
    }
}
