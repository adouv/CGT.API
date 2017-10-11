using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CGT.Api.DTO.Boss.TravelOrder
{
    /// <summary>
    /// 差旅订单审核结果查询请求类
    /// </summary>
    public class RequestTravelOrderAuditResult : RequestBaseModel
    {
        /// <summary>
        /// 分销商Code
        /// </summary>
        public string PayCenterCode { get; set; }

        /// <summary>
        /// 企业编号
        /// </summary>
        public int? EnterpriseId { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string TravelBatchId { get; set; }

        /// <summary>
        /// 创建开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 创建结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

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
