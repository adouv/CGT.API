

using CGT.DDD.Config;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace CGT.Entity.CgtLogModel
{

	 public partial class CgtLogDB : Database
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
            return JsonConfig.JsonRead("cgtLogConnection");
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

		

        public CgtLogDB() : base(OpenConnection())
        {
            CommonConstruct();
        }

        public CgtLogDB(string connectionStringName) : base(OpenConnection(connectionStringName))
        {
            CommonConstruct();
        }

        partial void CommonConstruct();

        public interface IFactory
        {
            CgtLogDB GetInstance();
        }

        public static IFactory Factory { get; set; }
        public static CgtLogDB GetInstance()
        {
            if (_instance != null)
                return _instance;

            if (Factory != null)
                return Factory.GetInstance();
            else
                return new CgtLogDB();
        }

        [ThreadStatic] static CgtLogDB _instance;

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
            public static CgtLogDB repo { get { return CgtLogDB.GetInstance(); } }
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


	
	 [TableName("dbo.HPCheckTicketResultLog")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class HPCheckTicketResultLog:CgtLogDB.Record<HPCheckTicketResultLog>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public string TicketNo {get;set;}
		[Column] public int? CheckStatus {get;set;}
		[Column] public string ErrorMessage {get;set;}
		[Column] public DateTime? AddTime {get;set;}
		
	 }
	
	 [TableName("dbo.AccountInterfaceLog")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class AccountInterfaceLog:CgtLogDB.Record<AccountInterfaceLog>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Interface {get;set;}
		[Column] public string Method {get;set;}
		[Column] public string Parameter {get;set;}
		[Column] public int? LogType {get;set;}
		[Column] public string Message {get;set;}
		[Column] public DateTime? AddTime {get;set;}
		
	 }
	
	 [TableName("dbo.APIExceptionLog")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class APIExceptionLog:CgtLogDB.Record<APIExceptionLog>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public string MerchantId {get;set;}
		[Column] public string IP {get;set;}
		[Column] public string Url {get;set;}
		[Column] public string ReturnParams {get;set;}
		[Column] public string SubmitParams {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public int? Status {get;set;}
		[Column] public string MessageCode {get;set;}
		[Column] public string Message {get;set;}
		
	 }
	
	 [TableName("dbo.ApiNotifyLog")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class ApiNotifyLog:CgtLogDB.Record<ApiNotifyLog>
	 {
		
		[Column] public int ID {get;set;}
		[Column] public Guid TableId {get;set;}
		[Column] public int? InterfaceType {get;set;}
		[Column] public int? BusinessType {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public string NodifyParameter {get;set;}
		[Column] public int? NodifyStatus {get;set;}
		[Column] public string ReNodifyParameter {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string BillId {get;set;}
		[Column] public string MerchantName {get;set;}
		[Column] public string ReapayMerchantNo {get;set;}
		[Column] public string MerchantCode {get;set;}
		
	 }
	
	 [TableName("dbo.BillLog")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class BillLog:CgtLogDB.Record<BillLog>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public string UserName {get;set;}
		[Column] public long? BillId {get;set;}
		[Column] public decimal? RepayAmount {get;set;}
		[Column] public decimal? BillAmount {get;set;}
		[Column] public DateTime? BillDate {get;set;}
		[Column] public int? RepayStatus {get;set;}
		[Column] public int? SettlementStatus {get;set;}
		[Column] public string PerformMethod {get;set;}
		[Column] public string PerformContent {get;set;}
		[Column] public string PerformResult {get;set;}
		[Column] public DateTime? BillTime {get;set;}
		[Column] public int? Status {get;set;}
		
	 }
	
	 [TableName("dbo.MessageEmailInfo")]
	 [PrimaryKey("EmailId")]
	 [ExplicitColumns]
     public partial class MessageEmailInfo:CgtLogDB.Record<MessageEmailInfo>
	 {
		
		[Column] public long EmailId {get;set;}
		[Column] public string MsgTitle {get;set;}
		[Column] public long? MessageId {get;set;}
		
	 }
	
	 [TableName("dbo.MessageSend")]
	 [PrimaryKey("id")]
	 [ExplicitColumns]
     public partial class MessageSend:CgtLogDB.Record<MessageSend>
	 {
		
		[Column] public long id {get;set;}
		[Column] public int? MessageType {get;set;}
		[Column] public string MsgContent {get;set;}
		[Column] public string FromUser {get;set;}
		[Column] public string ToUser {get;set;}
		[Column] public string FormUserName {get;set;}
		[Column] public string FormUserPwd {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? ExecuteTime {get;set;}
		[Column] public int? Status {get;set;}
		
	 }
	
	 [TableName("dbo.TradeLog")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class TradeLog:CgtLogDB.Record<TradeLog>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public long? TradeId {get;set;}
		[Column] public string UserName {get;set;}
		[Column] public string InTradeNo {get;set;}
		[Column] public string OutTradeNo {get;set;}
		[Column] public string PerformMethod {get;set;}
		[Column] public string PerformContent {get;set;}
		[Column] public string PerformResult {get;set;}
		[Column] public DateTime? TradeTime {get;set;}
		[Column] public int? Status {get;set;}
		
	 }
	
	 [TableName("dbo.UserLog")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class UserLog:CgtLogDB.Record<UserLog>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public string UserPhone {get;set;}
		[Column] public string UserEmail {get;set;}
		[Column] public DateTime? LoginTime {get;set;}
		[Column] public string LoginIp {get;set;}
		[Column] public string PerformMethod {get;set;}
		[Column] public string PerformContent {get;set;}
		[Column] public string PerformResult {get;set;}
		[Column] public int? Status {get;set;}
		
	 }
	
	 [TableName("dbo.PhoneMessageLog")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class PhoneMessageLog:CgtLogDB.Record<PhoneMessageLog>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public int? MessageType {get;set;}
		[Column] public string MessageContent {get;set;}
		[Column] public string Phone {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? CreateUserId {get;set;}
		
	 }
	
	 [TableName("dbo.XHInterFaceCheckTicketResultLog")]
	 [PrimaryKey("Id")]
	 [ExplicitColumns]
     public partial class XHInterFaceCheckTicketResultLog:CgtLogDB.Record<XHInterFaceCheckTicketResultLog>
	 {
		
		[Column] public long Id {get;set;}
		[Column] public string BatchNumber {get;set;}
		[Column] public int? RegisterStatus {get;set;}
		[Column] public int? CheckStatus {get;set;}
		[Column] public string TicketNum {get;set;}
		[Column] public DateTime? AddTime {get;set;}
		[Column] public DateTime? CheckTime {get;set;}
		
	 }

}








