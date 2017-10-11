using System;
using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelBatch;
using CGT.PetaPoco.Repositories.CgtTravel;

namespace CGT.Api.Service.Boss.TravelBatch
{
    public class GetTravelBatchService : ApiBase<RequestTravelBatch>
    {
        #region 注入服务
        public TravelBatchRep travelBatchRep { get; set; }
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
        /// 执行业务
        /// </summary>
        protected override void ExecuteMethod()
        {
            var result = travelBatchRep.GetTravelBatchPageList(
                this.Parameter.PayCenterCode,
                this.Parameter.EnterpriseId,
                this.Parameter.StartDate,
                this.Parameter.EndDate,
                this.Parameter.TravelRiskType,
                this.Parameter.TravelBatchId,
                this.Parameter.TranslationState,
                this.Parameter.PageIndex,
                this.Parameter.PageSize);

            this.Result.Data = result;
        }
    }
}
