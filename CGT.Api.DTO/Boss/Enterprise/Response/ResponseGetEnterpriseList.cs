using System;

namespace CGT.Api.DTO.Boss.Enterprise {
    /// <summary>
    /// 获取企业列表响应实体
    /// </summary>
    public class ResponseGetEnterpriseList {
        /// <summary>
        /// 企业白名单编号
        /// </summary>
        public long EnterpriseWhiteListID { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 商户子编号
        /// </summary>
        public string PayCenterCode { get; set; }
        /// <summary>
        /// 分销名称
        /// </summary>
        public string PayCenterName { get; set; }
        /// <summary>
        /// 分销共管账号
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// 账期
        /// </summary>
        public string AccountPeriod { get; set; }
        /// <summary>
        /// 信用金额
        /// </summary>
        public decimal CreditAmount { get; set; }
        /// <summary>
        /// 企业状态
        /// </summary>
        public int EnterpriseStatue { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string ModifiedName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }
        /// <summary>
        /// 剩余余额
        /// </summary>
        public decimal AccountBalance { get; set; }
    }
}
