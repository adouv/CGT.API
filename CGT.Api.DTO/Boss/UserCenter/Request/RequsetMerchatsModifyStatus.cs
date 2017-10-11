using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.UserCenter.Request
{

    public class RequsetMerchatsModifyStatus : RequestBaseModel
    {
        public int status { get; set; }
        public string companyCode { get; set; }
    }
}
