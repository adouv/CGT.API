namespace CGT.Api.DTO.Boss.UserCenter.Request
{
    /// <summary>
    /// 商户配置修改
    /// </summary>
    public class RequestMerchantsConfigurationSaveModel : RequestBaseModel
    {
        /// <summary>
        /// 当前登录名称
        /// </summary>
        public string loginName;
        /// <summary>
        /// 商户号
        /// </summary>
        public string companyCode;
        /// <summary>
        /// 金主号
        /// </summary>
        public string accountCode;
        /// <summary>
        /// 金主账户类型
        /// </summary>
        public int accountType;
        /// <summary>
        /// 金主业务类型
        /// </summary>
        public int accountBusiType;
        /// <summary>
        /// 商户业务类型
        /// </summary>
        public int busiType;
        /// <summary>
        /// 授信额度
        /// </summary>
        public string creditAmount;
        /// <summary>
        /// 账期
        /// </summary>
        public int billDays;
        /// <summary>
        /// 分销商总限额
        /// </summary>
        public string totalCreditAmount;
        /// <summary>
        /// 差旅费率
        /// </summary>
        public decimal travelRate;
    }
}
