using CGT.Entity.CgtTicketModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTicket
{
    public class InterRefundRep
    {
        /// <summary>
        /// 获取退票列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public Page<dynamic> GetInterRefundPage(int pageindex, int pagesize,DateTime? startDate, DateTime? endDate, InterRefund model)
        {
            string wherestr = string.Empty;

            if (!string.IsNullOrEmpty(model.OrderID))
            {
                wherestr += " AND InterRefund.OrderID = @0";
            }
            if (!string.IsNullOrEmpty(model.OrderOrderId))
            {
                wherestr += " AND [order].OrderID = @1";
            }
            if (model.AffairStatus != -1)
            {
                wherestr += " AND [InterRefund].AffairStatus = @2";
            }
            if (model.Status != -1)
            {
                wherestr += " AND [InterRefund].Status = @3";
            }
            if (startDate.HasValue)
            {
                wherestr +=string.Format(@" AND InterRefund.CreateTime>='{0}'", startDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (endDate.HasValue)
            {
                wherestr += string.Format(@" AND InterRefund.CreateTime<='{0}' ", endDate.Value.ToString("yyyy-MM-dd 23:59:59"));
            }
            string sql = string.Format(@"select [order].OrderID as OrderOrderId,[order].Platform,ReapalAccount,InterRefund.*
                           from [order] join 
                           InterRefund on[order].LocalId = InterRefund.OrderID WHERE 1 = 1 {0}", wherestr);
            return CgtTicketDB.GetInstance().Page<dynamic>(pageindex, pagesize, sql, model.OrderID, model.OrderOrderId,
                model.AffairStatus, model.Status);
        }
        /// <summary>
        /// 退票列表下载
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetInterRefundDownload(InterRefund model)
        {
            string wherestr = string.Empty;
            string sql = string.Format(@"SELECT  a.OrderID AS OrderOrderId ,
        a.[Platform] ,
        a.ReapalAccount ,
        b.Amount ,
        b.OrderID ,
        b.CreateTime ,
        b.ModifyUserName ,
        b.[Status],
        (CASE WHEN b.AffairStatus = 0 THEN '未锁定'
               WHEN b.AffairStatus = 1 THEN '锁定'
               ELSE NULL
          END ) AS AffairStatus
FROM    [order] a WITH(nolock)
        INNER JOIN InterRefund b WITH(nolock) ON a.LocalId = b.OrderID
WHERE   1 = 1  ", wherestr);
            return CgtTicketDB.GetInstance().Query<dynamic>(sql).ToList();
        }



        /// <summary>
        /// 修改锁单状态
        /// </summary>
        public int ModtifyInterRefundAffairStatus(InterRefund model)
        {
            return CgtTicketDB.GetInstance().Execute("UPDATE [InterRefund] set AffairStatus=@0 where OrderID=@1", model.AffairStatus.Value.ToString(), model.OrderID);
        }

        public int UpdateInterRefundAndInterRefundRemark(InterRefund refundModel, InterRefundRemark model)
        {
            var acr = CgtTicketDB.GetInstance();
            int i = 0;
            try
            {
                acr.BeginTransaction();
                acr.Insert(model);
                i += acr.Execute(@"UPDATE [InterRefund] SET ModifyUserId=@0,ModifyUserName=@1,ModifyTime=@2,[Status]=@4 WHERE [orderid]=@3", refundModel.ModifyUserId, refundModel.ModifyUserName, refundModel.ModifyTime, refundModel.OrderID, refundModel.Status);
                acr.CompleteTransaction();
            }
            catch (Exception ex)
            {
                acr.AbortTransaction();
                throw ex;
            }
            return i;
        }
    }
}
