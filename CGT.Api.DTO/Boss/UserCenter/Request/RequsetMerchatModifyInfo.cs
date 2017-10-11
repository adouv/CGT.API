using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.UserCenter.Request
{
    public class RequsetMerchatsModifyInfo : RequestBaseModel
    {
        public string loginName { get; set; }
        public string password { get; set; }
        public string companyCode { get; set; }
        public string companyName { get; set; }
        public string receivablesAccount { get; set; }
        public string merchantNo { get; set; }
        public string telphone { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string reapalPassword { get; set; }
        public string contactPerson { get; set; }
    }
}
