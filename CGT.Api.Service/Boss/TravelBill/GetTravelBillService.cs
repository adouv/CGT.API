using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelBill;
using CGT.DDD.Logger;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;

namespace CGT.Api.Service.Boss.TravelBill {
    /// <summary>
    /// 获取账单列表
    /// </summary>
    public class GetTravelBillService : ApiBase<RequestGetTravelBillList> {
        #region 注入服务
        public TravelBillRep travelBillRep { get; set; }
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
        /// 执行业务
        /// </summary>
        protected override void ExecuteMethod() {
            try {
                var result = travelBillRep.GetTravelBillList(
                Parameter.PayCenterCode,
                Parameter.Status,
                Parameter.BillType,
                Parameter.StartBillDate,
                Parameter.EndBillDate,
                Parameter.PageIndex,
                Parameter.PageSize);
                this.Result.Data = result;
            }
            catch (Exception ex) {
                LoggerFactory.Instance.Logger_Error(ex, "GetTravelBillServiceError");
                throw new AggregateException(ex.Message);
            }

        }
    }
}
