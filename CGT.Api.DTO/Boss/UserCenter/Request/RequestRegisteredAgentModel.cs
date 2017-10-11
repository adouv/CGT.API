using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.UserCenter.Request
{
    public class RequestRegisteredAgentModel : RequestBaseModel
    {
        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }
        /// <summary>
        /// 邮箱账号
        /// </summary>
        public string ReapayMerchantNo { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 融宝企业编号
        /// </summary>
        public string ReapalMerchantId { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string MerchantPwd { get; set; }
        /// <summary>
        /// 融宝企业登录密码
        /// </summary>
        public string ReapalMerchantPwd { get; set; }


    }
}
