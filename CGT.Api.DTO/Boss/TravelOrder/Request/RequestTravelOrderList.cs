using System;
using System.ComponentModel.DataAnnotations;

namespace CGT.Api.DTO.Boss.TravelOrder {
    /// <summary>
    /// 差旅订单列表请求实体
    /// </summary>
    public class RequestTravelOrderList : RequestBaseModel {
        /// <summary>
        /// 企业编号
        /// </summary>
        public int? EnterpriseID { get; set; }
        /// <summary>
        /// 商户子编号
        /// </summary>
        public string PayCenterCode { get; set; }
        /// <summary>
        /// 返现状态
        /// </summary>
        public int? BackStatus { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string TravelBatchId { get; set; }
        /// <summary>
        /// 出票起始时间
        /// </summary>
        public DateTime? TicketTimeBegion { get; set; }
        /// <summary>
        /// 出票结束时间
        /// </summary>
        public DateTime? TicketTimeEnd { get; set; }
        /// <summary>
        ///返现起始时间
        /// </summary>
        public DateTime? BackTimeBegion { get; set; }
        /// <summary>
        ///返现结束时间
        /// </summary>
        public DateTime? BackTimeEnd { get; set; }
        /// <summary>
        ///审核状态
        /// </summary>
        public string ReviewState { get; set; }
        /// <summary>
        ///审核开始时间
        /// </summary>
        public DateTime? ReviewTimeBegion { get; set; }
        /// <summary>
        ///审核结束时间
        /// </summary>
        public DateTime? ReviewTimeEnd { get; set; }
        /// <summary>
        /// 风控状态
        /// </summary>
        public int? TravelRiskState { get; set; }
        /// <summary>
        /// 风控类型
        /// </summary>
        public int? TravelRiskType { get; set; }
       
        /// <summary>
        /// 当前页码
        /// </summary>
        [Required(ErrorMessage = "当前页码Pageindex必填")]
        public int Pageindex { get; set; }

        /// <summary>
        /// 页面尺寸
        /// </summary>
        [Required(ErrorMessage = "页面尺寸Pagesize必填")]
        public int Pagesize { get; set; }
    }
}
