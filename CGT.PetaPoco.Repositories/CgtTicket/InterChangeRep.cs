using CGT.Entity.CgtTicketModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTicket
{
    public class InterChangeRep
    {
        /// <summary>
        /// 获取国际票改期列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public Page<dynamic> GetInterChangeList(int pageindex, int pagesize, DateTime? startDate, DateTime? endDate, InterChange model)
        {
            string wherestr = string.Empty;

            if (!string.IsNullOrEmpty(model.OrderID))
            {
                wherestr += " AND InterChange.OrderID = @0";
            }
            else if (!string.IsNullOrEmpty(model.OrderOrderId))
            {
                wherestr += " AND [order].OrderID = @1";
            }
            if (model.AffairStatus != -1)
            {
                wherestr += " AND [InterChange].AffairStatus = @2";
            }
            if (model.Status != -1)
            {
                wherestr += " AND [InterChange].Status = @3";
            }
            if (startDate.HasValue)
            {
                wherestr += string.Format(@" AND InterChange.CreateTime>='{0}'", startDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (endDate.HasValue)
            {
                wherestr += string.Format(@" AND InterChange.CreateTime<='{0}' ", endDate.Value.ToString("yyyy-MM-dd 23:59:59"));
            }

            string sql = string.Format(@"select [order].OrderID as OrderOrderId,[order].Platform,ReapalAccount,InterChange.* from [order] join 
                           InterChange on[order].LocalId = InterChange.OrderID WHERE 1 = 1 {0}", wherestr);
            return CgtTicketDB.GetInstance().Page<dynamic>(pageindex, pagesize, sql, model.OrderID, model.OrderOrderId,
           model.AffairStatus, model.Status);
        }

        /// <summary>
        /// 修改锁单状态
        /// </summary>
        public int ModtifyInterChangeAffairStatus(InterChange model)
        {
            return CgtTicketDB.GetInstance().Execute("UPDATE [InterChange] set AffairStatus=@0 where OrderID=@1", model.AffairStatus.Value.ToString(), model.OrderID);
        }

        public int UpdateInterChangeAndInterChangeRemark(InterChange ChangeModel, InterChangeRemark model)
        {
            var acr = CgtTicketDB.GetInstance();
            int i = 0;
            try
            {
                acr.BeginTransaction();
                acr.Insert(model);
                i += acr.Execute(@"UPDATE [InterChange] SET ModifyUserId=@0,ModifyUserName=@1,ModifyTime=@2,[Status]=@4 WHERE [orderid]=@3", ChangeModel.ModifyUserId, ChangeModel.ModifyUserName, ChangeModel.ModifyTime, ChangeModel.OrderID, ChangeModel.Status);
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
