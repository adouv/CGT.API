using CGT.Entity.CgtTravelModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTravel
{
    public class TravelBillRep
    {
        /// <summary>
        /// 获取账单列表
        /// </summary>
        /// <param name="PayCenterCode">分销商Code</param>
        /// <param name="Status">还款状态</param>
        /// <param name="BillType">异常账单类型</param>
        /// <param name="StartDate">账期开始时间</param>
        /// <param name="EndDate">账期结束时间</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页条数</param>
        /// <returns></returns>
        public Page<Bill> GetTravelBillList(string PayCenterCode, int Status, string BillType, DateTime? StartDate, DateTime? EndDate, int PageIndex, int PageSize)
        {
            if (StartDate == null)
            {
                StartDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            }
            if (EndDate == null)
            {
                EndDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            }

            string wherestr = string.Empty;

            #region sql
            wherestr = @"  
                AND A.PayCenterCode IN 
                ( 
                    SELECT C.PayCenterCode FROM cgt.dbo.UserAccount C WITH (NOLOCK)
                    WHERE 1=1 
                    AND C.PayCenterCode IS NOT NULL
                    AND C.PayCenterCode <> '' 
                )
            ";
            if (!string.IsNullOrWhiteSpace(PayCenterCode))
            {
                wherestr += " AND  A.PayCenterCode = @0 ";
            }
            if (Status > -1)
            {
                wherestr += " AND A.Status = @1";
            }
            if (!string.IsNullOrWhiteSpace(BillType))
            {
                if (BillType == "-1")
                {
                    wherestr += " AND A.BillType IN (0,1,2) ";
                }
                else
                {
                    wherestr += " AND A.BillType IN (" + BillType + ") ";
                }
            }
            if (StartDate != null)
            {
                wherestr += " AND  A.BillDate >= @2";
            }
            if (EndDate != null)
            {
                wherestr += " AND  A.BillDate <= @3";
            }

            string sql = string.Format(@"
                SELECT  A.*,
                B.GraceDay,
                B.GraceCount,
                B.GraceBate,
                B.OverdueBate,
                'DistributorInterest'=A.BillInterest,
                'StatusStr'=(CASE A.Status WHEN 0 THEN '未还款' ELSE '已还款' END),
                'BillTypeStr'=(CASE A.BillType WHEN 0 THEN '正常' WHEN 1 THEN '宽限期' ELSE '逾期' END),
                'BillDateStr'=CONVERT(NVARCHAR(10),BillDate,23),
                'GraceStartDate'=(CASE B.GraceDay WHEN 0 THEN '0' ELSE CONVERT(NVARCHAR(10),DATEADD(DAY,1,A.BillDate),23) END),
                'GraceEndDate'=(CASE B.GraceDay WHEN 0 THEN '0' ELSE CONVERT(NVARCHAR(10),DATEADD(DAY,B.GraceDay,A.BillDate),23) END),
                'OverdueStartDate'=CONVERT(NVARCHAR(10),DATEADD(DAY,(B.GraceDay+1),A.BillDate),23),
                'OverdueEndDate'=CONVERT(NVARCHAR(10),(CASE A.Status WHEN 1 THEN A.RealpayDateTime ELSE GETDATE() END),23),
                'OverdueDay'=DATEDIFF(DAY,DATEADD(DAY,(B.GraceDay+1),A.BillDate),(CASE A.Status WHEN 1 THEN A.RealpayDateTime ELSE GETDATE() END))+1
                FROM dbo.Bill A WITH (NOLOCK) 
                INNER JOIN [cgt].dbo.UserAccount B WITH (NOLOCK) 
                ON A.PayCenterCode=B.PayCenterCode
                WHERE 1=1 {0} 
            ", wherestr);
            #endregion

            var data = CgtTravelDB.GetInstance().Page<Bill>(PageIndex, PageSize, sql, PayCenterCode, Status, StartDate, EndDate);

            return data;
        }
        /// <summary>
        ///  下载账单
        /// </summary>
        /// <param name="PayCenterCode">分销商Code</param>
        /// <param name="Status">还款状态</param>
        /// <param name="BillType">异常账单类型</param>
        /// <param name="StartDate">账期开始时间</param>
        /// <param name="EndDate">账期结束时间</param>
        /// <returns></returns>
        public List<Bill> GetTravelBillListDownload(string PayCenterCode, int Status, string BillType, DateTime? StartDate, DateTime? EndDate)
        {
            if (StartDate == null)
            {
                StartDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            }
            if (EndDate == null)
            {
                EndDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            }

            string wherestr = string.Empty;

            #region sql
            wherestr = @"  
                AND A.PayCenterCode IN 
                ( 
                    SELECT C.PayCenterCode FROM cgt.dbo.UserAccount C WITH (NOLOCK)
                    WHERE 1=1 
                    AND C.PayCenterCode IS NOT NULL
                    AND C.PayCenterCode <> '' 
                )
            ";
            if (!string.IsNullOrWhiteSpace(PayCenterCode))
            {
                wherestr += " AND  A.PayCenterCode = @0 ";
            }
            if (Status > -1)
            {
                wherestr += " AND A.Status = @1";
            }
            if (!string.IsNullOrWhiteSpace(BillType))
            {
                if (BillType == "-1")
                {
                    wherestr += " AND A.BillType IN (1,2) ";
                }
                else
                {
                    wherestr += " AND A.BillType IN (" + BillType + ") ";
                }
            }
            if (StartDate != null)
            {
                wherestr += " AND  A.BillDate >= @2";
            }
            if (EndDate != null)
            {
                wherestr += " AND  A.BillDate <= @3";
            }

            string sql = string.Format(@"
                SELECT  A.*,'OverdueAmout'=CONVERT(DECIMAL(18,2),A.OverdueAmout),
                B.GraceDay,
                B.GraceCount,
                B.GraceBate,
                B.OverdueBate,
                'DistributorInterest'=A.BillInterest,
                'StatusStr'=(CASE A.Status WHEN 0 THEN '未还款' ELSE '已还款' END),
                'BillTypeStr'=(CASE A.BillType WHEN 0 THEN '正常' WHEN 1 THEN '宽限期' ELSE '逾期' END),
                'BillDateStr'=CONVERT(NVARCHAR(10),BillDate,23),
                'GraceStartDate'=(CASE B.GraceDay WHEN 0 THEN '0' ELSE CONVERT(NVARCHAR(10),DATEADD(DAY,1,A.BillDate),23) END),
                'GraceEndDate'=(CASE B.GraceDay WHEN 0 THEN '0' ELSE CONVERT(NVARCHAR(10),DATEADD(DAY,B.GraceDay,A.BillDate),23) END),
                'OverdueStartDate'=CONVERT(NVARCHAR(10),DATEADD(DAY,(B.GraceDay+1),A.BillDate),23),
                'OverdueEndDate'=CONVERT(NVARCHAR(10),(CASE A.Status WHEN 1 THEN A.RealpayDateTime ELSE GETDATE() END),23),
                'OverdueDay'=DATEDIFF(DAY,DATEADD(DAY,(B.GraceDay+1),A.BillDate),(CASE A.Status WHEN 1 THEN A.RealpayDateTime ELSE GETDATE() END))+1
                FROM dbo.Bill A WITH (NOLOCK) 
                INNER JOIN [cgt].dbo.UserAccount B WITH (NOLOCK) 
                ON A.PayCenterCode=B.PayCenterCode
                WHERE 1=1 {0} 
            ", wherestr);
            #endregion

            var data = CgtTravelDB.GetInstance().Query<Bill>(sql, PayCenterCode, Status, StartDate, EndDate).ToList();

            return data;
        }

        /// <summary>
        /// 获取账单下载数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TravelBillDownload> GetDownloadBillList(Bill model, DateTime? startdate, DateTime? enddate)
        {
            #region sql
            string wherestr = string.Empty;
            string sql = string.Empty;

            if (model.BillId > 0)
            {
                wherestr += " AND a.BillId= @0 ";
            }
            if (!string.IsNullOrWhiteSpace(model.PayCenterCode))
            {
                wherestr += " AND a.PayCenterCode = @1";
            }
            if (!string.IsNullOrWhiteSpace(model.PayCenterName))
            {
                wherestr += " AND a.PayCenterName LIKE @2";

            }
            if (!string.IsNullOrWhiteSpace(model.OwnerName))
            {
                wherestr += " AND a.OwnerName LIKE @3";
            }
            if (startdate != null)
            {
                wherestr += " AND a.CreateTime>= @4";
            }
            if (enddate != null)
            {
                wherestr += " AND a.CreateTime<= @5";
            }

            sql = string.Format(@"
                SELECT  a.BillId ,
                        a.BillDate ,
                        a.EnterpriseBillDate ,
                        a.BillAmount ,
                        a.AlreadyReimbursement ,
                         ( CASE a.Status
                            WHEN 0 THEN '未还款'
                            WHEN 1 THEN '已还款'
                            ELSE '未还款'
                          END ) AS StatusStr ,
                        a.BillInterest ,
                        a.UserInterestRate ,
                        a.FactoringInterestRate ,
                        ( SELECT    SUM(a)
                          FROM      (  SELECT    ( SELECT    dbo.FUN_GetFactoringInterestAmount(b.BillEveryDaysId,
                                                                              d.FactoringInterestRate,
                                                                              d.TicketAmount,
                                                                              d.BackTime,
                                                                              b.RealpayDateTime)
                                                ) a
                                      FROM      dbo.BillEveryDay b INNER JOIN dbo.EnterpriseOrder d ON
								                b.BillEveryDaysId = d.BillEveryDayId                    
                                      WHERE     b.BillId = a.BillId
                                    ) c
                        ) AS FactoringInterest ,
                        a.OwnerName ,
                        a.PayCenterName ,
		                0 AS OverdueDays ,
                        0 AS OverdueFine ,
                        a.SumTicketNo,
		                ( SELECT    SUM(a)
                          FROM      ( SELECT    ( SELECT    dbo.FUN_GetRefundAmount(b.RealpayDateTime,
                                                                              b.BillDate,
                                                                              b.EnterpriseBillDate,
                                                                              c.UserInterestRate,
                                                                              c.TicketAmount)
                                                ) a
                                      FROM      dbo.BillEveryDay b
                                                INNER JOIN dbo.EnterpriseOrder c ON b.BillEveryDaysId = c.BillEveryDayId
                                      WHERE     b.BillId = a.BillId
                                    ) t
                        ) AS InterestRefundAmount 
                FROM    dbo.Bill a 
                WHERE
                1=1 
                {0}
            ", wherestr); 
            #endregion

            var data = CgtTravelDB.GetInstance().Query<TravelBillDownload>(sql,
                model.BillId,
                model.PayCenterCode,
                (model.PayCenterName != null ? model.PayCenterName + "%" : null),
                (model.OwnerName != null ? model.OwnerName + "%" : null),
                Convert.ToDateTime(startdate).ToString("yyyy-MM-dd HH:mm:ss"),
                Convert.ToDateTime(enddate).ToString("yyyy-MM-dd HH:mm:ss")).ToList();
            return data;
        }



        /// <summary>
        /// 获取总账单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Bill> GetBillList(string payCenterCode, int isagent, DateTime? startdate, DateTime? enddate)
        {
            #region sql
            string wherestr = string.Empty;
            string sql = string.Empty;
            //代理
            if (isagent >= 1)
            {
                wherestr = @"  AND PayCenterCode IN ( SELECT  PayCenterCode
                               FROM     cgt.dbo.UserAccount
                               WHERE    PayCenterCode IS NOT NULL
                                        AND PayCenterCode <> '' )";
                if (!string.IsNullOrWhiteSpace(payCenterCode))
                {
                    wherestr += " AND  PayCenterCode = @0";
                }
                if (startdate != null)
                {
                    wherestr += " AND  CreateTime >= @1";
                }
                if (enddate != null)
                {
                    wherestr += " AND  CreateTime <= @2";
                }

                sql = string.Format(@"SELECT  *
                    FROM    dbo.Bill
                    WHERE   1 = 1 {0}  
                ", wherestr);
            }
            //分销
            else if (isagent == 0)
            {
                if (!string.IsNullOrWhiteSpace(payCenterCode))
                {
                    wherestr += " AND PayCenterCode = @0";
                }
                if (startdate != null)
                {
                    wherestr += " AND  CreateTime >= @1";
                }
                if (enddate != null)
                {
                    wherestr += " AND  CreateTime <= @2";
                }

                sql = string.Format(@"
 SELECT TOP 1 * FROM(
                SELECT  *
                FROM    dbo.Bill
                WHERE   1 = 1 {0} 
 ) t
ORDER BY BillId DESC
            ", wherestr);
            }
            #endregion

            var data = CgtTravelDB.GetInstance().Query<Bill>(sql,
                payCenterCode,
                Convert.ToDateTime(startdate).ToString("yyyy-MM-dd HH:mm:ss"),
                Convert.ToDateTime(enddate).ToString("yyyy-MM-dd HH:mm:ss")).ToList();
            return data;


        }


        /// <summary>
        /// 获取下载账单数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TravelBillDownload> GetBillList(Bill model, DateTime? startdate, DateTime? enddate)
        {
            string wherestr = string.Empty;
            string sql = string.Empty;

            if (model.BillId > 0)
            {
                wherestr += " AND a.BillId= @0 ";
            }
            if (!string.IsNullOrWhiteSpace(model.PayCenterCode))
            {
                wherestr += " AND a.PayCenterCode = @1";
            }
            if (!string.IsNullOrWhiteSpace(model.PayCenterName))
            {
                wherestr += " AND a.PayCenterName LIKE @2";

            }
            if (!string.IsNullOrWhiteSpace(model.OwnerName))
            {
                wherestr += " AND a.OwnerName LIKE @3";
            }
            if (startdate != null)
            {
                wherestr += " AND a.CreateTime>= @4";
            }
            if (enddate != null)
            {
                wherestr += " AND a.CreateTime<= @5";
            }

            sql = string.Format(@"
SELECT  a.BillId ,
        a.BillDate ,
        a.EnterpriseBillDate ,
        a.BillAmount ,
        a.AlreadyReimbursement ,
         ( CASE a.Status
            WHEN 0 THEN '未还款'
            WHEN 1 THEN '已还款'
            ELSE '未还款'
          END ) AS StatusStr ,
        a.BillInterest ,
        a.UserInterestRate ,
        a.FactoringInterestRate ,
        ( SELECT    SUM(a)
          FROM      (  SELECT    ( SELECT    dbo.FUN_GetFactoringInterestAmount(b.BillEveryDaysId,
                                                              d.FactoringInterestRate,
                                                              d.TicketAmount,
                                                              d.BackTime,
                                                              b.RealpayDateTime)
                                ) a
                      FROM      dbo.BillEveryDay b INNER JOIN dbo.EnterpriseOrder d ON
								b.BillEveryDaysId = d.BillEveryDayId                    
                      WHERE     b.BillId = a.BillId
                    ) c
        ) AS FactoringInterest ,
        a.OwnerName ,
        a.PayCenterName ,
		0 AS OverdueDays ,
        0 AS OverdueFine ,
        a.SumTicketNo,
		( SELECT    SUM(a)
          FROM      ( SELECT    ( SELECT    dbo.FUN_GetRefundAmount(b.RealpayDateTime,
                                                              b.BillDate,
                                                              b.EnterpriseBillDate,
                                                              c.UserInterestRate,
                                                              c.TicketAmount)
                                ) a
                      FROM      dbo.BillEveryDay b
                                INNER JOIN dbo.EnterpriseOrder c ON b.BillEveryDaysId = c.BillEveryDayId
                      WHERE     b.BillId = a.BillId
                    ) t
        ) AS InterestRefundAmount ,
        @6 as MerchantType
FROM    dbo.Bill a 
WHERE
1=1 
{0}
", wherestr);

            var data = CgtTravelDB.GetInstance().Query<TravelBillDownload>(sql,
                model.BillId,
                model.PayCenterCode,
                (model.PayCenterName != null ? model.PayCenterName + "%" : null),
                (model.OwnerName != null ? model.OwnerName + "%" : null),
                Convert.ToDateTime(startdate).ToString("yyyy-MM-dd HH:mm:ss"),
                Convert.ToDateTime(enddate).ToString("yyyy-MM-dd HH:mm:ss"),
                model.MerchantType).ToList();
            return data;
        }

    }
}
