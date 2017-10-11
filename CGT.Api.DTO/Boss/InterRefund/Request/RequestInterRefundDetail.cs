using CGT.Entity.CgtTicketModel;
using System.Collections.Generic;
using System.Linq;
namespace CGT.Api.DTO.Boss.InterRefund
{
    /// <summary>
    /// 国际票退票详情请求实体
    /// </summary>
    public class RequestInterRefundDetail : RequestBaseModel
    {
        /// <summary>
        /// 本地订单号
        /// </summary>
        public string LocalId { get; set; }

        /// <summary>
        /// 融宝账号
        /// </summary>
        public string ReapalAccount { get; set; }

    }
}
