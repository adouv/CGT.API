using System.ComponentModel.DataAnnotations;

namespace CGT.Api.DTO.Boss.TravelRisk {
    /// <summary>
    /// 获取风控列表请求实体
    /// </summary>
    public class RequsetGetTravelRiskList : RequestBaseModel {
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
        /// <summary>
        /// 风控规则类型
        /// </summary>
        public int? TravelRiskType { get; set; }
        /// <summary>
        /// 风控规则状态
        /// </summary>
        public int? TravelRiskState { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 分销名称
        /// </summary>
        public string PayCenterName { get; set; }
        /// <summary>
        /// 分销名称
        /// </summary>
        public string PayCenterCode { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public int? EnterpriseId { get; set; }

    }
}
