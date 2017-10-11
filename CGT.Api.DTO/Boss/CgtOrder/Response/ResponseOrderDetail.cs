using CGT.Entity.CgtTicketModel;
using System.Collections.Generic;
namespace CGT.Api.DTO.Boss.CgtOrder
{
    /// <summary>
    /// 国际票订单详情返回实体
    /// </summary>
    public class ResponseOrderDetail 
    {
        public IEnumerable<Passenger> PassengerList { get; set; }
        public IEnumerable<Voyage> VoyageList { get; set; }
    }
}
