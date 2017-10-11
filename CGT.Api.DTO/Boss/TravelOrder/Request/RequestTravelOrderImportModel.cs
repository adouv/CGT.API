using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.TravelOrder.Request
{
    /// <summary>
    /// 差旅订单导入
    /// </summary>
    public class RequestTravelOrderImportModel : RequestBaseModel
    {
        /// <summary>
        /// 商户code 
        /// </summary>
        public string MerchantCode { get; set; }


        public string PayCenterCode { get; set; }


        public string PayCenterName { get; set; }


        
    }
}
