using CGT.Entity.CgtTravelModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.PetaPoco.Repositories.CgtTravel
{
    public class BasicData_TestRep
    {
        /// <summary>
        /// 获取差旅项目测试数据集合
        /// </summary>
        /// <returns></returns>
        public List<BasicData_Test> GetBasicData_TestList(int num)
        {
            #region sql

            string sql =string.Format(@"SELECT TOP {0} * FROM BasicData_Test WITH(NOLOCK)", num);

            #endregion
            return CgtTravelDB.GetInstance().Fetch<BasicData_Test>(sql);

        }

        /// <summary>
        /// 删除差旅测试数据集合
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int DeleteBasicData_TestList(string Ids)
        {
            #region sql

            string sql = string.Format(@"DELETE BasicData_Test
                                         WHERE Id in ({0})", Ids);

            #endregion
            return CgtTravelDB.GetInstance().Execute(sql);
        }
    }
}
