using CGT.Api.DTO;
using CGT.Api.DTO.Boss.InterRefund;
using CGT.Entity.CgtTicketModel;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.PetaPoco.Repositories.CgtTicket;
using System.Linq;

namespace CGT.Api.Service.Boss.Refunds
{
    /// <summary>
    ///  国际票退票详细列表
    /// </summary>
    public class GetInterRefundDetailService : ApiBase<RequestInterRefundDetail>
    {
        #region 注入仓储
        public VoyageRep voyageRep { get; set; }

        public InterRefundRemarkRep interRefundRemarkRep { get; set; }

        public UserAccountRep userAccountRep { get; set; }

        public InterRefundTicketRep interRefundTicketRep { get; set; }

        public OrderRep orderRep { get; set; }

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
            ResponseInterRefundDetail result = new ResponseInterRefundDetail
            {
                VoyageList = voyageRep.GetVoyageList(new Voyage { Order = this.Parameter.LocalId }),
                PassengerList = interRefundTicketRep.GetInterRefundTicketPassengerList(new Passenger { Order = this.Parameter.LocalId }),
                InterRefundRemarkList = interRefundRemarkRep.GetInterRefundRemarkList(new InterRefundRemark { OrderId = this.Parameter.LocalId }),
                UserAccountModel = userAccountRep.GetUserAccount(new Entity.CgtModel.UserAccount { Email = this.Parameter.ReapalAccount }),
                OrderModel = orderRep.GetOrderList(new Order { LocalId = this.Parameter.LocalId }).FirstOrDefault()
            };
            this.Result.Data = result;
        }
    }
}
