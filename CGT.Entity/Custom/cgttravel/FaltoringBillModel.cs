using System;

namespace CGT.Entity.CgtTravelModel
{
    /// <summary>
    /// 差旅订单
    /// </summary>
    public class FactoringOrderModel
    {
        public string OrderId { get; set; }
        public DateTime? EndBillDate { get; set; }
        public DateTime? BeginBillDate { get; set; }
        public int? EnterpriseId { get; set; }
        public string EnterpriseName { get; set; }
        public int RepaymentStatus { get; set; }
        public long? BillId { get; set; }
        public string MerchantCode { get; set; }
        public int? MerchantType { get; set; }
        public int? MonthStatue { get; set; }
        public  string OrderTravelBatchId { get; set; }
        /// <summary>
        /// 数据来源 分销平台的订单是1 
        /// </summary>
        public string DataSource { get; set; }

        public  string PayCenterCode { get; set; }

        public  int? BackStatus { get; set; }
    }

    /// <summary>
    /// 订单报表下载实体
    /// </summary>
    public class FactoringOrderDownload
    {

        public string BillId { get; set; }
        public string OrderId { get; set; }
        public string AdvancesName { get; set; }
        public string PayCenterName { get; set; }
        public string EnterpriseName { get; set; }
        public string BillDate { get; set; }
        public string UserInterest { get; set; }
        public string UserInterestRate { get; set; }
        public string TicketAmount { get; set; }
        public string RepaymentTicketAmount { get; set; }
        public string RepaymentDate { get; set; }
        public string RepaymentStatusName { get; set; }
        public string OverdueDays { get; set; }
        public string OverdueFine { get; set; }
        public string RefundAmount { get; set; }
        public string FactoringInterestAmount { get; set; }
        public string TicketTime { get; set; }
        public string BackTime { get; set; }
        public string InsuredAmount { get; set; }
        public string FactoringInterestRate { get; set; }
        public string Pnr { get; set; }
        public string FlightNo { get; set; }
        public string PassengerName { get; set; }
        public string DepartureTime { get; set; }
        public int MerchantType { get; set; }
        public string BillAmount { get; set; }
        public string BillInterest { get; set; }

        public string BackStatus { get; set; }

        public  string OrderTravelBatchId { get; set; }
       public string BackStatusName { get; set; }
    }

    /// <summary>
    /// 账单下载
    /// </summary>
    public class TravelBillDownload
    {
        public string BillId { get; set; }
        public string BillDate { get; set; }
        public string EnterpriseBillDate { get; set; }
        public string BillAmount { get; set; }
        public string AlreadyReimbursement { get; set; }
        public string StatusStr { get; set; }
        public string BillInterest { get; set; }
        public string UserInterestRate { get; set; }
        public string FactoringInterestRate { get; set; }
        public string FactoringInterest { get; set; }
        public string OwnerName { get; set; }
        public string PayCenterName { get; set; }
        public string OverdueDays { get; set; }
        public string OverdueFine { get; set; }
        public string SumTicketNo { get; set; }
        public string InterestRefundAmount { get; set; }
        public int? MerchantType { get; set; }
    }
}
