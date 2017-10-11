using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.DTO.Boss.UserCenter.Request
{
    /// <summary>
    /// 
    /// </summary>
    public  class RequestMerchantsConfigurationUpdateModel : RequestBaseModel
    {
        /// <summary>
        /// 当前登录名称
        /// </summary>
        public string loginName { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string companyCode { get; set; }
        /// <summary>
        /// 金主编号（32位随机数）
        /// </summary>
        public string accountCode { get; set; }
        /// <summary>
        /// 垫子类型 1融宝 2廊坊银行
        /// </summary>
        public int accountType { get; set; }
        /// <summary>
        /// 垫子业务类型 0机票 1车贷 2保险 3差旅
        /// </summary>
        public int accountBusiType { get; set; }
        /// <summary>
        /// 商户业务类型 0机票 1车贷 2保险 3差旅
        /// </summary>
        public string busiType { get; set; }
        /// <summary>
        /// 授信额度
        /// </summary>
        public string creditAmount { get; set; }
        /// <summary>
        /// 账期
        /// </summary>
        public int billDays { get; set; }
        /// <summary>
        /// 分销商总限额
        /// </summary>
        public string totalCreditAmount { get; set; }
        /// <summary>
        /// 差旅费率
        /// </summary>
        public string travelRate { get; set; }
    }
}
