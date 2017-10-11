using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.TravelOrder{
    /// <summary>
    /// 审核差旅请求实体
    /// </summary>
    public class RequestReviweTravelOrder: RequestBaseModel {
        /// <summary>
        /// 审核差旅订单号
        /// </summary>
        public string EOrderIds { get; set; }
        /// <summary>
        /// 审核状态  审核状态 0 未审核  1审核成功  2审核失败  3待审核 4不审核
        /// </summary>
        public int ReviewState { get; set; }
        /// <summary>
        /// 拒绝失败原因
        /// </summary>
        public string RefuseReason { get; set; }
        /// <summary>
        /// 审核人编号
        /// </summary>
        public int ReviewUserId { get; set; }

    }
}
