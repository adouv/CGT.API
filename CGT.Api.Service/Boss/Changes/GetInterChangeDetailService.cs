using CGT.Api.DTO;
using CGT.Api.DTO.Boss.InterChange;
using CGT.Api.DTO.Boss.InterRefund;
using CGT.Entity.CgtTicketModel;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.PetaPoco.Repositories.CgtTicket;
using System.Linq;

namespace CGT.Api.Service.Boss.Changes
{
    /// <summary>
    ///  国际票改期详细列表
    /// </summary>
    public class GetInterChangeDetailService : ApiBase<RequestInterRefundDetail>
    {
        #region 注入仓储
        public VoyageRep voyageRep { get; set; }

        public InterChangeRemarkRep interChangeRemarkRep { get; set; }

        public UserAccountRep userAccountRep { get; set; }

        public InterChangeTicketRep interChangeTicketRep { get; set; }

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
            ResponseInterChangeDetail result = new ResponseInterChangeDetail
            {
                VoyageList = voyageRep.GetVoyageList(new Voyage { Order = this.Parameter.LocalId }),
                PassengerList = interChangeTicketRep.GetInterChangeTicketPassengerList(new Passenger { Order = this.Parameter.LocalId }),
                InterChangeRemarkList = interChangeRemarkRep.GetInterChangeRemarkList(new InterChangeRemark { OrderId = this.Parameter.LocalId }),
                UserAccountModel = userAccountRep.GetUserAccount(new Entity.CgtModel.UserAccount { Email = this.Parameter.ReapalAccount }),
                OrderModel = orderRep.GetOrderList(new Order { LocalId = this.Parameter.LocalId }).FirstOrDefault()
            };
            this.Result.Data = result;
        }

    }
}
