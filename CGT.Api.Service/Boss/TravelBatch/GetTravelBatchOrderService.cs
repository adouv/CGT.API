using System;
using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelBatch;
using CGT.PetaPoco.Repositories.CgtTravel;

namespace CGT.Api.Service.Boss.TravelBatch
{

    public class GetTravelBatchOrderService : ApiBase<RequestTravelBatchOrder>
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

            var result = travelBatchRep.GetTravelBatchOrderList(this.Parameter.TravelBatchId,this.Parameter.EnterpriseId);

            this.Result.Data = result;
        }
    }
}
