using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Entity.CgtTravelModel
{
    /// <summary>
    /// 风控日志
    /// </summary>
    public partial class TravelBatch
    {
        /// <summary>
        /// 当前页
        /// </summary>
        [ResultColumn]
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        [ResultColumn]
        public int PageSize { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [ResultColumn]
        public string StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [ResultColumn]
        public string EndDate { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        [ResultColumn]
        public int? OrderCount { get; set; }

        /// <summary>
        /// 审核成功订单数量
        /// </summary>
        [ResultColumn]
        public int? SucAuditCount { get; set; }

        /// <summary>
        /// 审核失败订单数量
        /// </summary>
        [ResultColumn]
        public int? FailAuditCount { get; set; }

        /// <summary>
        /// 审核返现成功数量
        /// </summary>
        [ResultColumn]
        public int? IsBackAuditCount { get; set; }
    }
}
