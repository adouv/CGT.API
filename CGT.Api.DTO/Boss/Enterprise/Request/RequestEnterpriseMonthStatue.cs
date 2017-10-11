using System;
using System.ComponentModel.DataAnnotations;

namespace CGT.Api.DTO.Boss.Enterprise
{

    public class RequestEnterpriseMonthStatue : RequestBaseModel
    {
      public long EnterpriseWhiteListID { get; set; }

        public int? MonthStatue { get; set; }

       public decimal? CreditMonthAmount { get; set; }

        public int? EnterpriseStatue { get; set; }

        public int? FreezeWay { get; set; }

        public string ModifyName { get; set; }
    }
}
