using CGT.Entity.CgtTicketModel;
using PetaPoco.NetCore;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTicket
{
    public class InterRefundRemarkRep
    {
        /// <summary>
        /// 获取国际票退票备注表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<InterRefundRemark> GetInterRefundRemarkList(InterRefundRemark model)
        {
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(model.OrderId))
            {
                wherestr += " AND OrderId=@0 ";
            }
            string sql =string.Format(@"select * from [InterRefundRemark]  where 1 = 1 {0} ",wherestr);
            return CgtTicketDB.GetInstance().Query<InterRefundRemark>(sql,model.OrderId);
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        public void Insert(InterRefundRemark model)
        {
            CgtTicketDB.GetInstance().Insert(model);
        }
    }
}
