using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.UserCenter.Request
{
    public class RequsetGoldInfo : RequestBaseModel
    {
        public string merchantNo { get; set; }
        public string loginName { get; set; }
        public string accountName { get; set; }
        public string accountCode { get; set; }

        public  decimal travelProfit { get; set; }

        public  int graceDay { get; set; }

        public string travelGraceRate { get; set; }


        public  string travelPenalty { get; set; }

        public  string companyCode { get; set; }

    }
}
