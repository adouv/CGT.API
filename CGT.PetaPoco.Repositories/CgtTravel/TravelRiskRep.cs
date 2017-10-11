using System.Collections.Generic;
using CGT.Entity.CgtTravelModel;
using PetaPoco.NetCore;
using System.Linq;

namespace CGT.PetaPoco.Repositories.CgtTravel {
    /// <summary>
    /// 差旅风控规则仓储
    /// </summary>
    public class TravelRiskRep {
        /// <summary>
        /// 获取差旅风控规则列表
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public Page<TravelRisk> GetTravelRiskList(TravelRisk model, int pageindex, int pagesize) {
            #region sql
            string wherestr = string.Empty;
            if (!string.IsNullOrWhiteSpace(model.EnterpriseName)) {
                wherestr += " AND EnterpriseName = @0";
            }
            if (!string.IsNullOrWhiteSpace(model.PayCenterName)) {
                wherestr += " AND tr.PayCenterName  = @1";
            }
            if (model.TravelRiskType != null && model.TravelRiskType >= 0) {
                wherestr += " AND TravelRiskType = @2";
            }
            if (model.TravelRiskState != null && model.TravelRiskState >= 0) {
                wherestr += " AND TravelRiskState = @3";
            }
            if (model.EnterpriseID != null && model.EnterpriseID > 0) {
                wherestr += " AND tr.EnterpriseID = @4";
            }
            if (!string.IsNullOrWhiteSpace(model.PayCenterCode)) {
                wherestr += " AND tr.PayCenterCode = @5";
            }
            string sql = string.Format(@"
SELECT  tr.* ,
        ew.EnterpriseName ,
        ue.UserName as ModifyName
FROM    dbo.TravelRisk tr
        LEFT JOIN EnterpriseWhiteList ew ON tr.EnterpriseID = ew.EnterpriseWhiteListID
        LEFT JOIN cgt_user.dbo.[User] AS ue ON tr.ModifyUserId = ue.UserId
WHERE   1 = 1
{0}
            ", wherestr);
            #endregion
            return CgtTravelDB.GetInstance().Page<TravelRisk>(pageindex, pagesize, sql,
                              model.EnterpriseName, model.PayCenterName, model.TravelRiskType, model.TravelRiskState, model.EnterpriseID, model.PayCenterCode);
        }
        /// <summary>
        /// 获取差旅风控规则通过公司编号
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public TravelRisk GetTravelRiskByEnterpriseID(TravelRisk model) {

            #region sql
            string wherestr = string.Empty;
            if (model.EnterpriseID > 0) {
                wherestr += " AND EnterpriseID = @0";
            }
            if (!string.IsNullOrWhiteSpace(model.PayCenterCode)) {
                wherestr += " AND PayCenterCode = @1";
            }
            string sql = string.Format(@"
SELECT  *
FROM    dbo.TravelRisk
WHERE   1 = 1 AND TravelRiskState=1
{0}
            ", wherestr);

            #endregion
            return CgtTravelDB.GetInstance().Query<TravelRisk>(sql,
                              model.EnterpriseID, model.PayCenterCode).ToList().FirstOrDefault();
        }
        /// <summary>
        /// 添加差旅风控规则
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public long AddTravelRisk(TravelRisk model) {
            CgtTravelDB.GetInstance().Insert(model);
            return model.TravelRiskId;
        }
        /// <summary>
        /// 修改差旅风控规则
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public int UpdateTravelRisk(TravelRisk model) {
            #region sql
            string wherestr = string.Empty;

            if (model.TravelRiskId > 0) {
                wherestr += " AND TravelRiskId = @0";
            }
            string sql = string.Format(@"
SET ModifyTime=@1,ModifyUserId=@2,EtermFailRate=@3,EtermSuccessRate=@4,TravelRiskState=@5,TravelRiskType=@6,WhiteFailRate=@7,WhiteSuccessRate=@8,UploadLowCount=@9,TicketMultiple=@10
WHERE   1 = 1
{0}
            ", wherestr);

            #endregion
            return CgtTravelDB.GetInstance().Update<TravelRisk>(sql, model.TravelRiskId, model.ModifyTime, model.ModifyUserId, model.EtermFailRate, model.EtermSuccessRate, model.TravelRiskState, model.TravelRiskType, model.WhiteFailRate, model.WhiteSuccessRate, model.UploadLowCount,model.TicketMultiple);
        }

        /// <summary>
        /// 批次获取企业风控信息
        /// </summary>
        /// <param name="enterpriseIds"></param>
        /// <returns></returns>

        public IEnumerable<TravelRisk> GetTravelRiskByEnterpriseIDs(IEnumerable<long> enterpriseIds)
        {
            using (var db = CgtTravelDB.GetInstance())
            {
                var sql = string.Format(@"SELECT  * FROM TravelRisk WITH (NOLOCK)   WHERE 1=1 AND EnterpriseID IN ({0}) AND TravelRiskState=1", string.Join(",", enterpriseIds));

                return db.Query<TravelRisk>(sql);
            }
        }
    }
}
