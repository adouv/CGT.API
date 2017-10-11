using System;
using System.Collections.Generic;

namespace CGT.PayCenter.Service {
    /// <summary>
    /// 基类
    /// </summary>
    public class ViewBase {
        /// <summary>
        /// 返回码
        /// </summary> 
        public string result_code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary> 
        public string result_msg { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string data { get; set; }
    }
    /// <summary>
    /// 保险支付
    /// </summary>
    public class InsurancePayView {
        /// <summary>
        /// 融宝交易流水号
        /// </summary>
        public string reapal_order_no { get; set; }
        /// <summary>
        /// 平台订单id
        /// </summary>
        public string orderid { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public string amount { get; set; }
    }

    /// <summary>
    /// 支付
    /// </summary>
    public class PayView {
        /// <summary>
        /// 融宝交易流水号
        /// </summary>
        public string reapal_order_no { get; set; }
        /// <summary>
        /// 平台订单id
        /// </summary>
        public string orderid { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 是否挂起
        /// </summary>
        public string hangUpStatus { get; set; }
        /// <summary>
        /// 挂起设置
        /// </summary>
        public string ticketHangUpStatus { get; set; }
        /// <summary>
        /// 解挂设置
        /// </summary>
        public string ticketHangSolutionStatus { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public string counterFee { get; set; }
        /// <summary>
        /// 授信比例
        /// </summary>
        public string solutionRate { get; set; }
        /// <summary>
        /// 利息费率
        /// </summary>
        public string interestRate { get; set; }
        /// <summary>
        /// 罚息费率
        /// </summary>
        public string penaltyRate { get; set; }
        /// <summary>
        /// 金主名称
        /// </summary>
        public string accountName { get; set; }
    }

    /// <summary>
    /// 询价
    /// </summary>
    public class InquiryPriceView {
        /// <summary>
        /// 余额
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 是否挂起
        /// </summary>
        public string hangUpStatus { get; set; }
        /// <summary>
        /// 挂起设置
        /// </summary>
        public string ticketHangUpStatus { get; set; }
    }
    /// <summary>
    /// 获取用户账单逾期费率
    /// </summary>
    public class UserAccountBalanceBillFeeView {
        /// <summary>
        /// 费率
        /// </summary>
        public decimal balanceBillFee { get; set; }

    }


    public class InsuranceInquiryPriceView {
        public string payStatus { get; set; }
        public string insuranceStatus { get; set; }

    }
    /// <summary>
    /// 返现
    /// </summary>
    public class ReMoney {
        public string ip { get; set; }
        public string remark { get; set; }
        public string orderId { get; set; }
        /// <summary>
        /// 乘机人
        /// </summary>
        public List<Passenger> passenger { get; set; }
    }

    /// <summary>
    /// 支付/返现/还款实体
    /// </summary>
    public class OrderInfo {
        //还款时候订单号格式：1234,123,123,123
        public string orderId { get; set; }
        public string orderIds { get; set; }
        public string ip { get; set; }
        public string remark { get; set; }
        public string notifyUrl { get; set; }
        public string returnUrl { get; set; }
        public string pnr { get; set; }
        public string ticketPrice { get; set; }

        ///乘机人信息
        public List<Passenger> passenger { get; set; }
        ///航段信息
        public Voyage voyage { get; set; }
    }
    /// <summary>
    /// 乘机人
    /// </summary>
    public class Passenger {
        public string passengerName { get; set; }
        public int passengerType { get; set; }
        public string certificateNumber { get; set; }
        public int certificateType { get; set; }
        public string birthday { get; set; }
        public string airTicketNo { get; set; }
        public int tickettype { get; set; }
        public string officeno { get; set; }

    }

    /// <summary>
    /// 航段
    /// </summary>
    public class Voyage {
        public string departure { get; set; }
        public string arrival { get; set; }
        public string departureTime { get; set; }
        public string arrivalTime { get; set; }
        public string bunk { get; set; }
        public string airline { get; set; }
        public string flightNo { get; set; }
    }


    /// <summary>
    /// 保险返现
    /// </summary>
    public class InsuranceReMoney {
        //public string ip { get; set; }
        /// <summary>
        /// 融宝用户编号
        /// </summary>
        public string personalMerchantNo { get; set; }

        /// <summary>
        /// 订单金额（不包含通道手续费的订单金额）
        /// </summary>
        public string amount { get; set; }

        /// <summary>
        /// 返现比率
        /// </summary>
        public string remoneyRate { get; set; }

        /// <summary>
        /// 返现账户
        /// </summary>
        public string returnAmountAccount { get; set; }

        /// <summary>
        /// 账户类型
        /// </summary>
        public string accountType { get; set; }

        /// <summary>
        /// 商户code
        /// </summary>
        public string companyCode { get; set; }

        /// <summary>
        /// 帐期
        /// </summary>
        public string accountDate { get; set; }

        /// <summary>
        /// 保单号
        /// </summary>
        public string insuranceNo { get; set; }

        /// <summary>
        /// 订单信息人
        /// </summary>
        public InsuranceOrderInfo orderInfo { get; set; }
    }

    /// <summary>
    ///保险 支付/返现/还款实体
    /// </summary>
    public class InsuranceOrderInfo {

        public string orderId { get; set; }
        public string remark { get; set; }
        public string ip { get; set; }
    }
    /// <summary>
    /// 保单 退款实体
    /// </summary>
    public class InsuranceRefundView {
        /// <summary>
        /// 流水号
        /// </summary>
        public string reapal_order_no { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 商户COde
        /// </summary>x
        public string companyCode { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string sellerEmail { get; set; }
        /// <summary>
        /// 是否全额
        /// </summary>
        public int IsFull { get; set; }
        /// <summary>
        /// 订单信息
        /// </summary>
        public InsuranceRefundOrder orderInfo { get; set; }
    }
    /// <summary>
    /// 订单信息实体类
    /// </summary>
    public class InsuranceRefundOrder {
        /// <summary>
        /// 订单号
        /// </summary>x
        public string orderId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        public string ip { get; set; }
    }

    /// <summary>
    ///还款实体
    /// </summary>
    public class InsuranceBillPayOrderInfo {

        public string orderIds { get; set; }
        public string remark { get; set; }
        public string ip { get; set; }
    }


    /// <summary>
    /// 分页基类
    /// </summary>
    public class BasePage {
        public int pageCount { get; set; }
        public int totalCount { get; set; }
        public int numPerPage { get; set; }
        public int currentPage { get; set; }
    }




    /// <summary>
    /// 兜底资金列表实体
    /// </summary>
    public class FundsRevealListView : BasePage {
        public List<FundsRevealList> recordList { get; set; }
    }

    /// <summary>
    /// 兜底资金信息
    /// </summary>
    public class FundsRevealList {
        /// <summary>
        /// 逾期日期
        /// </summary>
        public string billDateTime { get; set; }
        /// <summary>
        /// 逾期总金额
        /// </summary>
        public decimal penaltyAmount { get; set; }
        /// <summary>
        /// 商户名称
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 账单号
        /// </summary>
        public string billId { get; set; }
        /// <summary>
        /// 金主信息集合
        /// </summary>
        public List<RevealList> list { get; set; }
    }
    /// <summary>
    /// 金主信息
    /// </summary>
    public class RevealList {
        /// <summary>
        /// 金主
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 逾期金额
        /// </summary>
        public string reAmount { get; set; }
        /// <summary>
        /// 兜底资金金额
        /// </summary>
        public string revealAmount { get; set; }
        /// <summary>
        /// 启动兜底资金
        /// </summary>
        public string revealAmountStartTime { get; set; }
        /// <summary>
        /// 是否还款
        /// </summary>
        public string statusStr { get; set; }
        /// <summary>
        ///还款状态
        /// </summary>
        public int status { get; set; }


    }

    /// <summary>
    /// 利息列表
    /// </summary>
    public class InterestListView {
        public List<Interest> recordList { get; set; }
    }

    /// <summary>
    /// 利息明细
    /// </summary>
    public class Interest {
        /// <summary>
        /// 垫资名称
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 利息
        /// </summary>
        public string costAmount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDateTimeStr { get; set; }
        /// <summary>
        /// 每天利息和
        /// </summary>
        public string sumInterestAmount { get; set; }
        /// <summary>
        /// 1：已经扣款;0:未审核
        /// </summary>
        public string status { get; set; }
    }



    #region 交易接口查询实体
    /// <summary>
    /// 交易接口查询实体
    /// </summary>
    public class TradeInquiryListView : BasePage {
        public List<TradeInquiryList> recordList { get; set; }
    }

    /// <summary>
    /// 交易接口查询
    /// </summary>
    public class TradeInquiryList {
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDateTime { get; set; }
        /// <summary>
        ///订单号
        /// </summary>
        public string companyOrderId { get; set; }
        /// <summary>
        /// 逾期金额
        /// </summary>
        public decimal penaltyAmount { get; set; }
        /// <summary>
        /// 付款账户名称(个人/商户名称)
        /// </summary>
        public string payName { get; set; }
        /// <summary>
        /// 收款账户名称
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string reapalNo { get; set; }
        /// <summary>
        /// 付款账户
        /// </summary>
        public string payAccount { get; set; }
        /// <summary>
        /// 收款账户
        /// </summary>
        public string merchantNo { get; set; }
        /// <summary>
        /// 金额|明细
        /// </summary>
        public decimal incomeAmount { get; set; }
        /// <summary>
        /// 入金类型
        /// </summary>
        public string incomeTypeStr { get; set; }
    }

    #endregion

    #region 金主查询接口
    public class GlodInquiryListView : BasePage {
        public List<GlodInquiryList> recordList { get; set; }
    }
    public class GlodInquiryList {
        /// <summary>
        /// 金主名称
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 金主类型（1融宝   2廊坊）
        /// </summary>
        public string accountType { get; set; }
        /// <summary>
        /// Code关联时用
        /// </summary>
        public string accountCode { get; set; }

    }
    #endregion

    #region 垫资商户查询
    public class QueryAccountListView : BasePage {
        public List<QueryAccountList> recordList { get; set; }
    }
    public class QueryAccountList {
        /// <summary>
        /// 金主名称
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 金主类型（1融宝   2廊坊）
        /// </summary>
        public string accountType { get; set; }
        /// <summary>
        /// Code关联时用
        /// </summary>
        public string accountCode { get; set; }
        /// <summary>
        /// 垫款商户属性
        /// </summary>
        public string attributes { get; set; }

    }
    #endregion

    #region 商户垫资历史记录查询
    public class DayMgrHistoryListView : BasePage {
        public List<DayMgrHistoryList> recordList { get; set; }
    }
    public class DayMgrHistoryList {
        public string createDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string accountCode { get; set; }
        /// <summary>
        /// 金主名称
        /// </summary>
        public string companyCode { get; set; }
        /// <summary>
        //
        /// </summary>
        public string originHtml { get; set; }
    }
    #endregion

    #region 当前商户设置的天数查询
    public class DaySetOnlySelfListView : BasePage {

        public List<DaySetOnlySelfList> List { get; set; }
        public List<int> indexList { get; set; }
    }
    public class DaySetOnlySelfList {
        public string createDateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accountCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string dayNumber { get; set; }
        public int isDel { get; set; }
        public string limtDay { get; set; }
        public string attributes { get; set; }
        public string accountType { get; set; }
    }
    #endregion

    #region 保存商户设置的天数
    public class SaveDaysListView : BasePage {
        public string msg { get; set; }
        public string code { get; set; }
    }

    #endregion

    #region 金主垫资
    public class GoldElectronicInfoListView : BasePage {
        /// <summary>
        /// 今日可用额度
        /// </summary>
        public double useAmount { get; set; }
        /// <summary>
        /// 剩余总额度
        /// </summary>
        public double totalAmount { get; set; }
        /// <summary>
        /// 剩余利息
        /// </summary>
        public double interestAmount { get; set; }
        /// <summary>
        /// 日息
        /// </summary>
        public string costAmount { get; set; }
        /// <summary>
        /// 违约罚息
        /// </summary>
        public string defaultPenalty { get; set; }
        /// <summary>
        /// 兜底资金比例
        /// </summary>
        public string revealAmountProp { get; set; }

        public string accountName { get; set; }
    }

    #endregion

    #region 充值实体
    public class RechargeInquiryView : BasePage {
        public string msg { get; set; }
        public string code { get; set; }
    }

    #endregion

    #region 出金查询
    public class GoldExpendInquiryListView : BasePage {
        public string endPageIndex { get; set; }
        public string countResultMap { get; set; }
        public string beginPageIndex { get; set; }
        public List<GoldExpendInquiryList> recordList { get; set; }
    }
    public class GoldExpendInquiryList {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createDateTime { get; set; }
        /// <summary>
        /// 业务类型描述
        /// </summary>
        public string busiTypeStr { get; set; }
        /// <summary>
        /// 支付账户名称
        /// </summary>
        public string payName { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 融宝交易号
        /// </summary>
        public string reapalNo { get; set; }
        /// <summary>
        /// 错误编码
        /// </summary>
        public string errCode { get; set; }


        /// <summary>
        /// 支付账户
        /// </summary>
        public string payAccount { get; set; }
        /// <summary>
        /// 出金类型描述
        /// </summary>
        public string expenditureTypeStr { get; set; }
        /// <summary>
        /// 商户名称
        /// </summary>
        public string companyName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double expenditureAmount { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public string payOrderNo { get; set; }
        /// <summary>
        /// 商户code
        /// </summary>
        public string companyCode { get; set; }


        /// <summary>
        ///编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///账户名称
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public int busiType { get; set; }

        /// <summary>
        ///手续费
        /// </summary>
        public double expenditureFee { get; set; }

        /// <summary>
        ///外部定单号
        /// </summary>
        public string companyOrderId { get; set; }

        /// <summary>
        ///融宝商户编号
        /// </summary>
        public string merchantNo { get; set; }



        /// <summary>
        ///步骤号
        /// </summary>
        public string stepNo { get; set; }

        /// <summary>
        ///出金类型
        /// </summary>
        public int expenditureType { get; set; }

    }
    #endregion

    #region 出金查询导出
    public class GoldExpendExportList {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createDateTime { get; set; }
        /// <summary>
        /// 支付交易号
        /// </summary>
        public string payMerchantNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 出金类型描述
        /// </summary>
        public string expenditureTypeStr { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public string payOrderNo { get; set; }

        /// <summary>
        ///编号
        /// </summary>
        public string Id { get; set; }

        public string parentCompanyCode { get; set; }

        public string balance { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public int busiType { get; set; }


        public string feeType { get; set; }

        public string receivablesAccount { get; set; }

        public string feeValue { get; set; }

        public string busiTypeStr { get; set; }

        public string payName { get; set; }

        public string accountNote { get; set; }
        /// <summary>
        /// 融宝交易号
        /// </summary>
        public string reapalNo { get; set; }
        /// <summary>
        /// 错误编码
        /// </summary>
        public string errCode { get; set; }

        /// <summary>
        /// 支付账户
        /// </summary>
        public string payAccount { get; set; }

        public string productInfo { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string companyName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double expenditureAmount { get; set; }

        /// <summary>
        /// 商户code
        /// </summary>
        public string companyCode { get; set; }

        /// <summary>
        ///账户名称
        /// </summary>
        public string accountName { get; set; }

        /// <summary>
        ///手续费
        /// </summary>
        public double expenditureFee { get; set; }

        /// <summary>
        ///外部定单号
        /// </summary>
        public string companyOrderId { get; set; }

        /// <summary>
        ///融宝商户编号
        /// </summary>
        public string merchantNo { get; set; }

        /// <summary>
        ///步骤号
        /// </summary>
        public string stepNo { get; set; }

        /// <summary>
        ///出金类型
        /// </summary>
        public int expenditureType { get; set; }

    }
    #endregion

    #region 入金查询
    public class GoldIncomeInquiryListView : BasePage {

        public string endPageIndex { get; set; }
        public string countResultMap { get; set; }
        public string beginPageIndex { get; set; }
        public List<GoldIncomeInquiryList> recordList { get; set; }
    }
    public class GoldIncomeInquiryList {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createDateTime { get; set; }
        /// <summary>
        /// 业务类型描述
        /// </summary>
        public string busiTypeStr { get; set; }
        /// <summary>
        /// 入金类型描述
        /// </summary>
        public string incomeTypeStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PNR { get; set; }


        /// <summary>
        /// 支付账户名称
        /// </summary>
        public string payName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 融宝交易号
        /// </summary>
        public string reapalNo { get; set; }

        /// <summary>
        ///支付账户
        /// </summary>
        public string payAccount { get; set; }
        /// <summary>
        /// 商户名称
        /// </summary>
        public string companyName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double incomeAmount { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public string payOrderNo { get; set; }
        /// <summary>
        /// 商户code
        /// </summary>
        public string companyCode { get; set; }


        /// <summary>
        ///编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///账户名称
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public int busiType { get; set; }


        /// <summary>
        ///外部定单号
        /// </summary>
        public string companyOrderId { get; set; }

        /// <summary>
        ///融宝商户编号
        /// </summary>
        public string merchantNo { get; set; }


        /// <summary>
        ///步骤号
        /// </summary>
        public string stepNo { get; set; }

        /// <summary>
        ///入金类型
        /// </summary>
        public int incomeType { get; set; }

        /// <summary>
        ///入金手续费
        /// </summary>
        public double incomeFee { get; set; }

    }
    #endregion

    #region 入金查询下载
    public class GoldIncomeExportList {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createDateTime { get; set; }

        /// <summary>
        /// 支付交易号
        /// </summary>
        public string payMerchantNo { get; set; }
        /// <summary>
        /// 业务类型描述
        /// </summary>
        public string busiTypeStr { get; set; }

        /// <summary>
        /// 支付账户名称
        /// </summary>
        public string payName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 融宝交易号
        /// </summary>
        public string reapalNo { get; set; }

        /// <summary>
        /// 错误编码
        /// </summary>
        public int errCode { get; set; }

        /// <summary>
        ///支付账户
        /// </summary>
        public string payAccount { get; set; }

        /// <summary>
        /// 出金类型描述
        /// </summary>
        public string expenditureTypeStr { get; set; }


        /// <summary>
        /// 商户名称
        /// </summary>
        public string companyName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double expenditureAmount { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public string payOrderNo { get; set; }
        /// <summary>
        /// 商户code
        /// </summary>
        public string companyCode { get; set; }

        /// <summary>
        ///编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///账户名称
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public int busiType { get; set; }

        /// <summary>
        ///入金手续费
        /// </summary>
        public double incomeFee { get; set; }

        /// <summary>
        ///外部定单号
        /// </summary>
        public string companyOrderId { get; set; }

        /// <summary>
        ///融宝商户编号
        /// </summary>
        public string merchantNo { get; set; }


        /// <summary>
        ///步骤号
        /// </summary>
        public string stepNo { get; set; }

        /// <summary>
        ///入金类型
        /// </summary>
        public int incomeType { get; set; }
        /// <summary>
        /// 入金金额
        /// </summary>
        public double incomeAmount { get; set; }

    }
    #endregion

    #region 利息管理下载
    public class InterestInfoExportListView {
        public List<InterestInfoExportList> recordList { get; set; }
    }
    public class InterestInfoExportList {

        /// <summary>
        /// 日期
        /// </summary>
        public string createDateTimeStr { get; set; }
        /// <summary>
        /// 金主
        /// </summary>
        public string accountName { get; set; }


        /// <summary>
        /// 每天利息和
        /// </summary>
        public decimal sumInterestAmount { get; set; }
        /// <summary>
        /// 利息
        /// </summary>
        public string costAmount { get; set; }
        /// <summary>
        /// 1：已经扣款;0:未审核
        /// </summary>
        public int tatus { get; set; }
    }
    #endregion

    #region 订单关联查询

    public class OrderAssociateListView : BasePage {
        public List<OrderAssociateList> recordList { get; set; }
    }
    public class OrderAssociateList {
        /// <summary>
        /// 起飞时间
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public double payPrice { get; set; }
        /// <summary>
        /// 乘机人数
        /// </summary>
        public int passengerNum { get; set; }
        /// <summary>
        /// 还款状态
        /// </summary>
        public int reStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderAssociate> inList { get; set; }
        /// <summary>
        /// 还款状态描述
        /// </summary>
        public string reStatusStr { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string offAndLandPlace { get; set; }
        /// <summary>
        ///  出发开始日期
        /// </summary>
        public string departureStartDate { get; set; }
        /// <summary>
        /// PNR编码
        /// </summary>
        public string PNR { get; set; }
        /// <summary>
        /// 创建结束时间
        /// </summary>
        public string creDateEnd { get; set; }
        /// <summary>
        /// 创建开始时间
        /// </summary>
        public string creDateStart { get; set; }
        /// <summary>
        /// 是否返现
        /// </summary>
        public int isRebate { get; set; }
        /// <summary>
        /// 商户Code
        /// </summary>
        public string companyCode { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public string payOrderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string ticketNo { get; set; }
        /// <summary>
        /// 返现金额
        /// </sum商户订单号mary>
        public string companyOrderId { get; set; }
        /// <summary>
        /// 是否返现描述
        /// </summary>
        public string rebatePrice { get; set; }
        /// <summary>
        /// 是否返现描述
        /// </summary>
        public string isRebateStr { get; set; }
        /// <summary>
        /// 返现金额
        /// </summary>
        public string rebateStatus { get; set; }
        /// <summary>
        /// 票面价格
        /// </summary>
        public double ticketPrice { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public double orderPrice { get; set; }
        /// <summary>
        /// 起飞结束日期
        /// </summary>
        public string departureEndDate { get; set; }
        /// <summary>
        /// 账户号
        /// </summary>
        public string merchantNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDate { get; set; }
        /// <summary>
        /// 父级code
        /// </summary>
        public string parentCode { get; set; }
    }



    public class OrderAssociate {
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDateTime { get; set; }
        /// <summary>
        /// 支付账户编号
        /// </summary>
        public object payMerchantNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 入金类型描述
        /// </summary>
        public string incomeTypeStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object PNR { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public object payOrderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 父级商户code
        /// </summary>
        public object parentCompanyCode { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public object balance { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public int busiType { get; set; }
        /// <summary>
        /// 入金金额
        /// </summary>
        public float incomeAmount { get; set; }
        /// <summary>
        /// 手续费类型
        /// </summary>
        public int feeType { get; set; }
        /// <summary>
        /// 入金类型
        /// </summary>
        public int incomeType { get; set; }
        /// <summary>
        /// 垫资账户
        /// </summary>
        public object receivablesAccount { get; set; }
        /// <summary>
        /// 费率值
        /// </summary>
        public object feeValue { get; set; }
        /// <summary>
        /// 入金手续费
        /// </summary>
        public float incomeFee { get; set; }
        /// <summary>
        /// 业务类型备注
        /// </summary>
        public string busiTypeStr { get; set; }
        /// <summary>
        /// 支付账户名
        /// </summary>
        public string payName { get; set; }
        /// <summary>
        /// 账户备注
        /// </summary>
        public string accountNote { get; set; }
        /// <summary>
        /// 融宝交易号
        /// </summary>
        public string reapalNo { get; set; }
        /// <summary>
        /// 支付帐号
        /// </summary>
        public string payAccount { get; set; }
        /// <summary>
        /// 明细
        /// </summary>
        public string productInfo { get; set; }
        /// <summary>
        /// 商户名称
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 商户code
        /// </summary>
        public string companyCode { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string companyOrderId { get; set; }
        /// <summary>
        /// 账户编号
        /// </summary>
        public string merchantNo { get; set; }
        /// <summary>
        /// 步骤
        /// </summary>
        public string stepNo { get; set; }
    }
    #endregion

    #region 订单查询
    public class OrderListView : BasePage {

        public string endPageIndex { get; set; }
        public string countResultMap { get; set; }
        public string beginPageIndex { get; set; }
        public List<OrderList> recordList { get; set; }
    }
    public class OrderList {
        /// <summary>
        /// 
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public double payPrice { get; set; }
        /// <summary>
        /// 乘机人数
        /// </summary>
        public int passengerNum { get; set; }
        /// <summary>
        /// 还款状态
        /// </summary>
        public int reStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inList { get; set; }
        /// <summary>
        /// 还款状态描述
        /// </summary>
        public string reStatusStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string offAndLandPlace { get; set; }
        /// <summary>
        /// 起飞开始时间
        /// </summary>
        public string departureStartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PNR { get; set; }
        /// <summary>
        /// 创建结束时间
        /// </summary>
        public string creDateEnd { get; set; }
        /// <summary>
        /// 创建开始时间
        /// </summary>
        public string creDateStart { get; set; }
        /// <summary>
        /// 是否返现
        /// </summary>
        public int isRebate { get; set; }
        /// <summary>
        /// 商户Code
        /// </summary>
        public string companyCode { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public string payOrderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string ticketNo { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string companyOrderId { get; set; }
        /// <summary>
        /// 返现金额
        /// </summary>
        public double rebatePrice { get; set; }
        /// <summary>
        /// 是否返现描述
        /// </summary>
        public string isRebateStr { get; set; }
        /// <summary>
        /// 返现状态
        /// </summary>
        public string rebateStatus { get; set; }
        /// <summary>
        /// 票面价格
        /// </summary>
        public double ticketPrice { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public double orderPrice { get; set; }
        /// <summary>
        /// 起飞结束时间
        /// </summary>
        public string departureEndDate { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string merchantNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDate { get; set; }
        /// <summary>
        /// 父级商户Code
        /// </summary>
        public string parentCode { get; set; }
    }

    #endregion

    #region 订单下载
    public class orderExportList {
        /// <summary>
        /// 
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public double payPrice { get; set; }
        /// <summary>
        /// 乘机人数
        /// </summary>
        public int passengerNum { get; set; }
        /// <summary>
        /// 还款状态
        /// </summary>
        public int reStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inList { get; set; }
        /// <summary>
        /// 还款状态描述
        /// </summary>
        public string reStatusStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string offAndLandPlace { get; set; }
        /// <summary>
        /// 起飞开始时间
        /// </summary>
        public string departureStartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PNR { get; set; }
        /// <summary>
        /// 创建结束时间
        /// </summary>
        public string creDateEnd { get; set; }
        /// <summary>
        /// 创建开始时间
        /// </summary>
        public string creDateStart { get; set; }
        /// <summary>
        /// 是否返现
        /// </summary>
        public int isRebate { get; set; }
        /// <summary>
        /// String 	商户Code
        /// </summary>
        public string companyCode { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public string payOrderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string ticketNo { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string companyOrderId { get; set; }
        /// <summary>
        /// 返现金额
        /// </summary>
        public double rebatePrice { get; set; }
        /// <summary>
        /// 是否返现描述
        /// </summary>
        public string isRebateStr { get; set; }
        /// <summary>
        /// 返现状态
        /// </summary>
        public string rebateStatus { get; set; }
        /// <summary>
        /// 票面价
        /// </summary>
        public double ticketPrice { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public double orderPrice { get; set; }
        /// <summary>
        /// 起飞结束时间
        /// </summary>
        public string departureEndDate { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string merchantNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDate { get; set; }
        /// <summary>
        /// 父级商户Code
        /// </summary>
        public string parentCode { get; set; }
    }
    #endregion

    /// <summary>
    /// 返款列表查询
    /// </summary>
    public class GetloanListView : BasePage {
        public List<Getloan> recordList { get; set; }
    }

    /// <summary>
    /// 返款实体
    /// </summary>
    public class Getloan {
        public string createDateTime { get; set; }
        public string companyOrderId { get; set; }
        public string companyCode { get; set; }
        public string companyName { get; set; }
        public string accountTypeStr { get; set; }
        public string accountName { get; set; }
        public decimal amount { get; set; }
        public decimal ticketPrice { get; set; }
        public decimal orderPrice { get; set; }
        public decimal creditRate { get; set; }
        public decimal changeBackRateStr { get; set; }
    }

    /// <summary>
    /// 天数设置历史记录
    /// </summary>
    public class DaySetHistory {
        public string dayNumber { get; set; }
        public string accountCode { get; set; }

        public string limtDay { get; set; }

        public int isDel { get; set; }
    }

    /// <summary>
    /// 注册返回实体
    /// </summary>
    public class RegisteredMerchantView {
        public string msg { get; set; }
        public string code { get; set; }
    }

    /// <summary>
    /// 保理返现返回实体
    /// </summary>
    public class FinancingRemoneyView {
        public string ticketNo { get; set; }
        public string reapalTradeNo { get; set; }
        public string cashBackAmount { get; set; }
    }

    /// <summary>
    /// 保理返现订单
    /// </summary>
    public class FinancingOrder {
        public string orderId { get; set; }
        public string ticketNo { get; set; }
        public string passengerName { get; set; }
        public string departureTime { get; set; }
        public string departureCity { get; set; }
        public string reachCity { get; set; }
        public string pnr { get; set; }
        public string airline { get; set; }
        public string flightNo { get; set; }
        public string passengerNo { get; set; }
        public string orderPrice { get; set; }
    }


    /// <summary>
    /// 保理还款实体
    /// </summary> 
    public class FinancingRepaymentView {
        public string ticketNoList { get; set; }
        public string reapalTradeNo { get; set; }
        public string billDateTime { get; set; }
        public string refundCode { get; set; }
        public string repayAmount { get; set; }
        public string notRepayAmount { get; set; }
        public string balanceAmount { get; set; }
    }


}
