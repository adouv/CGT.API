namespace CGT.Api.DTO.Boss.TravelOrder.MiddleModel {
    /// <summary>
    /// 返回支付信息
    /// </summary>
    public class ResponsePayModel : ResponseBase {
        /// <summary>
        /// 平台订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// 支付流水号
        /// </summary>
        public string TradeNo { get; set; }
    }
}
