using CGT.Entity.CgtTicketModel;
using System.Collections.Generic;
namespace CGT.Api.DTO.Boss.InterRefund
{
    /// <summary>
    /// 国际票退票详情返回实体
    /// </summary>
    public class ResponseInterRefundDetail 
    {
        public IEnumerable<InterRefundRemark> InterRefundRemarkList { get; set; }

        public IEnumerable<Passenger> PassengerList { get; set; }

        public IEnumerable<Voyage> VoyageList { get; set; }

        public dynamic OrderModel { get; set; }

        public Entity.CgtModel.UserAccount UserAccountModel { get; set; }
    }
}
