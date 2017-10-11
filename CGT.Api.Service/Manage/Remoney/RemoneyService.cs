using CGT.Event.Model.Manage;

namespace CGT.Api.Service.Manage.Remoney {
    /// <summary>
    /// 差旅返现操作类
    /// </summary>
    public class RemoneyService : RemoneyBaseService {
        protected ManageRiskModel ManageRiskModel { get; set; }
        /// <summary>
        ///重构函数
        /// </summary>
        /// <param name="ManageRiskResultModelList"></param>
        public RemoneyService(ManageRiskModel ManageRiskResultModel) : base(ManageRiskResultModel) {
            ManageRiskModel = ManageRiskResultModel;
        }
        /// <summary>
        /// 执行返现
        /// </summary>
        public override void Execute() {
            //验证数据
            base.Validate();
            //获取风控结果状态
            var ViewResult = ValidationProcessorFactory<BaseRiskModel>.CreateValidationProcessor(ManageRiskModel.baseRiskModelList[0].TravelRiskType).GetValidationBate(ManageRiskModel.baseRiskModelList);
            // 通过状态
            if (ViewResult.BateResult.Equals((int)ReviewState.审核通过)) {
                //修改批次返现状态为返现中
                travelBatchRep.UpdateTravelBatchOrderListByTravelBatchId(ManageRiskModel.TravelBatchId, (int)TranslationState.返现中, (decimal)ViewResult.BlackBate, (decimal)ViewResult.WhiteBate);
                //添加订单和订单风控数据并支付
                AddEnterpriseOrderAndRiksListPay((int)ReviewState.审核通过);
            }
            //审核状态
            else if (ViewResult.BateResult.Equals((int)ReviewState.进入审核)) {
                //修改批次返现状态为完成
                travelBatchRep.UpdateTravelBatchOrderListByTravelBatchId(ManageRiskModel.TravelBatchId, (int)TranslationState.成功, (decimal)ViewResult.BlackBate, (decimal)ViewResult.WhiteBate);
                //添加订单和订单风控数据
                AddEnterpriseOrderAndRiksListPay((int)ReviewState.进入审核);
                //更新缓存
                ModifyTicketCache();
            }
            //拒绝状态
            else {
                //修改批次返现状态为完成
                travelBatchRep.UpdateTravelBatchOrderListByTravelBatchId(ManageRiskModel.TravelBatchId, (int)TranslationState.成功, (decimal)ViewResult.BlackBate, (decimal)ViewResult.WhiteBate);
                //更新缓存
                ModifyTicketCache();
            }
        }
    }
}
