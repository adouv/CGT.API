using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.Enterprise
{
    /// <summary>
    /// 获取总授信信息请求实体
    /// </summary>
    public class RequestGetEnterpriseOverView : RequestBaseModel
    {
        /// <summary>
        /// 分销商Code
        /// </summary>
        public string PayCenterCode { get; set; }
    }
}
