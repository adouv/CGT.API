namespace CGT.Api.DTO.Boss.TravelRisk {
    /// <summary>
    /// 风控规则添加请求实体
    /// </summary>
    public class RequsetAddTravelRisk : RequestBaseModel {
        /// <summary>
        ///风控编号
        /// </summary>
        public int TravelRiskId { get; set; }
        /// <summary>
        ///企业编号
        /// </summary>
        public int EnterpriseID { get; set; }
        /// <summary>
        ///风控类型
        /// </summary>
        public int TravelRiskType { get; set; }
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
        public decimal WhiteFailRate { get; set; }
        /// <summary>
        /// 差旅风控状态
        /// </decimal>
        public int TravelRiskState { get; set; }
        /// <summary>
        /// 创建人编号
        /// </summary>
        public long CreateUserId { get; set; }
        /// <summary>
        /// 修改人编号
        /// </summary>
        public long ModifyUserId { get; set; }
        /// <summary>
        /// 商户子编号
        /// </summary>
        public string PayCenterCode { get; set; }
        /// <summary>
        /// 分销名称
        /// </summary>
        public string PayCenterName { get; set; }
        /// <summary>
        /// 最低上传数量
        /// </summary>
        public int UploadLowCount { get; set; }
        /// <summary>
        /// 票价倍数
        /// </summary>
        public int TicketMultiple { get; set; }
    }
}
