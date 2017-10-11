
using CGT.Entity.CgtTravelModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTravel {
    /// <summary>
    /// 差旅订单仓储
    /// </summary>
    public class EnterpriseOrderRep : BaseRep {
        /// <summary>
        /// 分销平台订单查询
        /// </summary>
        /// <returns></returns>
        public dynamic GetDistriPaltFormTravelOrder(int? EnterpriseID, string PayCenterCode, string OrderId, string TravelBatchId, DateTime? TicketTimeBegion, DateTime? TicketTimeEnd, DateTime? BackTimeBegion, DateTime? BackTimeEnd, string ReviewState, DateTime? ReviewTimeBegion, DateTime? ReviewTimeEnd, int? TravelRiskState, int? TravelRiskType, int? BackStatus, int pageindex, int pagesize) {
            #region sql
            string wherestr = string.Empty;
            if (EnterpriseID > 0) {
                wherestr += " AND o.EnterpriseWhiteListID = @0";
            }
            if (!string.IsNullOrWhiteSpace(PayCenterCode)) {
                wherestr += " AND o.PayCenterCode = @1";
            }
            if (!string.IsNullOrWhiteSpace(OrderId)) {
                wherestr += " AND o.OrderId = @2";
            }
            if (!string.IsNullOrWhiteSpace(TravelBatchId)) {
                wherestr += " AND o.OrderTravelBatchId = @3";
            }
            if (TicketTimeBegion != null) {
                wherestr += " AND o.createtime >= @4";
            }
            if (TicketTimeEnd != null) {
                wherestr += " AND o.createtime <= @5";
            }
            if (BackTimeBegion != null) {
                wherestr += " AND BackTime>= @6";
            }
            if (BackTimeEnd != null) {
                wherestr += " AND BackTime<= @7";
            }
            if (BackStatus != null) {
                wherestr += " AND BackStatus= @8";
            }
            string sql = string.Format(@"
select o.* ,c.EnterpriseName from EnterpriseOrder o inner join EnterpriseWhiteList c
 on o.EnterpriseWhiteListID=c.EnterpriseWhiteListID
  where 1=1 {0} order by o.CreateTime desc
", wherestr);
            string sqlQuery = string.Format(@"select TicketAmount from EnterpriseOrder o inner join EnterpriseWhiteList c
 on o.EnterpriseWhiteListID=c.EnterpriseWhiteListID
  where 1=1 and BackStatus=1 {0}
", wherestr);
            #endregion
            var EnterpriseOrderList = CgtTravelDB.GetInstance().Query<EnterpriseOrder>(sqlQuery, EnterpriseID, PayCenterCode, OrderId, TravelBatchId, TicketTimeBegion, TicketTimeEnd, BackTimeBegion, BackTimeEnd, BackStatus);
            var TicketAmount = EnterpriseOrderList.Sum(r => r.TicketAmount);
            var page = CgtTravelDB.GetInstance().Page<dynamic>(pageindex, pagesize, sql, EnterpriseID, PayCenterCode, OrderId, TravelBatchId, TicketTimeBegion, TicketTimeEnd, BackTimeBegion, BackTimeEnd, BackStatus);
            return new {
                data = page,
                TicketAmount = TicketAmount
            };
        }

        /// <summary>
        /// 获取差旅订单和风控信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic GetEnterpriseOrderAndRiskList(int? EnterpriseID, string PayCenterCode, string OrderId, string TravelBatchId, DateTime? TicketTimeBegion, DateTime? TicketTimeEnd, DateTime? BackTimeBegion, DateTime? BackTimeEnd, string ReviewState, DateTime? ReviewTimeBegion, DateTime? ReviewTimeEnd, int? TravelRiskState, int? TravelRiskType, int? BackStatus, int pageindex, int pagesize) {

            #region sql
            string wherestr = string.Empty;
            if (EnterpriseID > 0) {
                wherestr += " AND o.EnterpriseWhiteListID = @0";
            }
            if (!string.IsNullOrWhiteSpace(PayCenterCode)) {
                wherestr += " AND o.PayCenterCode = @1";
            }
            if (!string.IsNullOrWhiteSpace(OrderId)) {
                wherestr += " AND o.OrderId = @2";
            }
            if (!string.IsNullOrWhiteSpace(TravelBatchId)) {
                wherestr += " AND r.TravelBatchId = @3";
            }
            if (TicketTimeBegion != null) {
                wherestr += " AND o.createtime >= @4";
            }
            if (TicketTimeEnd != null) {
                wherestr += " AND o.createtime <= @5";
            }
            if (BackTimeBegion != null) {
                wherestr += " AND BackTime>= @6";
            }
            if (BackTimeEnd != null) {
                wherestr += " AND BackTime<= @7";
            }
            if (!string.IsNullOrWhiteSpace(ReviewState)) {
                var ReviewStatelist = ReviewState.Split(',');
                var ReviewStates = string.Join("','", ReviewStatelist);
                wherestr += @" AND ReviewState IN ('" + ReviewStates + "')";
            }
            if (ReviewTimeBegion != null) {
                wherestr += " AND ReviewTime>= @8";
            }
            if (ReviewTimeEnd != null) {
                wherestr += " AND ReviewTime<= @9";
            }
            if (TravelRiskState > -1) {
                wherestr += " AND TravelRiskState= @10";
            }
            if (TravelRiskType > -1) {
                wherestr += " AND TravelRiskType= @11";
            }
            if (BackStatus != null) {
                wherestr += " AND BackStatus= @12";
            }
            string sql = string.Format(@"
SELECT o.*,r.*,c.EnterpriseName,ue.UserName as ReviewName,b.RealpayDateTime
FROM dbo.EnterpriseOrder o  
LEFT  JOIN dbo.EnterpriseOrderRisk r
ON o.OrderId =r.EOrderId 
left JOIN dbo.BillEveryDay b
on o.OrderId=b.OrderId
LEFT JOIN dbo.EnterpriseWhiteList c
ON c.EnterpriseWhiteListID=o.EnterpriseWhiteListID 
LEFT JOIN cgt_user.dbo.[user] ue on r.ReviewUserId = ue.UserId
WHERE   1 = 1 {0}
 order by o.createtime desc
", wherestr);
            #endregion
            var EnterpriseOrderList = CgtTravelDB.GetInstance().Query<EnterpriseOrder>(sql, EnterpriseID, PayCenterCode, OrderId, TravelBatchId, TicketTimeBegion, TicketTimeEnd, BackTimeBegion, BackTimeEnd, ReviewTimeBegion, ReviewTimeEnd, TravelRiskState, TravelRiskType, BackStatus);
            var TicketAmount = EnterpriseOrderList.Where(r => r.BackStatus == 1).Sum(r => r.TicketAmount);
            var page = CgtTravelDB.GetInstance().Page<EnterpriseOrder>(pageindex, pagesize, sql, EnterpriseID, PayCenterCode, OrderId, TravelBatchId, TicketTimeBegion, TicketTimeEnd, BackTimeBegion, BackTimeEnd, ReviewTimeBegion, ReviewTimeEnd, TravelRiskState, TravelRiskType, BackStatus);

            return new {
                data = page,
                TicketAmount = TicketAmount
            };
        }
        /// <summary>
        /// 订单数据下载
        /// </summary>
        /// <returns></returns>
        public List<EnterpriseOrder> GetDownloadEnterpriseOrderList(int? EnterpriseID, string PayCenterCode, string OrderId, string TravelBatchId, DateTime? TicketTimeBegion, DateTime? TicketTimeEnd, DateTime? BackTimeBegion, DateTime? BackTimeEnd, string ReviewState, DateTime? ReviewTimeBegion, DateTime? ReviewTimeEnd, int? TravelRiskState, int? TravelRiskType, int? BackStatus) {
            #region sql
            string wherestr = string.Empty;
            if (EnterpriseID > 0) {
                wherestr += " AND o.EnterpriseWhiteListID = @0";
            }
            if (!string.IsNullOrWhiteSpace(PayCenterCode)) {
                wherestr += " AND o.PayCenterCode = @1";
            }
            if (!string.IsNullOrWhiteSpace(OrderId)) {
                wherestr += " AND OrderId = @2";
            }
            if (!string.IsNullOrWhiteSpace(TravelBatchId)) {
                wherestr += " AND TravelBatchId = @3";
            }
            if (TicketTimeBegion != null) {
                wherestr += " AND o.createtime >= @4";
            }
            if (TicketTimeEnd != null) {
                wherestr += " AND o.createtime <= @5";
            }
            if (BackTimeBegion != null) {
                wherestr += " AND BackTime>= @6";
            }
            if (BackTimeEnd != null) {
                wherestr += " AND BackTime<= @7";
            }
            if (!string.IsNullOrWhiteSpace(ReviewState)) {
                var ReviewStatelist = ReviewState.Split(',');
                var ReviewStates = string.Join("','", ReviewStatelist);
                wherestr += @" AND ReviewState IN ('" + ReviewStates + "')";
            }
            if (ReviewTimeBegion != null) {
                wherestr += " AND ReviewTime>= @8";
            }
            if (ReviewTimeEnd != null) {
                wherestr += " AND ReviewTime<= @9";
            }
            if (TravelRiskState > -1) {
                wherestr += " AND TravelRiskState= @10";
            }
            if (TravelRiskType > -1) {
                wherestr += " AND TravelRiskType= @11";
            }
            if (BackStatus != null && BackStatus > -1) {
                wherestr += " AND BackStatus= @12";
            }
            string sql = string.Format(@"
                SELECT o.*,r.*,c.EnterpriseName,ue.UserName as ReviewName,(case o.BackStatus when 0 then '未返现' else '已返现' end) as BackStatusStr
                FROM dbo.EnterpriseOrder o  
                LEFT  JOIN dbo.EnterpriseOrderRisk r
                ON o.OrderId =r.EOrderId 
                LEFT JOIN dbo.EnterpriseWhiteList c
                ON c.EnterpriseWhiteListID=o.EnterpriseWhiteListID 
                LEFT JOIN cgt_user.dbo.[user] ue on r.ReviewUserId = ue.UserId
                WHERE   1 = 1
                {0}
            ", wherestr);
            #endregion

            return CgtTravelDB.GetInstance().Query<EnterpriseOrder>(sql, EnterpriseID, PayCenterCode, OrderId, TravelBatchId, TicketTimeBegion, TicketTimeEnd, BackTimeBegion, BackTimeEnd, ReviewTimeBegion, ReviewTimeEnd, TravelRiskState, TravelRiskType, BackStatus).ToList();
        }
        /// <summary>
        /// 查询差旅订单财务报表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public dynamic GetEnterpriseOrderData(FactoringOrderModel model, int pageindex, int pagesize) {
            string sql = GetsqlData(model);
            var dataPage = CgtTravelDB.GetInstance().Page<FactoringOrderDownload>(pageindex, pagesize, sql,
                  model.BillId,
                  Convert.ToDateTime(model.BeginBillDate).ToString("yyyy-MM-dd HH:mm:ss"),
                  Convert.ToDateTime(model.EndBillDate).ToString("yyyy-MM-dd") + " 23:59:59",
                  model.EnterpriseId,
                  model.EnterpriseName + "%",
                  model.MerchantCode,
                  model.MerchantType,
                  model.RepaymentStatus,
                  model.OrderTravelBatchId,
                  model.PayCenterCode,
                model.BackStatus
                );
            var dataList = CgtTravelDB.GetInstance().Query<FactoringOrderDownload>(sql,
                    model.BillId,
                    Convert.ToDateTime(model.BeginBillDate).ToString("yyyy-MM-dd HH:mm:ss"),
                    Convert.ToDateTime(model.EndBillDate).ToString("yyyy-MM-dd") + " 23:59:59",
                    model.EnterpriseId,
                    model.EnterpriseName + "%",
                    model.MerchantCode,
                    model.MerchantType,
                    model.RepaymentStatus,
                    model.OrderTravelBatchId,
                    model.PayCenterCode,
                    model.BackStatus
                  );
            var result = new {
                data = dataPage,
                TicketAmount = dataList.Sum(r => string.IsNullOrEmpty(r.TicketAmount) ? 0 : decimal.Parse(r.TicketAmount)),//应还款总金额
                InsuredAmount = dataList.Where(c => c.BackStatus == "1").Sum(r => string.IsNullOrEmpty(r.InsuredAmount) ? 0 : decimal.Parse(r.InsuredAmount)),//保证险总金额                                                                                                     // BillAmount=dataList.Sum(r=>string.IsNullOrEmpty())
                BillAmount = dataList.Where(c => c.BackStatus == "1").Sum(r => string.IsNullOrEmpty(r.TicketAmount) ? 0 : decimal.Parse(r.TicketAmount)),//返现总金额
                BillInterest = dataList.Where(c => c.BackStatus == "1").Sum(r => string.IsNullOrEmpty(r.BillInterest) ? 0 : decimal.Parse(r.BillInterest))//利息总金额
            };
            return result;
        }
        /// <summary>
        /// 下载订单财务报表
        /// </summary>
        /// <returns></returns>
        public List<FactoringOrderDownload> GetEnterpriseOrderDownload(FactoringOrderModel model) {
            string sql = GetsqlData(model);
            var data = CgtTravelDB.GetInstance().Query<FactoringOrderDownload>(sql,
                    model.BillId,
                    Convert.ToDateTime(model.BeginBillDate).ToString("yyyy-MM-dd HH:mm:ss"),
                    Convert.ToDateTime(model.EndBillDate).ToString("yyyy-MM-dd") + " 23:59:59",
                    model.EnterpriseId,
                    model.EnterpriseName + "%",
                    model.MerchantCode,
                    model.MerchantType,
                    model.RepaymentStatus,
                    model.OrderTravelBatchId,
                    model.PayCenterCode,
                    model.BackStatus
                  ).ToList();
            return data;
        }
        /// <summary>
        /// 财务报表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetsqlData(FactoringOrderModel model) {
            #region sql
            string wherestr = string.Empty;
            if (model.BillId != null && model.BillId > 0) {
                wherestr += " AND c.BillId = @0";
            }
            if (model.BeginBillDate != null) {
                if (!string.IsNullOrWhiteSpace(model.DataSource)) {
                    wherestr += " AND a.CreateTime >= @1";
                }
                else {
                    wherestr += " AND a.BackTime >= @1";
                }
            }
            if (model.EndBillDate != null) {
                if (!string.IsNullOrWhiteSpace(model.DataSource)) {
                    wherestr += " AND a.CreateTime <= @2";
                }
                else {
                    wherestr += " AND a.BackTime <= @2";
                }
            }
            if (model.EnterpriseId != null && model.EnterpriseId > 0) {
                wherestr += " AND b.EnterpriseWhiteListID = @3";
            }
            if (!string.IsNullOrWhiteSpace(model.EnterpriseName)) {
                wherestr += " AND b.EnterpriseName LIKE @4";
            }
            if (!string.IsNullOrWhiteSpace(model.MerchantCode)) {
                wherestr += " AND a.PayCenterCode = @5";
            }
            if (model.RepaymentStatus > 0) {
                wherestr += " AND RepaymentStatus=@7";
            }

            if (!string.IsNullOrWhiteSpace(model.OrderTravelBatchId)) {
                wherestr += " AND a.OrderTravelBatchId= @8";
            }
            if (!string.IsNullOrWhiteSpace(model.PayCenterCode)) {
                wherestr += " AND a.PayCenterCode= @9";
            }
            if (model.BackStatus != null) {
                wherestr += " AND BackStatus= @10";
            }
            string sql = string.Format(@"
SELECT  a.OrderTravelBatchId,
        ISNULL(c.BillId, 0) AS BillId ,
        a.OrderId ,
        a.AdvancesName ,
        a.PayCenterName ,
        b.EnterpriseName ,
        a.BillDate ,
        c.BillInterest AS UserInterest ,
        a.UserInterestRate,
        a.TicketAmount ,
        ( CASE WHEN a.RepaymentStatus >= 1 THEN a.TicketAmount
               ELSE 0
          END ) AS RepaymentTicketAmount ,
        ( CASE WHEN a.RepaymentStatus >= 1 THEN c.RealpayDateTime
               ELSE NULL
          END ) AS RepaymentDate ,
        ( CASE WHEN a.RepaymentStatus = 0 THEN '未还款'
               WHEN a.RepaymentStatus = 1 THEN '已还款'
               WHEN a.RepaymentStatus = 2 THEN '提前还款'
               WHEN a.RepaymentStatus = 3 THEN '逾期还款'
               ELSE NULL
          END ) AS RepaymentStatusName,
        a.RepaymentStatus,
        0 AS OverdueDays ,
        0 AS OverdueFine ,
        convert(decimal(16, 2), ISNULL(( SELECT dbo.FUN_GetRefundAmount(c.RealpayDateTime, a.BillDate,
                                                a.BillDateTime,
                                                a.UserInterestRate,
                                                a.TicketAmount)
               ), 0)) AS RefundAmount,
         convert(decimal(16, 2),ISNULL(( SELECT dbo.FUN_GetFactoringInterestAmount(c.BillEveryDaysId,
                                                           a.FactoringInterestRate,
                                                           a.TicketAmount,
                                                           a.BackTime,
                                                           c.RealpayDateTime)
               ), 0)) AS FactoringInterestAmount,
        a.FactoringInterestRate,
		a.TicketTime,
		a.BackTime,
		a.InsuredAmount,
		a.Pnr,
		a.FlightNo,
		a.PassengerName,
		a.DepartureTime,
        c.BillAmount,
        c.BillInterest,
         a.BackStatus,
        ( CASE WHEN a.BackStatus = 0 THEN '未返现'
               WHEN a.BackStatus = 1 THEN '返现成功'
               ELSE NULL
          END ) AS BackStatusName,
        @6 as MerchantType
FROM    dbo.EnterpriseOrder a WITH ( NOLOCK )
        INNER JOIN dbo.EnterpriseWhiteList b WITH ( NOLOCK ) ON a.EnterpriseWhiteListID = b.EnterpriseWhiteListID
        LEFT JOIN dbo.BillEveryDay c WITH ( NOLOCK ) ON a.BillEveryDayId = c.BillEveryDaysId
WHERE
1=1 
{0}
", wherestr);

            #endregion

            return sql;
        }
        /// <summary>
        /// 获取差旅订单
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public EnterpriseOrder GetEnterpriseOrder(string OrderId) {
            #region sql
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(OrderId)) {
                wherestr += " AND OrderId = @0";
            }
            string sql = string.Format(@"
            SELECT  *
            FROM    dbo.EnterpriseOrder
            WHERE   1 = 1
            {0}
            ", wherestr);
            #endregion

            return CgtTravelDB.GetInstance().SingleOrDefault<EnterpriseOrder>(sql,
                              OrderId);
        }
        /// <summary>
        /// 更新差旅订单返现状态
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public int UpdateEnterpriseOrderBackState(EnterpriseOrder _EnterpriseOrder) {
            #region sql
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(_EnterpriseOrder.OrderId)) {
                wherestr += " AND OrderId = @0";
            }
            string sql = string.Format(@"
            SET BackStatus=1,BackTime=@1,UserInterestRate=@2,UserInterest=@3
            WHERE   1 = 1
            {0}
            ", wherestr);
            #endregion
            return CgtTravelDB.GetInstance().Update<EnterpriseOrder>(sql, _EnterpriseOrder.OrderId, DateTime.Now, _EnterpriseOrder.UserInterestRate, _EnterpriseOrder.UserInterest);
        }
        /// <summary>
        /// 获取账单详情通过企业编号列表和账单号
        /// </summary>
        /// <returns></returns>
        public List<BillEveryDay> GetBillEveryDaysListByEnterpriseIdsBill(int billid, List<string> Enterpriselist) {
            #region sql
            string wherestr = string.Empty;
            if (billid > 0) {
                wherestr += @" AND BillId = @0 ";
            }
            if (Enterpriselist != null) {
                var Enterprises = string.Join("','", Enterpriselist);
                wherestr += @" AND EnterpriseId IN ('" + Enterprises + "')";
            }
            string sql = string.Format(@"SELECT  *
FROM    dbo.BillEveryDay
WHERE   1 = 1 AND Status=0 {0}"
, wherestr);
            #endregion

            var data = CgtTravelDB.GetInstance().Query<BillEveryDay>(sql, billid).ToList();
            return data;
        }
        /// <summary>
        /// 更新差旅订单通过日账单编号
        /// </summary>
        /// <returns></returns>
        public int UpdateEnterpriseOrderlistByBillEveryDayId(List<BillEveryDay> BillEveryDaylist, int repaymentStatus) {
            #region sql
            string wherestr = string.Empty;
            if (BillEveryDaylist.Any()) {
                var BillEveryDayIdlist = new List<long>();
                foreach (var BillEveryDay in BillEveryDaylist) {
                    BillEveryDayIdlist.Add(Convert.ToInt64(BillEveryDay.BillEveryDaysId));
                }
                var BillEveryDayliststr = string.Join(",", BillEveryDayIdlist);
                wherestr += @" AND BillEveryDayId IN (" + BillEveryDayliststr + ")";
            }
            string sql = string.Format(@"
                SET  RepaymentStatus=@0 WHERE   1 = 1 {0}
            ", wherestr);
            #endregion

            var data = CgtTravelDB.GetInstance().Update<EnterpriseOrder>(sql, repaymentStatus);
            return data;
        }

        /// <summary>
        /// 批量修改企业授信余额
        /// </summary>
        public int UpdateEnterpriseBalance(List<BillEveryDay> BillEveryDays) {
            int index = 0;
            var BillEveryDaysbyEnterprises = BillEveryDays.GroupBy(i => i.EnterpriseId);
            CgtTravelDB.GetInstance().BeginTransaction();
            try {
                foreach (var BillEveryDaysbyEnterprise in BillEveryDaysbyEnterprises) {
                    string wherestr = string.Empty;
                    var totalAmount = BillEveryDaysbyEnterprise.Sum(i => i.BillAmount);
                    var EnterpriseWhiteListId = BillEveryDaysbyEnterprise.Key;
                    wherestr += "EnterpriseWhiteListID=" + EnterpriseWhiteListId;
                    string sql = string.Format(@"SET  AccountBalance+=@0
WHERE   1 = 1 AND {0}"
                    , wherestr);
                    index += CgtTravelDB.GetInstance().Update<EnterpriseWhiteList>(sql, totalAmount);
                }
                CgtTravelDB.GetInstance().CompleteTransaction();
            }
            catch {
                CgtTravelDB.GetInstance().AbortTransaction();
            }
            return index;
        }

        /// <summary>
        /// 批量添加企业订单
        /// </summary>
        /// <returns></returns>
        public object AddEnterpriseOrders(List<EnterpriseOrder> EnterpriseOrderList) {
            return this.BulkInsert(EnterpriseOrderList, CgtTravelDB.GetInstance());
        }
        /// <summary>
        /// 获取企业制定时间内的订单数量
        /// </summary>
        /// <param name="entierprseIds"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public List<Tuple<long, decimal>> GetEnterpriseOrderSum(IEnumerable<long> entierprseIds, DateTime dtStart, DateTime dtEnd) {

            using (var db = CgtTravelDB.GetInstance()) {
                var result = db.Query<Tuple<long, decimal>>(
                   @"SELECT EnterpriseWhiteListID Item1,SUM(TicketAmount) Item2 FROM dbo.EnterpriseOrder  WHERE EnterpriseOrderId IN({0}) AND  BackStatus = 1 AND RepaymentStatus = 0  
AND BackTime>{1} AND BackTime < {2} GROUP BY EnterpriseWhiteListID; ",
                    new {
                        Join = string.Join(",", entierprseIds),
                        dtStart,
                        dtEnd
                    }).ToList();
                return result;
            }
        }

        /// <summary>
        /// 获取分销商票额
        /// </summary>
        /// <param name="PayCenterCode"></param>
        /// <returns></returns>
        public decimal GetDistributSum(string payCenterCode) {
            using (var db = CgtTravelDB.GetInstance()) {
                string sql = @"
                SELECT SUM(TicketAmount) FROM [dbo].[EnterpriseOrder]
                WHERE PayCenterCode=@0 AND BackStatus=1 AND RepaymentStatus=0 
               ";
                return db.ExecuteScalar<decimal>(sql, payCenterCode);
            }
        }
        /// <summary>
        /// 批量更新差旅订单返现状态和时间
        /// </summary>
        /// <returns></returns>
        public int UpdateEnterpriseOrderBatchBackState(List<string> EnterpriseOrderlist) {
            #region sql
            string wherestr = string.Empty;
            string sql = string.Empty;
            if (EnterpriseOrderlist.Any()) {
                var EnterpriseOrderliststr = string.Join("','", EnterpriseOrderlist);
                wherestr += @" AND OrderId IN ('" + EnterpriseOrderliststr + "')";
                sql = string.Format(@"
                SET  BackStatus=1   WHERE   1 = 1 {0}
            ", wherestr);
                #endregion
            }
            var data = CgtTravelDB.GetInstance().Update<EnterpriseOrder>(sql);
            return data;
        }

        /// <summary>
        /// 根据票号获取已有订单信息
        /// </summary>
        /// <param name="TicketNos"></param>
        /// <returns></returns>
        public List<EnterpriseOrder> GetTravelOrdersByTickets(List<string> ticketNos)
        {

            var orders = MTBatchGetData<EnterpriseOrder>(500, ticketNos, GetTravelOrdersAction);

            return orders;
        }


        private List<EnterpriseOrder> GetTravelOrdersAction(List<string> ticketNos)
        {

            var sql = string.Format("SELECT * FROM EnterpriseOrder WHERE TicketNo IN ('{0}')", string.Join("','", ticketNos));

            var data = CgtTravelDB.GetInstance()
                .Query<EnterpriseOrder>(sql).ToList();

            return data;
        }

        /// <summary>
        /// 多线程分批次查询 TODO
        /// </summary>
        /// <param name="iSize"></param>
        /// <param name="ids"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private List<T> MTBatchGetData<T>(int iSize, List<string> ids, Func<List<string>, List<T>> func)
        {
            iSize = iSize > 1 ? iSize : 100;
            //TODO
            return func(ids);
        }


        /// <summary>
        /// 分销平台订单查询
        /// </summary>
        /// <returns></returns>
        public Page<TravelBatch> GetTravelAuditResult(int? EnterpriseID, string PayCenterCode, string TravelBatchId, DateTime? CreateTimeBegion, DateTime? CreateTimeEnd, int pageindex, int pagesize)
        {
            #region sql
            string wherestr = string.Empty;
            if (EnterpriseID > 0)
            {
                wherestr += " AND tb.EnterpriseId = @0";
            }
            if (!string.IsNullOrWhiteSpace(PayCenterCode))
            {
                wherestr += " AND tb.PayCenterCode = @1";
            }
            if (!string.IsNullOrWhiteSpace(TravelBatchId))
            {
                wherestr += " AND tb.TravelBatchId = @2";
            }
            if (CreateTimeBegion != null && CreateTimeBegion != DateTime.MinValue)
            {
                wherestr += " AND tb.CreateTime >= @3";
            }
            if (CreateTimeEnd != null && CreateTimeEnd != DateTime.MinValue)
            {
                wherestr += " AND tb.CreateTime <= @4";
            }
            
            string sql = string.Format(@"
SELECT MAX(tb.TravelBatchId) AS TravelBatchId,MAX(tb.PayCenterName) AS PayCenterName,MAX(tb.EnterpriseName) AS EnterpriseName
,COUNT(er.EOrderId) AS OrderCount,SucAuditCount=(SELECT COUNT(EOrderId) FROM EnterpriseOrderRisk WITH(NOLOCK) WHERE TravelBatchId=tb.TravelBatchId AND ReviewState=1)
,FailAuditCount=(SELECT COUNT(EOrderId) FROM EnterpriseOrderRisk WITH(NOLOCK) WHERE TravelBatchId=tb.TravelBatchId AND ReviewState=2)
,IsBackAuditCount=(SELECT COUNT(OrderId) FROM EnterpriseOrder WITH(NOLOCK) WHERE OrderTravelBatchId=tb.TravelBatchId AND BackStatus=1 )
,MAX(tb.CreateTime) AS CreateTime
FROM dbo.TravelBatch tb WITH(NOLOCK)
LEFT JOIN EnterpriseOrderRisk er WITH(NOLOCK) ON tb.TravelBatchId=er.TravelBatchId  
WHERE tb.ReviewState=3 {0}
GROUP BY tb.TravelBatchId
", wherestr);
            #endregion
          
            var page = CgtTravelDB.GetInstance().Page<TravelBatch>(pageindex, pagesize, sql, EnterpriseID, PayCenterCode, TravelBatchId, CreateTimeBegion, CreateTimeEnd);
            return page;
        }

    }
}
