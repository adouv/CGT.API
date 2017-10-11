using CGT.Entity.CgtTicketModel;
using PetaPoco.NetCore;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTicket
{
    public class InterChangeRemarkRep
    {
        /// <summary>
        /// 获取国际票改签备注表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<InterChangeRemark> GetInterChangeRemarkList(InterChangeRemark model)
        {
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(model.OrderId))
            {
                wherestr += " AND OrderId=@0 ";
            }
            string sql = string.Format(@"select * from [dbo].[InterChangeRemark] where 1 = 1 {0} ", wherestr);
            return CgtTicketDB.GetInstance().Query<InterChangeRemark>(sql, model.OrderId);
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        public void Insert(InterChangeRemark model)
        {
            CgtTicketDB.GetInstance().Insert(model);
        }
    }
}
