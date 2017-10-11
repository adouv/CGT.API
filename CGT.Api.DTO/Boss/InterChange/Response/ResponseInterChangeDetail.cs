using CGT.Entity.CgtTicketModel;
using System.Collections.Generic;
namespace CGT.Api.DTO.Boss.InterChange
{
    /// <summary>
    /// 国际票改期详情返回实体
    /// </summary>
    public class ResponseInterChangeDetail 
    {
        public IEnumerable<InterChangeRemark> InterChangeRemarkList { get; set; }

        public IEnumerable<Passenger> PassengerList { get; set; }

        public IEnumerable<Voyage> VoyageList { get; set; }

        public dynamic OrderModel { get; set; }

        public Entity.CgtModel.UserAccount UserAccountModel { get; set; }
    }
}
