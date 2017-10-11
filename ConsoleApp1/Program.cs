using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new T4Builder("Data Source=182.92.4.68;Initial Catalog=cgt;MultipleActiveResultSets=True;user id=tonglei;password=reapal!tonglei;");
        }
    }
    public class T4Builder {
        public List<String> TableList = new List<String>();

        public List<TableFrame> Table = new List<TableFrame>();

        public T4Builder(string conectionstring) {
            SqlConnection con = new SqlConnection(conectionstring);
            con.Open();

            SqlCommand cmd = new SqlCommand("select name from sysobjects where xtype='u'", con);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var tablename = reader.GetValue(0).ToString();
                TableList.Add(tablename);
            }

            foreach (var tablename in TableList) {
                var TableFrame = new TableFrame();
                TableFrame.TableName = "dbo." + tablename;

                #region 获取主键
                SqlCommand cmd_prikey = new SqlCommand("EXEC sp_pkeys @table_name='" + tablename + "' ", con);
                var key_result = cmd_prikey.ExecuteReader();
                while (key_result.Read()) {
                    TableFrame.Primkey = key_result.GetValue(3) != null ? key_result.GetValue(3).ToString() : null;
                }
                SqlCommand cmd_isIdentity = new SqlCommand(string.Format("select  is_identity as 'identity' from sys.columns where object_ID = object_ID('{0}') and name = '{1}'", tablename, TableFrame.Primkey), con);
                var identity_result = cmd_isIdentity.ExecuteReader();
                while (identity_result.Read()) {
                    if (identity_result.GetValue(0) != null) {
                        TableFrame.IsIdentity = identity_result.GetValue(0).ToString().ToLower() == "true" ? true : false;
                    }
                }
                #endregion

                #region 获取列名
                SqlCommand cmd_table = new SqlCommand("select COLUMN_NAME,DATA_TYPE,NUMERIC_SCALE,IS_NULLABLE from information_schema.columns where TABLE_NAME='" + tablename + "'", con);
                var table_result = cmd_table.ExecuteReader();
                List<Colum> Column = new List<Colum>();

                while (table_result.Read()) {
                    Colum Columindex = new Colum();
                    Columindex.ColumnName = table_result.GetValue(0) != null ? table_result.GetValue(0).ToString() : null;
                    if (!String.IsNullOrEmpty(Columindex.ColumnName)) {
                        Columindex.ColumnAlias = tablename.ToString() == Columindex.ColumnName ? "_" + Columindex.ColumnName : null;
                        var ColumTypeStr = GetPropertyType(table_result.GetValue(1) != null ? table_result.GetValue(1).ToString() : null, table_result.GetValue(2) != null ? table_result.GetValue(2).ToString() : null);
                        if (table_result.GetValue(3).ToString() == "YES" && ColumTypeStr != "string" && ColumTypeStr != "Guid") {
                            ColumTypeStr = ColumTypeStr + "?";
                        }

                        Columindex.ColumnType = ColumTypeStr;
                        Column.Add(Columindex);
                    }
                }
                #endregion

                TableFrame.Column = Column;
                Table.Add(TableFrame);
            }
            con.Close();
            con.Dispose();
        }

        private string GetPropertyType(string sqlType, string dataScale) {
            string sysType = "string";
            sqlType = sqlType.ToLower();
            switch (sqlType) {
                case "bigint":
                    sysType = "long";
                    break;
                case "smallint":
                    sysType = "short";
                    break;
                case "int":
                    sysType = "int";
                    break;
                case "uniqueidentifier":
                    sysType = "Guid";
                    break;
                case "smalldatetime":
                case "datetime":
                case "date":
                    sysType = "DateTime";
                    break;
                case "float":
                    sysType = "double";
                    break;
                case "real":
                case "numeric":
                case "smallmoney":
                case "decimal":
                case "money":
                case "number":
                    sysType = "decimal";
                    break;
                case "tinyint":
                    sysType = "byte";
                    break;
                case "bit":
                    sysType = "bool";
                    break;
                case "image":
                case "binary":
                case "varbinary":
                case "timestamp":
                    sysType = "byte[]";
                    break;
            }
            if (sqlType == "number" && dataScale == "0")
                return "long";

            return sysType;
        }
    }



    public class TableFrame {
        public string TableName { get; set; }

        public string Primkey { get; set; }

        public bool IsIdentity { get; set; }

        public List<Colum> Column { get; set; }

        public string TableNameStr {
            get {
                return TableName != null ? TableName.Replace("dbo.", "").Trim() : null;
            }
        }
    }

    public class Colum {
        public string ColumnName { get; set; }

        public string ColumnType { get; set; }

        public string ColumnAlias { get; set; }
    }
}