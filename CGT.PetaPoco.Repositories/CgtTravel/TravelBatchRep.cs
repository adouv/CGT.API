using CGT.Entity.CgtTravelModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CGT.PetaPoco.Repositories.CgtTravel {
    public class TravelBatchRep {
        /// <summary>
        /// 获取风控日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Page<TravelBatch> GetTravelBatchPageList(string PayCenterCode, int? EnterpriseId, string StartDate, string EndDate, int? TravelRiskType, string TravelBatchId, int? TranslationState, int PageIndex, int PageSize) {
            if (string.IsNullOrWhiteSpace(StartDate)) {
                StartDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrWhiteSpace(EndDate)) {
                EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            #region sql
            string whereStr = "";
            if (!string.IsNullOrWhiteSpace(StartDate)) {
                whereStr += @" AND CONVERT(VARCHAR(100),CreateTime,23)>=@0 ";
            }
            if (!string.IsNullOrWhiteSpace(EndDate)) {
                whereStr += @" AND CONVERT(VARCHAR(100),CreateTime,23)<=@1 ";
            }
            if (TravelRiskType != null) {
                whereStr += @" AND TravelRiskType=@2 ";
            }
            if (!string.IsNullOrWhiteSpace(PayCenterCode)) {
                whereStr += @" AND PayCenterCode=@3 ";
            }
            if (EnterpriseId != null) {
                whereStr += @" AND EnterpriseId=@4 ";
            }
            if (!string.IsNullOrWhiteSpace(TravelBatchId)) {
                whereStr += @" AND TravelBatchId=@5 ";
            }
            if (TranslationState != null && TranslationState > -1) {
                whereStr += @" AND TranslationState=@6 ";
            }
            string sql = string.Format(@"
                SELECT * FROM dbo.TravelBatch WITH (NOLOCK)
                WHERE 1=1 {0}
            ", whereStr);
            #endregion

            return CgtTravelDB.GetInstance().Page<TravelBatch>(
                PageIndex,
                PageSize,
                sql,
                StartDate,
                EndDate,
                TravelRiskType,
                PayCenterCode,
                EnterpriseId,
                TravelBatchId,
                TranslationState);
        }
        /// <summary>
        /// 订单汇总详情
        /// </summary>
        /// <param name="TravelBatchId"></param>
        /// <returns></returns>
        public List<TravelBatchOrder> GetTravelBatchOrderList(string TravelBatchId, string EnterpriseId) {
            string whereStr = "";
            if (!string.IsNullOrWhiteSpace(TravelBatchId)) {
                whereStr += @" AND TravelBatchId=@0 ";
            }
            if (!string.IsNullOrWhiteSpace(EnterpriseId)) {
                whereStr += @" AND EnterpriseId=@1 ";
            }
            string sql = string.Format(@"
               select dbo.GetEnterpriseName(EnterpriseId)as EnterpriseName,* from TravelBatchOrder WITH (NOLOCK)
                WHERE 1=1 {0}
            ", whereStr);
            var db = CgtTravelDB.GetInstance();
            db.EnableAutoSelect = false;
            return db.Fetch<TravelBatchOrder>(sql, TravelBatchId, EnterpriseId);
        }
        /// <summary>
        /// 通过批次号修改流程状态
        /// </summary>
        /// <param name="TravelBatchId"></param>
        /// <param name="TranslationState"></param>
        /// <returns></returns>
        public int UpdateTravelBatchOrderListByTravelBatchId(string TravelBatchId, int TranslationState,decimal EtermRiskRate,decimal WhithRiskRate) {
            #region sql
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(TravelBatchId)) {
                wherestr += " AND TravelBatchId = @0";
            }
            string sql = string.Format(@"
            SET TranslationState=@1,EtermRiskRate=@2,WhithRiskRate=@3
            WHERE   1 = 1
            {0}
            ", wherestr);
            #endregion
            return CgtTravelDB.GetInstance().Update<TravelBatch>(sql, TravelBatchId, TranslationState, EtermRiskRate, WhithRiskRate);
        }
        /// <summary>
        /// 通过批次号修改流程状态
        /// </summary>
        /// <param name="TravelBatchId"></param>
        /// <param name="TranslationState"></param>
        /// <returns></returns>
        public int UpdateTravelBatchOrderTranslationStateByTravelBatchId(string TravelBatchId, int TranslationState) {
            #region sql
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(TravelBatchId)) {
                wherestr += " AND TravelBatchId = @0";
            }
            string sql = string.Format(@"
            SET TranslationState=@1
            WHERE   1 = 1
            {0}
            ", wherestr);
            #endregion
            return CgtTravelDB.GetInstance().Update<TravelBatch>(sql, TravelBatchId, TranslationState);
        }

        /// <summary>
        /// 保存批次数据
        /// </summary>
        /// <param name="modeles"></param>
        /// <returns></returns>
        public int Insert(List<TravelBatch> modeles)
        {
            try {
                // todo  1. transction，2 values ()()()
                using (var db = CgtTravelDB.GetInstance())
                {

                    Parallel.ForEach(modeles, new ParallelOptions() { MaxDegreeOfParallelism = 8 }, item => {

                        db.Insert(item);
                    });

                }

            } catch (Exception ex) {

                return 0;
            }

            return 1;
        }
        /// <summary>
        /// 获取批次主表信息
        /// </summary>
        /// <param name="TravelBatchId">批次号</param>
        /// <param name="EnterpriseId">企业编号</param>
        /// <returns></returns>
        public TravelBatch GetTravelBatch(string TravelBatchId, string EnterpriseId)
        {
            string whereStr = "";
            if (!string.IsNullOrWhiteSpace(TravelBatchId))
            {
                whereStr += @" AND TravelBatchId=@0 ";
            }
            if (!string.IsNullOrWhiteSpace(EnterpriseId))
            {
                whereStr += @" AND EnterpriseId=@1 ";
            }
            string sql = string.Format(@"
               select * from TravelBatch WITH (NOLOCK)
                WHERE 1=1 {0}
            ", whereStr);
            var db = CgtTravelDB.GetInstance();
            db.EnableAutoSelect = false;
            return db.Single<TravelBatch>(sql, TravelBatchId, EnterpriseId);
        }



    }
}
