using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Insurance.InsuranceOrder.Request
{
    public class RequestQueryInsuranceOrder : RequestBaseModel
    {
        public long? UserId { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
        public string OthOrderCode { get; set; }

        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
