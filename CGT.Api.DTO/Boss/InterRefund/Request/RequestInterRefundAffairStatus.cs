namespace CGT.Api.DTO.Boss.InterRefund
{
    /// <summary>
    /// 国际票退票详情请求实体
    /// </summary>
    public class RequestInterRefundAffairStatus: RequestBaseModel
    {
        /// <summary>
        /// 本地订单号
        /// </summary>
        public string LocalId { get; set; }
        /// <summary>
        /// 锁单状态 0-未锁单 ；1-未锁单
        /// </summary>
        public int AffairStatus { get; set; }
    }
}
