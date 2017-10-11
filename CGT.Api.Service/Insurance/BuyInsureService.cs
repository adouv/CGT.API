using CGT.Api.DTO;
using CGT.Api.DTO.Insurance.InsuranceOrder.Request;
using CGT.DDD;
using CGT.DDD.Config;
using CGT.DDD.Logger;
using CGT.Entity.CgtInsuranceModel;
using CGT.PetaPoco.Repositories.Insurance;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace CGT.Api.Service.Insurance
{

    /// <summary>
    ///买保险
    /// </summary>
    public class BuyInsureService : ApiBase<RequestInsuranceOrder>
    {

        #region 注入仓储
        public InsuranceOrderRep insuranceOrderRep { get; set; }
        #endregion

        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json)
        {
            base.SetData(json);
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate()
        {
            base.Validate();
        }
        protected override void ExecuteMethod()
        {
            try
            {
               // string url = "http://localhost:2049/";
                string url = JsonConfig.JsonRead("InsuranceSelfApi", "InsuranceSelfApi");
                this.Parameter.OthOrderCode = ServerCommon.CreateOrderId();
                LoggerFactory.Instance.Logger_Debug("url:" + url.ToString() + "othercode:" + this.Parameter.OthOrderCode, "InSurRequestInfo");
                LoggerFactory.Instance.Logger_Debug(JsonConvert.SerializeObject(this.Parameter), "InSurRequestInfo");
                var data = PostData(string.Format("{0}api/Insure/SaveInsure", url), JsonConvert.SerializeObject(this.Parameter));

                if (!string.IsNullOrEmpty(data))
                {
                    var view = JsonConvert.DeserializeObject<ViewBase>(data);

                    if (view.Status == "100")
                    {
                        var returnResult = JsonConvert.DeserializeObject<ResultView>(view.Result);
                        var list = this.Parameter.InsuredPersonList.AsParallel().Select(r => new InsurancedPerson()
                        {
                            ApplyNum = r.applyNum,
                            IdentifyNumber = r.IdentifyNumber,
                            InsuredName = r.InsuredName,
                            IdentifyType = r.IdentifyType,
                            Mobile = r.Mobile,
                            Relation = r.Relation
                        }).ToList();
                        string msg = "";
                        var i = insuranceOrderRep.Insert(new InsuranceOrder()
                        {
                            FlightNo = this.Parameter.flightNo,
                            FlightDate = this.Parameter.flightDate,
                            AppliName = this.Parameter.AppliName,
                            EndDate = Convert.ToDateTime(this.Parameter.EndDate),
                            IdentifyNumber = this.Parameter.IdentifyNumber,
                            IdentifyType = this.Parameter.IdentifyType,
                            OthOrderCode = this.Parameter.OthOrderCode,
                            StartDate = Convert.ToDateTime(this.Parameter.StartDate),
                            TotalAmount = decimal.Parse(this.Parameter.TotalAmount),
                            TotalPremium = decimal.Parse(this.Parameter.TotalPremium),
                            UserId = long.Parse(this.Parameter.UserId),
                            Mobile = this.Parameter.Mobile,
                            ProposalNo = returnResult.ProposalNo,
                            PolicyNo = returnResult.PolicyNo,
                            UUID = returnResult.UUID,
                            SendTime = returnResult.SendTime
                        }, list, out msg);

                        if (i < 1)
                        {
                            this.Result.IsSuccess = false;
                            LoggerFactory.Instance.Logger_Debug(msg, "InSurdboInfo");
                            if (!string.IsNullOrEmpty(msg)) throw new Exception(msg);
                        }
                    }
                    else
                    {
                        this.Result.IsSuccess = false;
                        LoggerFactory.Instance.Logger_Debug(view.Message, "InSurInfo");
                        throw new Exception(view.Message);
                    }
                }
                else
                {
                    this.Result.IsSuccess = false;
                    this.Result.Message = "系统超时";
                    LoggerFactory.Instance.Logger_Warn("系统超时", "InSurTimeout");
                }
            }
            catch (Exception ex)
            {
                this.Result.IsSuccess = false;
                this.Result.Message = ex.Message;
                LoggerFactory.Instance.Logger_Error(ex, "InSurError");
            }
        }
        public string PostData(string requestURL, string requestData)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            byte[] byteArray = Encoding.UTF8.GetBytes(requestData);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(requestURL));
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.ContinueTimeout = 30000;
            System.IO.Stream newStream = webRequest.GetRequestStreamAsync().Result;
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Dispose();
            HttpWebResponse response;
            string data = string.Empty;
            try
            {
                var t = webRequest.GetResponseAsync();
                t.Wait();
                response = (HttpWebResponse)t.Result;
                data = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "InSurPostError");
                throw new AggregateException(ex);
            }
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            LoggerFactory.Instance.Logger_Info(string.Format("请求花费{0}ms", ts.TotalMilliseconds), "BuyReQuestTime");
            return data;

        }

    }
    /// <summary>
    /// 基类
    /// </summary>
    public class ViewBase
    {
        /// <summary>
        /// 返回码
        /// </summary> 
        public string MessageCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary> 
        public string Message { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string Result { get; set; }
    }

    public class ResultView
    {

        public string UUID { get; set; }//保险公司交易流水号
        public string PolicyNo { get; set; }//保单号
        public string ProposalNo { get; set; }//投保单号
        public string SendTime { get; set; }//发送时间
                                            //  public string comID { get; set; }//保险公司ID
                                            //  public string formID { get; set; }//请求方id
                                            //  public string serialNo { get; set; }//流水号
                                            //  public string policyURL { get; set; }//电子保单下载URL
                                            //   public string MainInsuredCount { get; set; }//计划导入被保险人总人数
                                            //   public string UploadInsuredCount { get; set; }//已经上传成功被保险人总人数
    }
}
