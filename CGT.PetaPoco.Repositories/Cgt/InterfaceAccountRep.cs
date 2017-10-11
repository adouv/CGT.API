using CGT.Entity.CgtModel;
using PetaPoco.NetCore;

namespace CGT.PetaPoco.Repositories.Cgt
{
    /// <summary>
    /// 商户（代理）信息仓储
    /// </summary>
    public class InterfaceAccountRep
    {

        public void Insert(InterfaceAccount model)
        {
            CgtDB.GetInstance().Insert(model);
        }

        public int Update(InterfaceAccount model)
        {
            return CgtDB.GetInstance().Update(model);
        }
        /// <summary>
        /// 获取接口用户信息
        /// </summary>
        /// <param name="InterfaceAccount">商户信息实体</param>
        /// <returns>InterfaceAccount</returns>
        public InterfaceAccount GetInterfaceAccount(InterfaceAccount model)
        {
            #region sql
            string wherestr = string.Empty;

            if (!string.IsNullOrWhiteSpace(model.MerchantCode))
            {
                wherestr += " AND MerchantCode = @0 ";
            }
            if (!string.IsNullOrWhiteSpace(model.ReapayMerchantNo))
            {
                wherestr += " AND ReapayMerchantNo = @1 ";
            }
            if (!string.IsNullOrWhiteSpace(model.MerchantPwd))
            {
                wherestr += " AND MerchantPwd = @2 ";
            }

            string sql = string.Format(@"
SELECT  *
FROM    [dbo].[InterfaceAccount]
WHERE   1 = 1
        {0} ", wherestr);
            #endregion

            var interfaceAccount = CgtDB.GetInstance().SingleOrDefault<InterfaceAccount>(sql,
                model.MerchantCode,
                model.ReapayMerchantNo,
                model.MerchantPwd);

            return interfaceAccount;
        }
        /// <summary>
        /// 获取接口用户信息
        /// </summary>
        /// <param name="InterfaceAccount">商户信息实体</param>
        /// <returns>InterfaceAccount</returns>
        public Page<InterfaceAccount> GetInterfaceAccountList(int pageindex, int pagesize)
        {
            #region sql
            string wherestr = string.Empty;
            string sql = string.Format(@"
SELECT  *
FROM    [dbo].[InterfaceAccount]
WHERE   1 = 1
        {0} ", wherestr);
            #endregion

            var interfaceAccount = CgtDB.GetInstance().Page<InterfaceAccount>(pageindex, pagesize, sql);

            return interfaceAccount;
        }
        /// <summary>
        /// 修改商户启用禁用状态
        /// </summary>
        /// <param name="payCentCode"></param>
        /// <returns></returns>
        public int UpdateInterfaceAccountStatus(InterfaceAccount model)
        {
            string sql = @"set [Status]=@0 where MerchantCode=@1";
            return CgtDB.GetInstance().Update<InterfaceAccount>(sql, model.Status, model.MerchantCode);
        }
        ///// <summary>
        ///// 修改商户信息
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public int UpdateUserAccountInfo(InterfaceAccount model)
        //{
        //    string sql = "";
        //    return CgtDB.GetInstance().Update<InterfaceAccount>(sql);
        //}
    }
}
