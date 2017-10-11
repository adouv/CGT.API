using CGT.Api.DTO;
using CGT.Api.DTO.Boss.CgtOrder;
using CGT.Api.DTO.Boss.InterRefund;
using CGT.Entity.CgtTicketModel;
using CGT.PetaPoco.Repositories.CgtTicket;

namespace CGT.Api.Service.Boss.CgtOrder
{
    /// <summary>
    /// 国际订单详情
    /// </summary>
    public class CgtInterOrderDetailService : ApiBase<RequestInterRefundDetail>
    {
        #region 注入仓储
        public OrderRep orderRep { get; set; }
        public VoyageRep voyageRep { get; set; }

        public PassengerRep passengerRep { get; set; }
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
        protected override void ExecuteMethod()
        {
            ResponseOrderDetail result = new ResponseOrderDetail
            {
                VoyageList = voyageRep.GetVoyageList(new Voyage { Order = this.Parameter.LocalId }),
                PassengerList = passengerRep.GetPassengerList(new Passenger { Order = this.Parameter.LocalId })
            };
            this.Result.Data = result;
        }
    }
}
