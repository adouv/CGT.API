using CGT.Entity.CgtInsuranceModel;

namespace CGT.PetaPoco.Repositories.Insurance
{
    public class InsuranceUserRep
    {
        /// <summary>
        /// 用户登录,返回实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public InsuranceUser Login(InsuranceUser model) {

            #region sql
            string sql = string.Format(@"
                SELECT UserId,UserName,UserAccountId,Status,CreateTime,Ip 
                FROM dbo.InsuranceUser WITH (NOLOCK)
                WHERE 1=1 AND UserName=@0 AND UserPwd=@1 AND Status=1
            "); 
            #endregion

            return CgtInsuranceDB.GetInstance().FirstOrDefault<InsuranceUser>(sql, model.UserName, model.UserPwd);
        }

        /// <summary>
        /// 更新每个月月限数量
        /// </summary>
        /// <returns></returns>
        public int UpdateRemainingCount()
        {
            #region sql
            string sql = string.Format(@"update [InsuranceUser] set RemainingCount=MonthLimitCount");
            #endregion
            return CgtInsuranceDB.GetInstance().Execute(sql);
        }
    }
}
