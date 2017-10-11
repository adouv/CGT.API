namespace CGT.Api.DTO.Boss.TravelRisk {
    /// <summary>
    /// 风控规则更新请求实体
    /// </summary>
    public class RequsetUpdateTravelRisk : RequestBaseModel {
        /// <summary>
        ///商户子编码
        /// </summary>
        public int TravelRiskId { get; set; }
        /// <summary>
        ///风控类型
        /// </summary>
        public int TravelRiskType { get; set; }
        /// <summary>
        ///风控状态
        /// </summary>
        public int TravelRiskState { get; set; }
        /// <summary>
        /// 黑屏通过比率
        /// </summary>
        public decimal EtermSuccessRate { get; set; }
        /// <summary>
        /// 黑屏拒绝失败率
        /// </summary>
        public decimal EtermFailRate { get; set; }
        /// <summary>
        /// 白名单通过比率
        /// </summary>
        public decimal WhiteSuccessRate { get; set; }
        /// <summary>
        /// 白名单拒绝失败率
        /// </decimal>
        public decimal? WhiteFailRate { get; set; }
        /// <summary>
        /// 修改人编号
        /// </summary>
        public long ModifyUserId { get; set; }
    }
}
