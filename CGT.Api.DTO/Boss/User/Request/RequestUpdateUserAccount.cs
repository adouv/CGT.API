

namespace CGT.Api.DTO.Boss.User {
    /// <summary>
    /// 分销用户修改请求实体
    /// </summary>
    public class RequestUpdateUserAccount : RequestBaseModel {
        /// <summary>
        ///用户code
        /// </summary>
        public string PayCenterCode { get; set; }
        /// <summary>
        /// 宽限期天数
        /// </summary>
        public int? GraceDay { get; set; }
        /// <summary>
        /// 修改名称
        /// </summary>
        public string ModifyName { get; set; }
        /// <summary>
        /// 宽限期费率
        /// </summary>
        public decimal? GraceBate { get; set; }
        /// <summary>
        /// 逾期费率
        /// </summary>
        public decimal? OverdueBate { get; set; }
        /// <summary>
        /// 商户code
        /// </summary>
        public string MerchantCode { get; set; }
        /// <summary>
        /// 分销商授信额度
        /// </summary>
        public decimal? CreditAmount { get; set; }
        /// <summary>
        /// 差旅分销商利率
        /// </summary>
        public decimal? FactoringInterestRate { get; set; }

    }
}
