using CGT.Entity.CgtTravelModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.PetaPoco.Repositories.CgtTravel
{
    public class Rules_TestRep
    {
        /// <summary>
        /// 获取差旅项目测试规则
        /// </summary>
        /// <returns></returns>
        public List<Rules_Test> GetRules_TestList()
        {
            #region sql
            
            string sql = @"SELECT * FROM Rules_Test WITH(NOLOCK)";

            #endregion
            return CgtTravelDB.GetInstance().Fetch<Rules_Test>(sql);
           
        }

    }
}
