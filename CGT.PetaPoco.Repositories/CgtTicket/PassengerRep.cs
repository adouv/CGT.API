using CGT.Entity.CgtTicketModel;
using PetaPoco.NetCore;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTicket
{
    public class PassengerRep
    {
        /// <summary>
        /// 乘客列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<Passenger> GetPassengerList(Passenger model)
        {
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(model.Order))
            {
                wherestr += " AND [Order]=@0 ";
            }
            string sql = string.Format(@"select * from [dbo].[Passenger] where 1 = 1 {0} ", wherestr);
            return CgtTicketDB.GetInstance().Query<Passenger>(sql, model.Order);
        }
    }
}
