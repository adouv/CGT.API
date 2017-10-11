using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.Enterprise
{
    /// <summary>
    /// 修改企业授信额度请求实体
    /// </summary>
    public class RequestUpdateEnterpriseCreditAmount : RequestBaseModel
    {
        /// <summary>
        /// 企业编号
        /// </summary>
        public long EnterpriseId { get; set; }

        /// <summary>
        /// 售信金额
        /// </summary>
        public decimal CreditAmount { get; set; }
    }
}
