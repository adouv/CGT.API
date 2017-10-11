using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelRisk;
using CGT.PetaPoco.Repositories.CgtTravel;
namespace CGT.Api.Service.Boss.TravelRisk {
    /// <summary>
    /// 获取风控规则列表
    /// </summary>
    public class GetTravelRiskListService : ApiBase<RequsetGetTravelRiskList> {
        #region 注入服务
        public TravelRiskRep travelRiskRep { get; set; }
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
            var model = new Entity.CgtTravelModel.TravelRisk() {
                TravelRiskType = Parameter.TravelRiskType,
                TravelRiskState = Parameter.TravelRiskState,
                EnterpriseName = Parameter.EnterpriseName,
                PayCenterName = Parameter.PayCenterName,
                PayCenterCode = Parameter.PayCenterCode,
                EnterpriseID = Parameter.EnterpriseId
            };
            var data = travelRiskRep.GetTravelRiskList(model, Parameter.Pageindex, Parameter.Pagesize);
            this.Result.Data = data;
        }
    }
}
