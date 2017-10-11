using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.UserCenter.Request
{
    /// <summary>
    /// 商户配置详情实体
    /// </summary>
    public class RequestMerchantsConfigurationViewModel : RequestBaseModel
    {
        /// <summary>
        /// 商户Code
        /// </summary>
        public string companyCode { get; set; }
    }
}
