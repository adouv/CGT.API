using System;
using System.Collections.Generic;
using System.Text;
using PetaPoco.NetCore;

namespace CGT.Api.DTO.Boss.TravelOrder.Response
{
    /// <summary>
    /// 差旅审核订单结果返回类
    /// </summary>
    public class ResponseTravelOrderAuditResult
    {
        /// <summary>
        /// 批次号
        /// </summary>
        [ResultColumn]
        public string TravelBatchId { get; set; }

        /// <summary>
        /// 分销商名称
        /// </summary>
        [ResultColumn]
        public string PayCenterName { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [ResultColumn]
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 订单总数
        /// </summary>
        [ResultColumn]
        public int OrderCount { get; set; }

        /// <summary>
        /// 审核通过订单数量
        /// </summary>
        [ResultColumn]
        public int SucAuditCount { get; set; }

        /// <summary>
        /// 审核拒单订单数量
        /// </summary>
        [ResultColumn]
        public int FailAuditCount { get; set; }

        /// <summary>
        /// 审核已返现订单数量
        /// </summary>
        [ResultColumn]
        public int IsBackAuditCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ResultColumn]
        public DateTime CreateTime { get; set; }
    }
}
