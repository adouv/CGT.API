using CGT.Entity.CgtTravelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGT.PetaPoco.Repositories.CgtTravel
{
    public class UserFactoringRep
    {
        public int UpdateUserFactoring(UserFactoring model)
        {
            string sql = "SET InterestRate=@0 WHERE   FactoringReapalNo=@1";

            return CgtTravelDB.GetInstance().Update<UserFactoring>(sql, model.InterestRate, model.FactoringReapalNo);
        }

        public void InsertUserFactoring(UserFactoring model)
        {
            CgtTravelDB.GetInstance().Insert(model);

        } 
        /// <summary>
        /// 获取分销商保理企业
        /// </summary>
        /// <param name="factoringCode"></param>
        /// <returns></returns>
        public UserFactoring GetUserFactoring(string factoringCode)
        {
            using (var db = CgtTravelDB.GetInstance())
            {
                return db.Query<UserFactoring>("SELECT * FROM UserFactoring  WHERE FactoringCode=@0 ", factoringCode).FirstOrDefault();
            }
        }
    }
}
