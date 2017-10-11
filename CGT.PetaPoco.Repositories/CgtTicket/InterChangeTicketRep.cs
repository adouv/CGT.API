using CGT.Entity.CgtTicketModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTicket
{
    public class InterChangeTicketRep
    {
        /// <summary>
        /// 国际票改期乘客列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<Passenger> GetInterChangeTicketPassengerList(Passenger model)
        {
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(model.Order))
            {
                wherestr += " AND Passenger.[Order]=@0 ";
            }
            string sql = string.Format(@"select * from InterChangeTicket inner join [Passenger]
                                         on InterChangeTicket.PassengerId=Passenger.Id where 1 = 1 {0} ", wherestr);
            return CgtTicketDB.GetInstance().Query<Passenger>(sql, model.Order);
        }

    }
}
