using CGT.DDD.Logger;
using CGT.Entity.CgtModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;

namespace CGT.PetaPoco.Repositories.Cgt
{
    /// <summary>
    /// 用户（分销）信息仓储
    /// </summary>
    public class UserAccountRep
    {

        public void Insert(UserAccount model)
        {
            CgtDB.GetInstance().Insert(model);
        }

        public int Update(UserAccount model)
        {
            return CgtDB.GetInstance().Update(model);
        }
        /// <summary>
        /// 获取分销用户信息列表
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public Page<UserAccount> GetUserAccountList(UserAccount model, DateTime? ModifyBegionTime, DateTime? ModifyEndTime, int pageindex, int pagesize)
        {
            #region sql
            string wherestr = string.Empty;
            if (model.UserType != null)
            {
                wherestr += " AND UserType = @0";
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                wherestr += " AND Email = @1";
            }
            if (!string.IsNullOrEmpty(model.PayCenterCode))
            {
                wherestr += " AND PayCenterCode = @2";
            }
            if (ModifyBegionTime != null)
            {
                wherestr += " AND ModifyTime >= @3";
            }
            if (ModifyEndTime != null)
            {
                wherestr += " AND ModifyTime <= @4";
            }
            string sql = string.Format(@"
            SELECT  *
            FROM    dbo.UserAccount
            WHERE   1 = 1
            {0}  ORDER BY ModifyTime DESC
            ", wherestr);
            #endregion

            return CgtDB.GetInstance().Page<UserAccount>(pageindex, pagesize, sql,
                  model.UserType, model.Email, model.PayCenterCode, ModifyBegionTime, ModifyEndTime);
        }
        /// <summary>
        /// 获取分销用户信息
        /// </summary>
        /// <returns></returns>
        public List<UserAccount> GetUserAccountPagyCenterList()
        {
            string sql = "SELECT Distinct  b.UserId,b.MerchantCode,b.PayCenterCode,b.UserCompanyName,b.UserName,b.ReapalMerchantId FROM cgt_factoring.dbo.EnterpriseWhiteList a INNER JOIN cgt.dbo.UserAccount b ON a.PayCenterCode = b.PayCenterCode";
            return CgtDB.GetInstance().Fetch<UserAccount>(sql);
        }
        /// <summary>
        /// 获取分销用户信息列表
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public UserAccount GetUserAccount(UserAccount model)
        {
            #region sql
            string wherestr = string.Empty;

            if (model.UserType != null)
            {
                wherestr += " AND UserType = @0";
            }
             if (!string.IsNullOrEmpty(model.Email))
            {
                wherestr += " AND Email = @1";
            }
             if (!string.IsNullOrEmpty(model.PayCenterCode))
            {
                wherestr += " AND PayCenterCode = @2";
            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                wherestr += " AND UserName = @3";
            }

            string sql = string.Format(@"
            SELECT  *
            FROM    dbo.UserAccount
            WHERE   1 = 1
            {0}
            ", wherestr);
            #endregion
            return CgtDB.GetInstance().SingleOrDefault<UserAccount>(sql,
                              model.UserType, model.Email, model.PayCenterCode,model.UserName);
        }
        /// <summary>
        /// 获取分销商信息和返现总额列表(分页)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Page<UserAccount> GetUserAccountAndBackMoneyPageList(UserAccount model, DateTime? CreateBegionTime, DateTime? CreateEndTime, int pageindex, int pagesize)
        {
            #region sql
            string wherestr = string.Empty;
            if (model.UserType != null)
            {
                wherestr += " AND b.UserType = @0 ";
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                wherestr += " AND b.Email = @1 ";
            }
            if (!string.IsNullOrEmpty(model.PayCenterCode))
            {
                wherestr += " AND b.PayCenterCode = @2 ";
            }
            if (CreateBegionTime != null)
            {
                wherestr += " AND b.CreateTime >= @3 ";
            }
            if (CreateEndTime != null)
            {
                wherestr += " AND b.CreateTime <= @4 ";
            }
            string sql = string.Format(@"
           SELECT  b.* ,
          TicketAmount,
		  BillDateTime
          FROM        (SELECT  SUM(TicketAmount) AS TicketAmount ,
                    PayCenterCode,
					BillDateTime
          FROM      cgt_factoring.dbo.EnterpriseOrder WITH ( NOLOCK )
          WHERE     1 = 1
                    AND BackStatus = 1
                    AND RepaymentStatus = 0
          GROUP BY  PayCenterCode,BillDateTime
          ) aa
          RIGHT JOIN UserAccount b ON aa.PayCenterCode = b.PayCenterCode
		  WHERE 1 = 1
          {0} ORDER BY b.CreateTime DESC
           ", wherestr);
            #endregion

            return CgtDB.GetInstance().Page<UserAccount>(pageindex, pagesize, sql,
                  model.UserType, model.Email, model.PayCenterCode, CreateBegionTime, CreateEndTime);
        }
        /// <summary>
        /// 修改分销用户信息
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public int UpdateUserAccount(UserAccount model)
        {
            #region sql
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(model.PayCenterCode))
            {
                wherestr += " AND PayCenterCode = @0 ";
            }
            if (!string.IsNullOrEmpty(model.MerchantCode))
            {
                wherestr += " AND MerchantCode = @1 ";
            }

            string setstr = string.Empty;
            if (model.GraceDay != null)
            {
                setstr += " , GraceDay = @2 ";
            }
            if (model.GraceBate != null)
            {
                setstr += " , GraceBate = @3 ";
            }
            if (model.OverdueBate != null)
            {
                setstr += " , OverdueBate = @4 ";
            }
            if (!string.IsNullOrWhiteSpace(model.ModifyName))
            {
                setstr += " , ModifyName = @5 ";
            }
            if (model.CreditAmount != null)
            {
                setstr += " , CreditAmount = @6 ";
            }
            if (model.FactoringInterestRate != null)
            {
                setstr += " , FactoringInterestRate = @7 ";
            }

            string sql = string.Format(@"
            SET ModifyTime=getdate() {1}
            WHERE   1 = 1
            {0}
            ", wherestr, setstr);
            #endregion
            return CgtDB.GetInstance().Update<UserAccount>(sql,
                              model.PayCenterCode, model.MerchantCode, model.GraceDay, model.GraceBate, model.OverdueBate, model.ModifyName, model.CreditAmount, model.FactoringInterestRate);
        }

        /// <summary>
        /// 修改分销用户宽限期次数
        /// </summary>
        /// <returns></returns>
        public int UpdateUserAccountGraceCount(string PayCenterCode)
        {
            #region sql
            string sql = @"SET GraceCount = GraceCount+1 WHERE PayCenterCode=@0";
            #endregion
            try
            {
                LoggerFactory.Instance.Logger_Info("正在增加分销商宽限期次数，分销商Code：" + PayCenterCode + " \r\n", "CalculateInterestGrace");
                return CgtDB.GetInstance().Update<UserAccount>(sql, PayCenterCode);
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "CalculateInterestGraceException");
                return 0;
            }
        }

        /// <summary>
        /// 修改分销用户逾期次数
        /// </summary>
        /// <returns></returns>
        public int UpdateUserAccountOverdueCount(string PayCenterCode)
        {
            #region sql
            string sql = @"SET OverdueCount = OverdueCount+1 WHERE PayCenterCode=@0";
            #endregion
            try
            {
                LoggerFactory.Instance.Logger_Info("正在增加分销商逾期次数，分销商Code：" + PayCenterCode + " \r\n", "CalculateOverdueAmout");
                return CgtDB.GetInstance().Update<UserAccount>(sql, PayCenterCode);
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "CalculateOverdueAmoutException");
                return 0;
            }
        }
        /// <summary>
        /// 根据金主号修改商户配置
        /// </summary>
        /// <param name="FactoringInterestRate">差旅费率</param>
        /// <param name="UserFactoringCode">金主号</param>
        /// <param name="MerchantCode">商户Code</param>
        /// <returns>int</returns>
        public int UpdateMerchantsConfigurationByFactoringCode(string FactoringInterestRate, string UserFactoringCode, string MerchantCode)
        {
            string sql = string.Empty;
            sql += "SET FactoringInterestRate = @0,UserFactoringCode = @1 WHERE MerchantCode = @2";
            try
            {
                LoggerFactory.Instance.Logger_Info("修改商户配置" + MerchantCode, "UpdateMerchantsConfigurationByFactoringCode");
                return CgtDB.GetInstance().Update<UserAccount>(sql, decimal.Parse(FactoringInterestRate), UserFactoringCode, MerchantCode);
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "UpdateMerchantsConfigurationByFactoringCode");
                return 0;
            }
        }
    }


}
