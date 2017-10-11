

using CGT.DDD.Config;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CGT.Entity.CgtHangfireModel
{

	 public partial class CgtHangfireDB : Database
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
            return JsonConfig.JsonRead("cgtHangfireConnection");
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


        public CgtHangfireDB() : base(OpenConnection())
        {
            CommonConstruct();
        }

        public CgtHangfireDB(string connectionStringName) : base(OpenConnection(connectionStringName))
        {
            CommonConstruct();
        }

        partial void CommonConstruct();

        public interface IFactory
        {
            CgtHangfireDB GetInstance();
        }

        public static IFactory Factory { get; set; }
        public static CgtHangfireDB GetInstance()
        {
            if (_instance != null)
                return _instance;

            if (Factory != null)
                return Factory.GetInstance();
            else
                return new CgtHangfireDB();
        }

        [ThreadStatic] static CgtHangfireDB _instance;

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
            public static CgtHangfireDB repo { get { return CgtHangfireDB.GetInstance(); } }
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


	
	 [TableName("HangFire.Schema")]
	 [ExplicitColumns]
     public partial class Schema:CgtHangfireDB.Record<Schema>
	 {
		
		[Column] public int Version {get;set;}
		
	 }
	
	 [TableName("HangFire.Job")]
	 [ExplicitColumns]
     public partial class Job:CgtHangfireDB.Record<Job>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public int? StateId {get;set;}
		[Column] public string StateName {get;set;}
		[Column] public string InvocationData {get;set;}
		[Column] public string Arguments {get;set;}
		[Column] public DateTime CreatedAt {get;set;}
		[Column] public DateTime? ExpireAt {get;set;}
		
	 }
	
	 [TableName("HangFire.State")]
	 [ExplicitColumns]
     public partial class State:CgtHangfireDB.Record<State>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public int JobId {get;set;}
		[Column] public string Name {get;set;}
		[Column] public string Reason {get;set;}
		[Column] public DateTime CreatedAt {get;set;}
		[Column] public string Data {get;set;}
		
	 }
	
	 [TableName("HangFire.JobParameter")]
	 [ExplicitColumns]
     public partial class JobParameter:CgtHangfireDB.Record<JobParameter>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public int JobId {get;set;}
		[Column] public string Name {get;set;}
		[Column] public string Value {get;set;}
		
	 }
	
	 [TableName("HangFire.JobQueue")]
	 [ExplicitColumns]
     public partial class JobQueue:CgtHangfireDB.Record<JobQueue>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public int JobId {get;set;}
		[Column] public string Queue {get;set;}
		[Column] public DateTime? FetchedAt {get;set;}
		
	 }
	
	 [TableName("HangFire.Server")]
	 [ExplicitColumns]
     public partial class Server:CgtHangfireDB.Record<Server>
	 {
		
		[Column] public string Id {get;set;}
		[Column] public string Data {get;set;}
		[Column] public DateTime LastHeartbeat {get;set;}
		
	 }
	
	 [TableName("HangFire.List")]
	 [ExplicitColumns]
     public partial class List:CgtHangfireDB.Record<List>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Key {get;set;}
		[Column] public string Value {get;set;}
		[Column] public DateTime? ExpireAt {get;set;}
		
	 }
	
	 [TableName("HangFire.Set")]
	 [ExplicitColumns]
     public partial class Set:CgtHangfireDB.Record<Set>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Key {get;set;}
		[Column] public double Score {get;set;}
		[Column] public string Value {get;set;}
		[Column] public DateTime? ExpireAt {get;set;}
		
	 }
	
	 [TableName("HangFire.Counter")]
	 [ExplicitColumns]
     public partial class Counter:CgtHangfireDB.Record<Counter>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Key {get;set;}
		[Column] public short Value {get;set;}
		[Column] public DateTime? ExpireAt {get;set;}
		
	 }
	
	 [TableName("HangFire.Hash")]
	 [ExplicitColumns]
     public partial class Hash:CgtHangfireDB.Record<Hash>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Key {get;set;}
		[Column] public string Field {get;set;}
		[Column] public string Value {get;set;}
		[Column] public string ExpireAt {get;set;}
		
	 }
	
	 [TableName("HangFire.AggregatedCounter")]
	 [ExplicitColumns]
     public partial class AggregatedCounter:CgtHangfireDB.Record<AggregatedCounter>
	 {
		
		[Column] public int Id {get;set;}
		[Column] public string Key {get;set;}
		[Column] public long Value {get;set;}
		[Column] public DateTime? ExpireAt {get;set;}
		
	 }

}








