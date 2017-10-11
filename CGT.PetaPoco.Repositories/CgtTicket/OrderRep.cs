using CGT.Entity.CgtTicketModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTicket
{
    public class OrderRep
    {
        /// <summary>
        /// 获取国际票订单列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Page<Order> GetOrderPage(int pageindex, int pagesize, DateTime? startDate, DateTime? endDate, Order model)
        {
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(model.OrderId))
            {
                wherestr += " AND OrderId=@0 ";
            }
             if (!string.IsNullOrEmpty(model.LocalId))
            {
                wherestr += " AND LocalId=@1 ";
            }
            if (model.TravelType.HasValue)
            {
                wherestr += " AND TravelType=@2 ";
            }
            if (model.Status != -1)
            {
                wherestr += " AND [Status] = @3";
            }
            if (startDate.HasValue)
            {
                wherestr += string.Format(@" AND CreateTime>='{0}'", startDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (endDate.HasValue)
            {
                wherestr += string.Format(@" AND CreateTime<='{0}' ", endDate.Value.ToString("yyyy-MM-dd 23:59:59"));
            }
            string sql = string.Format(@"select * from [dbo].[Order] where 1 = 1 {0} ", wherestr);
            return CgtTicketDB.GetInstance().Page<Order>(pageindex, pagesize, sql,model.OrderId,model.LocalId,model.TravelType, model.Status);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetOrderList(Order model)
        {
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(model.OrderId))
            {
                wherestr += " AND a.OrderId=@0 ";
            }
            if (!string.IsNullOrEmpty(model.LocalId))
            {
                wherestr += " AND a.LocalId=@1 ";
            }
            string sql = string.Format(@"select a.*,b.AffairStatus,b.ModifyUserId from [dbo].[Order] a inner join [dbo].[InterRefund]  b on a.LocalId=b.OrderID where 1 = 1 {0} ", wherestr);
            return CgtTicketDB.GetInstance().Query<dynamic>(sql, model.OrderId, model.LocalId);
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        public void Insert(Order model)
        {
            CgtTicketDB.GetInstance().Insert(model);
        }
    }
}
