using CGT.Entity.CgtTravelModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTravel
{
    /// <summary>
    /// 每日账单
    /// </summary>
    public class BillEveryDayRep
    {
        /// <summary>
        /// 获取每日账单
        /// </summary>
        /// <param name="PayCenterCode">子商户CODE</param>
        /// <param name="EnterpriseId">企业编号</param>
        /// <param name="Status">还款状态</param>
        /// <param name="StartDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        public Page<BillEveryDay> GetBillEveryDaysList(string PayCenterCode, int? EnterpriseId, int Status, string StartDate, string EndDate, int pageIndex, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(StartDate))
            {
                StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrWhiteSpace(EndDate))
            {
                EndDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }

            #region sql
            string whereStr = string.Empty;
            if (Status > -1)
            {
                whereStr += @" AND Status =  @0 ";
            }
            if (!string.IsNullOrWhiteSpace(PayCenterCode))
            {
                whereStr += @" AND PayCenterCode = @1 ";
            }
            if (EnterpriseId != null)
            {
                whereStr += @" AND EnterpriseId = @2 ";
            }
            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                whereStr += @" AND CONVERT(NVARCHAR(10),CreateTime,23)>=@3 ";
            }
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                whereStr += @" AND CONVERT(NVARCHAR(10),CreateTime,23)<=@4 ";
            }
            string sql = string.Format(@"
                SELECT * FROM 
                (
                    SELECT  BillId,ISNULL(SUM(BillAmount),0) AS BillAmount,Status,
                    ( 
	                    CASE 
		                    WHEN Status = 0 THEN '未还款'
		                    WHEN status = 1 THEN '已还款'
		                    ELSE '未还款'
	                    END 
                    ) AS StatusStr,
                    BillDate,EnterpriseBillDate,PayCenterCode,
                    ISNULL(SUM(BillInterest),0) AS BillInterest,ISNULL(SUM(SumTicketNo),0) AS SumTicketNo,ISNULL(UserInterestRate,0) AS UserInterestRate,
                    ISNULL(dbo.FUN_GetRefundAmount(RealpayDateTime, BillDate,
                                                EnterpriseBillDate,
                                                UserInterestRate,
                                                ISNULL(SUM(BillAmount),0)),0) AS RefundAmount,
                    ISNULL(dbo.FUN_GetUserInterest(UserInterestRate,SUM(BillAmount)),0) AS DistributorInterest,
                    EnterpriseId,EnterpriseName,PayCenterName,OwnerName
                    FROM  dbo.BillEveryDay
                    WHERE 1 = 1 {0}
                    GROUP BY BillId, 
                    Status,BillDate,PayCenterCode,
                    EnterpriseId,EnterpriseName,
                    PayCenterName,OwnerName,EnterpriseBillDate,UserInterestRate,RealpayDateTime
                ) XXX
            ", whereStr);
            #endregion
            
            var data = CgtTravelDB.GetInstance().Page<BillEveryDay>(pageIndex, pageSize, sql, Status, PayCenterCode, EnterpriseId, StartDate, EndDate);

            return data;
        }
        /// <summary>
        /// 获取日账单下载明细
        /// </summary>
        /// <param name="EnterpriseId"></param>
        /// <param name="BillDate"></param>
        /// <returns></returns>
        public List<BillEveryDay> GetBillEveryDayDownload(int? EnterpriseId, string BillDate, int Status)
        {
            string where = string.Empty;
            string sql = string.Format(@"
                SELECT * FROM 
                (
                    SELECT *,
                        ( 
	                        CASE 
		                        WHEN Status = 0 THEN '未还款'
		                        WHEN Status = 1 THEN '已还款'
		                        ELSE '未还款'
	                        END 
                        ) AS StatusStr,
                    ISNULL(dbo.FUN_GetUserInterest(UserInterestRate,BillAmount),0) AS DistributorInterest,
                    'BillDateStr'=CONVERT(NVARCHAR(10),BillDate,23)
                    FROM dbo.BillEveryDay WITH (NOLOCK)
                    WHERE 1=1 
                    AND CONVERT(NVARCHAR(10),BillDate,23)=@0 
                    AND EnterpriseId=@1
                    AND Status=@2
                ) XXX
            ");

            return CgtTravelDB.GetInstance().Query<BillEveryDay>(sql, BillDate, EnterpriseId, Status).ToList();
        }
        /// <summary>
        /// 更新企业日账单通过账单编号
        /// </summary>
        /// <param name="billid"></param>
        /// <param name="Enterpriselist"></param>
        /// <returns></returns>
        public int UpdateBillEveryDaysByBillid(int billid, List<string> Enterpriselist, string BatchTradeNo) {
            #region sql
            string wherestr = string.Empty;
            if (billid > 0) {
                wherestr += @" AND BillId =" + billid;
            }
            if (Enterpriselist!=null) {
                var Enterprises = string.Join("','", Enterpriselist);
                wherestr += @" AND EnterpriseId IN ('" + Enterprises + "')";
            }
            string sql = string.Format(@"
            Set Status =1,BatchTradeNo=@0,RealpayDateTime=getdate() WHERE 1 = 1 AND Status =0 {0}", wherestr);
            #endregion

            var data = CgtTravelDB.GetInstance().Update<BillEveryDay>(sql, BatchTradeNo);
            return data;
        }
    }
}
