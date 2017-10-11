using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco.NetCore;
using CGT.DDD.Logger;

namespace CGT.PetaPoco.Repositories {
    public class BaseRep {

        /// <summary>
        /// 多线程分批次查询 TODO
        /// </summary>
        /// <param name="iSize"></param>
        /// <param name="ids"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected List<T> MTBatchGetData<T>(int iSize, List<string> ids, Func<List<string>, List<T>> func) {
            iSize = iSize > 1 ? iSize : 100;
            var index = 0;

            //ids.Skip(index).Take(iSize);

            //TODO
            return func(ids);
        }

        /// <summary>
        /// TODO add Transaction
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="batchSize"></param>
        public int BulkInsert(IEnumerable<object> objects, Database db, int batchSize = 100) {

            var pd = Database.PocoData.ForType(objects.First().GetType());
            var tableName = pd.TableInfo.TableName;
            var parmerKey = pd.TableInfo.PrimaryKey;
            var autoInc = pd.TableInfo.AutoIncrement;

            var cols = new List<string>();

            //获取表行
            foreach (var i in pd.Columns) {

                if (i.Value.ResultColumn)
                    continue;
                if (autoInc && i.Value.ColumnName == parmerKey)
                    continue;
                cols.Add(i.Value.ColumnName);
            }

            var index = 0;

            var executeCount = 0;
            do {
                var currentList = objects.Skip(index).Take(batchSize).ToList();

                index += batchSize;

                var execuSql = new Sql();
                var allValues = new List<string>();
                foreach (var poco in currentList) {
                    var values = new List<string>();
                    var valuesIndex =new List<string>();
                    var columnIndex = 0;
                    foreach (var i in pd.Columns)
                    {
                        if (i.Value.ResultColumn)
                            continue;

                        if (autoInc && i.Value.ColumnName == parmerKey)
                            continue;

                        var value = i.Value.GetValue(poco);

                        values.Add( null ==value?"":value.ToString());

                        valuesIndex.Add("@"+columnIndex++);
                    }

                    execuSql.Append(string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, string.Join(", ", cols),string.Join(",",valuesIndex)), values.ToArray());

                    //allValues.Add("(" + string.Join(",", values.ToArray()) + ")");
                }

                // var sql = string.Format("INSERT INTO {0} ({1}) VALUES {2}", tableName, string.Join(", ", cols), string.Join(", ", allValues));

                LoggerFactory.Instance.Logger_Info("sql语句：" + execuSql.SQL, "TravelOrderImportService");
                LoggerFactory.Instance.Logger_Info("sql参数：" + execuSql.Arguments.ToString(), "TravelOrderImportService");

                executeCount += db.Execute(execuSql);

            } while (index <= objects.Count());

            return executeCount;
        }
    }
}
