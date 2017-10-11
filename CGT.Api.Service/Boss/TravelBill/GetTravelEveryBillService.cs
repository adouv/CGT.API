using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelBill;
using CGT.DDD.Logger;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
namespace CGT.Api.Service.Boss.TravelBill
{
    /// <summary>
    /// 获取日账单列表
    /// </summary>
    public class GetTravelEveryBillService : ApiBase<RequestGetTravelEveryBillList>
    {
        #region 注入服务
        public BillEveryDayRep billEveryDaysRep { get; set; }
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
            try
            {
                var result = billEveryDaysRep.GetBillEveryDaysList(
                    Parameter.PayCenterCode,
                    Parameter.EnterpriseId,
                    Parameter.Status,
                    Parameter.StartDate,
                    Parameter.EndDate,
                    Parameter.PageIndex,
                    Parameter.PageSize);

                this.Result = new ResponseMessageModel()
                {
                    Data = result
                };
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "GetTravelEveryBillServiceError");
                throw new AggregateException(ex.Message);
            }

        }
    }
}
