using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.TravelBill {
    /// <summary>
    /// 金主还款确认请求实体
    /// </summary>
    public class RequestRepayTravelBillByGold : RequestBaseModel {
        public int billId { get; set; }

    }
}
