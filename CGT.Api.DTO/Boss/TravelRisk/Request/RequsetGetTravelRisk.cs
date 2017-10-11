namespace CGT.Api.DTO.Boss.TravelRisk {
    /// <summary>
    /// 获取风控规则请求实体
    /// </summary>
    public class RequsetGetTravelRisk : RequestBaseModel {
        /// <summary>
        /// 风控规则编号
        /// </summary>
        public int TravelRiskId { get; set; }
    }
}
