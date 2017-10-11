using CGT.Entity.CgtTicketModel;
using CGT.Entity.CgtUserModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtUser
{
    public class UserRep
    {
        /// <summary>
        /// 获取退票列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public User GetUser(User model)
        {
            string wherestr = string.Empty;

            if (model.UserId!=-1)
            {
                wherestr += " AND UserId = @0";
            }
            string sql = string.Format(@"select * from [dbo].[User] WHERE 1 = 1 {0}", wherestr);
            return CgtUserDB.GetInstance().SingleOrDefault<User>(sql, model.UserId);
        }
     
    }
}
