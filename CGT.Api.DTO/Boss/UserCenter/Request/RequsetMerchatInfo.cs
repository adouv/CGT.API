using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.UserCenter.Request
{
 public class RequsetMerchatsInfo: RequestBaseModel
    {

        public  int PageIndex { get; set; }

        public  int PageSize { get; set; }

        public  int Status { get; set; }

        public string CompanyName { get; set; }

        public  string CreateBeginTime { get; set; }

        public  string CreateEndTime { get; set; }
    }
}
