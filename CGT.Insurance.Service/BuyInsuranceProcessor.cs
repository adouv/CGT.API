
using CGT.DDD.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGT.Api.DTO.Insurance;
using Newtonsoft.Json;
using CGT.Api.DTO.Insurance.InsuranceOrder.Request;
using System.Net;

namespace CGT.Insurance.Service
{
    /// <summary>
    /// 买保险
    /// </summary>
    //public class BuyInsuranceProcessor : ProcessorBase<ViewBase>
    //{

    //    /// <summary>
    //    /// Post提交
    //    /// </summary>
    //    /// <param name="requestURL">请求地址</param>
    //    /// <param name="requestData">请求数据</param>
    //    /// <returns></returns>
    //    public static string apiPost(string requestURL, string requestData)
    //    {
    //        byte[] byteArray = Encoding.UTF8.GetBytes(requestData);
    //        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(requestURL));
    //        webRequest.Method = "POST";
    //        webRequest.ContentType = "application/json";
    //        System.IO.Stream newStream = webRequest.GetRequestStreamAsync().Result;
    //        newStream.Write(byteArray, 0, byteArray.Length);
    //        newStream.Dispose();
    //        HttpWebResponse response;
    //        try
    //        {
    //            response = (HttpWebResponse)webRequest.GetResponseAsync().Result;
    //        }
    //        catch (WebException ex)
    //        {
    //            response = (HttpWebResponse)ex.Response;
    //        }
    //        var data = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
    //        return data;
    //    }


    //}
       
    //     RequestInsuranceOrder _order = new RequestInsuranceOrder();
    //    public BuyInsuranceProcessor(RequestInsuranceOrder  order)
    //    {
    //        _order = order;
    //    }

    //    protected override bool IsBase
    //    {
    //        get
    //        {
    //            return false;
    //        }
    //    }

    //    protected override string RequestAddress
    //    {
    //        get { return "Insure/SaveInsure"; }
    //    }

    //    protected override string ServiceAddress
    //    {
    //        get { return PayCenterBossApiUrl; }
    //    }


    //    protected override Dictionary<string, object> PrepareRequestCore()
    //    {
    //        var request = new Dictionary<string, object>();
    //        request.Add("UserId", _order.UserId);
    //        request.Add("OthOrderCode", _order.OthOrderCode);
    //        request.Add("TotalAmount", _order.TotalAmount);
    //        request.Add("TotalPremium", _order.TotalPremium);
    //        request.Add("StartDate", _order.StartDate);
    //        request.Add("EndDate", _order.EndDate);
    //        request.Add("AppliName", _order.AppliName);
    //        request.Add("IdentifyType", _order.IdentifyType);
    //        request.Add("IdentifyNumber", _order.IdentifyNumber);
    //        request.Add("flightNo", _order.flightNo);
    //        request.Add("flightDate", _order.flightDate);
    //        request.Add("InsuredPersonList", JsonConvert.SerializeObject(_order.InsuredPersonList));
    //        return request;
    //    }
    //}
}
