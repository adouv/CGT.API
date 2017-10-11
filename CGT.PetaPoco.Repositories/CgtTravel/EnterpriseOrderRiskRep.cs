using CGT.Entity.CgtTravelModel;
using System.Collections.Generic;

namespace CGT.PetaPoco.Repositories.CgtTravel {
    /// <summary>
    /// 差旅风控信息仓储
    /// </summary>
    public class EnterpriseOrderRiskRep:BaseRep {
        /// <summary>
        /// 修改风控信息审核状态
        /// </summary>
        /// <returns></returns>
        public int UpdateEnterpriseOrderRiskReviewState(string OrderId, int ReviewState, string RefuseReason, int ReviewUserId) {
            #region sql
            string wherestr = string.Empty;
            if (!string.IsNullOrWhiteSpace(OrderId)) {
                wherestr += " AND EOrderId = @0";
            }
            string sql = string.Format(@"
SET ReviewState=@1,RefuseReason=@2,ReviewUserId=@3,ReviewTime=getdate()
WHERE   1 = 1
{0}
            ", wherestr);
            #endregion
            return CgtTravelDB.GetInstance().Update<EnterpriseOrderRisk>(sql, OrderId, ReviewState, RefuseReason, ReviewUserId);
        }
        public object AddEnterpriseOrderRisks(List<EnterpriseOrderRisk> EnterpriseOrderRiskList) {
            return this.BulkInsert(EnterpriseOrderRiskList, CgtTravelDB.GetInstance());
        }
    }
}
