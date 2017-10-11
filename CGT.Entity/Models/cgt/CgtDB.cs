

using CGT.DDD.Config;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CGT.Entity.CgtModel
{

	 public partial class CgtDB : Database
     {
        private static SqlConnection con;
        /// <summary>
        /// open the connection
        /// </summary>
        /// <returns></returns>
        private static SqlConnection OpenConnection()
        {
            if (con == null)
            {
                con = new SqlConnection(GetConn());
            }
            else
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
            }
            return con;
        }

		private static string GetConn()
        {
            return JsonConfig.JsonRead("cgtConnection");
        }

        private static SqlConnection OpenConnection(string name)
        {
            if (con == null)
            {
                con = new SqlConnection(JsonConfig.JsonRead(name));
            }
            else
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
            }
            return con;
        }


        public CgtDB() : base(OpenConnection())
        {
            CommonConstruct();
        }

        public CgtDB(string connectionStringName) : base(OpenConnection(connectionStringName))
        {
            CommonConstruct();
        }

        partial void CommonConstruct();

        public interface IFactory
        {
            CgtDB GetInstance();
        }

        public static IFactory Factory { get; set; }
        public static CgtDB GetInstance()
        {
            if (_instance != null)
                return _instance;

            if (Factory != null)
                return Factory.GetInstance();
            else
                return new CgtDB();
        }

        [ThreadStatic] static CgtDB _instance;

        public override void OnBeginTransaction()
        {
            if (_instance == null)
                _instance = this;
        }

        public override void OnEndTransaction()
        {
            if (_instance == this)
                _instance = null;
        }

		public static int BulkUpdate<T>(string tableName, List<T> data, Func<T, string> funColumns) 
        {
            try
            {
			    using (SqlConnection conn = OpenConnection())
                {
                    conn.Open();

                    String sql = "";

                    foreach (var item in data)
                    {
                        sql += string.Format("UPDATE dbo.[{0}] SET {1} ;", tableName, funColumns(item));
                    }

                    SqlCommand comm = new SqlCommand()
                    {
                        CommandText = sql,
                        Connection = conn
                    };

                    return comm.ExecuteNonQuery();
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public class Record<T> where T : new()
        {
            public static CgtDB repo { get { return CgtDB.GetInstance(); } }
            public bool IsNew() { return repo.IsNew(this); }
            public object Insert() { return repo.Insert(this); }

            public void Save() { repo.Save(this); }
            public int Update() { return repo.Update(this); }

            public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
            public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
            public static int Update(Sql sql) { return repo.Update<T>(sql); }
            public int Delete() { return repo.Delete(this); }
            public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
            public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
            public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
            public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
            public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
            public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
            public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
            public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
            public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
            public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
            public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
            public static T Single(Sql sql) { return repo.Single<T>(sql); }
            public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
            public static T First(Sql sql) { return repo.First<T>(sql); }
            public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
            public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }

            public static List<T> Fetch(int page, int itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }

            public static List<T> SkipTake(int skip, int take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
            public static List<T> SkipTake(int skip, int take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
            public static Page<T> Page(int page, int itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
            public static Page<T> Page(int page, int itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
            public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
            public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

        }

    }


	
	 [TableName("dbo.LoginTokenInfo")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class LoginTokenInfo:CgtDB.Record<LoginTokenInfo>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public string Token {get;set;}
		[Column] public string Email {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public string Ip {get;set;}
		[Column] public string Mac {get;set;}
		
	 }
	
	 [TableName("dbo.WithdrawalTrade")]
	 [PrimaryKey("WithdrawalTradeId")]
	 [ExplicitColumns]
     public partial class WithdrawalTrade:CgtDB.Record<WithdrawalTrade>
	 {
		
		[Column] public long WithdrawalTradeId {get;set;}
		[Column] public string MerchantOrderId {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public decimal? Amout {get;set;}
		[Column] public string BankNumber {get;set;}
		[Column] public string BankOpenName {get;set;}
		[Column] public string BankName {get;set;}
		[Column] public int? State {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string Note {get;set;}
		
	 }
	
	 [TableName("dbo.HistoryPartnerInfo")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class HistoryPartnerInfo:CgtDB.Record<HistoryPartnerInfo>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public int? UAccountId {get;set;}
		[Column] public string HisPartnerCode {get;set;}
		[Column] public string PartnerCode {get;set;}
		[Column] public string MerchantName {get;set;}
		[Column] public string Operation {get;set;}
		[Column] public string Remark {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		
	 }
	
	 [TableName("dbo.BillRepayDetail")]
	 [PrimaryKey("RepayDetailId")]
	 [ExplicitColumns]
     public partial class BillRepayDetail:CgtDB.Record<BillRepayDetail>
	 {
		
		[Column] public long RepayDetailId {get;set;}
		[Column] public long TradeId {get;set;}
		[Column] public long BillDetailId {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.CompanyAccount")]
	 [PrimaryKey("CompanyID")]
	 [ExplicitColumns]
     public partial class CompanyAccount:CgtDB.Record<CompanyAccount>
	 {
		
		[Column] public long CompanyID {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public string CompanyName {get;set;}
		[Column] public string CompanyAccountName {get;set;}
		[Column] public string CompanyReapalNo {get;set;}
		[Column] public string PartnerCode {get;set;}
		[Column] public int Vip {get;set;}
		[Column] public string YYZZUrl {get;set;}
		[Column] public string ZZJGUrl {get;set;}
		[Column] public string SWDJUrl {get;set;}
		[Column] public string CZDJUrl {get;set;}
		[Column] public string YHKHUrl {get;set;}
		[Column] public string IATAUrl {get;set;}
		[Column] public bool IsThreeInOne {get;set;}
		[Column] public long CreateUserId {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		
	 }
	
	 [TableName("dbo.BillRepayLog")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class BillRepayLog:CgtDB.Record<BillRepayLog>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public decimal? RepayAmount {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? CreateUserId {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public long? UpdateTimeUserId {get;set;}
		
	 }
	
	 [TableName("dbo.GuaranteeBill")]
	 [PrimaryKey("GuaranteeBillId")]
	 [ExplicitColumns]
     public partial class GuaranteeBill:CgtDB.Record<GuaranteeBill>
	 {
		
		[Column] public long GuaranteeBillId {get;set;}
		[Column] public long BillId {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public long GuaranteeUserId {get;set;}
		[Column] public long? CreateUserId {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public string GuaranteeUserName {get;set;}
		[Column] public long? UpdateUserId {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public int? GuaranteeCount {get;set;}
		[Column] public DateTime? GuaranteeDate {get;set;}
		[Column] public int? GuaranteeHour {get;set;}
		
	 }
	
	 [TableName("dbo.PartnerBusiness")]
	 [PrimaryKey("PartnerBusinessId")]
	 [ExplicitColumns]
     public partial class PartnerBusiness:CgtDB.Record<PartnerBusiness>
	 {
		
		[Column] public long PartnerBusinessId {get;set;}
		[Column] public string BusinessType {get;set;}
		[Column] public string BusinessName {get;set;}
		[Column] public string BusinessRemark {get;set;}
		[Column] public string BusinessCode {get;set;}
		[Column] public int? Status {get;set;}
		
	 }
	
	 [TableName("dbo.PartnerPermissions")]
	 [PrimaryKey("PartnerPermissionsId")]
	 [ExplicitColumns]
     public partial class PartnerPermissions:CgtDB.Record<PartnerPermissions>
	 {
		
		[Column] public long PartnerPermissionsId {get;set;}
		[Column] public string PartnerCode {get;set;}
		[Column] public long PartnerBusinessId {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? CreateUserId {get;set;}
		[Column] public int? Status {get;set;}
		
	 }
	
	 [TableName("dbo.PersonIdCard")]
	 [ExplicitColumns]
     public partial class PersonIdCard:CgtDB.Record<PersonIdCard>
	 {
		
		[Column] public string ProvinceName {get;set;}
		[Column] public string CityName {get;set;}
		[Column] public string RegionName {get;set;}
		[Column] public string Date {get;set;}
		[Column] public string Sex {get;set;}
		[Column] public string IDCard {get;set;}
		[Column] public int IsCheck {get;set;}
		[Column] public int Clock {get;set;}
		
	 }
	
	 [TableName("dbo.TicketOrderRefound")]
	 [PrimaryKey("RefoundId")]
	 [ExplicitColumns]
     public partial class TicketOrderRefound:CgtDB.Record<TicketOrderRefound>
	 {
		
		[Column] public long RefoundId {get;set;}
		[Column] public long TradeId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string PayTradeNo {get;set;}
		[Column] public decimal RefoundMoney {get;set;}
		[Column] public int Status {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public string Name {get;set;}
		[Column] public string SuccessPnr {get;set;}
		[Column] public string ApplyPnr {get;set;}
		
	 }
	
	 [TableName("dbo.TradeSegment")]
	 [PrimaryKey("TradeSegmentId")]
	 [ExplicitColumns]
     public partial class TradeSegment:CgtDB.Record<TradeSegment>
	 {
		
		[Column] public long? TradeId {get;set;}
		[Column] public long TradeSegmentId {get;set;}
		[Column] public string Airline {get;set;}
		[Column] public string FlightNo {get;set;}
		[Column] public string Origin {get;set;}
		[Column] public string Destination {get;set;}
		[Column] public DateTime? DepartureDate {get;set;}
		[Column] public DateTime? ArrivalDate {get;set;}
		[Column] public string Cabin {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string PlatformOrderId {get;set;}
		
	 }
	
	 [TableName("dbo.TicketPolicyRule")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class TicketPolicyRule:CgtDB.Record<TicketPolicyRule>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public string AirCode {get;set;}
		[Column] public string CabinDesc {get;set;}
		[Column] public string Cabin {get;set;}
		[Column] public string Discount {get;set;}
		[Column] public bool? VoluntaryTicketPolicy {get;set;}
		[Column] public string Remark {get;set;}
		[Column] public DateTime? ReleaseDateTime {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		
	 }
	
	 [TableName("dbo.TicketUserInfo")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class TicketUserInfo:CgtDB.Record<TicketUserInfo>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public long? CGTUserID {get;set;}
		[Column] public int PlatformType {get;set;}
		[Column] public string UserName {get;set;}
		[Column] public string AgentCode {get;set;}
		[Column] public string InterfacePwd {get;set;}
		[Column] public string SafetyCode {get;set;}
		[Column] public string ContactName {get;set;}
		[Column] public string ContactPhone {get;set;}
		[Column] public string ModifyUser {get;set;}
		[Column] public long? ModifyUserId {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public string AddUser {get;set;}
		[Column] public DateTime? AddTime {get;set;}
		
	 }
	
	 [TableName("dbo.TradeAliPayOriginalMemberList")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class TradeAliPayOriginalMemberList:CgtDB.Record<TradeAliPayOriginalMemberList>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public string TransactionNo {get;set;}
		[Column] public string MerchantOrderNo {get;set;}
		[Column] public DateTime? CreateTranTime {get;set;}
		[Column] public string PayTime {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public string TranSource {get;set;}
		[Column] public string TypeDes {get;set;}
		[Column] public string CounterParty {get;set;}
		[Column] public string TradeName {get;set;}
		[Column] public decimal? Price {get;set;}
		[Column] public string BalancePayment {get;set;}
		[Column] public string TradeStatusDes {get;set;}
		[Column] public string ServiceCharge {get;set;}
		[Column] public decimal? SuccessRefund {get;set;}
		[Column] public string Remark {get;set;}
		[Column] public string FromUrl {get;set;}
		[Column] public DateTime? AddTime {get;set;}
		
	 }
	
	 [TableName("dbo.TradeCreditPayDetail")]
	 [PrimaryKey("CreditPayDetailId")]
	 [ExplicitColumns]
     public partial class TradeCreditPayDetail:CgtDB.Record<TradeCreditPayDetail>
	 {
		
		[Column] public long CreditPayDetailId {get;set;}
		[Column] public decimal PayMoney {get;set;}
		[Column] public long TradeId {get;set;}
		[Column] public int Status {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.TradeNotify")]
	 [PrimaryKey("TradeNotifyId")]
	 [ExplicitColumns]
     public partial class TradeNotify:CgtDB.Record<TradeNotify>
	 {
		
		[Column] public long TradeNotifyId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string Notify {get;set;}
		[Column] public int NotifyNum {get;set;}
		[Column] public DateTime PerformTime {get;set;}
		[Column] public string PerformData {get;set;}
		[Column] public DateTime? SuccessTime {get;set;}
		[Column] public int Status {get;set;}
		
	 }
	
	 [TableName("dbo.TicketRiskControlRule")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class TicketRiskControlRule:CgtDB.Record<TicketRiskControlRule>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public string MerchantCode {get;set;}
		[Column] public string MerchantName {get;set;}
		[Column] public int? RuleType {get;set;}
		[Column] public int? RuleSort {get;set;}
		[Column] public string Day {get;set;}
		[Column] public string Proportion {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public int? ModifyUser {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.TradeReapalOriginalMemberList")]
	 [ExplicitColumns]
     public partial class TradeReapalOriginalMemberList:CgtDB.Record<TradeReapalOriginalMemberList>
	 {
		
		[Column] public string MerchantOrderNo {get;set;}
		[Column] public string OrderTradeNo {get;set;}
		[Column] public string BankOrderNo {get;set;}
		[Column] public string ProductName {get;set;}
		[Column] public string BankPayment {get;set;}
		[Column] public string DebitAndCreditType {get;set;}
		[Column] public string TradeAmount {get;set;}
		[Column] public string TradeCommissionType {get;set;}
		[Column] public string MerchantCommissionType {get;set;}
		[Column] public string MerchantCommission {get;set;}
		[Column] public string MerchantSettlement {get;set;}
		[Column] public string TranCurrency {get;set;}
		[Column] public string State {get;set;}
		[Column] public string TranComplateTime {get;set;}
		[Column] public string PaymentRole {get;set;}
		[Column] public string GatheringRole {get;set;}
		[Column] public string Remark1 {get;set;}
		[Column] public string DataSourcePath {get;set;}
		
	 }
	
	 [TableName("dbo.TradeTicketInfo")]
	 [PrimaryKey("TradeInfoId")]
	 [ExplicitColumns]
     public partial class TradeTicketInfo:CgtDB.Record<TradeTicketInfo>
	 {
		
		[Column] public long TradeInfoId {get;set;}
		[Column] public long TradeId {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public string UserPwd {get;set;}
		[Column] public int UserType {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public decimal OrderPrice {get;set;}
		[Column] public string PlatformOrderId {get;set;}
		[Column] public string PlatformName {get;set;}
		[Column] public int PlatformCode {get;set;}
		[Column] public string PlatformLoginPwd {get;set;}
		[Column] public string PartnerCode {get;set;}
		[Column] public int IsAllPayMoney {get;set;}
		[Column] public string NumberRefund {get;set;}
		[Column] public decimal TicketPrice {get;set;}
		[Column] public decimal PayPrice {get;set;}
		[Column] public decimal RebatePrice {get;set;}
		[Column] public decimal? Rebate {get;set;}
		[Column] public string Pnr {get;set;}
		[Column] public string PayUrl {get;set;}
		[Column] public string NotifyUrl {get;set;}
		[Column] public string Ip {get;set;}
		[Column] public int TimesTamp {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public int PassengerNum {get;set;}
		[Column] public DateTime StartDate {get;set;}
		[Column] public string ChannelOrderId {get;set;}
		[Column] public string ChannelUrl {get;set;}
		[Column] public decimal? NumberRefundRebate {get;set;}
		[Column] public string ReturnUrl {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public decimal? PlatFee {get;set;}
		[Column] public decimal? Agio {get;set;}
		[Column] public decimal? Tax {get;set;}
		
	 }
	
	 [TableName("dbo.UserToLCCEmail")]
	 [PrimaryKey("UserToLCCEmailId")]
	 [ExplicitColumns]
     public partial class UserToLCCEmail:CgtDB.Record<UserToLCCEmail>
	 {
		
		[Column] public long UserToLCCEmailId {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public string AirLineEmail {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string Airline {get;set;}
		[Column] public string AirlineName {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public int? ModifyUserId {get;set;}
		
	 }
	
	 [TableName("dbo.TradeWithdrawal")]
	 [PrimaryKey("WithdrawalId")]
	 [ExplicitColumns]
     public partial class TradeWithdrawal:CgtDB.Record<TradeWithdrawal>
	 {
		
		[Column] public long WithdrawalId {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public string ReapalBindId {get;set;}
		[Column] public string InTradeNo {get;set;}
		[Column] public string OutTradeNo {get;set;}
		[Column] public int Status {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? SuccessTime {get;set;}
		
	 }
	
	 [TableName("dbo.UserAccount")]
	 [PrimaryKey("UserId")]
	 [ExplicitColumns]
     public partial class UserAccount:CgtDB.Record<UserAccount>
	 {
		
		[Column] public Guid TableId {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public string UserName {get;set;}
		[Column] public string Email {get;set;}
		[Column] public string UserPwd {get;set;}
		[Column] public string RealName {get;set;}
		[Column] public string IdNumber {get;set;}
		[Column] public string BankCardNumber {get;set;}
		[Column] public string Phone {get;set;}
		[Column] public string Ip {get;set;}
		[Column] public int Status {get;set;}
		[Column] public string ReapalMemberNo {get;set;}
		[Column] public string ReapalBindId {get;set;}
		[Column] public string ReapalMerchantId {get;set;}
		[Column] public int? UserType {get;set;}
		[Column] public string PartnerCode {get;set;}
		[Column] public int? Vip {get;set;}
		[Column] public int? IsOnVip {get;set;}
		[Column] public string TicketDelayEmail {get;set;}
		[Column] public string MerchantCode {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public int? CreateUserID {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public int? ModifyUserID {get;set;}
		[Column] public decimal? BillLateFee {get;set;}
		[Column] public int? InsuranceBillDays {get;set;}
		[Column] public string UserCompanyName {get;set;}
		[Column] public string ReapalUserKey {get;set;}
		[Column] public string PayCenterCode {get;set;}
		[Column] public string LCCReceivesEmail {get;set;}
		[Column] public decimal? FactoringInterestRate {get;set;}
		[Column] public int? GraceCount {get;set;}
		[Column] public int? OverdueCount {get;set;}
		[Column] public int? GraceDay {get;set;}
		[Column] public string ModifyName {get;set;}
		[Column] public decimal? GraceBate {get;set;}
		[Column] public decimal? OverdueBate {get;set;}
		[Column] public decimal? CreditAmount {get;set;}
		[Column] public string UserFactoringCode {get;set;}
		
	 }
	
	 [TableName("dbo.Bill")]
	 [PrimaryKey("BillId")]
	 [ExplicitColumns]
     public partial class Bill:CgtDB.Record<Bill>
	 {
		
		[Column] public long BillId {get;set;}
		[Column] public decimal BillAmount {get;set;}
		[Column] public decimal RepayAmount {get;set;}
		[Column] public decimal RefundAmount {get;set;}
		[Column] public DateTime BillDate {get;set;}
		[Column] public int RepayStatus {get;set;}
		[Column] public int SettlementStatus {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public int BillStartToEndDate {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public int SumPassengerNum {get;set;}
		[Column] public decimal? TodayBillAmount {get;set;}
		[Column] public decimal? BalanceBillAmount {get;set;}
		[Column] public int? BillType {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public int? RepayType {get;set;}
		[Column] public decimal? BalanceBillFree {get;set;}
		[Column] public decimal? BalanceBillFreeAmount {get;set;}
		[Column] public decimal? AllBalanceBillFreeAmount {get;set;}
		[Column] public decimal? BalanceBillRefundAmount {get;set;}
		[Column] public decimal? BillInterest {get;set;}
		[Column] public decimal? AllBillInterest {get;set;}
		[Column] public decimal? LastBalanceBillAmount {get;set;}
		
	 }
	
	 [TableName("dbo.UserAutoRefund")]
	 [PrimaryKey("UserAutoRefundId")]
	 [ExplicitColumns]
     public partial class UserAutoRefund:CgtDB.Record<UserAutoRefund>
	 {
		
		[Column] public long UserAutoRefundId {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public int? Stauts {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public long? UpdateUserId {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.TradeReapalMemberList")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class TradeReapalMemberList:CgtDB.Record<TradeReapalMemberList>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public int TransType {get;set;}
		[Column] public string TransName {get;set;}
		[Column] public decimal TransAmount {get;set;}
		[Column] public string TransNo {get;set;}
		[Column] public int TransStatus {get;set;}
		[Column] public string TransCreateTime {get;set;}
		[Column] public string CreateTime {get;set;}
		[Column] public string FromAccountName {get;set;}
		[Column] public string ToAccountName {get;set;}
		[Column] public string MerchantOrderNo {get;set;}
		[Column] public int? TransferBillType {get;set;}
		[Column] public string Remark1 {get;set;}
		[Column] public string PlateCode {get;set;}
		[Column] public int? IsaLiPay {get;set;}
		[Column] public int? IsBill {get;set;}
		[Column] public string RefundTicketId {get;set;}
		[Column] public int? IsWallet {get;set;}
		
	 }
	
	 [TableName("dbo.TicketDigitalPolicyRule")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class TicketDigitalPolicyRule:CgtDB.Record<TicketDigitalPolicyRule>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public long TicketPolicyRuleId {get;set;}
		[Column] public string AirCode {get;set;}
		[Column] public string Cabin {get;set;}
		[Column] public string RefundTicketRule {get;set;}
		[Column] public string ChangeTicketRule {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.MessageManage")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class MessageManage:CgtDB.Record<MessageManage>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public int? ManageType {get;set;}
		[Column] public string MobilePhone {get;set;}
		[Column] public string MessageNote {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? OperationUserId {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.MessageTemplate")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class MessageTemplate:CgtDB.Record<MessageTemplate>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public int? BusinessType {get;set;}
		[Column] public int? StepType {get;set;}
		[Column] public string TemplateContent {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? OperationUserId {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.HangLog")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class HangLog:CgtDB.Record<HangLog>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public long? TradeId {get;set;}
		[Column] public string TradePassengerName {get;set;}
		[Column] public string TradeTicketNo {get;set;}
		[Column] public int? HangStatus {get;set;}
		[Column] public DateTime? HangTime {get;set;}
		[Column] public string HangOrder {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string Operation {get;set;}
		
	 }
	
	 [TableName("dbo.TicketScanInfo")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class TicketScanInfo:CgtDB.Record<TicketScanInfo>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public string MerchantCode {get;set;}
		[Column] public string BackUserName {get;set;}
		[Column] public string BackMerchantCode {get;set;}
		[Column] public int? IsSuspend {get;set;}
		[Column] public string TicketNo {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public string MerchantName {get;set;}
		[Column] public string DepartureTime {get;set;}
		[Column] public string FlightNo {get;set;}
		[Column] public string Airline {get;set;}
		[Column] public int? TicketType {get;set;}
		[Column] public string PassengerName {get;set;}
		[Column] public int? ScanStatus {get;set;}
		[Column] public DateTime? ScanTime {get;set;}
		[Column] public int? RechargeStatus {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.TicketRiskCabin")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class TicketRiskCabin:CgtDB.Record<TicketRiskCabin>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public string Airline {get;set;}
		[Column] public string Cabin {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? OperationUserId {get;set;}
		
	 }
	
	 [TableName("dbo.TradeWalletPayOriginalMemberList")]
	 [ExplicitColumns]
     public partial class TradeWalletPayOriginalMemberList:CgtDB.Record<TradeWalletPayOriginalMemberList>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public string A3 {get;set;}
		[Column] public DateTime? CompleteDate {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public string FailReason {get;set;}
		[Column] public decimal? FreeMoney {get;set;}
		[Column] public string MerchantOrder {get;set;}
		[Column] public string Operator {get;set;}
		[Column] public string PayAccount {get;set;}
		[Column] public string PayAccountName {get;set;}
		[Column] public decimal? PayAmount {get;set;}
		[Column] public string PayCompanyName {get;set;}
		[Column] public decimal? PayNotUseMoney {get;set;}
		[Column] public string PayTime {get;set;}
		[Column] public int? PayTranSubType {get;set;}
		[Column] public int? PayTranType {get;set;}
		[Column] public decimal? PayUseMoney {get;set;}
		[Column] public string PayWalletAccount {get;set;}
		[Column] public string PayWalletKey {get;set;}
		[Column] public string Pnr {get;set;}
		[Column] public string ReceiveAccount {get;set;}
		[Column] public string ReceiveAccountName {get;set;}
		[Column] public decimal? ReceiveAmount {get;set;}
		[Column] public string ReceiveCompanyName {get;set;}
		[Column] public DateTime? ReceiveTime {get;set;}
		[Column] public string ReceiveWalletAccount {get;set;}
		[Column] public string ReceiveWalletKey {get;set;}
		[Column] public decimal? RedPacket {get;set;}
		[Column] public string Remark {get;set;}
		[Column] public string SingStr {get;set;}
		[Column] public string StrTrackID {get;set;}
		[Column] public string SubTransactionOrder {get;set;}
		[Column] public DateTime? ThirdCompleteTime {get;set;}
		[Column] public string ThirdHandleResult {get;set;}
		[Column] public string ThirdTradeNo {get;set;}
		[Column] public string TickeNumber {get;set;}
		[Column] public int? TranId {get;set;}
		[Column] public int? TranMainStatus {get;set;}
		[Column] public string TranMainStatusText {get;set;}
		[Column] public decimal? TranMoney {get;set;}
		[Column] public int? TranSubStatus {get;set;}
		[Column] public string TranSubStatusText {get;set;}
		[Column] public int? TranType {get;set;}
		[Column] public string TransactionName {get;set;}
		[Column] public string TransactionOrder {get;set;}
		[Column] public int? UserType {get;set;}
		[Column] public decimal? balance {get;set;}
		[Column] public DateTime? AddTime {get;set;}
		
	 }
	
	 [TableName("dbo.TicketRiskRefund")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class TicketRiskRefund:CgtDB.Record<TicketRiskRefund>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public long? TradeId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string Passengers {get;set;}
		[Column] public DateTime? StartDate {get;set;}
		[Column] public string Airline {get;set;}
		[Column] public string Cabin {get;set;}
		[Column] public string Origin {get;set;}
		[Column] public string Destination {get;set;}
		[Column] public decimal? Amount {get;set;}
		[Column] public string PlatformOrderId {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? BillId {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public decimal? RefundAmout {get;set;}
		[Column] public int? RefundStatus {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.TicketRiskRoute")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class TicketRiskRoute:CgtDB.Record<TicketRiskRoute>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public string Origin {get;set;}
		[Column] public string Destination {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? OperationUserId {get;set;}
		
	 }
	
	 [TableName("dbo.BillDetail")]
	 [PrimaryKey("BillDetailId")]
	 [ExplicitColumns]
     public partial class BillDetail:CgtDB.Record<BillDetail>
	 {
		
		[Column] public long BillDetailId {get;set;}
		[Column] public long? BillId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public decimal? OrderTotalAmout {get;set;}
		[Column] public decimal FreezeAmount {get;set;}
		[Column] public decimal ShouldRepayAmount {get;set;}
		[Column] public decimal RepayAmount {get;set;}
		[Column] public int RepayStatus {get;set;}
		[Column] public DateTime BillDate {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public int? Isrefundment {get;set;}
		[Column] public string PlatformName {get;set;}
		[Column] public int? PlatformCode {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public int? AuthStatus {get;set;}
		[Column] public decimal? OrderInterest {get;set;}
		[Column] public string GoldMasterName {get;set;}
		[Column] public int? OrderSource {get;set;}
		[Column] public int? OrderType {get;set;}
		
	 }
	
	 [TableName("dbo.OfficeConfig")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class OfficeConfig:CgtDB.Record<OfficeConfig>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public int Type {get;set;}
		[Column] public string Eterm {get;set;}
		[Column] public string EtermPw {get;set;}
		[Column] public string OfficeNo {get;set;}
		[Column] public string SI {get;set;}
		[Column] public string ServerAddress {get;set;}
		[Column] public string Port {get;set;}
		[Column] public string B2BName {get;set;}
		[Column] public string B2BPw {get;set;}
		
	 }
	
	 [TableName("dbo.TicketScannedLog")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class TicketScannedLog:CgtDB.Record<TicketScannedLog>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string TicketNo {get;set;}
		[Column] public int? TicketType {get;set;}
		[Column] public string MerchantCode {get;set;}
		[Column] public string MerchantName {get;set;}
		[Column] public string BackUserName {get;set;}
		[Column] public string BackMerchantCode {get;set;}
		[Column] public DateTime? BackTime {get;set;}
		[Column] public int? RechargeStatus {get;set;}
		[Column] public int? IsSuspend {get;set;}
		[Column] public DateTime? DepartureTime {get;set;}
		[Column] public string FlightNo {get;set;}
		[Column] public string PassengerName {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.TicketPolicyRuleDetail")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class TicketPolicyRuleDetail:CgtDB.Record<TicketPolicyRuleDetail>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public long TicketPolicyRuleId {get;set;}
		[Column] public int? StartHour {get;set;}
		[Column] public int? EndHour {get;set;}
		[Column] public string StartHourDesc {get;set;}
		[Column] public string EndHourDesc {get;set;}
		[Column] public string StartDesc {get;set;}
		[Column] public string EndDesc {get;set;}
		[Column] public int? RuleType {get;set;}
		
	 }
	
	 [TableName("dbo.ErrorInfo")]
	 [PrimaryKey("ErrorId")]
	 [ExplicitColumns]
     public partial class ErrorInfo:CgtDB.Record<ErrorInfo>
	 {
		
		[Column] public int ErrorId {get;set;}
		[Column] public int PlatformType {get;set;}
		[Column] public string PlatformErrorCode {get;set;}
		[Column] public string PlatformErrorMsg {get;set;}
		[Column] public string SyatemCode {get;set;}
		[Column] public string SyatemMsg {get;set;}
		[Column] public int? ErrorWhere {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? CreateUserId {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public long? UpdateUserId {get;set;}
		
	 }
	
	 [TableName("dbo.Trade")]
	 [PrimaryKey("TradeId")]
	 [ExplicitColumns]
     public partial class Trade:CgtDB.Record<Trade>
	 {
		
		[Column] public long TradeId {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public long? BillId {get;set;}
		[Column] public DateTime? BillDate {get;set;}
		[Column] public string InTradeNo {get;set;}
		[Column] public string OutTradeNo {get;set;}
		[Column] public int TradeType {get;set;}
		[Column] public bool PayType {get;set;}
		[Column] public int? OrderPayType {get;set;}
		[Column] public decimal TradeSumMoney {get;set;}
		[Column] public decimal UserPayMoney {get;set;}
		[Column] public decimal? CreditPayMoney {get;set;}
		[Column] public int TradeStatus {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? SuccessTime {get;set;}
		[Column] public string ReapalOutTradeNo {get;set;}
		[Column] public int? BackStatus {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string PayUserName {get;set;}
		[Column] public int? IsRemoney {get;set;}
		[Column] public int? Isuspended {get;set;}
		[Column] public int? suspendedStatus {get;set;}
		[Column] public decimal? Poundage {get;set;}
		[Column] public int? SupendedSet {get;set;}
		[Column] public int? UnsupendedSet {get;set;}
		[Column] public decimal? SolutionRate {get;set;}
		[Column] public int? OutOrderPayType {get;set;}
		[Column] public decimal? RefundAmout {get;set;}
		[Column] public decimal? InterestRate {get;set;}
		[Column] public int? EnterpriseWhiteListID {get;set;}
		[Column] public string GoldMasterName {get;set;}
		[Column] public string RedTradeNo {get;set;}
		[Column] public int? TravelType {get;set;}
		[Column] public int? GoldMasterType {get;set;}
		[Column] public int? IsBill {get;set;}
		[Column] public int? TradeSource {get;set;}
		
	 }
	
	 [TableName("dbo.ABEErrorInfo")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class ABEErrorInfo:CgtDB.Record<ABEErrorInfo>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string PNRContent {get;set;}
		[Column] public int ErrorType {get;set;}
		[Column] public string ErrorMessage {get;set;}
		[Column] public int Status {get;set;}
		[Column] public string ABEDesc {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public string ModifyUser {get;set;}
		
	 }
	
	 [TableName("dbo.OfficeNoConfig")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class OfficeNoConfig:CgtDB.Record<OfficeNoConfig>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public string AirCompanyCode {get;set;}
		[Column] public string OfficeNo {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public string ModifyUser {get;set;}
		
	 }
	
	 [TableName("dbo.InterfaceAccount")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class InterfaceAccount:CgtDB.Record<InterfaceAccount>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public string MerchantName {get;set;}
		[Column] public string MerchantIP {get;set;}
		[Column] public string MerchantCode {get;set;}
		[Column] public string ReapayMerchantNo {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public string UserKey {get;set;}
		[Column] public string CertAddress {get;set;}
		[Column] public string CertPassword {get;set;}
		[Column] public string Contact {get;set;}
		[Column] public string Phone {get;set;}
		[Column] public string Email {get;set;}
		[Column] public string Address {get;set;}
		[Column] public string ReapalMerchantId {get;set;}
		[Column] public int? UserType {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? CreateUserID {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public long? UpdateUserID {get;set;}
		[Column] public Guid TableID {get;set;}
		[Column] public string SuspendedServiceUrl {get;set;}
		[Column] public string MerchantPwd {get;set;}
		[Column] public string ReapalUserKey {get;set;}
		[Column] public int? IsCheckPrice {get;set;}
		
	 }
	
	 [TableName("dbo.Guaranteetimes")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class Guaranteetimes:CgtDB.Record<Guaranteetimes>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public long? UserID {get;set;}
		[Column] public string StepOneTime {get;set;}
		[Column] public string StepTwoTime {get;set;}
		[Column] public string StepThreeTime {get;set;}
		[Column] public string StepFourTime {get;set;}
		[Column] public DateTime? Date {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public int? IsRecharge {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.SUnsuspendErrorInfo")]
	 [PrimaryKey("SUnsuspendId")]
	 [ExplicitColumns]
     public partial class SUnsuspendErrorInfo:CgtDB.Record<SUnsuspendErrorInfo>
	 {
		
		[Column] public int SUnsuspendId {get;set;}
		[Column] public long? TradeId {get;set;}
		[Column] public string PlatformOrderId {get;set;}
		[Column] public int? Category {get;set;}
		[Column] public string ReMoneyName {get;set;}
		[Column] public string MerchantId {get;set;}
		[Column] public string ErrorDescript {get;set;}
		[Column] public int? ErrorType {get;set;}
		[Column] public int? TicketType {get;set;}
		[Column] public string OperationUser {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public int? SuStatus {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string ServiceUrl {get;set;}
		[Column] public string PersonalMerchantNo {get;set;}
		[Column] public string StartDate {get;set;}
		[Column] public string Amount {get;set;}
		[Column] public string AccountType {get;set;}
		[Column] public string ReturnAmountAccount {get;set;}
		[Column] public string NotifyUrl {get;set;}
		[Column] public decimal? NumberRefundRebate {get;set;}
		
	 }
	
	 [TableName("dbo.TradeTopUp")]
	 [PrimaryKey("TradeTopUpId")]
	 [ExplicitColumns]
     public partial class TradeTopUp:CgtDB.Record<TradeTopUp>
	 {
		
		[Column] public long TradeTopUpId {get;set;}
		[Column] public long? FromUserId {get;set;}
		[Column] public long? ToUserId {get;set;}
		[Column] public long? TradeId {get;set;}
		[Column] public decimal? Amount {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? SuccessTime {get;set;}
		[Column] public Guid TableID {get;set;}
		[Column] public string InTradeNo {get;set;}
		[Column] public string OutTradeNo {get;set;}
		
	 }
	
	 [TableName("dbo.UserBalanceRefund")]
	 [PrimaryKey("UserBalanceRefundId")]
	 [ExplicitColumns]
     public partial class UserBalanceRefund:CgtDB.Record<UserBalanceRefund>
	 {
		
		[Column] public long UserBalanceRefundId {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public int Stauts {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public long? UpdateUserId {get;set;}
		
	 }
	
	 [TableName("dbo.InterfaceAccountAndUserAccount")]
	 [PrimaryKey("InterfaceAccountAndUserAccountId")]
	 [ExplicitColumns]
     public partial class InterfaceAccountAndUserAccount:CgtDB.Record<InterfaceAccountAndUserAccount>
	 {
		
		[Column] public long InterfaceAccountAndUserAccountId {get;set;}
		[Column] public string InterfaceName {get;set;}
		[Column] public string UserName {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.TicketRiskControlExc")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class TicketRiskControlExc:CgtDB.Record<TicketRiskControlExc>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public string MerchantCode {get;set;}
		[Column] public int? TicketType {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string TicketNo {get;set;}
		[Column] public int? ExceptionType {get;set;}
		[Column] public string ExceptionDes {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.BindCard")]
	 [PrimaryKey("BindCardId")]
	 [ExplicitColumns]
     public partial class BindCard:CgtDB.Record<BindCard>
	 {
		
		[Column] public long BindCardId {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public string BankNumber {get;set;}
		[Column] public string BankOpenName {get;set;}
		[Column] public string BankName {get;set;}
		[Column] public string CenterBank {get;set;}
		[Column] public string BranchBank {get;set;}
		[Column] public int? BankCardType {get;set;}
		[Column] public string Province {get;set;}
		[Column] public string City {get;set;}
		[Column] public string Phone {get;set;}
		[Column] public int? CertificateType {get;set;}
		[Column] public string CertificateNumber {get;set;}
		[Column] public string UserAgreementNo {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.TradePassenger")]
	 [PrimaryKey("TradePassengerId")]
	 [ExplicitColumns]
     public partial class TradePassenger:CgtDB.Record<TradePassenger>
	 {
		
		[Column] public long? TradeId {get;set;}
		[Column] public long TradePassengerId {get;set;}
		[Column] public string PassengerName {get;set;}
		[Column] public int? PassengerType {get;set;}
		[Column] public int? PsgIndexInPNR {get;set;}
		[Column] public int? CardType {get;set;}
		[Column] public string CardNo {get;set;}
		[Column] public DateTime? Birthday {get;set;}
		[Column] public string AirTicketNo {get;set;}
		[Column] public int? TiketNoHangStatus {get;set;}
		[Column] public string PlatformOrderId {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public int? TicketType {get;set;}
		[Column] public string OfficeNo {get;set;}
		
	 }
	
	 [TableName("dbo.BankPayLimitInfo")]
	 [PrimaryKey("BankPayLimitInfoId")]
	 [ExplicitColumns]
     public partial class BankPayLimitInfo:CgtDB.Record<BankPayLimitInfo>
	 {
		
		[Column] public int BankPayLimitInfoId {get;set;}
		[Column] public string BankName {get;set;}
		[Column] public string BankCode {get;set;}
		[Column] public string CardKind {get;set;}
		[Column] public string BusinessType {get;set;}
		[Column] public string SingleMoney {get;set;}
		[Column] public string DayMoney {get;set;}
		[Column] public string Mobile {get;set;}
		[Column] public string Remark {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }

}








