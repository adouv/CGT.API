using CGT.Entity.CgtTravelModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTravel {
    /// <summary>
    /// 企业白名单仓储
    /// </summary>
    public class EnterpriseWhiteRep {
        /// <summary>
        /// 获取企业白名单列表
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public List<EnterpriseWhiteList> GetEnterpriseWhiteList(EnterpriseWhiteList model) {
            #region sql
            string wherestr = string.Empty;

            if (!string.IsNullOrWhiteSpace(model.PayCenterCode)) {
                wherestr += " AND PayCenterCode = @0";
            }
            string sql = string.Format(@"
SELECT  *
FROM    dbo.EnterpriseWhiteList
WHERE   1 = 1
{0}
            ", wherestr);

            #endregion

            return CgtTravelDB.GetInstance().Query<EnterpriseWhiteList>(sql,
                  model.PayCenterCode).ToList();
        }
        /// <summary>
        /// 获取企业白名单列表(分页)
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public Page<EnterpriseWhiteList> GetEnterpriseWhitePageList(EnterpriseWhiteList model, int PageIndex, int PageSize) {
            #region sql
            string wherestr = string.Empty;

            if (!string.IsNullOrWhiteSpace(model.PayCenterCode)) {
                wherestr += " AND PayCenterCode = @0";
            }
            if (model.EnterpriseWhiteListID > 0) {
                wherestr += " AND EnterpriseWhiteListID = @1";
            }
            if (model.EnterpriseStatue > -1) {
                wherestr += " AND EnterpriseStatue = @2";
            }
            if (model.FreezeWay > -1) {
                wherestr += " AND FreezeWay = @3";
            }
            if (model.BeginDate != null) {
                wherestr += " AND CreateTime >= @4";
            }
            if (model.EndDate != null) {
                wherestr += " AND CreateTime <= @5";
            }
            if (model.MonthStatue.HasValue && model.MonthStatue != -1) {
                wherestr += " AND MonthStatue=@6";
            }
            string sql = string.Format(@"
SELECT  *
FROM    dbo.EnterpriseWhiteList
WHERE   1 = 1
{0}
            ", wherestr);

            #endregion

            return CgtTravelDB.GetInstance().Page<EnterpriseWhiteList>(PageIndex, PageSize, sql,
                  model.PayCenterCode, model.EnterpriseWhiteListID, model.EnterpriseStatue, model.FreezeWay,
                    Convert.ToDateTime(model.BeginDate).ToString("yyyy-MM-dd HH:mm:ss"),
                    Convert.ToDateTime(model.EndDate).ToString("yyyy-MM-dd") + " 23:59:59",
                                      model.MonthStatue
                  );
        }
        /// <summary>
        /// 获取企业信息和当月返现总额列表(分页)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public Page<EnterpriseWhiteList> GetEnterpriseWhiteAndBackMoneyPageList(EnterpriseWhiteList model, int PageIndex, int PageSize) {
            //初始化当月第一天最后一天
            var date = DateTime.Now;
            var d1 = new DateTime(date.Year, date.Month, 1);
            var d2 = d1.AddMonths(1);
            var sql = GetSqlData(model);
            return CgtTravelDB.GetInstance().Page<EnterpriseWhiteList>(PageIndex, PageSize, sql,
                  model.PayCenterCode, model.EnterpriseWhiteListID, model.EnterpriseStatue, model.FreezeWay,

                    Convert.ToDateTime(model.BeginDate).ToString("yyyy-MM-dd HH:mm:ss"),
                    Convert.ToDateTime(model.EndDate).ToString("yyyy-MM-dd") + " 23:59:59",
                                      model.MonthStatue,
                                      d1,
                                      d2
                  );

        }
        /// <summary>
        /// 获取企业信息和当月返现总额列表(不分页)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<EnterpriseWhiteList> GetEnterpriseWhiteAndBackMoneyList(EnterpriseWhiteList model) {
            //初始化当月第一天最后一天
            var date = DateTime.Now;
            var d1 = new DateTime(date.Year, date.Month, 1);
            var d2 = d1.AddMonths(1);
            var sql = GetSqlData(model);
            return CgtTravelDB.GetInstance().Fetch<EnterpriseWhiteList>(sql,
                  model.PayCenterCode, model.EnterpriseWhiteListID, model.EnterpriseStatue, model.FreezeWay,

                    Convert.ToDateTime(model.BeginDate).ToString("yyyy-MM-dd HH:mm:ss"),
                    Convert.ToDateTime(model.EndDate).ToString("yyyy-MM-dd") + " 23:59:59",
                                      model.MonthStatue,
                                      d1,
                                      d2);
        }

        /// <summary>
        /// 根据企业id获取企业白名单信息
        /// </summary>
        /// <param name="enterpriseIds"></param>
        /// <returns></returns>

 
        public IEnumerable<EnterpriseWhiteList> GetEnterpriseWhiteLists(IEnumerable<long> enterpriseIds,string payCenterCode)
        {
 

            using (var db = CgtTravelDB.GetInstance()) {

 
                var strSql = string.Format("SELECT * FROM dbo.EnterpriseWhiteList WHERE EnterpriseWhiteListID IN({0}) AND   EnterpriseStatue IN(1, 2) AND PayCenterCode='{1}'", string.Join(",", enterpriseIds), payCenterCode);
 
                var result =
                    db.Query<EnterpriseWhiteList>(strSql).ToList();

                return result;
            }
        }

        private string GetSqlData(EnterpriseWhiteList model) {
            #region sql
            string wherestr = string.Empty;

            if (!string.IsNullOrWhiteSpace(model.PayCenterCode)) {
                wherestr += " AND b.PayCenterCode = @0";
            }
            if (model.EnterpriseWhiteListID > 0) {
                wherestr += " AND b.EnterpriseWhiteListID = @1";
            }
            if (model.EnterpriseStatue > -1) {
                wherestr += " AND b.EnterpriseStatue = @2";
            }
            if (model.FreezeWay > -1) {
                wherestr += " AND b.FreezeWay = @3";
            }
            if (model.BeginDate != null) {
                wherestr += " AND b.CreateTime >= @4";
            }
            if (model.EndDate != null) {
                wherestr += " AND b.CreateTime <= @5";
            }
            if (model.MonthStatue.HasValue && model.MonthStatue != -1) {
                wherestr += " AND b.MonthStatue=@6";
            }
            string sql = string.Format(@"
	SELECT b.*,TicketAmount FROM 
(SELECT   SUM(TicketAmount) AS TicketAmount ,
							EnterpriseWhiteListID
				   FROM     dbo.EnterpriseOrder WITH ( NOLOCK )
				   WHERE    1 = 1
							AND BackStatus = 1
							AND RepaymentStatus = 0
							AND BackTime BETWEEN @7 AND @8
				   GROUP BY EnterpriseWhiteListID) aa 
RIGHT JOIN (  SELECT   *
				   FROM     dbo.EnterpriseWhiteList ) b
ON aa.EnterpriseWhiteListID=b.EnterpriseWhiteListID
WHERE   1 = 1
{0} order by AccountBalance
            ", wherestr);

            #endregion 

            return sql;
        }

        /// <summary>
        /// 查询所有企业和分销2级联动列表数据   
        /// </summary>
        /// <returns></returns>
        public List<EnterpriseWhiteList> EnterpriseInfoList() {
            using (var db = CgtTravelDB.GetInstance()) {
                string sql = "SELECT a.EnterpriseName,a.EnterpriseWhiteListID,a.PayCenterName,a.PayCenterCode,b.UserId,b.UserName FROM dbo.EnterpriseWhiteList a INNER JOIN " +
                             "cgt.dbo.UserAccount b ON a.PayCenterCode=b.PayCenterCode ORDER BY  a.PayCenterName DESC";
                return db.Query<EnterpriseWhiteList>(sql).ToList();
            }
        }
        /// <summary>
        /// 修改月限额金额
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditEnterpriseWhiteListMonthAmount(EnterpriseWhiteList model) {
            return CgtTravelDB.GetInstance().Execute("update [EnterpriseWhiteList] set CreditMonthAmount=@0 where EnterpriseWhiteListID=@1", model.CreditMonthAmount, model.EnterpriseWhiteListID);
        }
        /// <summary>
        /// 修改月限额状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditEnterpriseWhiteListMonthStatus(EnterpriseWhiteList model) {
            return CgtTravelDB.GetInstance().Execute("update [EnterpriseWhiteList] set MonthStatue=@0 where EnterpriseWhiteListID=@1", model.MonthStatue, model.EnterpriseWhiteListID);
        }
        /// <summary>
        /// 修改企业冻结状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditEnterpriseWhiteListEnterpriseStatue(EnterpriseWhiteList model) {
            return CgtTravelDB.GetInstance().Execute("update [EnterpriseWhiteList] set EnterpriseStatue=@0,ModifiedName=@2,ModifiedTime=getdate() where EnterpriseWhiteListID=@1", model.EnterpriseStatue, model.EnterpriseWhiteListID, model.ModifiedName);
        }
        /// <summary>
        /// 修改企业冻结方式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditEnterpriseWhiteListFreezeWay(EnterpriseWhiteList model) {
            return CgtTravelDB.GetInstance().Execute("update [EnterpriseWhiteList] set FreezeWay=@0,ModifiedName=@2,ModifiedTime=getdate() where EnterpriseWhiteListID=@1", model.FreezeWay, model.EnterpriseWhiteListID, model.ModifiedName);
        }

        /// <summary>
        /// 获取售信总额度和剩余售信额度
        /// </summary>
        /// <param name="RemainingCreditLimit">剩余售信额度</param>
        /// <returns>售信总额度</returns>
        public decimal GetEnterpriseWhiteOverView(out decimal RemainingCreditLimit) {

            RemainingCreditLimit = 0m;
            #region sql

            string sql = @"
            SELECT  SUM(CreditAmount)
            FROM    dbo.EnterpriseWhiteList
            WHERE PayCenterCode not in('PXTXCSZH79710107','self_001','RBXSCS33710101')
            ";
            string strsql = @"
            SELECT  SUM(AccountBalance)
            FROM    dbo.EnterpriseWhiteList
            WHERE PayCenterCode not in('PXTXCSZH79710107','self_001','RBXSCS33710101')
            ";
            #endregion

            RemainingCreditLimit = CgtTravelDB.GetInstance().ExecuteScalar<decimal>(strsql);
            return CgtTravelDB.GetInstance().ExecuteScalar<decimal>(sql);
        }

        /// <summary>
        /// 获取售信总额度和剩余售信额度（按分销商）
        /// </summary>
        /// <param name="RemainingCreditLimit">剩余售信额度</param>
        /// <param name="PayCenterCode">分销商Code</param>
        /// <returns>售信总额度</returns>
        public decimal GetEnterpriseWhiteOverView(string PayCenterCode, out decimal RemainingCreditLimit) {

            RemainingCreditLimit = 0m;
            #region sql

            string sql = string.Format(@"
            SELECT  SUM(CreditAmount)
            FROM    dbo.EnterpriseWhiteList
            WHERE PayCenterCode ='{0}'", PayCenterCode);
            string strsql = string.Format(@"
            SELECT  SUM(AccountBalance)
            FROM    dbo.EnterpriseWhiteList
            WHERE PayCenterCode ='{0}'", PayCenterCode);
            #endregion

            RemainingCreditLimit = CgtTravelDB.GetInstance().ExecuteScalar<decimal>(strsql.ToString());
            return CgtTravelDB.GetInstance().ExecuteScalar<decimal>(sql.ToString());
        }
        /// <summary>
        /// 修改企业余额
        /// </summary>
        /// <returns></returns>
        public int UpdateEnterpriseWhiteListAccountBalance(decimal Amount, int EnterpriseWhiteListID, int Type) {
            #region sql
            string wherestr = string.Empty;
            wherestr += " AND EnterpriseWhiteListID = @1";
            string sql = "";
            if (Type == 0) {
                sql = string.Format(@"
            SET AccountBalance=AccountBalance+@0
            WHERE   1 = 1
            {0}
            ", wherestr);
            }
            else {
                sql = string.Format(@"
            SET AccountBalance=AccountBalance-@0
            WHERE   1 = 1
            {0}
            ", wherestr);
            }
            #endregion
            return CgtTravelDB.GetInstance().Update<EnterpriseOrder>(sql, Amount, EnterpriseWhiteListID);
        }

        /// <summary>
        /// 修改企业授信额度
        /// </summary>
        /// <param name="EnterpriseId">企业ID</param>
        /// <param name="CreditAmount">授信总额度</param>
        /// <returns></returns>
        public int UpdateEnterpriseCreditAmount(long EnterpriseId, decimal CreditAmount)
        {
            #region sql
            string wherestr = string.Empty;
            wherestr += " AND EnterpriseWhiteListID = @1";
            string sql = "";
            
            sql = string.Format(@"
            SET AccountBalance=AccountBalance+@0,CreditAmount=CreditAmount+@2
            WHERE   1 = 1
            {0}
            ", wherestr);
            
            #endregion
            return CgtTravelDB.GetInstance().Update<EnterpriseWhiteList>(sql, CreditAmount, EnterpriseId, CreditAmount);
        }
    }
}
