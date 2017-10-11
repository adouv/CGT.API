

using CGT.DDD.Config;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CGT.Entity.CgtInsuranceModel
{

	 public partial class CgtInsuranceDB : Database
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
            return JsonConfig.JsonRead("cgtInsuranceConnection");
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


        public CgtInsuranceDB() : base(OpenConnection())
        {
            CommonConstruct();
        }

        public CgtInsuranceDB(string connectionStringName) : base(OpenConnection(connectionStringName))
        {
            CommonConstruct();
        }

        partial void CommonConstruct();

        public interface IFactory
        {
            CgtInsuranceDB GetInstance();
        }

        public static IFactory Factory { get; set; }
        public static CgtInsuranceDB GetInstance()
        {
            if (_instance != null)
                return _instance;

            if (Factory != null)
                return Factory.GetInstance();
            else
                return new CgtInsuranceDB();
        }

        [ThreadStatic] static CgtInsuranceDB _instance;

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
            public static CgtInsuranceDB repo { get { return CgtInsuranceDB.GetInstance(); } }
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


	
	 [TableName("dbo.InsuranceBill_old")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class InsuranceBill_old:CgtInsuranceDB.Record<InsuranceBill_old>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public string UserName {get;set;}
		[Column] public string Email {get;set;}
		[Column] public DateTime BillDate {get;set;}
		[Column] public decimal BillAmount {get;set;}
		[Column] public decimal RepayAmount {get;set;}
		[Column] public int RepayStatus {get;set;}
		[Column] public int SettlementStatus {get;set;}
		[Column] public int RepayType {get;set;}
		[Column] public decimal BalanceBillAmount {get;set;}
		[Column] public decimal Amount {get;set;}
		[Column] public decimal Rate {get;set;}
		[Column] public decimal BalanceBillFreeAmount {get;set;}
		[Column] public decimal AllBalanceBillFreeAmount {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public DateTime UpdateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.InsuranceCarInfo_old")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class InsuranceCarInfo_old:CgtInsuranceDB.Record<InsuranceCarInfo_old>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public int TradeId {get;set;}
		[Column] public string CarLicense {get;set;}
		[Column] public string EngineNo {get;set;}
		[Column] public string CarModel {get;set;}
		[Column] public string CarNo {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.InsuranceOrder_old")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class InsuranceOrder_old:CgtInsuranceDB.Record<InsuranceOrder_old>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public int TradeId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string PlatformName {get;set;}
		[Column] public int Platformtype {get;set;}
		[Column] public int InsuranceType {get;set;}
		[Column] public string InsuranceName {get;set;}
		[Column] public string NotifyUrl {get;set;}
		[Column] public int InsurancepPassengerNum {get;set;}
		[Column] public string InsuranceInfoName {get;set;}
		[Column] public string PolicyHolderName {get;set;}
		[Column] public string Phone {get;set;}
		[Column] public int CertificateType {get;set;}
		[Column] public string CertificateNo {get;set;}
		[Column] public DateTime Birthday {get;set;}
		[Column] public string Address {get;set;}
		[Column] public decimal InsurancepSumMoney {get;set;}
		[Column] public string InsurancepNo {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string ReturnUrl {get;set;}
		
	 }
	
	 [TableName("dbo.InsurancePassenger_old")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class InsurancePassenger_old:CgtInsuranceDB.Record<InsurancePassenger_old>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public int TradeId {get;set;}
		[Column] public string PassengerName {get;set;}
		[Column] public string CardNo {get;set;}
		[Column] public int InsurancedCertificateType {get;set;}
		[Column] public string InsurancedPhone {get;set;}
		[Column] public string InsurancepNo {get;set;}
		[Column] public int InsuranceStuts {get;set;}
		[Column] public DateTime InsurancedBirthday {get;set;}
		[Column] public int InsurancePassengerType {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.Trade_old")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class Trade_old:CgtInsuranceDB.Record<Trade_old>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public decimal TradeSumMoney {get;set;}
		[Column] public int TradeType {get;set;}
		[Column] public decimal TradeAmount {get;set;}
		[Column] public DateTime BillDate {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public decimal CreditPayMoney {get;set;}
		[Column] public DateTime? SucessTime {get;set;}
		[Column] public string ReapalPayTradeNo {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public string CreditReapalNo {get;set;}
		[Column] public string CreditUserName {get;set;}
		[Column] public string MerchantCode {get;set;}
		[Column] public int InsuranceStatus {get;set;}
		[Column] public int BackStatus {get;set;}
		[Column] public string ReapalCreditTradeNo {get;set;}
		[Column] public string PayUserName {get;set;}
		[Column] public string PayUserNameNo {get;set;}
		[Column] public string SettlementReapalNo {get;set;}
		[Column] public int PayStatus {get;set;}
		[Column] public string BackRate {get;set;}
		[Column] public string IncomeAmount {get;set;}
		[Column] public string PayChannelNumber {get;set;}
		[Column] public int BillId {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string SettlementReapal {get;set;}
		[Column] public decimal? RepealReturnAmount {get;set;}
		
	 }
	
	 [TableName("dbo.InsuranceRefund_old")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class InsuranceRefund_old:CgtInsuranceDB.Record<InsuranceRefund_old>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public int? TradeId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string companyCode {get;set;}
		[Column] public string ReapalOrderNo {get;set;}
		[Column] public decimal? RefundAmount {get;set;}
		[Column] public string Email {get;set;}
		[Column] public DateTime? RefundTime {get;set;}
		[Column] public string Remark {get;set;}
		[Column] public string Ip {get;set;}
		[Column] public int? RefundStatus {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.InsuranceBillDetail_old")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class InsuranceBillDetail_old:CgtInsuranceDB.Record<InsuranceBillDetail_old>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public int BillId {get;set;}
		[Column] public int TradeId {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public decimal Amount {get;set;}
		[Column] public decimal OrderAmount {get;set;}
		[Column] public int? RepayStatus {get;set;}
		[Column] public decimal RepayAmount {get;set;}
		[Column] public DateTime BillDate {get;set;}
		[Column] public long UserId {get;set;}
		[Column] public DateTime CreateTime {get;set;}
		[Column] public DateTime UpdateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.ReapalMerchant_old")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class ReapalMerchant_old:CgtInsuranceDB.Record<ReapalMerchant_old>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public string UserKey {get;set;}
		[Column] public string MerchantCode {get;set;}
		[Column] public string MerchantName {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public Guid TableId {get;set;}
		
	 }
	
	 [TableName("dbo.InsuranceUser")]
	 [PrimaryKey("UserId")]
	 [ExplicitColumns]
     public partial class InsuranceUser:CgtInsuranceDB.Record<InsuranceUser>
	 {
		
		[Column] public long UserId {get;set;}
		[Column] public string UserName {get;set;}
		[Column] public long? UserAccountId {get;set;}
		[Column] public string UserPwd {get;set;}
		[Column] public int Status {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public string Ip {get;set;}
		[Column] public int? MonthLimitCount {get;set;}
		[Column] public int? RemainingCount {get;set;}
		
	 }
	
	 [TableName("dbo.InsuranceOrder")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class InsuranceOrder:CgtInsuranceDB.Record<InsuranceOrder>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public string OthOrderCode {get;set;}
		[Column] public decimal TotalAmount {get;set;}
		[Column] public decimal TotalPremium {get;set;}
		[Column] public DateTime StartDate {get;set;}
		[Column] public DateTime EndDate {get;set;}
		[Column] public string FlightNo {get;set;}
		[Column] public string FlightDate {get;set;}
		[Column] public string AppliName {get;set;}
		[Column] public string IdentifyType {get;set;}
		[Column] public string IdentifyNumber {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public string Mobile {get;set;}
		[Column] public string PolicyNo {get;set;}
		[Column] public string ProposalNo {get;set;}
		[Column] public string UUID {get;set;}
		[Column] public string SendTime {get;set;}
		
	 }
	
	 [TableName("dbo.InsurancedPerson")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class InsurancedPerson:CgtInsuranceDB.Record<InsurancedPerson>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public string InsuredName {get;set;}
		[Column] public int ApplyNum {get;set;}
		[Column] public string IdentifyType {get;set;}
		[Column] public string IdentifyNumber {get;set;}
		[Column] public string Relation {get;set;}
		[Column] public string Mobile {get;set;}
		[Column] public long InsuredOrderId {get;set;}
		
	 }

}








