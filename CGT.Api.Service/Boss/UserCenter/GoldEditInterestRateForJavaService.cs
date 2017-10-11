using System;
using CGT.Api.DTO;
using CGT.Api.DTO.Boss.UserCenter.Request;
using CGT.Entity.CgtModel;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.PetaPoco.Repositories.CgtTravel;
using CGT.UserCenter.Service;
using Org.BouncyCastle.Utilities.IO;

namespace CGT.Api.Service.Boss.UserCenter
{
    /// <summary>
    /// 金主费率修改（java用）
    /// </summary>
    public class GoldEditInterestRateForJavaService : ApiBase<RequsetGoldInfo>
    {
        #region 注入服务
        public UserAccountRep userAccountRep { get; set; }
        public UserFactoringRep userFactoringRep { set; get; }
        #endregion
        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json)
        {
            base.SetData(json);
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate()
        {
            base.Validate();
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod()
        {
            var result = userFactoringRep.UpdateUserFactoring(new UserFactoring()
            {
                FactoringReapalNo = this.Parameter.merchantNo,
                InterestRate = this.Parameter.travelProfit
            });
            if (result < 1)
            {
                throw new System.Exception("更新数据库失败");
            }
            else
            {
                var userResult = userAccountRep.UpdateUserAccount(new UserAccount()
                {
                    GraceDay = this.Parameter.graceDay,
                    GraceBate =string.IsNullOrWhiteSpace(this.Parameter.travelGraceRate)?0M:decimal.Parse(this.Parameter.travelGraceRate),
                    OverdueBate = string.IsNullOrWhiteSpace(this.Parameter.travelPenalty) ? 0M : decimal.Parse(this.Parameter.travelPenalty),
                    MerchantCode = this.Parameter.companyCode
                });
                if (userResult < 1)
                {
                    throw new System.Exception("更新数据库失败");
                }
            }
        }
    }
}
