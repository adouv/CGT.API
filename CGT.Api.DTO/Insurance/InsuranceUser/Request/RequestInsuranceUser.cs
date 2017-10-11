using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Insurance.InsuranceUser.Request
{
    public class RequestInsuranceUser : RequestBaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd { get; set; }
    }
}
