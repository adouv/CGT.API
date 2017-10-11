using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.Enterprise
{
    /// <summary>
    /// 获取总授信额度信息返回实体
    /// </summary>
    public class ResponseGetEnterpriseOverView
    {
        /// <summary>
        /// 售信总额度
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// 剩余售信额度
        /// </summary>
        public decimal RemainingCreditLimit { get; set; }

        /// <summary>
        /// 未还款总金额
        /// </summary>
        public decimal OutstandingAmount  { get; set; }

        /// <summary>
        /// 逾期总金额
        /// </summary>
        public decimal OverdueAmout { get; set; }

    }
}
