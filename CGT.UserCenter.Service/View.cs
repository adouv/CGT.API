using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.UserCenter.Service
{
    public class ViewBase<T>
    {
        /// <summary>
        /// 返回码
        /// </summary> 
        public string errorCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary> 
        public string message { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }
    }

    /// <summary>
    /// 分页基类
    /// </summary>
    public class BasePage
    {
        public int pageCount { get; set; }
        public int totalCount { get; set; }
        public int numPerPage { get; set; }
        public int currentPage { get; set; }
    }

    public class MerchantsQueryResult : BasePage
    {

        public MerchantsInfo[] recordList { get; set; }
    }

    public class MerchantsInfo
    {
        public string createDateTime { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string loginName { get; set; }
        /// <summary>
        /// 商户code值
        /// </summary>
        public string companyCode { get; set; }
        /// <summary>
        /// 商户名称
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 结算账户
        /// </summary>
        public string receivablesAccount { get; set; }
        /// <summary>
        ///结算商户号
        /// </summary>
        public string merchantNo { get; set; }

        /// <summary>
        /// 业务类型 0:机票 1:车贷；2：保险；3：差旅
        /// </summary>
        public int busiType { get; set; }
        /// <summary>
        /// 状态 0:有效 1:无效
        /// </summary>
        public int status { get; set; }
        public string telphone { get; set; }
        public string email { get; set; }

        public  string payAccountName { get; set; }

        public  string payAccountNo { get; set; }

        public int isConfig { get; set; }
    }


    public class MerchantsModifyResult{
    
    }
    /// <summary>
    /// 商户配置查询响应实体
    /// </summary>
    public class MerchantsConfigurationQueryResult : BasePage
    {
        public MerchantsConfigurationQueryModel[] recordList { get; set; }
    }
    /// <summary>
    /// 商户配置查询响应实体
    /// </summary>
    public class MerchantsConfigurationQueryModel
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string loginName { get; set; }
        /// <summary>
        /// 商户Code
        /// </summary>
        public string companyCode { get; set; }
        /// <summary>
        /// 商户名称
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 结算账户
        /// </summary>
        public string receivablesAccount { get; set; }
        /// <summary>
        /// 结算商户号
        /// </summary>
        public string merchantNo { get; set; }
        /// <summary>
        /// 总授信额度
        /// </summary>
        public string creditAmount { get; set; }
        /// <summary>
        /// 利息费率
        /// </summary>
        public string travelRate { get; set; }
        /// <summary>
        /// 账期
        /// </summary>
        public int billDays { get; set; }
        /// <summary>
        /// 账期日期
        /// </summary>
        public string billDateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string createUser { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string updateDateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string updateUser { get; set; }
        /// <summary>
        /// 状态
        /// -1全部 0有效 1无效
        /// </summary>
        public int status { get; set; }
    }
    /// <summary>
    /// 商户配置详情
    /// </summary>
    public class MerchantsConfigurationViewResult
    {
        public int accountBusiType { get; set; }
        public string accountCode { get; set; }
        public int accountType { get; set; }
        public string billDays { get; set; }
        public int busiType { get; set; }
        public string companyCode { get; set; }
        public string createDateTime { get; set; }
        public string createUser { get; set; }
        public string creditAmount { get; set; }
        public int id { get; set; }
        public string merchantNo { get; set; }
        public int status { get; set; }
        public string totalCreditAmount { get; set; }
        public string travelRate { get; set; }
    }
    /// <summary>
    /// 商户配置修改
    /// </summary>
    public class MerchantsConfigurationSaveResult
    { }
}
