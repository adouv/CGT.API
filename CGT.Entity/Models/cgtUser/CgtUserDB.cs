

using CGT.DDD.Config;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace CGT.Entity.CgtUserModel
{

	 public partial class CgtUserDB : Database
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
            return JsonConfig.JsonRead("cgtUserConnection");
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


        public CgtUserDB() : base(OpenConnection())
        {
            CommonConstruct();
        }

        public CgtUserDB(string connectionStringName) : base(OpenConnection(connectionStringName))
        {
            CommonConstruct();
        }

        partial void CommonConstruct();

        public interface IFactory
        {
            CgtUserDB GetInstance();
        }

        public static IFactory Factory { get; set; }
        public static CgtUserDB GetInstance()
        {
            if (_instance != null)
                return _instance;

            if (Factory != null)
                return Factory.GetInstance();
            else
                return new CgtUserDB();
        }

        [ThreadStatic] static CgtUserDB _instance;

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
            public static CgtUserDB repo { get { return CgtUserDB.GetInstance(); } }
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


	
	 [TableName("dbo.Permissions")]
	 [PrimaryKey("PermissionsId")]
	 [ExplicitColumns]
     public partial class Permissions:CgtUserDB.Record<Permissions>
	 {
		
		[Column] public long PermissionsId {get;set;}
		[Column] public long? RoleId {get;set;}
		[Column] public long? UserId {get;set;}
		[Column] public long MenuId {get;set;}
		[Column] public int? PermissionsStatus {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? CreateUserId {get;set;}
		
	 }
	
	 [TableName("dbo.Role")]
	 [PrimaryKey("RoleId")]
	 [ExplicitColumns]
     public partial class Role:CgtUserDB.Record<Role>
	 {
		
		[Column] public long RoleId {get;set;}
		[Column] public string RoleName {get;set;}
		[Column] public int? RoleStatus {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		
	 }
	
	 [TableName("dbo.User")]
	 [PrimaryKey("UserId")]
	 [ExplicitColumns]
     public partial class User:CgtUserDB.Record<User>
	 {
		
		[Column] public long UserId {get;set;}
		[Column] public string UserName {get;set;}
		[Column] public string UserPwd {get;set;}
		[Column] public long RoleId {get;set;}
		[Column] public int UserType {get;set;}
		[Column] public int UserStatus {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public long? CreateUserId {get;set;}
		[Column] public long? UpdateUserId {get;set;}
		
	 }
	
	 [TableName("dbo.OrderRemarkInfo")]
	 [PrimaryKey("ID")]
	 [ExplicitColumns]
     public partial class OrderRemarkInfo:CgtUserDB.Record<OrderRemarkInfo>
	 {
		
		[Column] public long ID {get;set;}
		[Column] public string OrderId {get;set;}
		[Column] public string OrderRemark {get;set;}
		[Column] public long? CreateUserId {get;set;}
		[Column] public string CreateUserName {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		
	 }
	
	 [TableName("dbo.Menu")]
	 [PrimaryKey("MenuId")]
	 [ExplicitColumns]
     public partial class Menu:CgtUserDB.Record<Menu>
	 {
		
		[Column] public long MenuId {get;set;}
		[Column] public long? MenuFatherId {get;set;}
		[Column] public string MenuName {get;set;}
		[Column] public string MenuIco {get;set;}
		[Column] public int? MenuType {get;set;}
		[Column] public int? MenuLevel {get;set;}
		[Column] public int MenuStatus {get;set;}
		[Column] public string Controller {get;set;}
		[Column] public string Action {get;set;}
		[Column] public DateTime? CreateTime {get;set;}
		[Column] public long? CreateUserId {get;set;}
		[Column] public DateTime? UpdateTime {get;set;}
		[Column] public long? UpdateUserId {get;set;}
		[Column] public int? Sort {get;set;}
		
	 }

}








