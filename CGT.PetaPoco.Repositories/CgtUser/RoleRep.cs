using CGT.Entity.CgtUserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.PetaPoco.Repositories.CgtUser {
    /// <summary>
    /// 角色信息仓储
    /// </summary>
    public class RoleRep {
        /// <summary>
        /// 获取角色仓储
        /// </summary>
        /// <returns></returns>
        public Role GetRole(int RoleId) {
            string wherestr = string.Empty;
            wherestr += " AND RoleId = @0";

            string sql = string.Format(
                @"select * from [dbo].[Role] 
WHERE 1 = 1 {0}", wherestr);
            return CgtUserDB.GetInstance().SingleOrDefault<Role>(sql, RoleId);
        }
    }
}
