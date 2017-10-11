using CGT.Entity.CgtTravelModel;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CGT.DDD.Logger;
using System.Text;

namespace CGT.PetaPoco.Repositories.CgtTravel
{

    /// <summary>
    /// 差旅月账单
    /// </summary>
    public class BillRep
    {

        /// <summary>
        /// 更改总账单为还款状态，还款金额
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateBillByBillid(int billid, int Status)
        {
            #region sql
            string wherestr = string.Empty;
            if (billid > 0)
            {
                wherestr += " AND BillId = " + billid;
            }
            string sql = string.Format(@"
                 SET AlreadyReimbursement=BillAmount,Status={0}
WHERE   1 = 1 {1}
            ", Status, wherestr);
            #endregion
            var data = CgtTravelDB.GetInstance().Update<Bill>(sql);
            return data;
        }

        /// <summary>
        /// 获取所有未还款账单
        /// </summary>
        /// <returns></returns>
        public List<Bill> GetAllOutstandingBill()
        {
            #region sql
            string sql = @"SELECT a.*,b.GraceBate,b.OverdueBate,b.GraceDay FROM cgt_factoring.dbo.Bill a 
                           INNER JOIN cgt.dbo.UserAccount b
                           ON a.PayCenterCode = b.PayCenterCode
                           WHERE a.Status = 0";
            #endregion

            using (var db = CgtTravelDB.GetInstance())
            {
                var result = db.Query<Bill>(sql);
                return result.ToList();
            }
        }

        /// <summary>
        /// 更新宽限期利息
        /// </summary>
        /// <param name="GraceAmoutEveryDay"></param>
        /// <param name="BillId"></param>
        /// <returns></returns>
        public int UpdateGraceAmout(decimal GraceAmoutEveryDay, int BillId)
        {
            LoggerFactory.Instance.Logger_Info("正在计算账单ID：" + BillId + " \r\n" + "当日宽限期利息金额为：" + GraceAmoutEveryDay + " \r\n", "CalculateInterestGrace");
            #region sql
            string sql = @"SET GraceAmout= @0
                           WHERE BillId=@1";
            #endregion
            int result = 0;
            using (var db = CgtTravelDB.GetInstance())
            {
                db.BeginTransaction();
                try
                {
                    result = db.Update<Bill>(sql, GraceAmoutEveryDay, BillId);
                    db.CompleteTransaction();
                }
                catch (Exception ex)
                {
                    LoggerFactory.Instance.Logger_Info("账单ID：" + BillId + " \r\n" + "当日宽限期利息SQL存储异常 \r\n", "CalculateInterestGrace");
                    LoggerFactory.Instance.Logger_Error(ex, "CalculateInterestGraceException");
                    db.AbortTransaction();
                }
            }
            return result;
        }

        /// <summary>
        /// 更新逾期利息
        /// </summary>
        /// <param name="OverdueAmoutEveryDay"></param>
        /// <param name="BillId"></param>
        /// <returns></returns>
        public int UpdateOverdueAmout(decimal OverdueAmoutEveryDay, int BillId)
        {
            LoggerFactory.Instance.Logger_Info("正在计算账单ID：" + BillId + " \r\n" + "当日逾期利息：" + OverdueAmoutEveryDay + " \r\n", "CalculateOverdueAmout");
            #region sql
            string sql = @"SET OverdueAmout=@0
                           WHERE BillId=@1";
            #endregion
            int result = 0;
            using (var db = CgtTravelDB.GetInstance())
            {
                db.BeginTransaction();
                try
                {
                    result = db.Update<Bill>(sql, OverdueAmoutEveryDay, BillId);
                    db.CompleteTransaction();
                }
                catch (Exception ex)
                {
                    LoggerFactory.Instance.Logger_Info("账单ID：" + BillId + " \r\n" + "当日逾期利息存储SQL异常失败 \r\n", "CalculateOverdueAmoutException");
                    LoggerFactory.Instance.Logger_Error(ex, "CalculateOverdueAmoutException");
                    db.AbortTransaction();
                }
            }
            return result;
        }

        /// <summary>
        /// 修改账单状态
        /// </summary>
        /// <param name="BillType">0/1/2</param>
        /// <param name="BillId"></param>
        /// <returns></returns>
        public int UpdateBillType(int BillType, int BillId)
        {
            using (var db = CgtTravelDB.GetInstance())
            {
                #region sql
                string sql = @"SET BillType = @0
                           WHERE BillId=@1";
                #endregion
                int result = 0;
                try
                {
                    LoggerFactory.Instance.Logger_Info("账单ID：" + BillId + " \r\n" + "修改当前状态ID：" + BillType + " \r\n", "BillTypeChange");
                    result = db.Update<Bill>(sql, BillType, BillId);
                }
                catch (Exception ex)
                {
                    LoggerFactory.Instance.Logger_Info("账单ID：" + BillId + " \r\n" + "修改当前状态ID：" + BillType + " \r\n" + "更新异常 \r\n", "BillTypeChange");
                    LoggerFactory.Instance.Logger_Error(ex, "BillTypeChangeException");
                }
                return result;
            }
        }

        public void FrozenEnterpriseList(int BillId)
        {
            using (var db = CgtTravelDB.GetInstance())
            {
                db.BeginTransaction();
                var IDList = new List<String>();
                try
                {
                    #region sql
                    string sql = @"SELECT DISTINCT(EnterpriseId) AS 'EnterpriseWhiteListID' ,b.FreezeWay FROM cgt_factoring.dbo.BillEveryDay a 
                                   LEFT JOIN 
                                   cgt_factoring.dbo.EnterpriseWhiteList b 
                                   ON 
                                   a.EnterpriseId=b.EnterpriseWhiteListID
                                   WHERE BillId = @0 AND Status=0";
                    var list = db.Query<EnterpriseWhiteList>(sql, BillId).ToList();
                    foreach (var item in list)
                    {
                        if (item.FreezeWay == 1)
                        {
                            var result = db.Update<EnterpriseWhiteList>("SET EnterpriseStatue=0 WHERE EnterpriseWhiteListID = @0", item.EnterpriseWhiteListID);
                            if (result > 0)
                            {
                                IDList.Add(item.EnterpriseWhiteListID.ToString());
                            }
                        }
                    }
                    if (IDList.Count > 0)
                    {
                        LoggerFactory.Instance.Logger_Info("冻结企业ID列表：" + String.Join(",", IDList) + " \r\n", "FrozenEnterprise");
                    }
                    db.CompleteTransaction();
                }
                catch (Exception ex)
                {
                    LoggerFactory.Instance.Logger_Info("冻结企业ID列表：" + String.Join(",", IDList) + " 发生异常 \r\n" + "账单ID：" + BillId + " \r\n", "FrozenEnterpriseException");
                    LoggerFactory.Instance.Logger_Error(ex, "FrozenEnterpriseException");
                    db.AbortTransaction();
                }
                #endregion
            }
        }

        /// <summary>
        /// 获取账单逾期总金额
        /// </summary>
        /// <returns></returns>
        public decimal GetAllGraceAndOverdue()
        {
            #region sql

            string sql = @"
                 SELECT Amount=
                 case when (SUM(BillAmount)-SUM(AlreadyReimbursement)+SUM(GraceAmout)+SUM(OverdueAmout)) is null then 0 
                 else (SUM(BillAmount)-SUM(AlreadyReimbursement)+SUM(GraceAmout)+SUM(OverdueAmout)) end 
                 FROM Bill 
                 WHERE [Status] = 0 AND BillType>0 AND PayCenterCode not in('PXTXCSZH79710107','self_001','RBXSCS33710101')
                    
            ";
            #endregion
            var data = CgtTravelDB.GetInstance().ExecuteScalar<decimal>(sql);
            
            return data;
        }

        /// <summary>
        /// 获取账单逾期总金额（根据分销商）
        /// </summary>
        /// <param name="PayCenterCode">分销商Code</param>
        /// <returns></returns>
        public decimal GetAllGraceAndOverdue(string PayCenterCode)
        {
            #region sql

            string sql =string.Format( @"
                 SELECT Amount=
                 case when (SUM(BillAmount)-SUM(AlreadyReimbursement)+SUM(GraceAmout)+SUM(OverdueAmout)) is null then 0 
                 else (SUM(BillAmount)-SUM(AlreadyReimbursement)+SUM(GraceAmout)+SUM(OverdueAmout)) end 
                 FROM Bill 
                 WHERE [Status] = 0 AND BillType>0 AND PayCenterCode ='{0}'",PayCenterCode);
            #endregion
            var data = CgtTravelDB.GetInstance().ExecuteScalar<decimal>(sql.ToString());

            return data;
        }
    }
}
