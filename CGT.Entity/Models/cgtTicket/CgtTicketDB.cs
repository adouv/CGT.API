

using CGT.DDD.Config;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace CGT.Entity.CgtTicketModel
{

	 public partial class CgtTicketDB : Database
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
            return JsonConfig.JsonRead("cgtTicketConnection");
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


        public CgtTicketDB() : base(OpenConnection())
        {
            CommonConstruct();
        }

        public CgtTicketDB(string connectionStringName) : base(OpenConnection(connectionStringName))
        {
            CommonConstruct();
        }

        partial void CommonConstruct();

        public interface IFactory
        {
            CgtTicketDB GetInstance();
        }

        public static IFactory Factory { get; set; }
        public static CgtTicketDB GetInstance()
        {
            if (_instance != null)
                return _instance;

            if (Factory != null)
                return Factory.GetInstance();
            else
                return new CgtTicketDB();
        }

        [ThreadStatic] static CgtTicketDB _instance;

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
            public static CgtTicketDB repo { get { return CgtTicketDB.GetInstance(); } }
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


	
	 [TableName("dbo.Passenger")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class Passenger:CgtTicketDB.Record<Passenger>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Order {get;set;}
		[Column] public string Name {get;set;}
		[Column] public int Type {get;set;}
		[Column] public string CertificateNumber {get;set;}
		[Column] public string VirtualCertNumber {get;set;}
		[Column] public int CertificateType {get;set;}
		[Column] public string TicketNumber {get;set;}
		[Column] public string Phone {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public int? Sex {get;set;}
		[Column] public string Country {get;set;}
		[Column] public string Valid {get;set;}
		[Column] public string Birthday {get;set;}
		
	 }
	
	 [TableName("dbo.Pnr")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class Pnr:CgtTicketDB.Record<Pnr>
	 {
		
		[Column] public int Id {get;set;}
		[Column("Pnr")] public string _Pnr {get;set;}
		[Column] public string Office {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		
	 }
	
	 [TableName("dbo.Refund")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class Refund:CgtTicketDB.Record<Refund>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Order {get;set;}
		[Column] public string PlatformRefundId {get;set;}
		[Column] public string ChannelRefundId {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public int Platform {get;set;}
		[Column] public string Reason {get;set;}
		[Column] public string CanceledPnr {get;set;}
		[Column] public int Status {get;set;}
		[Column] public decimal? Amount {get;set;}
		[Column] public string SubReason {get;set;}
		[Column] public string RefundReason {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string UploadAddress {get;set;}
		
	 }
	
	 [TableName("dbo.RefundTicket")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class RefundTicket:CgtTicketDB.Record<RefundTicket>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public int Refund {get;set;}
		[Column] public string Passenger {get;set;}
		[Column] public string TicketNumber {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.Voyage")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class Voyage:CgtTicketDB.Record<Voyage>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Order {get;set;}
		[Column] public string Departure {get;set;}
		[Column] public string Arrival {get;set;}
		[Column] public DateTime DepartureTime {get;set;}
		[Column] public DateTime ArrivalTime {get;set;}
		[Column] public string Bunk {get;set;}
		[Column] public int SerialNumber {get;set;}
		[Column] public string Airline {get;set;}
		[Column] public string FlightNo {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.Change")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class Change:CgtTicketDB.Record<Change>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string OrderID {get;set;}
		[Column] public int ChangeType {get;set;}
		[Column] public string Airline {get;set;}
		[Column] public string FlightNo {get;set;}
		[Column] public string Bunk {get;set;}
		[Column] public string Departure {get;set;}
		[Column] public string Arrival {get;set;}
		[Column] public DateTime DepartureTime {get;set;}
		[Column] public DateTime? ArrivalTime {get;set;}
		[Column] public string NewFlightNo {get;set;}
		[Column] public string NewBunk {get;set;}
		[Column] public DateTime? NewDepartureTime {get;set;}
		[Column] public DateTime? NewArrivalTime {get;set;}
		[Column] public decimal? ChangeAmount {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public int? SerialNumber {get;set;}
		[Column] public string Remark {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public long? ModifyUserId {get;set;}
		[Column] public string ModifyUserName {get;set;}
		[Column] public string Phone {get;set;}
		[Column] public string Pnr {get;set;}
		[Column] public int? TicketIdStatus {get;set;}
		[Column] public int? AuthStatus {get;set;}
		[Column] public string ChangeRule {get;set;}
		[Column] public string ReturnRemark {get;set;}
		
	 }
	
	 [TableName("dbo.ChangeLog")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class ChangeLog:CgtTicketDB.Record<ChangeLog>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public int? ChangeID {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public string ModifyUserName {get;set;}
		[Column] public int? ModifyStatus {get;set;}
		
	 }
	
	 [TableName("dbo.ChangeTicket")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class ChangeTicket:CgtTicketDB.Record<ChangeTicket>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public int? Change {get;set;}
		[Column] public int? PassengerId {get;set;}
		[Column] public string Passenger {get;set;}
		[Column] public string TicketNumber {get;set;}
		[Column] public string NewTicketNumber {get;set;}
		
	 }
	
	 [TableName("dbo.Delay")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class Delay:CgtTicketDB.Record<Delay>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public string OrderID {get;set;}
		[Column("Delay")] public string _Delay {get;set;}
		[Column] public DateTime? DepartureTime {get;set;}
		[Column] public DateTime? ArrivalTime {get;set;}
		[Column] public string SMS {get;set;}
		
	 }
	
	 [TableName("dbo.OrderPnr")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class OrderPnr:CgtTicketDB.Record<OrderPnr>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Order {get;set;}
		[Column] public string OriginPnr {get;set;}
		[Column] public string OriginContent {get;set;}
		[Column] public string ReplacePnr {get;set;}
		[Column] public string ReplaceContent {get;set;}
		[Column] public string PrintPnr {get;set;}
		[Column] public string PrintContent {get;set;}
		[Column] public string AirCompanyCode {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		
	 }
	
	 [TableName("dbo.Phone")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class Phone:CgtTicketDB.Record<Phone>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Order {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column("Phone")] public string _Phone {get;set;}
		
	 }
	
	 [TableName("dbo.PhoneSum")]
	 [PrimaryKey("id")]
	 [ExplicitColumns]
     public partial class PhoneSum:CgtTicketDB.Record<PhoneSum>
	 {
		
		[Column] public int id {get;set;}
		[Column] public string Phone {get;set;}
		[Column] public string PhoneCount {get;set;}
		
	 }
	
	 [TableName("dbo.PolicyRule")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class PolicyRule:CgtTicketDB.Record<PolicyRule>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public string PolicyId {get;set;}
		[Column] public string PlatCode {get;set;}
		[Column] public string TktOffice {get;set;}
		[Column] public int? SwitchPnr {get;set;}
		[Column] public DateTime? FltStart {get;set;}
		[Column] public DateTime? FltEnd {get;set;}
		[Column] public int? PreTkt {get;set;}
		[Column] public DateTime? TktStart {get;set;}
		[Column] public DateTime? TktEnd {get;set;}
		[Column] public decimal? Fare {get;set;}
		[Column] public double? TaxAmount {get;set;}
		[Column] public decimal? CommAmount {get;set;}
		[Column] public decimal? PaymentFee {get;set;}
		[Column] public decimal? CommRate {get;set;}
		[Column] public decimal? CommMoney {get;set;}
		[Column] public string FareBase {get;set;}
		[Column] public string TktType {get;set;}
		[Column] public string AutoTicket {get;set;}
		[Column] public string Receipt {get;set;}
		[Column] public string PaymentType {get;set;}
		[Column] public string Remark {get;set;}
		[Column] public string RefundRules {get;set;}
		[Column] public string Efficiency {get;set;}
		[Column] public string TktWorktime {get;set;}
		[Column] public string RefundWorkTime {get;set;}
		[Column] public string WastWorkTime {get;set;}
		[Column] public string WeekendOutWorkTime {get;set;}
		[Column] public string WeekendRefundTime {get;set;}
		[Column] public bool? IsSpecial {get;set;}
		[Column] public decimal? Price {get;set;}
		[Column] public int? AccountLevel {get;set;}
		[Column] public string PlatformType {get;set;}
		[Column] public string PlatformName {get;set;}
		[Column] public string PlatformMappingCode {get;set;}
		[Column] public string PlatCodeForShort {get;set;}
		[Column] public string AirPortTax {get;set;}
		[Column] public string FuelTax {get;set;}
		[Column] public string OrderID {get;set;}
		
	 }
	
	 [TableName("dbo.SendSms")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class SendSms:CgtTicketDB.Record<SendSms>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public string phone {get;set;}
		[Column] public string reCode {get;set;}
		[Column] public string SendContent {get;set;}
		[Column] public string msgid {get;set;}
		[Column] public string orderId {get;set;}
		
	 }
	
	 [TableName("dbo.ErrorRefundOrder")]
	 [PrimaryKey("ErrorRefundOrderId")]
	 [ExplicitColumns]
     public partial class ErrorRefundOrder:CgtTicketDB.Record<ErrorRefundOrder>
	 {
		
		[Column] public long ErrorRefundOrderId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string ErrorCode {get;set;}
		[Column] public string ErrorMsg {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public int? ProcStatus {get;set;}
		[Column] public long? ModifyUserId {get;set;}
		[Column] public string ModifyUserName {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		
	 }
	
	 [TableName("dbo.Order")]
	 [PrimaryKey("LocalId", autoIncrement = false)]
	 [ExplicitColumns]
     public partial class Order:CgtTicketDB.Record<Order>
	 {
		
		[Column] public string LocalId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string ChannelOrderId {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public int Status {get;set;}
		[Column] public int Platform {get;set;}
		[Column] public int Channel {get;set;}
		[Column] public string Account {get;set;}
		[Column] public string Policy {get;set;}
		[Column] public decimal Amount {get;set;}
		[Column] public string ReapalAccount {get;set;}
		[Column] public bool AllowDisplay {get;set;}
		[Column] public string PurchaserOffice {get;set;}
		[Column] public string ProviderOffice {get;set;}
		[Column] public decimal? Par {get;set;}
		[Column] public bool IsCreditOrder {get;set;}
		[Column] public int? AuthStatus {get;set;}
		[Column] public int? TicketIdStatus {get;set;}
		[Column] public string Phone {get;set;}
		[Column] public string TicketDelayEmail {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public bool? RequireChangePNR {get;set;}
		[Column] public decimal? RedPacket {get;set;}
		[Column] public int? TravelType {get;set;}
		[Column] public decimal? JinBiTong {get;set;}
		
	 }
	
	 [TableName("dbo.InterRefundTicket")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class InterRefundTicket:CgtTicketDB.Record<InterRefundTicket>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public int? InterRefundId {get;set;}
		[Column] public int? PassengerId {get;set;}
		[Column] public string PassengerName {get;set;}
		[Column] public string TicketNumber {get;set;}
		
	 }
	
	 [TableName("dbo.InterChangeTicket")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class InterChangeTicket:CgtTicketDB.Record<InterChangeTicket>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public int? InterChangeId {get;set;}
		[Column] public int? PassengerId {get;set;}
		[Column] public string PassengerName {get;set;}
		[Column] public string TicketNumber {get;set;}
		[Column] public string NewTicketNumber {get;set;}
		
	 }
	
	 [TableName("dbo.InterChangeRemark")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class InterChangeRemark:CgtTicketDB.Record<InterChangeRemark>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public int? CreateUserId {get;set;}
		[Column] public string CreateUserName {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public string Remark {get;set;}
		
	 }
	
	 [TableName("dbo.InterRefundRemark")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class InterRefundRemark:CgtTicketDB.Record<InterRefundRemark>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public int? CreateUserId {get;set;}
		[Column] public string CreateUserName {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public string Remark {get;set;}
		
	 }
	
	 [TableName("dbo.InterChange")]
	 [ExplicitColumns]
     public partial class InterChange:CgtTicketDB.Record<InterChange>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string OrderID {get;set;}
		[Column] public string Reason {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public int? ModifyUserId {get;set;}
		[Column] public string ModifyUserName {get;set;}
		[Column] public int? AffairStatus {get;set;}
		[Column] public string Remark {get;set;}
		
	 }
	
	 [TableName("dbo.InterRefund")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class InterRefund:CgtTicketDB.Record<InterRefund>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string OrderID {get;set;}
		[Column] public string Reason {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public int? ModifyUserId {get;set;}
		[Column] public string ModifyUserName {get;set;}
		[Column] public int? AffairStatus {get;set;}
		[Column] public decimal? Amount {get;set;}
		
	 }
	
	 [TableName("dbo.ErrorPayOrder")]
	 [PrimaryKey("ErrorPayOrderId")]
	 [ExplicitColumns]
     public partial class ErrorPayOrder:CgtTicketDB.Record<ErrorPayOrder>
	 {
		
		[Column] public long ErrorPayOrderId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string ErrorCode {get;set;}
		[Column] public string ErrorMsg {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? ModifyUserId {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public int? ProcStatus {get;set;}
		[Column] public string ModifyUserName {get;set;}
		
	 }

}








