using CGT.Entity.CgtTravelModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CGT.PetaPoco.Repositories.CgtTravel
{
    public class TravelUserFactoringRep
    {
        /// <summary>
        /// 获取金主列表
        /// </summary>
        /// <param name="FactoringName">金主名称</param>
        /// <param name="FactoringCode">金主Code</param>
        /// <returns>List<UserFactoring></returns>
        public List<UserFactoring> GetUserFactoringList(string FactoringName,string FactoringCode)
        {
            string sqlWhere = string.Empty;
            string sql = string.Empty;
            if (!string.IsNullOrWhiteSpace(FactoringName))
            {
                sqlWhere += " AND FactoringName=@0 ";
            }
            if (!string.IsNullOrWhiteSpace(FactoringCode))
            {
                sqlWhere += " AND FactoringCode=@1 ";
            }
            sql = string.Format(@"
                SELECT FactoringName,FactoringCode 
                FROM dbo.UserFactoring WITH (NOLOCK)
                WHERE 1=1 {0}
            ", sqlWhere);

            return CgtTravelDB.GetInstance().Query<UserFactoring>(sql, FactoringName, FactoringCode).ToList();
        }
    }
}
