

using CGT.DDD.Config;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace CGT.Entity.CgtTravelModel
{

	 public partial class CgtTravelDB : Database
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
            return JsonConfig.JsonRead("cgtTravelConnection");
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

		

        public CgtTravelDB() : base(OpenConnection())
        {
            CommonConstruct();
        }

        public CgtTravelDB(string connectionStringName) : base(OpenConnection(connectionStringName))
        {
            CommonConstruct();
        }

        partial void CommonConstruct();

        public interface IFactory
        {
            CgtTravelDB GetInstance();
        }

        public static IFactory Factory { get; set; }
        public static CgtTravelDB GetInstance()
        {
            if (_instance != null)
                return _instance;

            if (Factory != null)
                return Factory.GetInstance();
            else
                return new CgtTravelDB();
        }

        [ThreadStatic] static CgtTravelDB _instance;

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
            public static CgtTravelDB repo { get { return CgtTravelDB.GetInstance(); } }
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


			

			/*public static int BulkInsert(string tableName, List<T> data, Func<T, string> funColumns)
            {
                try
                {
                    Sql sql = new Sql();

                    var names = new List<string>();

                    foreach (var item in data)
                    {
					    var properties = item.GetType().GetProperties();

						foreach (var propertie in properties)
						{
							names.Add(propertie.Name);
						}

                        sql.Append("INSERT INTO {0} ({1}) VALUES ({2}) ;", tableName, string.Join(",",names),funColumns(item));
                    }
                    return _instance.Execute(sql);
                }
                catch (Exception x)
                {
                    throw x;
                }
            }*/


		}	 
    }


	
	 [TableName("dbo.EnterpriseOrder")]
	 [PrimaryKey("EnterpriseOrderId")]
	 [ExplicitColumns]
     public partial class EnterpriseOrder:CgtTravelDB.Record<EnterpriseOrder>
	 {
		
		[Column] public long EnterpriseOrderId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string TicketNo {get;set;}
		[Column] public string PassengerName {get;set;}
		[Column] public DateTime? DepartureTime {get;set;}
		[Column] public string DepartureCity {get;set;}
		[Column] public string ReachCity {get;set;}
		[Column] public string Pnr {get;set;}
		[Column] public decimal? TicketAmount {get;set;}
		[Column] public string Airline {get;set;}
		[Column] public string FlightNo {get;set;}
		[Column] public string PassengerNo {get;set;}
		[Column] public int? BackStatus {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? BackTime {get;set;}
		[Column] public string PayCenterCode {get;set;}
		[Column] public string PayCenterName {get;set;}
		[Column] public int? AdvancesId {get;set;}
		[Column] public string AdvancesName {get;set;}
		[Column] public int? EnterpriseWhiteListID {get;set;}
		[Column] public string EtermStatus {get;set;}
		[Column] public int? IsBackStatus {get;set;}
		[Column] public string NoBackReason {get;set;}
		[Column] public string SuspendedServiceUrl {get;set;}
		[Column] public string CashBackEmial {get;set;}
		[Column] public string CashBackReapalNo {get;set;}
		[Column] public string AdvancesEmail {get;set;}
		[Column] public string AdvancesReapalNo {get;set;}
		[Column] public int? BillDateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public int? RepaymentStatus {get;set;}
		[Column] public int? BillEveryDayId {get;set;}
		[Column] public DateTime? BillDate {get;set;}
		[Column] public DateTime? TicketTime {get;set;}
		[Column] public decimal? FactoringInterestRate {get;set;}
		[Column] public decimal? UserInterestRate {get;set;}
		[Column] public decimal? InsuredAmount {get;set;}
		[Column] public int? TravelBatchResult {get;set;}
		[Column] public string OrderTravelBatchId {get;set;}
		[Column] public string OrderEnterpriseName {get;set;}
		[Column] public decimal? UserInterest {get;set;}
		[Column] public int? IsInsurance {get;set;}
		
	 }
	
	 [TableName("dbo.EnterpriseTempoary")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class EnterpriseTempoary:CgtTravelDB.Record<EnterpriseTempoary>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public string EnterpriseName {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public string DistributionAccount {get;set;}
		[Column] public string TravelServiceAgreementURL {get;set;}
		[Column] public int? AuditStatus {get;set;}
		[Column] public string RefuseReason {get;set;}
		[Column] public string AccountNumber {get;set;}
		[Column] public string AccountPeriod {get;set;}
		[Column] public decimal? CreditAmount {get;set;}
		
	 }
	
	 [TableName("dbo.EnterpriseOrderRisk")]
	 [PrimaryKey("EnterpriseOrderRiskId")]
	 [ExplicitColumns]
     public partial class EnterpriseOrderRisk:CgtTravelDB.Record<EnterpriseOrderRisk>
	 {
		
		[Column] public long EnterpriseOrderRiskId {get;set;}
		[Column] public string TravelBatchId {get;set;}
		[Column] public string EOrderId {get;set;}
		[Column] public int? TravelRiskType {get;set;}
		[Column] public int? TravelRiskState {get;set;}
		[Column] public DateTime? RiskCreateTime {get;set;}
		[Column] public int? ReviewState {get;set;}
		[Column] public long? ReviewUserId {get;set;}
		[Column] public string FailReason {get;set;}
		[Column] public string RefuseReason {get;set;}
		[Column] public DateTime? ReviewTime {get;set;}
		
	 }
	
	 [TableName("dbo.EnterpriseStaff")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class EnterpriseStaff:CgtTravelDB.Record<EnterpriseStaff>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public long EnterpriseId {get;set;}
		[Column] public string StaffName {get;set;}
		[Column] public string IdentificationNumber {get;set;}
		[Column] public byte? DocumentType {get;set;}
		[Column] public byte? IsJob {get;set;}
		[Column] public string PhoneNumber {get;set;}
		[Column] public string SubsidiaryDepartment {get;set;}
		[Column] public string JobPosition {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public long? Modifier {get;set;}
		
	 }
	
	 [TableName("dbo.TravelBatch")]
	 [PrimaryKey("TravelBatchResultId")]
	 [ExplicitColumns]
     public partial class TravelBatch:CgtTravelDB.Record<TravelBatch>
	 {
		
		[Column] public long TravelBatchResultId {get;set;}
		[Column] public string TravelBatchId {get;set;}
		[Column] public string PayCenterCode {get;set;}
		[Column] public string PayCenterName {get;set;}
		[Column] public int? EnterpriseId {get;set;}
		[Column] public string EnterpriseName {get;set;}
		[Column] public int? TravelRiskType {get;set;}
		[Column] public int? TravelRiskState {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public decimal? EtermSuccessRate {get;set;}
		[Column] public decimal? EtermFailRate {get;set;}
		[Column] public decimal? EtermRiskRate {get;set;}
		[Column] public decimal? WhithSuccessRate {get;set;}
		[Column] public decimal? WhithFailRate {get;set;}
		[Column] public decimal? WhithRiskRate {get;set;}
		[Column] public int? TotalCount {get;set;}
		[Column] public int? TranslationState {get;set;}
		[Column] public decimal? TotalAmount {get;set;}
		[Column] public int? UserFactoringId {get;set;}
		[Column] public string FactoringName {get;set;}
		[Column] public string FactoringEmail {get;set;}
		[Column] public string FactoringReapalNo {get;set;}
		[Column] public decimal? InterestRate {get;set;}
		[Column] public string UserName {get;set;}
		[Column] public string BackReapalNo {get;set;}
		[Column] public decimal? FactoringInterestRate {get;set;}
		[Column] public int? AccountPeriod {get;set;}
		[Column] public int? BackCount {get;set;}
		[Column] public int? ReviewState {get;set;}
		
	 }
	
	 [TableName("dbo.TravelRepay")]
	 [PrimaryKey("TravelRepayId")]
	 [ExplicitColumns]
     public partial class TravelRepay:CgtTravelDB.Record<TravelRepay>
	 {
		
		[Column] public long TravelRepayId {get;set;}
		[Column] public string BatchTradeNo {get;set;}
		[Column] public string EnterpriseID {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public decimal? RepayAmount {get;set;}
		
	 }
	
	 [TableName("dbo.TravelGraceDay")]
	 [PrimaryKey("TravelGraceDayId")]
	 [ExplicitColumns]
     public partial class TravelGraceDay:CgtTravelDB.Record<TravelGraceDay>
	 {
		
		[Column] public long TravelGraceDayId {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public int? GraceDayCount {get;set;}
		[Column] public long? UpdateUserId {get;set;}
		[Column] public string PayCenterCode {get;set;}
		
	 }
	
	 [TableName("dbo.UserQuest")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class UserQuest:CgtTravelDB.Record<UserQuest>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public string QuestAction {get;set;}
		
	 }
	
	 [TableName("dbo.EnterpriseWhiteList")]
	 [PrimaryKey("EnterpriseWhiteListID")]
	 [ExplicitColumns]
     public partial class EnterpriseWhiteList:CgtTravelDB.Record<EnterpriseWhiteList>
	 {
		
		[Column] public long EnterpriseWhiteListID {get;set;}
		[Column] public string EnterpriseName {get;set;}
		[Column] public string PayCenterCode {get;set;}
		[Column] public string PayCenterName {get;set;}
		[Column] public string AccountNumber {get;set;}
		[Column] public string AccountPeriod {get;set;}
		[Column] public string TravelServiceAgreementURL {get;set;}
		[Column] public decimal CreditAmount {get;set;}
		[Column] public int EnterpriseStatue {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public string ModifiedName {get;set;}
		[Column] public DateTime ModifiedTime {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public decimal? AccountBalance {get;set;}
		[Column] public decimal? CreditMonthAmount {get;set;}
		[Column] public int? MonthStatue {get;set;}
		[Column] public int? FreezeWay {get;set;}
		
	 }
	
	 [TableName("dbo.Bill")]
	 [PrimaryKey("BillId")]
	 [ExplicitColumns]
     public partial class Bill:CgtTravelDB.Record<Bill>
	 {
		
		[Column] public int BillId {get;set;}
		[Column] public decimal BillAmount {get;set;}
		[Column] public decimal AlreadyReimbursement {get;set;}
		[Column] public decimal BillInterest {get;set;}
		[Column] public int Status {get;set;}
		[Column] public int EnterpriseBillDate {get;set;}
		[Column] public DateTime BillDate {get;set;}
		[Column] public string PayCenterCode {get;set;}
		[Column] public int SumTicketNo {get;set;}
		[Column] public string PayCenterName {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public decimal? FactoringInterestRate {get;set;}
		[Column] public decimal? UserInterestRate {get;set;}
		[Column] public string OwnerName {get;set;}
		[Column] public decimal? InterestRefundAmount {get;set;}
		[Column] public decimal? GraceAmout {get;set;}
		[Column] public decimal? OverdueAmout {get;set;}
		[Column] public int? BillType {get;set;}
		[Column] public DateTime? RealpayDateTime {get;set;}
		
	 }
	
	 [TableName("dbo.ApplyRepay")]
	 [ExplicitColumns]
     public partial class ApplyRepay:CgtTravelDB.Record<ApplyRepay>
	 {
		
		[Column] public int id {get;set;}
		[Column] public int BillId {get;set;}
		[Column] public string Operatorer {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public string Reason {get;set;}
		[Column] public DateTime? RepayTime {get;set;}
		[Column] public string Auditer {get;set;}
		[Column] public DateTime? AuditTime {get;set;}
		
	 }
	
	 [TableName("dbo.UserFactoring")]
	 [PrimaryKey("UserFactoringId")]
	 [ExplicitColumns]
     public partial class UserFactoring:CgtTravelDB.Record<UserFactoring>
	 {
		
		[Column] public long UserFactoringId {get;set;}
		[Column] public string FactoringName {get;set;}
		[Column] public string FactoringEmail {get;set;}
		[Column] public string FactoringReapalNo {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public decimal? InterestRate {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string FactoringCode {get;set;}
		
	 }
	
	 [TableName("dbo.Rules_Test")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class Rules_Test:CgtTravelDB.Record<Rules_Test>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public DateTime? SetTime {get;set;}
		[Column] public int? UserNum {get;set;}
		[Column] public int? TicketNum {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		
	 }
	
	 [TableName("dbo.BasicData_Test")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class BasicData_Test:CgtTravelDB.Record<BasicData_Test>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public decimal? TicketPrice {get;set;}
		[Column] public decimal? OrderPrice {get;set;}
		[Column] public DateTime? StratDate {get;set;}
		[Column] public string DepCode {get;set;}
		[Column] public string ArrCode {get;set;}
		[Column] public string FlightNo {get;set;}
		[Column] public string Cabin {get;set;}
		[Column] public string PersonName {get;set;}
		[Column] public string TicketNo {get;set;}
		[Column] public DateTime? TicketDate {get;set;}
		[Column] public string PNR {get;set;}
		[Column] public int? EnterpriseId {get;set;}
		[Column] public string EnterpriseName {get;set;}
		[Column] public string PayCenterCode {get;set;}
		[Column] public string MerchantCode {get;set;}
		[Column] public string PayCenterName {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		
	 }
	
	 [TableName("dbo.TravelRisk")]
	 [PrimaryKey("TravelRiskId")]
	 [ExplicitColumns]
     public partial class TravelRisk:CgtTravelDB.Record<TravelRisk>
	 {
		
		[Column] public long TravelRiskId {get;set;}
		[Column] public int? EnterpriseID {get;set;}
		[Column] public int? TravelRiskType {get;set;}
		[Column] public decimal? EtermSuccessRate {get;set;}
		[Column] public decimal? EtermFailRate {get;set;}
		[Column] public decimal? WhiteSuccessRate {get;set;}
		[Column] public decimal? WhiteFailRate {get;set;}
		[Column] public int? TravelRiskState {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? CreateUserId {get;set;}
		[Column] public DateTime? ModifyTime {get;set;}
		[Column] public long? ModifyUserId {get;set;}
		[Column] public string PayCenterCode {get;set;}
		[Column] public string PayCenterName {get;set;}
		[Column] public int? UploadLowCount {get;set;}
		[Column] public int? EtermType {get;set;}
		[Column] public int? TicketMultiple {get;set;}
		
	 }
	
	 [TableName("dbo.TravelBatchOrder")]
	 [PrimaryKey("TravelBatchOrderId")]
	 [ExplicitColumns]
     public partial class TravelBatchOrder:CgtTravelDB.Record<TravelBatchOrder>
	 {
		
		[Column] public long TravelBatchOrderId {get;set;}
		[Column] public string TravelBatchId {get;set;}
		[Column] public string PayCenterCode {get;set;}
		[Column] public string PayCenterName {get;set;}
		[Column] public long? EnterpriseId {get;set;}
		[Column] public string PassengerName {get;set;}
		[Column] public string FlightNo {get;set;}
		[Column] public string PNR {get;set;}
		[Column] public DateTime? DepartureTime {get;set;}
		[Column] public DateTime? TicketTime {get;set;}
		[Column] public string MatchResult {get;set;}
		[Column] public string TicketNo {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public int? TravelRiskType {get;set;}
		[Column] public int? EtermType {get;set;}
		[Column] public int? WhiteResultState {get;set;}
		[Column] public int? BlackResultState {get;set;}
		[Column] public string UUId {get;set;}
		[Column] public int? RegisterStatus {get;set;}
		[Column] public int? CheckStatus {get;set;}
		[Column] public int? EtermStatus {get;set;}
		[Column] public string DepartCode {get;set;}
		[Column] public string ArriveCode {get;set;}
		[Column] public string Cabin {get;set;}
		[Column] public decimal? OrderAmount {get;set;}
		[Column] public decimal? TicketPrice {get;set;}
		
	 }
	
	 [TableName("dbo.BillEveryDay")]
	 [PrimaryKey("BillEveryDaysId")]
	 [ExplicitColumns]
     public partial class BillEveryDay:CgtTravelDB.Record<BillEveryDay>
	 {
		
		[Column] public int BillEveryDaysId {get;set;}
		[Column] public int? BillId {get;set;}
		[Column] public decimal? BillAmount {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public DateTime BillDate {get;set;}
		[Column] public string PayCenterCode {get;set;}
		[Column] public decimal? BillInterest {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public int? SumTicketNo {get;set;}
		[Column] public long? EnterpriseId {get;set;}
		[Column] public string EnterpriseName {get;set;}
		[Column] public string PayCenterName {get;set;}
		[Column] public int? EnterpriseBillDate {get;set;}
		[Column] public string BatchTradeNo {get;set;}
		[Column] public DateTime? RealpayDateTime {get;set;}
		[Column] public decimal? FactoringInterestRate {get;set;}
		[Column] public decimal? UserInterestRate {get;set;}
		[Column] public string OwnerName {get;set;}
		[Column] public decimal RefundAmount {get;set;}
		[Column] public string OrderId {get;set;}
		
	 }

}








