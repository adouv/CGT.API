using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelOrder;
using CGT.PetaPoco.Repositories.CgtTravel;

namespace CGT.Api.Service.Boss.TravelOrder {
    /// <summary>
    /// 获取差旅订单详情
    /// </summary>
    public class GetDistriPaltFormTravelOrderService : ApiBase<RequestTravelOrderList> {

        #region 注入服务
        public EnterpriseOrderRep enterpriseOrderRep { get; set; }
        #endregion
        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json) {
            base.SetData(json);
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate() {
            base.Validate();
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod() {
            var data = enterpriseOrderRep.GetDistriPaltFormTravelOrder(Parameter.EnterpriseID, Parameter.PayCenterCode, Parameter.OrderId, Parameter.TravelBatchId, Parameter.TicketTimeBegion, Parameter.TicketTimeEnd?.AddDays(1).AddSeconds(-1), Parameter.BackTimeBegion, Parameter.BackTimeEnd?.AddDays(1).AddSeconds(-1), Parameter.ReviewState, Parameter.ReviewTimeBegion, Parameter.ReviewTimeEnd?.AddDays(1).AddSeconds(-1), Parameter.TravelRiskState, Parameter.TravelRiskType, Parameter.BackStatus, Parameter.Pageindex, Parameter.Pagesize);
            this.Result.Data = data;
        }
    }
}
