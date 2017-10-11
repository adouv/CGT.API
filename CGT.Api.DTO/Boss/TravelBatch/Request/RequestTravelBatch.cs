using System;
using System.ComponentModel.DataAnnotations;
namespace CGT.Api.DTO.Boss.TravelBatch {
    /// <summary>
    /// 差旅批次查询请求实体
    /// </summary>
    public class RequestTravelBatch : RequestBaseModel {
        /// <summary>
        /// 分销Code
        /// </summary>
        public string PayCenterCode { get; set; }
        /// <summary>
        /// 企业编号
        /// </summary>
        public int? EnterpriseId { get; set; }
        /// <summary>
        /// 风控类型
        /// 0黑屏 1员工白名单 2黑屏+白名单
        /// </summary>
        public int? TravelRiskType { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public int? TranslationState { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string TravelBatchId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
